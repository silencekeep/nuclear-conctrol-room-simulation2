using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class post_dropdown : MonoBehaviour
{
    public string f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, f13, f14, f15;
   
   public int a=0;
    void Start()
    {
        GameObject.Find("post_droplist/Dropdown1").GetComponent<Dropdown>().onValueChanged.AddListener(valueselect);
    }

    private void valueselect(int value)
    {
        switch (value)
        {
            case 0:
                a = 1;
                f1 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm";
                f2 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm";
                f3 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand";
                f4 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandIndex1";
                f5 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandMiddle1";
                f6 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandPinky1";
                f7 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandRing1";
                f8 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandIndex1/swat:LeftHandIndex2";
                f9 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandMiddle1/swat:LeftHandMiddle2";
                f10 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandPinky1/swat:LeftHandPinky2";
                f11 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandRing1/swat:LeftHandRing2";
                f12 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandIndex1/swat:LeftHandIndex2/swat:LeftHandIndex3";
                f13 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandMiddle1/swat:LeftHandMiddle2/swat:LeftHandMiddle3";
                f14 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandPinky1/swat:LeftHandPinky2/swat:LeftHandPinky3";
                f15 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandRing1/swat:LeftHandRing2/swat:LeftHandRing3";
                break;
            case 1:
                a = 2;
                f1 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm";
                f2 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm";
                f3 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand";
                f4 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandIndex1";
                f5 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandMiddle1";
                f6 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandPinky1";
                f7 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandRing1";
                f8 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandIndex1/swat:RightHandIndex2";
                f9 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandMiddle1/swat:RightHandMiddle2";
                f10 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandPinky1/swat:RightHandPinky2";
                f11 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandRing1/swat:RightHandRing2";
                f12 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandIndex1/swat:RightHandIndex2/swat:RightHandIndex3";
                f13 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandMiddle1/swat:RightHandMiddle2/swat:RightHandMiddle3";
                f14 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandPinky1/swat:RightHandPinky2/swat:RightHandPinky3";
                f15 = "/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandRing1/swat:RightHandRing2/swat:RightHandRing3";
                break;
            
        }

    }
}
