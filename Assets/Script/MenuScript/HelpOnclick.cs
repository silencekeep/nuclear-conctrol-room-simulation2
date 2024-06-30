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

public class HelpOnclick : MonoBehaviour
{
    //[MenuItem("Example/Import human")]
    public GameObject reach11, reach12, reach21, reach22, reach31, reach32;
    public Dropdown whichperson, reachtype;

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }


    private void OnClick()
    {
        switch (whichperson.value)
        {
            case 0:
                reach11.SetActive(true);
                reach12.SetActive(true);
                break;
            case 1:
                reach21.SetActive(true);
                reach22.SetActive(true);
                break;
            case 2:
                reach31.SetActive(true); 
                reach32.SetActive(true); 
                break;
        }
    }

    public void OnClick2()
    {
        reach11.SetActive(false);
        reach12.SetActive(false);
        reach21.SetActive(false);
        reach22.SetActive(false);
        reach31.SetActive(false);
        reach32.SetActive(false);
    }
}
