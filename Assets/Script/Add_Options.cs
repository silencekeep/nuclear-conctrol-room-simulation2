using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Add_Options : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    public GameObject obj1, obj2, obj3, obj4;
    void Start()
    {
        //var button = GetComponent<Button>();
        // button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        if (!obj.activeInHierarchy)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
    }
    public void OnClick1()
    {
        if (!obj1.activeInHierarchy)
        {
            obj1.SetActive(true);
        }
        else
        {
            obj1.SetActive(false);
        }
    }
    public void OnClick2()
    {
        if (!obj2.activeInHierarchy)
        {
            obj2.SetActive(true);
        }
        else
        {
            obj2.SetActive(false);
        }
    }

    public void OnClick3()
    {
        if (!obj3.activeInHierarchy)
        {
            obj3.SetActive(true);
        }
        else
        {
            obj3.SetActive(false);
        }
    }

    public void OnClick4()
    {
        if (!obj4.activeInHierarchy)
        {
            obj4.SetActive(true);
        }
        else
        {
            obj4.SetActive(false);
        }
    }
}