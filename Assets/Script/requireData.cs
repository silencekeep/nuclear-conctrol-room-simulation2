using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

public class requireData : MonoBehaviour
{
    public GameObject saveButton, saveOkPanel, Standing, Sitting, importPanel, importOkPanel, RequireDataPanel;
    public InputField nameIputField_Standing, height_StandingInputField, seegle_StandingInputField, shoulder_StandingInputField, fingertips_StandingInputField,
                      stretch_StandingInputField, axisDistance_StandingInputField, centralAxisToEye_StandingInputField, eyeToSide_StandingInputField, shoulderToSide_StandingInputField,//站姿
                      nameIputField_Sitting, belowTheKnee_SittingInputField, heightAboveChair_SittingInputField, seegleAboveChair_SittingInputField,
                      shoulderAboveChair_SittingInputField, stretch_SittingInputField,thigh_SittingInputField, hipToKneeMesial_SittingInputField,kneeHeight_SittingInputField, 
                      axisDistance_SittingInputField, centralAxisToEye_SittingInputField, seegleHeight_SittingInputField,shoulderHeight_SittingInputField, 
                      hipToKnee_SittingInputField, foot_SittingInputField, bodyToToes_SittingInputField, eyeToSide_SittingInputField, shoulderToSide_SittingInputField;//坐姿
    public Dropdown posSelect;
    //private SqlConnection sqlCon;
    //private string sqlAddress = "server=127.0.0.1;database=NuclearControlRoomItemDB;uid=sa;pwd=123456";

    
    //站姿
    string height_Standing;
    string seegle_Standing;
    string shoulder_Standing;
    string fingertips_Standing;
    string stretch_Standing;
    string axisDistance_Standing;
    string centralAxisToEye_Standing;
    string eyeToSide_Standing;
    string shoulderToSide_Standing;
    string percentage_Standing;

    //坐姿
    string belowTheKnee_Sitting;
    string heightAboveChair_Sitting;
    string seegleAboveChair_Sitting;
    string shoulderAboveChair_Sitting;
    string stretch_Sitting;
    string thigh_Sitting;
    string hipToKneeMesial_Sitting;
    string kneeHeight_Sitting;
    string axisDistance_Sitting;
    string centralAxisToEye_Sitting;
    string seegleHeight_Sitting;
    string shoulderHeight_Sitting;
    string hipToKnee_Sitting;
    string foot_Sitting;
    string bodyToToes_Sitting;
    string eyeToSide_Sitting;
    string shoulderToSide_Sitting;
    string percentage_Sitting;

    public void selectMethod()
    {
        if (posSelect.value == 0)
        {
            Standing.SetActive(true);
            Sitting.SetActive(false);
        }
        else
        {
            if (posSelect.value == 1)
            {
                Sitting.SetActive(true);
                Standing.SetActive(false);
            }
        }
    }

    //清空文本框中的数据
    public void AddOnClick() {
        height_StandingInputField.text=" ";
        seegle_StandingInputField.text = " ";
        shoulder_StandingInputField.text = " ";
        fingertips_StandingInputField.text = " ";
        stretch_StandingInputField.text = " ";
        axisDistance_StandingInputField.text = " ";
        centralAxisToEye_StandingInputField.text = " ";
        eyeToSide_StandingInputField.text = " ";
        shoulderToSide_StandingInputField.text = " ";
        nameIputField_Standing.text = " ";
        belowTheKnee_SittingInputField.text = " ";
        heightAboveChair_SittingInputField.text = " ";
        seegleAboveChair_SittingInputField.text = " ";
        shoulderAboveChair_SittingInputField.text = " ";
        stretch_SittingInputField.text = " ";
        thigh_SittingInputField.text = " ";
        hipToKneeMesial_SittingInputField.text = " ";
        kneeHeight_SittingInputField.text = " ";
        axisDistance_SittingInputField.text = " ";
        centralAxisToEye_SittingInputField.text = " ";
        seegleHeight_SittingInputField.text = " ";
        shoulderHeight_SittingInputField.text = " ";
        hipToKnee_SittingInputField.text = " ";
        foot_SittingInputField.text = " ";
        bodyToToes_SittingInputField.text = " ";
        eyeToSide_SittingInputField.text = " ";
        shoulderToSide_SittingInputField.text = " ";
        nameIputField_Sitting.text = " ";
    }



    public void AddRequestData_Standing()//站姿新增
    {
        height_Standing = height_StandingInputField.text;
        seegle_Standing = seegle_StandingInputField.text;
        shoulder_Standing = shoulder_StandingInputField.text;
        fingertips_Standing = fingertips_StandingInputField.text;
        stretch_Standing = stretch_StandingInputField.text;
        axisDistance_Standing = axisDistance_StandingInputField.text;
        centralAxisToEye_Standing = centralAxisToEye_StandingInputField.text;
        eyeToSide_Standing = eyeToSide_StandingInputField.text;
        shoulderToSide_Standing = shoulderToSide_StandingInputField.text;
        percentage_Standing = nameIputField_Standing.text;

        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();

        string requestData_StandingInsert = "insert into zhanzi (height,seegle,shoulder,fingertips,stretch,axisDistance,centralAxisToEye,eyeToSide,shoulderToSide,percentage) " + "values ('" + height_Standing + "','" + seegle_Standing + "','" + shoulder_Standing + "','" + fingertips_Standing + "','" + stretch_Standing + "','" + axisDistance_Standing + "','" + centralAxisToEye_Standing + "','" + eyeToSide_Standing + "','" + shoulderToSide_Standing + "','" + percentage_Standing + "')";
        sqlconnect.Instance.sqladd(requestData_StandingInsert);
        //SqlCommand sqlCommand = new SqlCommand(requestData_StandingInsert, sqlCon);
        //sqlCommand.ExecuteNonQuery();
        //sqlCon.Close();
        saveOkPanel.SetActive(true);
    }

   

    public void AddRequestData_Sitting()//坐姿新增
    {
        belowTheKnee_Sitting = belowTheKnee_SittingInputField.text;
        heightAboveChair_Sitting = heightAboveChair_SittingInputField.text;
        seegleAboveChair_Sitting = seegleAboveChair_SittingInputField.text;
        shoulderAboveChair_Sitting = shoulderAboveChair_SittingInputField.text; 
        stretch_Sitting = stretch_SittingInputField.text;
        thigh_Sitting = thigh_SittingInputField.text;
        hipToKneeMesial_Sitting = hipToKneeMesial_SittingInputField.text;
        kneeHeight_Sitting = kneeHeight_SittingInputField.text;
        axisDistance_Sitting = axisDistance_SittingInputField.text;
        centralAxisToEye_Sitting = centralAxisToEye_SittingInputField.text;
        seegleHeight_Sitting = seegleHeight_SittingInputField.text;
        shoulderHeight_Sitting = shoulderHeight_SittingInputField.text;
        hipToKnee_Sitting = hipToKnee_SittingInputField.text;
        foot_Sitting = foot_SittingInputField.text;
        bodyToToes_Sitting = bodyToToes_SittingInputField.text;
        eyeToSide_Sitting = eyeToSide_SittingInputField.text;
        shoulderToSide_Sitting = shoulderToSide_SittingInputField.text;
        percentage_Sitting = nameIputField_Sitting.text;


        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();

        string requestData_SittingInsert = "insert into zuozi (belowTheKnee,heightAboveChair,seegleAboveChair,shoulderAboveChair,stretch,thigh,hipToKneeMesial,kneeHeight,axisDistance,centralAxisToEye,seegleHeight,shoulderHeight,hipToKnee,foot,bodyToToes,eyeToSide,shoulderToSide,percentage) " + 
            "values ('" + belowTheKnee_Sitting + "','" + heightAboveChair_Sitting + "','" + seegleAboveChair_Sitting + "','" + shoulderAboveChair_Sitting + "','" + stretch_Sitting + "','" + thigh_Sitting + "','" + hipToKneeMesial_Sitting + "','" + kneeHeight_Sitting + "','" + axisDistance_Sitting + "','" + centralAxisToEye_Sitting + "'," +
            "'" + seegleHeight_Sitting + "','" + shoulderHeight_Sitting + "','" + hipToKnee_Sitting + "','" + foot_Sitting + "','" + bodyToToes_Sitting + "','" + eyeToSide_Sitting + "','" + shoulderToSide_Sitting + "','" + percentage_Sitting + "')";
        sqlconnect.Instance.sqladd(requestData_SittingInsert);
        //SqlCommand sqlCommand = new SqlCommand(requestData_SittingInsert, sqlCon);
        //sqlCommand.ExecuteNonQuery();
        //sqlCon.Close();
        saveOkPanel.SetActive(true);
    }


    //将页面数据存入数据库
    public void SaveMethod()
    {
        if (posSelect.value == 0)
        {
            AddRequestData_Standing();
        }
        else
        {
            if (posSelect.value == 1)
            {
                AddRequestData_Sitting();
            }
        }
    }

   




    public void exitButton()
    {
        saveOkPanel.SetActive(false);
    }

    public void ImportPanelShow()
    {
        importPanel.SetActive(true);
    }
    public void ImportOkYesButtonClicked()
    {
        importOkPanel.SetActive(false);
        importPanel.SetActive(false);
    }
    public void ImportPanelClose()
    {
        importPanel.SetActive(false);
    }
    public void RequireOkCloseImageButtonClicked()
    {
        RequireDataPanel.SetActive(false);

    }


}
