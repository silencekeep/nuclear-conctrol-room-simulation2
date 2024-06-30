using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;
using System;

public class RowScripts : MonoBehaviour
{

    public static string height_StandingText;
    public static string seegle_StandingText;
    public static string shoulder_StandingText;
    public static string fingertips_StandingText;
    public static string stretch_StandingText;
    public static string axisDistance_StandingText;
    public static string centralAxisToEye_StandingText;
    public static string eyeToSide_StandingText;
    public static string shoulderToSide_StandingText;
    public static string percentage_StandingText;
    public static string belowTheKnee_SittingText,
        heightAboveChair_SittingText ,
        seegleAboveChair_SittingText,
        shoulderAboveChair_SittingText ,
        stretch_SittingText ,
        thigh_SittingText ,
        hipToKneeMesial_SittingText ,
        kneeHeight_SittingText ,
        axisDistance_SittingText ,
        centralAxisToEye_SittingText,
        seegleHeight_SittingText,
        shoulderHeight_SittingText,
        hipToKnee_SittingText,
        foot_SittingText ,
        bodyToToes_SittingText,
        eyeToSide_SittingText ,
        shoulderToSide_SittingText,
        percentage_SittingText;

    //private SqlConnection sqlCon;
    //private string sqlAddress = "server=127.0.0.1;database=NuclearControlRoomItemDB;uid=sa;pwd=123456";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RowEditClick1()  //数据库编辑功能 站姿
    {

        height_StandingText = transform.GetChild(0).GetComponent<Text>().text;
        seegle_StandingText = transform.GetChild(1).GetComponent<Text>().text;
        shoulder_StandingText = transform.GetChild(2).GetComponent<Text>().text;
        fingertips_StandingText = transform.GetChild(3).GetComponent<Text>().text;
        stretch_StandingText = transform.GetChild(4).GetComponent<Text>().text;
        axisDistance_StandingText = transform.GetChild(5).GetComponent<Text>().text;
        centralAxisToEye_StandingText = transform.GetChild(6).GetComponent<Text>().text;
        eyeToSide_StandingText = transform.GetChild(7).GetComponent<Text>().text;
        shoulderToSide_StandingText = transform.GetChild(8).GetComponent<Text>().text;
        percentage_StandingText = transform.GetChild(9).GetComponent<Text>().text;

        ActManager.editpanelsign = true;
        ActManager.editshowsign = true;
        //ActManager.EditActPathSign = true;
        //EditShow();
    }

    public void RowEditClick2()  //数据库编辑功能 坐姿
    {

        belowTheKnee_SittingText = transform.GetChild(0).GetComponent<Text>().text;
        heightAboveChair_SittingText = transform.GetChild(1).GetComponent<Text>().text;
        seegleAboveChair_SittingText = transform.GetChild(2).GetComponent<Text>().text;
        shoulderAboveChair_SittingText = transform.GetChild(3).GetComponent<Text>().text;
        stretch_SittingText = transform.GetChild(4).GetComponent<Text>().text;
        thigh_SittingText = transform.GetChild(5).GetComponent<Text>().text;
        hipToKneeMesial_SittingText = transform.GetChild(6).GetComponent<Text>().text;
        kneeHeight_SittingText = transform.GetChild(7).GetComponent<Text>().text;
        axisDistance_SittingText = transform.GetChild(8).GetComponent<Text>().text;
        centralAxisToEye_SittingText = transform.GetChild(9).GetComponent<Text>().text;
        seegleHeight_SittingText = transform.GetChild(10).GetComponent<Text>().text;
        shoulderHeight_SittingText = transform.GetChild(11).GetComponent<Text>().text;
        hipToKnee_SittingText = transform.GetChild(12).GetComponent<Text>().text;
        foot_SittingText = transform.GetChild(13).GetComponent<Text>().text;
        bodyToToes_SittingText = transform.GetChild(14).GetComponent<Text>().text;
        eyeToSide_SittingText = transform.GetChild(15).GetComponent<Text>().text;
        shoulderToSide_SittingText = transform.GetChild(16).GetComponent<Text>().text;
        percentage_SittingText = transform.GetChild(17).GetComponent<Text>().text;


        ActManager.editpanelsign = true;
        ActManager.editshowsign = true;
        //ActManager.EditActPathSign = true;
        //EditShow();
    }





    public void RowDeleteClick1()//数据库删除功能  站姿
    {
        percentage_StandingText = transform.GetChild(9).GetComponent<Text>().text;
        Destroy(transform.gameObject);
        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        string ActDelete = "delete from zhanzi where percentage = '" + percentage_StandingText + "'";
        sqlconnect.Instance.sqladd(ActDelete);
        //SqlCommand sqlCommand = new SqlCommand(ActDelete, sqlCon);
        //sqlCommand.ExecuteNonQuery();
        //sqlCon.Close();
    }



    public void RowDeleteClick2()//数据库删除功能  坐姿
    {
        percentage_SittingText = transform.GetChild(17).GetComponent<Text>().text;
        Destroy(transform.gameObject);
        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        string ActDelete = "delete from zuozi where percentage = '" + percentage_SittingText + "'";
        sqlconnect.Instance.sqladd(ActDelete);
        //SqlCommand sqlCommand = new SqlCommand(ActDelete, sqlCon);
        //sqlCommand.ExecuteNonQuery();
        //sqlCon.Close();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
