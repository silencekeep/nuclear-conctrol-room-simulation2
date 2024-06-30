using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide2 : MonoBehaviour
{
    public GameObject obj;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject obj6;
    public GameObject obj7;
    // Start is called before the first frame update
    void Start()
    {
        //obj.SetActive(false);
        obj2.SetActive(false);
        obj3.SetActive(false);
        obj4.SetActive(false);
        obj5.SetActive(false);
        obj6.SetActive(false);
        obj7.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    if (!obj.activeInHierarchy)
        //    {
        //        obj.SetActive(true);
        //    }
        //    else {
        //        obj.SetActive(false);
        //    }
        //}
    }
}
