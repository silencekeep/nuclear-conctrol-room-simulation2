using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class experimentUI : MonoBehaviour
{
    public GameObject experimentalPanel,exit_image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Btn_experimentUI()
    {
        experimentalPanel.SetActive(true);
        exit_image.SetActive(true);
    }

    public void Btn_experimentUI_Exit()
    {
        experimentalPanel.SetActive(false);
        exit_image.SetActive(false);
    }

}
