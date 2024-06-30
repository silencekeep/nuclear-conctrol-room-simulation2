using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubemove : MonoBehaviour
{
    click click1;
    click1 click2;
    public float MoveSpeed = 10;
    HullDelaunayVoronoi.CollisionDetection1 collision1;
    public void Start()
    {
        click1 = FindObjectOfType<click>();
        click2 = FindObjectOfType<click1>();
        collision1= FindObjectOfType<HullDelaunayVoronoi.CollisionDetection1>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (collision1.collisionname.Length == 0)
        {
            if (click1.lister == 1)
            {
                if (click2.lister == 0)
                {


                    transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime, Space.World);
                }
            }

        }
    }

}
