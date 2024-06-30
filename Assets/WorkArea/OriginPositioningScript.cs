using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginPositioningScript : MonoBehaviour
{
    public Vector3 GetGlobalOriginPosition()
    {
        return this.gameObject.transform.position;
    }

    public float GetGroundCoordinates()
    {
        return this.gameObject.transform.position.z;
    }
}
