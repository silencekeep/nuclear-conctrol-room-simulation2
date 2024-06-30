using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class extractRequireData : MonoBehaviour
{
    public InputField height_StandingInputField, seegle_StandingInputField, shoulder_StandingInputField, fingertips_StandingInputField, stretch_StandingInputField,
                       axisDistance_StandingInputField, centralAxisToEye_StandingInputField, eyeToSide_StandingInputField, shoulderToSide_StandingInputField,//站姿
                       belowTheKnee_SittingInputField, heightAboveChair_SittingInputField, seegleAboveChair_SittingInputField, shoulderAboveChair_SittingInputField,
                       stretch_SittingInputField, thigh_SittingInputField, hipToKneeMesial_SittingInputField, kneeHeight_SittingInputField,
                       axisDistance_SittingInputField, centralAxisToEye_SittingInputField, seegleHeight_SittingInputField, shoulderHeight_SittingInputField,
                       hipToKnee_SittingInputField, foot_SittingInputField, bodyToToes_SittingInputField, eyeToSide_SittingInputField, shoulderToSide_SittingInputField;//坐姿
    public GameObject HumanDataPanel, RequireDataPanel;

    public Text HeigtNowText, SeegleNowText, ArmNowText, HandNowText;
    public void ExtractData()
    {
        RequireDataPanel.SetActive(true);
        HumanDataPanel.SetActive(false);

        height_StandingInputField.text = HeigtNowText.text;
        double seegleHeight_Standing = System.Convert.ToDouble(height_StandingInputField.text) - 11;
        seegle_StandingInputField.text = seegleHeight_Standing.ToString();
        //height_StandingInputField.text = GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HeigtNowText").GetComponent<Text>().text;
        //seegle_StandingInputField.text = GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/SeegleNowText").GetComponent<Text>().text;

        //伸展范围 ArmNowText HandNowText
        double stretch = System.Convert.ToDouble(ArmNowText.text) + System.Convert.ToDouble(HandNowText.text);
        //double stretch = System.Convert.ToDouble(GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/ArmNowText").GetComponent<Text>().text) + System.Convert.ToDouble(GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HandNowText").GetComponent<Text>().text);
        stretch_StandingInputField.text = stretch.ToString("0.0");
        stretch_SittingInputField.text = stretch_StandingInputField.text;
        //臂长
        double shoulder_Standing = 0.89 * System.Convert.ToDouble(height_StandingInputField.text) - 13.09;
        double numTwice_shoulder_Standing = Math.Round(shoulder_Standing * 2, 0);
        double num = numTwice_shoulder_Standing / 2;
        shoulder_StandingInputField.text = num.ToString("0.0");

        //指尖高
        double fingertips_Standing = 0.28 * System.Convert.ToDouble(height_StandingInputField.text) + 16.28;
        double numTwice_fingertips_Standing = Math.Round(fingertips_Standing * 2, 0);
        double num_fingertips_Standing = numTwice_fingertips_Standing / 2;
        fingertips_StandingInputField.text = num_fingertips_Standing.ToString("0.0");

        //人体轴线至台边距离
        double axisDistance_Standing = -0.01 * System.Convert.ToDouble(height_StandingInputField.text) + 14.74;
        double numTwice_axisDistance_Standing = Math.Round(axisDistance_Standing * 2, 0);
        double num_axisDistance_Standing = numTwice_axisDistance_Standing / 2;
        axisDistance_StandingInputField.text = num_axisDistance_Standing.ToString("0.0");
        shoulderToSide_StandingInputField.text = axisDistance_StandingInputField.text;// 肩至盘台前沿=人体轴线至台边距离
        axisDistance_SittingInputField.text = axisDistance_StandingInputField.text;
        shoulderToSide_SittingInputField.text = axisDistance_StandingInputField.text;

        //人体中心轴至眼距离
        double centralAxisToEye_Standing = 0.02 * System.Convert.ToDouble(height_StandingInputField.text) + 4.54;
        double numTwice_centralAxisToEye_Standing = Math.Round(centralAxisToEye_Standing * 2, 0);
        double num_centralAxisToEye_Standing = numTwice_centralAxisToEye_Standing / 2;
        centralAxisToEye_StandingInputField.text = num_centralAxisToEye_Standing.ToString("0.0");
        centralAxisToEye_SittingInputField.text = centralAxisToEye_StandingInputField.text;//坐姿同站姿

        //眼至盘台前沿
        double eyeToSide_Standing = -0.03 * System.Convert.ToDouble(height_StandingInputField.text) + 10.2;
        double numTwice_eyeToSide_Standing = Math.Round(eyeToSide_Standing * 2, 0);
        double num_eyeToSide_Standing = numTwice_eyeToSide_Standing / 2;
        eyeToSide_StandingInputField.text = num_eyeToSide_Standing.ToString("0.0");
        eyeToSide_SittingInputField.text = eyeToSide_StandingInputField.text;//坐姿同站姿

        //坐姿
        //腿部弯曲部位的高度（膝下方）
        double belowTheKnee_Sitting = 0.36 * System.Convert.ToDouble(height_StandingInputField.text) - 19.86;
        double numTwice_belowTheKnee_Sitting = Math.Round(belowTheKnee_Sitting * 2, 0);
        double num_belowTheKnee_Sitting = numTwice_belowTheKnee_Sitting / 2;
        belowTheKnee_SittingInputField.text = num_belowTheKnee_Sitting.ToString("0.0");

        //椅面以上的身高（伸直）
        double heightAboveChair_Sitting = 0.51 * System.Convert.ToDouble(height_StandingInputField.text) + 4.92;
        double numTwice_heightAboveChair_Sitting = Math.Round(heightAboveChair_Sitting * 2, 0);
        double num_heightAboveChair_Sitting = numTwice_heightAboveChair_Sitting / 2;
        heightAboveChair_SittingInputField.text = num_heightAboveChair_Sitting.ToString("0.0");

        //椅面以上眼高
        double seegleAboveChair_Sitting = 0.65 * System.Convert.ToDouble(height_StandingInputField.text) - 29.97;
        double numTwice_seegleAboveChair_Sitting = Math.Round(seegleAboveChair_Sitting * 2, 0);
        double num_seegleAboveChair_Sitting = numTwice_seegleAboveChair_Sitting / 2;
        seegleAboveChair_SittingInputField.text = num_seegleAboveChair_Sitting.ToString("0.0");

        //椅面以上肩高
        double shoulderAboveChair_Sitting = 0.42 * System.Convert.ToDouble(height_StandingInputField.text) - 10.93;
        double numTwice_shoulderAboveChair_Sitting = Math.Round(shoulderAboveChair_Sitting * 2, 0);
        double num_shoulderAboveChair_Sitting = numTwice_shoulderAboveChair_Sitting / 2;
        shoulderAboveChair_SittingInputField.text = num_shoulderAboveChair_Sitting.ToString("0.0");

        //大腿净高度
        double thigh_Sitting = 0.13 * System.Convert.ToDouble(height_StandingInputField.text) - 8.08;
        double numTwice_thigh_Sitting = Math.Round(thigh_Sitting * 2, 0);
        double num_thigh_Sitting = numTwice_thigh_Sitting / 2;
        thigh_SittingInputField.text = num_thigh_Sitting.ToString("0.0");

        //臀部至膝弯内侧距离
        double hipToKneeMesial_Sitting = 0.32 * System.Convert.ToDouble(height_StandingInputField.text) - 7.33;
        double numTwice_hipToKneeMesial_Sitting = Math.Round(hipToKneeMesial_Sitting * 2, 0);
        double num_hipToKneeMesial_Sitting = numTwice_hipToKneeMesial_Sitting / 2;
        hipToKneeMesial_SittingInputField.text = num_hipToKneeMesial_Sitting.ToString("0.0");

        //膝的高度
        double neeHeight_Sitting = 0.37 * System.Convert.ToDouble(height_StandingInputField.text) - 12.68;
        double numTwice_neeHeight_Sitting = Math.Round(neeHeight_Sitting * 2, 0);
        double num_neeHeight_Sitting = numTwice_neeHeight_Sitting / 2;
        kneeHeight_SittingInputField.text = num_neeHeight_Sitting.ToString("0.0");

        //坐姿眼高
        double seegleHeight_Sitting = 0.89 * System.Convert.ToDouble(height_StandingInputField.text) - 27.87;
        double numTwice_seegleHeight_Sitting = Math.Round(seegleHeight_Sitting * 2, 0);
        double num_seegleHeight_Sitting = numTwice_seegleHeight_Sitting / 2;
        seegleHeight_SittingInputField.text = num_seegleHeight_Sitting.ToString("0.0");


        //坐姿肩高
        double shoulderHeight_Sitting = 0.79 * System.Convert.ToDouble(height_StandingInputField.text) - 30.78;
        double numTwice_shoulderHeight_Sitting = Math.Round(shoulderHeight_Sitting * 2, 0);
        double num_shoulderHeight_Sitting = numTwice_shoulderHeight_Sitting / 2;
        shoulderHeight_SittingInputField.text = num_shoulderHeight_Sitting.ToString("0.0");

        //臀膝距
        double hipToKnee_Sitting = 0.34 * System.Convert.ToDouble(height_StandingInputField.text) - 1.5;
        double numTwice_hipToKnee_Sitting = Math.Round(hipToKnee_Sitting * 2, 0);
        double num_hipToKnee_Sitting = numTwice_hipToKnee_Sitting / 2;
        hipToKnee_SittingInputField.text = num_hipToKnee_Sitting.ToString("0.0");

        //脚长
        double foot_Sitting = 0.18 * System.Convert.ToDouble(height_StandingInputField.text) - 4.71;
        double numTwice_foot_Sitting = Math.Round(foot_Sitting * 2, 0);
        double num_foot_Sitting = numTwice_foot_Sitting / 2;
        foot_SittingInputField.text = num_foot_Sitting.ToString("0.0");

        //身体前端至脚尖距离
        double bodyToToes_Sitting = 0.47 * System.Convert.ToDouble(height_StandingInputField.text) - 19.38;
        double numTwice_bodyToToes_Sitting = Math.Round(bodyToToes_Sitting * 2, 0);
        double num_bodyToToes_Sitting = numTwice_bodyToToes_Sitting / 2;
        bodyToToes_SittingInputField.text = num_bodyToToes_Sitting.ToString("0.0");

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
