using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCamera : MonoBehaviour
{
    public GameObject eye;
    private Vector3 eye1;
    // Start is called before the first frame update
    void Start()
    {
        eye1 = eye.transform.position + new Vector3(2, 0, 0);
        gameObject.transform.position = eye1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
