using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handfind : MonoBehaviour
{
    public GameObject left, right;
    public Vector3 lefthand, righthand;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        lefthand = new Vector3(left.GetComponent<Transform>().position.x, left.GetComponent<Transform>().position.y, left.GetComponent<Transform>().position.z);
    }
}
