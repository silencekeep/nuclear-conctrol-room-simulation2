using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using Button = UnityEngine.UI.Button;

public class SaveScene : MonoBehaviour
{
    
    //public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(SaveSceneToXML);
    }

    // Update is called once per frame
    void Update()
    {
        //string path = EditorUtility.OpenFilePanel("Load Scene", Application.dataPath, "xml");
        //if (path.Length != 0)
        //{
        //    SaveAndLoadXML.LoadFromXML(path);
        //}
    }
    static void SaveSceneToXML()
    {
        string pathhh;
        OpenFileDialog form = new OpenFileDialog();
        form.InitialDirectory = UnityEngine.Application.dataPath;
        //form.Filter = "xml";
        if (form.ShowDialog() == DialogResult.OK)
        {
          pathhh = form.FileName;
        }
        
            //= EditorUtility.SaveFilePanel("Save Scene", Application.dataPath, "Scene", "xml");
        //if (pathhh.Length != 0)
        //{
        //    SaveAndLoadXML.SaveToXML(pathhh);
        //}
    }
}
