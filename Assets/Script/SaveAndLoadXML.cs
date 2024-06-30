using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.EventSystems;

//[ExecuteInEditMode]
public class SaveAndLoadXML
{
    public static SaveAndLoadXML instance;

    public static Data data = new Data();

    public static void SaveToXML(string path)
    {
        data.gameObjectList.Clear();
        var allGameObjects = Object.FindObjectsOfType<GameObject>();

        foreach (GameObject o in allGameObjects)
        {
            List<string> componentNames = new List<string>();
            var components = o.GetComponents(typeof(Component));
            bool IsSavable = false;
            foreach (var item in components)
            {
                if(item != null && item.GetType().ToString() == "IsSavableCompo" )
                {
                    IsSavable = true;
                }
                //componentNames.Add(item.GetType().ToString());
            }
            if (IsSavable)
                //if (o.name == "Cube")
            {
                SavableObject savableObject = new SavableObject();
                savableObject.name = o.name;
                savableObject.transform.position = o.transform.position;
                savableObject.transform.rotation = o.transform.rotation;
                savableObject.transform.localScale = o.transform.localScale;
                if (o.GetComponent<MeshFilter>()) savableObject.mesh = o.GetComponent<MeshFilter>().sharedMesh;

                //List<string> componentNames = new List<string>();
                //var components = o.GetComponents(typeof(Component));

                foreach (var item in components)
                {
                    componentNames.Add(item.GetType().ToString());
                }

                savableObject.components = componentNames;
                data.gameObjectList.Add(savableObject);
            }
        }

        XmlSerializer serializer = new XmlSerializer(typeof(Data));
        FileStream stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, data);
        stream.Close();
        Debug.Log($"Save Successfully {allGameObjects.Length} objects");
    }

    public static void LoadFromXML(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Data));
        FileStream stream = new FileStream(path, FileMode.Open);
        data = serializer.Deserialize(stream) as Data;
        stream.Close();

        Debug.Log($"Load Successfully {SpawnGameObjects()} objects");

    }

    private static int SpawnGameObjects()
    {
        int i = 0;
        //foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        //{
        //    o.SetActive(false);
        //    Object.DestroyImmediate(o);
        //}

        foreach (SavableObject gameObject in data.gameObjectList)
        {
            i++;
            GameObject newGameObject = new GameObject(gameObject.name);
            newGameObject.transform.position = gameObject.transform.position;
            newGameObject.transform.rotation = gameObject.transform.rotation;
            newGameObject.transform.localScale = gameObject.transform.localScale;

            foreach (var com in gameObject.components)
            {
                AddComponentByName(newGameObject, com);
            }

            if (newGameObject.GetComponent<MeshFilter>()) newGameObject.GetComponent<MeshFilter>().sharedMesh = gameObject.mesh;
    
        }
        
        return i;
    }

    private static void AddComponentByName(GameObject gameObject, string name)
    {
        switch (name)
        {
            case "UnityEngine.Camera":
                gameObject.AddComponent<Camera>();
                break;

            case "UnityEngine.Light":
                var light = gameObject.AddComponent<Light>();
                light.type = LightType.Directional;
                break;

            case "UnityEngine.BoxCollider":
                gameObject.AddComponent<BoxCollider>();
                break;

            case "UnityEngine.MeshFilter":
                gameObject.AddComponent<MeshFilter>();
                break;

            case "UnityEngine.MeshRenderer":
                gameObject.AddComponent<MeshRenderer>();
                gameObject.GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                gameObject.GetComponent<Renderer>().sharedMaterial.color = Color.white;
                break;
            case "Standalone Input Module":
                gameObject.AddComponent<StandaloneInputModule> ();
                break;

            default:
                break;

        }

    }

}

[System.Serializable]
public class SavableObject
{
    public string name;
    public TransformComponent transform = new TransformComponent();
    public Mesh mesh;
    public List<string> components = new List<string>();
}

[System.Serializable]
public class TransformComponent
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 localScale;
}

[System.Serializable]
public class Data
{
    public List<SavableObject> gameObjectList = new List<SavableObject>();
}

