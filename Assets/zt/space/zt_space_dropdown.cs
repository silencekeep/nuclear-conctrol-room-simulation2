using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_space_dropdown : MonoBehaviour
{
    public int a=0;
    public GameObject dropdown;
    void Start()
    {
        dropdown.GetComponent<Dropdown>().onValueChanged.AddListener(change1);
    }

    private void change1(int value)
    {
        switch(value)
        {
           case 0:
                a = 2;
                break;
            case 1:
                a = 3;
                break;
            case 2:
                a = 4;
                break;
            

        }

    }
    
}
