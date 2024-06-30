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

public class MoveOnclick : MonoBehaviour
{
    //[MenuItem("Example/Import human")]
    public GameObject cad_imported;
    public Globle other;
    public GameObject cam;
    int if_cam;
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        if_cam = 1;
    }


    private void OnClick()
    {
        if(if_cam == 1)
        {
            cam.SetActive(false);
            other.globle = 1;
            Console.WriteLine(other.globle);
            Debug.Log(other.globle);
            if_cam = 0;
        }
        else
        {
            cam.SetActive(true);
            other.globle = 0;
            Debug.Log(other.globle);
            if_cam = 1;
        }
        
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //if (!obj.activeInHierarchy)
        //{
        //    obj.SetActive(true);
        //}
        //else
        //{
        //    obj.SetActive(false);
        //}
        //}
    }
}
