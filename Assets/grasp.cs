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

public class grasp : MonoBehaviour
{
    //[MenuItem("Example/Import human")]
    public GameObject sphere;
    public Dropdown cdp;
    public GameObject go;

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }


    public void OnClick()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        if (!sphere.activeInHierarchy)
        {
            sphere.SetActive(true);
        }
        else
        {
            sphere.SetActive(false);
        }
        //}
    }

    public void ChangeColorObj()
    {
        if (cdp.value == 0)
        {
            go.GetComponent<MeshRenderer>().material.color = Color.red;
            //go.AddComponent<Material>().color = Color.red;
        }
        if (cdp.value == 1)
        {
            go.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        if (cdp.value == 2)
        {
            go.GetComponent<MeshRenderer>().material.color = Color.blue;
        }       
    }

}
