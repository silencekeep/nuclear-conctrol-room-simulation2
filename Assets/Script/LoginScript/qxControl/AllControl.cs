using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllControl : MonoBehaviour
{
    public GameObject RoleControl;
    public GameObject UseControl;
    public TMP_Dropdown RAU;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void valueChange(){
        string RAUValue = RAU.options[RAU.value].text;
        if ("用户管理".Equals(RAUValue)){
            UseControl.SetActive(true);
            RoleControl.SetActive(false);
            UseControl.GetComponent<UseControl>().inition();
        }
        else{
            RoleControl.SetActive(true);
            UseControl.SetActive(false);
            RoleControl.GetComponent<RoleControl>().roleinition();
        }

    }
}
