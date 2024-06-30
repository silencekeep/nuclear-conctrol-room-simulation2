using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_comfort_open1 : MonoBehaviour
{
    public int lister_comfort = 0;
    public int lister_comfort1 = 0;
    public GameObject comfort_button;
    void Start()
    {
        GameObject.Find("Menu/Analysis/GameObject/Comfort").GetComponent<Button>().onClick.AddListener(onclick1);
        comfort_button.GetComponent<Button>().onClick.AddListener(onclick2);
    }

    private void onclick1()
    {
        lister_comfort = 1;
    }
    private void onclick2()
    {
        lister_comfort1 = 1;
    }
}
