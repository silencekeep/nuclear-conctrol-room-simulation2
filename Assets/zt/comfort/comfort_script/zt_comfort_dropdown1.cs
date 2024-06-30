using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_comfort_dropdown1 : MonoBehaviour
{
    public string RightHand;
    public string LeftHand;
    public string RightArm;
    public string LeftArm;
    public string Spine;
    public string RightForeArm;
    public string LeftForeArm;
    public int d1, d2, d3 = 0;
    public GameObject dropdown;
    zt_comfort_open1 open;

    void Start()
    {
        dropdown.GetComponent<Dropdown>().onValueChanged.AddListener(ConsoleResult1);
        open= FindObjectOfType <zt_comfort_open1>();
    }

    private void ConsoleResult1(int value)
    {
        switch (value)
        {
            case 0:
                RightHand= "soldier1/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/rCollar/rShldrBend/rShldrTwist/rForearmBend/rForearmTwist/rHand";
                LeftHand = "soldier1/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/lCollar/lShldrBend/lShldrTwist/lForearmBend/lForearmTwist/lHand";
                RightArm= "soldier1/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/rCollar/rShldrBend";
                LeftArm = "soldier1/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/lCollar/lShldrBend";
                RightForeArm = "soldier1/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/rCollar/rShldrBend/rShldrTwist/rForearmBend";
                LeftForeArm = "soldier1/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/lCollar/lShldrBend/lShldrTwist/lForearmBend" ;
                Spine = "soldier1/Male/hip/abdomenLower";
                d1 = 1;
                break;
            case 1:
                RightHand = "soldier2/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/rCollar/rShldrBend/rShldrTwist/rForearmBend/rForearmTwist/rHand";
                LeftHand = "soldier2/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/lCollar/lShldrBend/lShldrTwist/lForearmBend/lForearmTwist/lHand";
                RightArm = "soldier2/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/rCollar/rShldrBend";
                LeftArm = "soldier2/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/lCollar/lShldrBend";
                RightForeArm = "soldier2/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/rCollar/rShldrBend/rShldrTwist/rForearmBend";
                LeftForeArm = "soldier2/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/lCollar/lShldrBend/lShldrTwist/lForearmBend";
                Spine = "soldier2/Male/hip/abdomenLower";
                d2 = 1;

                break;
            case 2:
                RightHand = "soldier3/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/rCollar/rShldrBend/rShldrTwist/rForearmBend/rForearmTwist/rHand";
                LeftHand = "soldier3/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/lCollar/lShldrBend/lShldrTwist/lForearmBend/lForearmTwist/lHand";
                RightArm = "soldier3/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/rCollar/rShldrBend";
                LeftArm = "soldier3/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/lCollar/lShldrBend";
                RightForeArm = "soldier3/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/rCollar/rShldrBend/rShldrTwist/rForearmBend";
                LeftForeArm = "soldier3/Male/hip/abdomenLower/abdomenUpper/chestLower/chestUpper/lCollar/lShldrBend/lShldrTwist/lForearmBend";
                Spine = "soldier3/Male/hip/abdomenLower";
                d3 = 1;
                break;

        }

    }
}
