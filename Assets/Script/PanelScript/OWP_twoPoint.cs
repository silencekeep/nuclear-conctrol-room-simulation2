using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OWP_twoPoint : MonoBehaviour
{
    [SerializeField] Transform LeftPoint;
    [SerializeField] Transform RightPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetLeftPointPosition()
    {
        return LeftPoint.position;
    }

    public void SetLeftPointPosition(Vector3 vector3)
    {
        LeftPoint.position = vector3;
    }

    public Vector3 GetRightPointPosition()
    {
        return RightPoint.position;
    }

    public void SetRightPointPosition(Vector3 vector3)
    {
        RightPoint.position = vector3;
    }
}
