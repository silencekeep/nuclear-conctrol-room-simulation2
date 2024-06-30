using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class post_dropdown2 : MonoBehaviour
{
    public string post;
    void Start()
    {
        GameObject.Find("Dropdown").GetComponent<Dropdown>().onValueChanged.AddListener(valueselect1);
    }

    private void valueselect1(int value)
    {
        switch (value)
        {
            case 0:
                post = "swat";
                break;
            case 1:
                post = "swat";
                break;
            case 2:
                post = "swat";
                break;

        }
    }
}
