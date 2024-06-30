using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdown1 : MonoBehaviour
{
    public bool ValueChange;
    void Start()
    {
        GameObject.Find("Dropdown1").GetComponent<Dropdown>().onValueChanged.AddListener(ConsoleResult);
    }
    private void ConsoleResult(int value)
    {
        switch (value)
        {
            case 0:
                ValueChange = false;

                break;
            case 1:
                ValueChange = true;
                break;
           
        }
    }
}
