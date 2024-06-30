using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsLock : MonoBehaviour
{
    [SerializeField] Transform[] selfPointList;
    [SerializeField] Transform[] LockPointList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < selfPointList.Length; i++)
        {
            if (!Iscoincide(selfPointList[i], LockPointList[i]))
            {
                Splice(selfPointList[i],LockPointList[i]);
            }
        }
    }

    void Splice(Transform targetPoint,Transform referPoint)//targetPoint, referPoint
    {
        Vector3 relativePosition = GetRelativePosition(targetPoint, referPoint);//targetPoint, referPoint
                                                                                // gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x - relativeEulerAngles.x, transform.eulerAngles.y - relativeEulerAngles.y, transform.eulerAngles.z - relativeEulerAngles.z);
        transform.position = new Vector3(transform.position.x - relativePosition.x, transform.position.y - relativePosition.y, transform.position.z - relativePosition.z);
    }

    Vector3 GetRelativePosition(Transform target, Transform refer)
    {
        Vector3 relative = Vector3.zero;
        relative.x = target.position.x - refer.position.x;
        relative.y = target.position.y - refer.position.y;
        relative.z = target.position.z - refer.position.z;
        return relative;
    }

    bool Iscoincide(Transform targetPoint, Transform referPoint)
    {
        float x = Math.Abs(targetPoint.transform.position.x - referPoint.transform.position.x);
        float y = Math.Abs(targetPoint.transform.position.y - referPoint.transform.position.y);
        float z = Math.Abs(targetPoint.transform.position.z - referPoint.transform.position.z);
        if (x < 0.001 && y < 0.001 && z < 0.001)    //厂房最大边长不得超过1km；
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
