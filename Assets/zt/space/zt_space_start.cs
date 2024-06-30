using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zt_space_start : MonoBehaviour
{
    zt_space_dropdown dropdown1;
    public GameObject human1_RightArm, human1_RightForeArm, human1_RightHand, human1_LeftHandIndex1, human1_LeftArm, human1_LeftForeArm, human1_LeftHand, human1_RightHandIndex1;
    public GameObject human2_RightArm, human2_RightForeArm, human2_RightHand, human2_LeftHandIndex1, human2_LeftArm, human2_LeftForeArm, human2_LeftHand, human2_RightHandIndex1;
    public GameObject human3_RightArm, human3_RightForeArm, human3_RightHand, human3_LeftHandIndex1,human3_LeftArm, human3_LeftForeArm, human3_LeftHand, human3_RightHandIndex1;


    void Start()
    {
        dropdown1 = FindObjectOfType<zt_space_dropdown>();
        //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm").GetComponent<zt_space_rightupperarm>().enabled = false;
        //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm").GetComponent<zt_space_rightForearm>().enabled = false;
        //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand").GetComponent<zt_space_right1>().enabled = false;
        //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandIndex1").GetComponent<zt_space_right2>().enabled = false;

        //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm").GetComponent<zt_space_leftupperarm>().enabled = false;
        //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm").GetComponent<zt_space_leftForearm>().enabled = false;
        //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand").GetComponent<zt_space_left1>().enabled = false;
        //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandIndex1").GetComponent<zt_space_left2>().enabled = false;



        human1_RightArm.GetComponent<zt_space_rightupperarm>().enabled = false;
        human1_RightForeArm.GetComponent<zt_space_rightForearm>().enabled = false;
        human1_RightHand.GetComponent<zt_space_right1>().enabled = false;
        human1_RightHandIndex1.GetComponent<zt_space_right2>().enabled = false;
        human1_LeftArm.GetComponent<zt_space_leftupperarm>().enabled = false;
        human1_LeftForeArm.GetComponent<zt_space_leftForearm>().enabled = false;
        human1_LeftHand.GetComponent<zt_space_left1>().enabled = false;
        human1_LeftHandIndex1.GetComponent<zt_space_left2>().enabled = false;

        human2_RightArm.GetComponent<zt_space_2rightupperarm>().enabled = false;
        human2_RightForeArm.GetComponent<zt_space_2rightForearm>().enabled = false;
        human2_RightHand.GetComponent<zt_space_2right1>().enabled = false;
        human2_RightHandIndex1.GetComponent<zt_space_2right2>().enabled = false;
        human2_LeftArm.GetComponent<zt_space_2leftupperarm>().enabled = false;
        human2_LeftForeArm.GetComponent<zt_space_2leftForearm>().enabled = false;
        human2_LeftHand.GetComponent<zt_space_2left1>().enabled = false;
        human2_LeftHandIndex1.GetComponent<zt_space_2left2>().enabled = false;

        human3_RightArm.GetComponent<zt_space_3rightupperarm>().enabled = false;
        human3_RightForeArm.GetComponent<zt_space_3rightForearm>().enabled = false;
        human3_RightHand.GetComponent<zt_space_3right1>().enabled = false;
        human3_RightHandIndex1.GetComponent<zt_space_3right2>().enabled = false;
        human3_LeftArm.GetComponent<zt_space_3leftupperarm>().enabled = false;
        human3_LeftForeArm.GetComponent<zt_space_3leftForearm>().enabled = false;
        human3_LeftHand.GetComponent<zt_space_3left1>().enabled = false;
        human3_LeftHandIndex1.GetComponent<zt_space_3left2>().enabled = false;

        //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm").GetComponent<zt_space_leftupperarm>().enabled = false;
        //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm").GetComponent<zt_space_leftForearm>().enabled = false;
        //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand").GetComponent<zt_space_left1>().enabled = false;
        //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandIndex1").GetComponent<zt_space_left2>().enabled = false;


        //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm").GetComponent<zt_space_rightupperarm>().enabled = false;
        //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm").GetComponent<zt_space_rightForearm>().enabled = false;
        //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand").GetComponent<zt_space_right1>().enabled = false;
        //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandIndex1").GetComponent<zt_space_right2>().enabled = false;

        //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm").GetComponent<zt_space_leftupperarm>().enabled = false;
        //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm").GetComponent<zt_space_leftForearm>().enabled = false;
        //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand").GetComponent<zt_space_left1>().enabled = false;
        //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandIndex1").GetComponent<zt_space_left2>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dropdown1.a == 2)
        {
            //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm").GetComponent<zt_space_rightupperarm>().enabled = true;
            //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm").GetComponent<zt_space_rightForearm>().enabled = true;
            //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand").GetComponent<zt_space_right1>().enabled = true;
            //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandIndex1").GetComponent<zt_space_right2>().enabled = true;

            //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm").GetComponent<zt_space_leftupperarm>().enabled = true;
            //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm").GetComponent<zt_space_leftForearm>().enabled = true;
            //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand").GetComponent<zt_space_left1>().enabled = true;
            //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandIndex1").GetComponent<zt_space_left2>().enabled = true;
            human1_RightArm.GetComponent<zt_space_rightupperarm>().enabled = true;
            human1_RightForeArm.GetComponent<zt_space_rightForearm>().enabled = true;
            human1_RightHand.GetComponent<zt_space_right1>().enabled = true;
            human1_RightHandIndex1.GetComponent<zt_space_right2>().enabled = true;
            human1_LeftArm.GetComponent<zt_space_leftupperarm>().enabled = true;
            human1_LeftForeArm.GetComponent<zt_space_leftForearm>().enabled = true;
            human1_LeftHand.GetComponent<zt_space_left1>().enabled = true;
            human1_LeftHandIndex1.GetComponent<zt_space_left2>().enabled = true;
        }
        if (dropdown1.a == 3)
        {
            //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm").GetComponent<zt_space_rightupperarm>().enabled = true;
            //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm").GetComponent<zt_space_rightForearm>().enabled = true;
            //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand").GetComponent<zt_space_right1>().enabled = true;
            //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm/swat:RightHand/swat:RightHandIndex1").GetComponent<zt_space_right2>().enabled = true;

            //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm").GetComponent<zt_space_leftupperarm>().enabled = true;
            //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm").GetComponent<zt_space_leftForearm>().enabled = true;
            //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand").GetComponent<zt_space_left1>().enabled = true;
            //GameObject.Find("swat1/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand/swat:LeftHandIndex1").GetComponent<zt_space_left2>().enabled = true;
            human2_RightArm.GetComponent<zt_space_2rightupperarm>().enabled = true;
            human2_RightForeArm.GetComponent<zt_space_2rightForearm>().enabled = true;
            human2_RightHand.GetComponent<zt_space_2right1>().enabled = true;
            human2_RightHandIndex1.GetComponent<zt_space_2right2>().enabled = true;
            human2_LeftArm.GetComponent<zt_space_2leftupperarm>().enabled = true;
            human2_LeftForeArm.GetComponent<zt_space_2leftForearm>().enabled = true;
            human2_LeftHand.GetComponent<zt_space_2left1>().enabled = true;
            human2_LeftHandIndex1.GetComponent<zt_space_2left2>().enabled = true;
        }
        if (dropdown1.a == 4)
        {
            human3_RightArm.GetComponent<zt_space_3rightupperarm>().enabled = true;
            human3_RightForeArm.GetComponent<zt_space_3rightForearm>().enabled = true;
            human3_RightHand.GetComponent<zt_space_3right1>().enabled = true;
            human3_RightHandIndex1.GetComponent<zt_space_3right2>().enabled = true;
            human3_LeftArm.GetComponent<zt_space_3leftupperarm>().enabled = true;
            human3_LeftForeArm.GetComponent<zt_space_3leftForearm>().enabled = true;
            human3_LeftHand.GetComponent<zt_space_3left1>().enabled = true;
            human3_LeftHandIndex1.GetComponent<zt_space_3left2>().enabled = true;
        }
    }
}
