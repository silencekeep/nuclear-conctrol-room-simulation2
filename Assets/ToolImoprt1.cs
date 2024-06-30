using System.IO;
using System;
using UnityEngine;
using UnityEditor;
//using SFB;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToolImport1 : MonoBehaviour
{
    //[MenuItem("Example/Import human")]
    public GameObject sphere;
    public Dropdown dp1;

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }


    public void OnClick()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        GameObject.Find(dp1.captionText.text).SetActive(true);
        
        //}
    }
}
