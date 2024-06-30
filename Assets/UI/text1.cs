using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text1 : MonoBehaviour
{
    Text text2;
    HullDelaunayVoronoi.lineconvex  convex;
    void Start()
    {
        text2 = GameObject.Find("Text").GetComponent<Text>();
        convex = FindObjectOfType<HullDelaunayVoronoi.lineconvex>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (convex.i >= 2500)
        //{
            text2.text = "蓝色包络线：末端节点操作空间" + "\n" + "红色包络线：所考察关节运动包络面";
        //}
    }
}
