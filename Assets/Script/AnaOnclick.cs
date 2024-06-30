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

public class AnaOnclick : MonoBehaviour
{
    //[MenuItem("Example/Import human")]
    public GameObject obj;

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }


    private void OnClick()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        if (!obj.activeInHierarchy)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
        //}
    }
}
