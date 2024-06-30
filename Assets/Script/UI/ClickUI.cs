using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickUI : MonoBehaviour
{
    [SerializeField] GameObject[] uiContainer = null;
    [SerializeField] Button button;
    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
        //for (int i = 0; i < uiContainer.Length; i++)
        //{
        //    uiContainer[i].SetActive(false);
        //}
    }

    private void OnButtonClick()
    {
        for (int i = 0; i < uiContainer.Length; i++)
        {
            uiContainer[i].SetActive(!uiContainer[i].activeInHierarchy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
