using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OWP_cornerPoint : MonoBehaviour
{
    [SerializeField] Transform SceenMid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetMiddlePointPosition()
    {
        return SceenMid.position;
    }

    public void SetMiddlePointPosition(Vector3 vector3)
    {
        SceenMid.position = vector3;
    }
}
