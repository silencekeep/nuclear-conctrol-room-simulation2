using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_space_open1 : MonoBehaviour
{
    public int lister_space = 0;
    public int lister_space1 = 0;
    public GameObject space_button;
    void Start()
    {
        GameObject.Find("Menu/Analysis/GameObject/Space").GetComponent<Button>().onClick.AddListener(onclick1);
        space_button.GetComponent<Button>().onClick.AddListener(onclick2);
    }

    private void onclick1()
    {
        lister_space = 1;
    }
    private void onclick2()
    {
        lister_space1 = 1;
    }
}
