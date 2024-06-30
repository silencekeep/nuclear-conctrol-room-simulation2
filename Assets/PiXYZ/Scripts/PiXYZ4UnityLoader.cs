using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

using PiXYZ.PiXYZImportScript;
using UnityEditor;

namespace PiXYZ
{

    public delegate void ProgressHandler(string text, float value);

    public class PiXYZ4UnityLoader : ScriptableObject
    {
        public event ProgressHandler progressChanged;

        private int m_PartsCount;
        private int m_NodesCount;
        private int m_PolyCount;

        List<UnityEngine.Object> m_loadedObject = new List<UnityEngine.Object>();

        public string getErrorMessage()
        {
            return errorMsg;
        }

        private float _scale;
        private bool _mirrorX = false;
        private bool _zup = false;
        private bool _createPrefab = false;

        private object _handle = new object();
        private LODsMode _lodsMode = LODsMode.NONE;
        private MetadataSettings _MetadataMODE = MetadataSettings.NONE;
        private bool _support32BytesIndex = false;

        private volatile string _errorMsg = "";
        public volatile float _progress;
        public volatile string _progressStatus;
        public GameObject lastImportedObject;
        private string errorMsg
        {
            get
            {
                string tmp;
                lock (_handle)
                {
                    tmp = _errorMsg;
                }
                return tmp;
            }
            set
            {
                lock (_handle)
                {
                    _errorMsg = value;
                }
            }
        }
        public float progress
        {
            get
            {
                float tmp;
                lock (_handle)
                {
                    tmp = _progress;
                }
                return tmp;
            }
            set
            {
                lock (_handle)
                {
                    _progress = value;
                }
            }
        }
        public string progressStatus
        {
            get
            {
                string tmp;
                lock (_handle)
                {
                    tmp = _progressStatus;
                }
                return tmp;
            }
            set
            {
                lock (_handle)
                {
                    _progressStatus = value;
                }
            }
        }

        public List<UnityEngine.Object> loadedObject
        {
            get
            {
                List<UnityEngine.Object> tmp;
                lock (_handle)
                {
                    tmp = m_loadedObject;
                }
                return tmp;
            }
            set
            {
                lock (_handle)
                {
                    m_loadedObject = value;
                }
            }
        }

        public void setSourceCoordinatesSystem(bool rightHanded, bool zUp, float scaleFactor)
        {
            _scale = scaleFactor;
            _mirrorX = rightHanded;
            _zup = zUp;
        }

        public void configure(double scale, bool orient, double mapUV3dSize, TreeProcessType treeProcess, LODsMode lodsMode, Plugin4UnityI.LODList lods, bool support32BytesIndex, bool useMergeFinalAssemblies, MetadataSettings loadMetadata, bool createPrefab)
        {
            _lodsMode = lodsMode;
            _MetadataMODE = loadMetadata;
            _support32BytesIndex = support32BytesIndex;
            _createPrefab = createPrefab;
            if (treeProcess == TreeProcessType.MERGE || treeProcess == TreeProcessType.MERGE_BY_MATERIAL)
            {
                _lodsMode = LODsMode.ROOT;
            }
            else if (treeProcess == TreeProcessType.MERGE_FINAL_ASSEMBLIES) {
                treeProcess = TreeProcessType.NONE;
                useMergeFinalAssemblies = true;
            } else {
                useMergeFinalAssemblies = false;
            }
            Plugin4UnityI.configure(scale, orient, mapUV3dSize, (int)treeProcess, support32BytesIndex, useMergeFinalAssemblies, (int)lodsMode, lods);
        }
        
        private void importThread(string filePath, out Plugin4UnityI.Assembly assembly)
        {
            errorMsg = "";
            progress = 0;
            progressStatus = "Initializing PiXYZ";
            try
            {
                PiXYZ4UnityUtils.clear();
                progressStatus = "Importing file in PiXYZ";
                progress += 0.05f;
                assembly = Plugin4UnityI.importFile(new Plugin4UnityI.FilePath(filePath));
            }
            catch (Exception e)
            {
                assembly = null;
                errorMsg = e.Message;
                return;
            }
        }

        public void setProgress(string text, float value) {
            //Debug.Log("Progress: " + value + " - " + text);
            progressStatus = text;
            progress = value;
            if (progressChanged != null)
                progressChanged.Invoke(text, value);
        }

        public IEnumerator loadFileRuntime(GameObject parent, string filePath, bool editor)
        {
            if (editor)
                loadedObject.Clear();

            setProgress("Reading file...", 0.05f);

            Plugin4UnityI.Assembly assembly = 0;
            Plugin4UnityI.setResourceFolder(new Plugin4UnityI.DirectoryPath(Application.dataPath + "/PiXYZ/Resources/"));

			PiXYZConfig.CheckLicense();
            Thread _thread = new Thread(() => importThread(filePath, out assembly));

            m_PartsCount = 0;
            m_PolyCount = 0;
            m_NodesCount = 0;

            _thread.Start();

            while (_thread.IsAlive) {
                yield return null;
            }

            setProgress("Converting data...", 0.40f); // = Marshalling

            if (getErrorMessage() != "")
            {
                yield break;
            }
            materials = new Dictionary<int, Material>();

            Plugin4UnityI.ScenePath path = new Plugin4UnityI.ScenePath(assembly == 0?1:2);
            path[0] = Plugin4UnityI.getSceneRoot();
            if (assembly != 0)
            {
                path[1] = assembly;
            }

            lastImportedObject = null;
            var loadedObjects = _createPrefab ? m_loadedObject : null;

            setProgress("Converting data...", 0.45f);

            Plugin4UnityI.SceneExtract scene = Plugin4UnityI.getSceneExtract(path);

            setProgress("Creating Unity objects...", 0.75f);

            lastImportedObject = PiXYZ4UnityUtils.createScene(scene, _scale, _mirrorX, _zup, _support32BytesIndex, _MetadataMODE, _lodsMode, ref loadedObjects);
            loadedObject = loadedObjects;
            if (parent!=null)
                lastImportedObject.transform.SetParent(parent.transform);

            foreach (KeyValuePair<int, Material> kvpair in PiXYZ4UnityUtils.getCreatedMaterials())
                if (!materials.ContainsKey(kvpair.Key))
                    materials.Add(kvpair.Key, kvpair.Value);
            PiXYZ4UnityUtils.clear();
        }

        bool isPartOrAssembly(Plugin4UnityI.SceneNode node)
        {
            try
            {
                Plugin4UnityI.ComponentType comType = Plugin4UnityI.getComponentType(new Plugin4UnityI.Component(node));
                return comType == Plugin4UnityI.ComponentType.PART || comType == Plugin4UnityI.ComponentType.ASSEMBLY;
            }
            catch(Exception)
            {
                return false;
            }
        }

        bool isPart(Plugin4UnityI.SceneNode node)
        {
            if (Plugin4UnityI.getSceneNodeType(node) != Plugin4UnityI.SceneNodeType.COMPONENT)
                return false;
            return (Plugin4UnityI.getComponentType(new Plugin4UnityI.Component(node)) == Plugin4UnityI.ComponentType.PART);
        }

        //!isPart && !isAssembly => Light or camera
        bool isAssembly(Plugin4UnityI.SceneNode node)
        {
            if (Plugin4UnityI.getSceneNodeType(node) != Plugin4UnityI.SceneNodeType.COMPONENT)
                return false;
            return Plugin4UnityI.getComponentType(new Plugin4UnityI.Component(node)) == Plugin4UnityI.ComponentType.ASSEMBLY;
        }

        //HOOPS workaround
        bool isHidden(Plugin4UnityI.ScenePath subTree)
        {
            return Plugin4UnityI.isHidden(subTree);
        }

        PiXYZProperties loadProperties(Plugin4UnityI.SceneNode nodeId, GameObject gameObject)
        {
            Plugin4UnityI.Properties properties = Plugin4UnityI.getNodeProperties(nodeId);

            if (properties.names.Length() == 0)
                return null;

            var propertiesBehaviour = gameObject.AddComponent<PiXYZProperties>();
            propertiesBehaviour.properties = properties;
            return propertiesBehaviour;
        }

        public Dictionary<int, Material> materials = new Dictionary<int, Material>();

        Material getMaterial(int id, bool editor)
        {
            Material material;
            if (materials.TryGetValue(id, out material))
                return material;

            material = PiXYZ4UnityUtils.getMaterial(id);

            materials.Add(id, material);
            return material;
        }

        public void GetCounts(out int p_PartsCount, out int p_PolyCount, out int p_NodesCount)
        {
            p_PartsCount = m_PartsCount;
            p_PolyCount = m_PolyCount;
            p_NodesCount = m_NodesCount;
        }
    }
}