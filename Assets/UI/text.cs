using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text : MonoBehaviour
{
    Text text1;
    HullDelaunayVoronoi.pointconvex point;
    HullDelaunayVoronoi.CollisionDetection1 collision1;
    // Start is called before the first frame update
    void Start()
    {
        collision1 = FindObjectOfType<HullDelaunayVoronoi.CollisionDetection1>();
        text1 = GameObject.Find("Text1").GetComponent<Text>();
        point = FindObjectOfType<HullDelaunayVoronoi.pointconvex>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collision1.collisionname.Length != 0)
        {
            text1.text = "碰撞的物体:" + "正方体" ;
        }
    }

    public static implicit operator text(string v)
    {
        throw new NotImplementedException();
    }
}
