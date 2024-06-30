using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zt_comfort_start : MonoBehaviour
{
    public GameObject target;
    
    zt_comfort_click2 click3;

    void Start()
    {
        
        click3 = FindObjectOfType<zt_comfort_click2>();
        target.GetComponent<Animator>().speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
            //if(click3.lister1==0)
            //{
            //    target.GetComponent<Animator>().speed = 1.0f;
            //}
            //else
            //{
            //    target.GetComponent<Animator>().speed = 0.0f;
            //}
           
        }
    
}
