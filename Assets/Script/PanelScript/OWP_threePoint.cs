using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OWP_threePoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform MiddlePoint;
    [SerializeField] Transform LeftPoint;
    [SerializeField] Transform RightPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetMiddlePointPosition()
    {
        return MiddlePoint.position;
    }

    public void SetMiddlePointPosition(Vector3 vector3)
    {
        MiddlePoint.position = vector3;
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
