using System.IO;
using System;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HumChildOnclick : MonoBehaviour
{
    public GameObject Human1;
    public GameObject Human2;
    public GameObject Human3;
    public GameObject CreateBtn;
    public GameObject GameObjectHum;
    // Start is called before the first frame update
    void Start()
    {
        CreateBtn = GameObject.Find("Create");
        CreateBtn.GetComponent<Button>().onClick.AddListener(CreateHumanOnClick);
    }
    private void CreateHumanOnClick()
    {
        Human1.SetActive(true);
        Human2.SetActive(true);
        Human3.SetActive(true);


    }
    // Update is called once per frame
    void Update()
    {
        if(Human1.activeInHierarchy == true && Human2.activeInHierarchy == true)
        {
            GameObjectHum.SetActive(false);
        }
    }
}
