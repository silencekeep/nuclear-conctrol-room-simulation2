using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdown : MonoBehaviour
{
    public string ValueChange;
    public string joint;
    void Start()
    {
        GameObject.Find("Dropdown").GetComponent<Dropdown>().onValueChanged.AddListener(ConsoleResult);
    }
    private void ConsoleResult(int value)
    {
        switch (value)
        {
            case 0:
                ValueChange = "swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand";
                joint = "swat:LeftHand";
                break;
            case 1:
                ValueChange = "swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm";
                joint = "swat:LeftForeArm";
                break;
            case 2:
                ValueChange = "swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm";
                joint = "swat:LeftArm";
                break;
            case 3:
                ValueChange = "swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand";
                joint = "swat:RightHand";
                break;
            
            case 4:
                ValueChange = "swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm";
                joint = "swat:RightForeArm";
                break;
            case 5:
                ValueChange = "swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm";
                joint = "swat:RightArm";
                break;
        }
    }
}
