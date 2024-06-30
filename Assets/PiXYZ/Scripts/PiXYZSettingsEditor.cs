using System;
using System.Collections.Generic;
using System.Reflection;
using PiXYZ.PiXYZImportScript;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif


namespace PiXYZ
{
    [System.Serializable]
    public class PiXYZSettingsEditor : PiXYZSettings
    {
#if UNITY_EDITOR
        public void getEditorPref()
        {
            string version = InternalEditorUtility.GetFullUnityVersion();
            version = version.Substring(0, version.LastIndexOf('.'));
            originalFilename = "";
            orient = EditorPrefs.GetBool("PiXYZ.Orient", false);
            mapUV = EditorPrefs.GetBool("PiXYZ.MapUV", false);
            mapUV3dSize = EditorPrefs.GetFloat("PiXYZ.MapUV3dSize", 100.0f);
            scaleFactor = EditorPrefs.GetFloat("PiXYZ.ScaleFactor", 0.001f);
            isRightHanded = EditorPrefs.GetBool("PiXYZ.IsRightHanded", true);
            isZUp = EditorPrefs.GetBool("PiXYZ.IsZUp", true);
            treeProcess = (TreeProcessType)EditorPrefs.GetInt("PiXYZ.TreeProcess", 0);
            lodCurrentIndex = EditorPrefs.GetInt("PiXYZ.LODCurrentIndex", 0);
            lodSettingCount = EditorPrefs.GetInt("PiXYZ.LODSettingCount", 1);
            useLods = EditorPrefs.GetBool("PiXYZ.UseLods", false);
            lodsMode = (LODsMode)EditorPrefs.GetInt("PiXYZ.LODsMode", 2);
            lodSettings = new Plugin4UnityI.LODList(lodSettingCount);
            for (int i = 0; i < lodSettingCount; ++i)
            {
                PiXYZLoDSettingsEditor lod = new PiXYZLoDSettingsEditor();
                lod.index = i;
                lod.getEditorPref();
                lodSettings[i] = lod;
            }
            splitTo16BytesIndex = EditorPrefs.GetBool("PiXYZ.SplitTo16BytesIndex", false);
            useScaleOnTolerances = EditorPrefs.GetBool("PiXYZ.UseScaleOnTolerances", false);
            useMergeFinalAssemblies = EditorPrefs.GetBool("PiXYZ.UseMergeFinalAssemblies", false);
            createPrefab = EditorPrefs.GetBool("PiXYZ.CreatePrefab", false);
            loadMetadata = (MetadataSettings)EditorPrefs.GetInt("PiXYZ.LoadMetadata", 0);
            
        }

        public void factoryReset()
        {
            EditorPrefs.DeleteKey("PiXYZ.Orient");
            EditorPrefs.DeleteKey("PiXYZ.MapUV");
            EditorPrefs.DeleteKey("PiXYZ.MapUV3dSize");
            EditorPrefs.DeleteKey("PiXYZ.ScaleFactor");
            EditorPrefs.DeleteKey("PiXYZ.IsRightHanded");
            EditorPrefs.DeleteKey("PiXYZ.IsZUp");
            EditorPrefs.DeleteKey("PiXYZ.TreeProcess");
            EditorPrefs.DeleteKey("PiXYZ.LODCurrentIndex");
            EditorPrefs.DeleteKey("PiXYZ.LODSettingCount");
            EditorPrefs.DeleteKey("PiXYZ.UseLods");
            EditorPrefs.DeleteKey("PiXYZ.LODsMode");
            lodSettings = new Plugin4UnityI.LODList();
            EditorPrefs.DeleteKey("PiXYZ.SplitTo16BytesIndex");
            EditorPrefs.DeleteKey("PiXYZ.UseMergeFinalAssemblies");
            EditorPrefs.DeleteKey("PiXYZ.ShowPopupLods");
            EditorPrefs.DeleteKey("PiXYZ.AutoUpdate");
            EditorPrefs.DeleteKey("PiXYZ.LoadMetadata");
            EditorPrefs.SetBool("PiXYZ.ShowPopupLods", true);
            EditorPrefs.SetBool("PiXYZ.AutoUpdate", true);
            EditorPrefs.SetBool("PiXYZ.DoNotShowAgainDocumentationPopup", false);
            EditorPrefs.SetBool("PiXYZ.CreatePrefab", false);
            PiXYZLoDSettingsEditor.factoryReset();
            getEditorPref();
        }

        public static void saveEditorPref(SerializedObject serializedObject, string prefix = PiXYZImportScript.PiXYZSettings.serializePrefix)
        {
            SerializedProperty serializedProperty = serializedObject.FindProperty(prefix);
            EditorPrefs.SetBool("PiXYZ.Orient", serializedProperty.FindPropertyRelative("orient").boolValue);
            EditorPrefs.SetBool("PiXYZ.MapUV", serializedProperty.FindPropertyRelative("mapUV").boolValue);
            EditorPrefs.SetFloat("PiXYZ.MapUV3dSize", serializedProperty.FindPropertyRelative("mapUV3dSize").floatValue);
            EditorPrefs.SetFloat("PiXYZ.ScaleFactor", serializedProperty.FindPropertyRelative("scaleFactor").floatValue);
            EditorPrefs.SetBool("PiXYZ.IsRightHanded", serializedProperty.FindPropertyRelative("isRightHanded").boolValue);
            EditorPrefs.SetBool("PiXYZ.IsZUp", serializedProperty.FindPropertyRelative("isZUp").boolValue);
            EditorPrefs.SetInt("PiXYZ.TreeProcess", serializedProperty.FindPropertyRelative("treeProcess").intValue);
            EditorPrefs.SetInt("PiXYZ.LODCurrentIndex", serializedProperty.FindPropertyRelative("lodCurrentIndex").intValue);
            EditorPrefs.SetBool("PiXYZ.UseLods", serializedProperty.FindPropertyRelative("useLods").boolValue);
            EditorPrefs.SetInt("PiXYZ.LODsMode", serializedProperty.FindPropertyRelative("lodsMode").intValue);
            EditorPrefs.SetInt("PiXYZ.LODSettingCount", serializedProperty.FindPropertyRelative("lodSettingCount").intValue);
            for (int i = 0; i < serializedProperty.FindPropertyRelative("lodSettingCount").intValue; ++i)
            {
                PiXYZLoDSettingsEditor.saveEditorPref(i, serializedObject);
            }
            EditorPrefs.SetBool("PiXYZ.UseMergeFinalAssemblies", serializedProperty.FindPropertyRelative("useMergeFinalAssemblies").boolValue);
            EditorPrefs.SetBool("PiXYZ.UseScaleOnTolerances", serializedProperty.FindPropertyRelative("useScaleOnTolerances").boolValue);
            EditorPrefs.SetBool("PiXYZ.SplitTo16BytesIndex", serializedProperty.FindPropertyRelative("splitTo16BytesIndex").boolValue);
            EditorPrefs.SetBool("PiXYZ.CreatePrefab", serializedProperty.FindPropertyRelative("createPrefab").boolValue);
            EditorPrefs.SetInt("PiXYZ.LoadMetadata", serializedProperty.FindPropertyRelative("loadMetadata").intValue);
        }
#endif
    }

    [System.Serializable]
    public class PiXYZLoDSettingsEditor : Plugin4UnityI.LOD
    {
        public int index;

        public PiXYZLoDSettingsEditor() { }
        public PiXYZLoDSettingsEditor(PiXYZLoDSettingsEditor other)
        {
            Quality = other.Quality;
            Threshold = other.Threshold;
            index = other.index;
        }
#if UNITY_EDITOR
        public void getEditorPref()
        {
            string lodName = index >= 0 ? "lod" + index : "lodDefault";
            Quality = (Plugin4UnityI.LODQuality)EditorPrefs.GetInt("PiXYZ." + lodName + ".Quality", 2);
            Threshold = EditorPrefs.GetFloat("PiXYZ." + lodName + ".Threshold", 0.1f);// 0.25f - index * 0.12f);
        }
        public static void factoryReset()
        {
            for (int index = 0; index < 5; index++)
            {
                string lodName = index >= 0 ? "lod" + index : "lodDefault";
                if (EditorPrefs.HasKey("PiXYZ." + lodName + ".Quality"))
                    EditorPrefs.DeleteKey("PiXYZ." + lodName + ".Quality");
                if (EditorPrefs.HasKey("PiXYZ." + lodName + ".Threshold"))
                    EditorPrefs.DeleteKey("PiXYZ." + lodName + ".Threshold");
            }
        }
        public static void saveEditorPref(int index, SerializedObject serializedObject, string prefix = "settings.lodSettings.list.Array")
        {
            SerializedProperty lodProperties = serializedObject.FindProperty(prefix);
            SerializedProperty lodProperty = null;
            string lodName = index >= 0 ? "lod" + index : "lodDefault";
            if (lodProperties.isArray)
            {
                lodProperty = lodProperties.GetArrayElementAtIndex(index);
            }
            else
            {
                lodProperty = lodProperties;
            }
            if (lodProperty != null)
            {
                EditorPrefs.SetInt("PiXYZ." + lodName + ".Quality", lodProperty.FindPropertyRelative("Quality").enumValueIndex);
                EditorPrefs.SetFloat("PiXYZ." + lodName + ".Threshold", (float)lodProperty.FindPropertyRelative("Threshold.v").doubleValue);
            }
        }
        public static void insertAt(int index, SerializedObject serializedObject, string prefix = "settings.lodSettings.list")
        {
            insertAt(index, serializedObject, index - 1, prefix);
        }
        public static void insertAt(int index, SerializedObject serializedObject, int from, string prefix = "settings.lodSettings.list")
        {
            SerializedProperty lodProperties = serializedObject.FindProperty(prefix);
            if (lodProperties.isArray)
            {
                SerializedProperty model = lodProperties.GetArrayElementAtIndex(Math.Max(0, Math.Min(from, lodProperties.arraySize - 1)));
                lodProperties.InsertArrayElementAtIndex(index);
                SerializedProperty lodProperty = lodProperties.GetArrayElementAtIndex(index);
                SerializedProperty end = model.GetEndProperty(true);
                while (model.Next(true) && lodProperty.Next(true) && !SerializedProperty.EqualContents(model, end))
                {
                    if (model.name == "index")
                        continue;
                    string propertyName = model.propertyType.ToString();
                    switch (propertyName)
                    {
                        case "Boolean":
                            propertyName = "Bool";
                            break;
                        case "Integer":
                            propertyName = "Int";
                            break;
                    }
                    propertyName = propertyName.ToLower() + "Value";
                    PropertyInfo prop = typeof(SerializedProperty).GetProperty(propertyName);
                    if (prop != null)
                        prop.SetValue(lodProperty, prop.GetValue(model, null), null);
                }
            }
        }

        public static void removeAt(int index, SerializedObject serializedObject, string prefix = "settings.lodSettings.list.Array")
        {
            SerializedProperty lodProperties = serializedObject.FindProperty(prefix);
            if (lodProperties.isArray && lodProperties.arraySize > index)
            {
                if (lodProperties.arraySize != 1)
                {
                    if (lodProperties.arraySize - 1 == index)
                        lodProperties.GetArrayElementAtIndex(index - 1).FindPropertyRelative("Threshold.v").doubleValue = lodProperties.GetArrayElementAtIndex(index).FindPropertyRelative("Threshold.v").doubleValue;
                    lodProperties.DeleteArrayElementAtIndex(index);
                }
            }
        }

        public static SerializedProperty getIndexProperty(int index, SerializedObject serializedObject, string property, string prefix = "settings.lodSettings.list.Array")
        {
            SerializedProperty lodProperties = serializedObject.FindProperty(prefix);
            if ( lodProperties != null && lodProperties.isArray && lodProperties.arraySize > index)
            {
                SerializedProperty lodProperty = lodProperties.GetArrayElementAtIndex(index);
                return lodProperty.FindPropertyRelative(property);
            }
            return null;
        }
#endif
    }
}