using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_space_click : MonoBehaviour
{
    public int lister = 0;
    public int lister1 = 0;
    public GameObject  clickbutton;
    public GameObject daochu;
    void Start()
    {
        clickbutton.GetComponent<Button>().onClick.AddListener(OnClick);
        daochu.GetComponent<Button>().onClick.AddListener(OnClick1);

    }

    private void OnClick()
    {
        lister = 1;
    }
    private void OnClick1()
    {
        lister1 = 1;
    }

}
