using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ztt_poster : MonoBehaviour
{
    Transform transform1;
    void Start()
    {
        GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm").GetComponent<Transform>().localEulerAngles = new Vector3(0, -30, -20);
        GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm").GetComponent<Transform>().localEulerAngles = new Vector3(0, -30, -20);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm").GetComponent<Transform>().localEulerAngles = new Vector3(0, -30, -20);
        GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm").GetComponent<Transform>().localEulerAngles = new Vector3(0, -30, -20);
    }
}
