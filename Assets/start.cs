using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour
{
    public GameObject target;
    click click2;
    HullDelaunayVoronoi.CollisionDetection1 collision;
    click1 click3;
    public void Start()
    {
       target.GetComponent<Animator>().speed=0.0f;
        click2 = FindObjectOfType<click>();
        collision = FindObjectOfType<HullDelaunayVoronoi.CollisionDetection1>();
        click3 = FindObjectOfType<click1>();
    }
    public void Update()
    {
        if (click2.lister == 1)
        {
            if (click3.lister == 0)
            {
                if (collision.iscollision == false)
                {
                    target.GetComponent<Animator>().speed = 1.0f;
                }
                else
                {
                    target.GetComponent<Animator>().speed = 0.0f;
                }
            }
            else
            {
                target.GetComponent<Animator>().speed = 0.0f;
            }
            
        }
    }
    
    
}
