using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;
using System;

public class CombRowScripts : MonoBehaviour
{
    public static string RowActCombIDText;
    public static string RowActCombNameText;
    public static string RowActCombTimeText;
   // public static string RowActCombTypeText;
   // public static string RowActCombType1Text;
    public static string RowActCombDescriptionText;
    public static string RowActCombPathText;
    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";

    // Start is called before the first frame update
    void Start()
    {

    }

    public void RowEditClick()
    {
        Text RowActCombID = transform.GetChild(0).GetComponent<Text>();
        RowActCombIDText = RowActCombID.text;
        Text RowActCombName = transform.GetChild(1).GetComponent<Text>();
        RowActCombNameText = RowActCombName.text;
        Text RowActCombTime = transform.GetChild(2).GetComponent<Text>();
        RowActCombTimeText = RowActCombTime.text;
        //Text RowActType = transform.GetChild(2).GetComponent<Text>();
        //RowActCombTypeText = RowActType.text;
        //Text RowActType1 = transform.GetChild(1).GetComponent<Text>();
        //RowActCombType1Text = RowActType1.text;
        Text RowActCombPath = transform.GetChild(4).GetComponent<Text>();
        RowActCombPathText = RowActCombPath.text;
        Text RowActCombDescription = transform.GetChild(3).GetComponent<Text>();
        RowActCombDescriptionText = RowActCombDescription.text;
        ActCombManager.Combeditpanelsign = true;
        ActCombManager.Combeditshowsign = true;
        ActCombManager.CombEditActPathSign = true;
        //EditShow();
    }
    public void RowDeleteClick()//数据库删除功能
    {
        Text RowActCombID = transform.GetChild(0).GetComponent<Text>();
        RowActCombIDText = RowActCombID.text;
        Destroy(transform.gameObject);
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        string ActDelete = "delete from actionCombine where actionCombineID = '" + RowActCombIDText + "'";

        SqlCommand sqlCommand = new SqlCommand(ActDelete, sqlCon);
        sqlCommand.ExecuteNonQuery();
        sqlCon.Close();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
