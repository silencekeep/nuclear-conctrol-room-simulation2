using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class input : MonoBehaviour
{
    public string endValue1;
    
    private void Start()
    {


        GameObject.Find("InputField").GetComponent<InputField>().onEndEdit.AddListener(EndValue1);//文本输入结束时会调用
       

    }
    //用户输入时的变化

    private void EndValue1(string value)
    {
        if(value=="工具1")
        {
            endValue1 = "target";//捕捉数据，方便后续操作
        }
        

    }
   
}
