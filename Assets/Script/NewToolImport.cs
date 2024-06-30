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

public class NewToolImport : MonoBehaviour
{
    //[MenuItem("Example/Import human")]
    public Dropdown dp1;
    private GameObject b;
    public GameObject GB;
    public GameObject JL;
    public GameObject qian;
    public GameObject NL;
    public GameObject ShouTao;

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }


    public void OnClick()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        string c = dp1.captionText.text;
        if (c == GB.name)
        {
            b = GB;
        }
        else if (c == JL.name)
        {
            b = JL;
        }
        else if (c == qian.name)
        {
            b = qian;
        }
        else if (c == NL.name)
        {
            b = NL;
        }
        else if (c == ShouTao.name)
        {
            b = NL;
        }

        b.SetActive(true);
        //}
    }
}
