using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenReport : MonoBehaviour
{
    public string path;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void queding()
    {
        System.Diagnostics.Process.Start("C:/Users/17888/Desktop/报告_小王_20201224201.xls");
    }
}
