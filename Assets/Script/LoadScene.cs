//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using UnityEngine.UI;

//public class LoadScene : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        var button = GetComponent<Button>();
//        button.onClick.AddListener(LoadSceneFromXML);
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    static void LoadSceneFromXML()
//    {
//        string path = EditorUtility.OpenFilePanel("Load Scene", Application.dataPath, "xml");
//        if (path.Length != 0)
//        {
//            SaveAndLoadXML.LoadFromXML(path);
//        }
//    }
//}
