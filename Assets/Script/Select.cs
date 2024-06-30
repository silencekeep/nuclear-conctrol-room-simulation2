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

public class Select : MonoBehaviour
{
    //[MenuItem("Example/Imp
    public GameObject quan,quan2;
    //public GameObject quan, quan2, textTitle, textSize;
    public Button thisButton;
    public Sprite BlackHand;
    public Sprite WhiteHand;
    public Text text;
    

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }


    public void OnClick()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        if (!quan.activeInHierarchy)
        {
            quan.SetActive(true);
            quan2.SetActive(true);
            thisButton.image.sprite = WhiteHand;
        }
        else if (quan.activeInHierarchy)
        {
            quan.SetActive(false);
            quan2.SetActive(false);
            if(text.text != null) text.text = "";
            thisButton.image.sprite = BlackHand;
        }
        //}
    }
}
