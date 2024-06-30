using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_comfort_dropdown : MonoBehaviour
{
    public int ValueChange;
    public GameObject dropdown1;
    void Start()
    {
        dropdown1.GetComponent<Dropdown>().onValueChanged.AddListener(ConsoleResult);
    }

    private void ConsoleResult(int value)
    {
        switch (value)
        {
            case 0:
                ValueChange = 0;

                break;
            case 1:
                ValueChange = 1;

                break;

        }
       
    }
}
