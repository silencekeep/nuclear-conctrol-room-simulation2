//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.UI;

//public class TxtOut : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        var button = GetComponent<Button>();
//        button.onClick.AddListener(OnClick);
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//    private void OnClick()
//    {
//        string path = EditorUtility.SaveFilePanel("Output Log", Application.dataPath, "Analysis Log", "txt");
//        string infromation = "test";
//        if (path.Length != 0)
//        {
//            Save(path, infromation);

//        }

//        Debug.Log("导出成功");
//    }
//    public void Save(string Path, string information)
//    {
//        FileStream aFile = new FileStream(@"" + Path, FileMode.OpenOrCreate);
//        StreamWriter sw = new StreamWriter(aFile);
//        sw.Write(information);
//        sw.Close();
//        sw.Dispose();
//    }

//}
