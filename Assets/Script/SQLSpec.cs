﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System;
using System.IO;

public class SQLSpec : MonoBehaviour
{
    // Start is called before the first frame update private SqlConnection sqlCon;
    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";
    public Dropdown Drd_IPList;
    public Dropdown dp1;
    void Start()
    {
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
    }

    // Update is called once per frame
    void Update()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;

        dp1.onValueChanged.AddListener(delegate
        {
            SqlDataAdapter DC = new SqlDataAdapter("select distinct SPEC from ToolPath where ToolName = '" + dp1.captionText.text + "'", sqlCon);
            System.Data.DataSet dc = new System.Data.DataSet();
            DC.Fill(dc);




            string[] Id_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
            for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的一列数据放进数组中
            {
                string Id = Convert.ToString(dc.Tables[0].Rows[i][0]);//double
                if (Id != null)
                    Id_arr[i] = Id;
                //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
                //dc.Tables[0].Rows[i][0])表示取数据表中第一列所有数据
            }
            Drd_IPList.options.Clear();
            Dropdown.OptionData tempData;
            for (int k = 0; k < dc.Tables[0].Rows.Count; k++)   //将数组中的数据取出显示在下拉列表中
            {
                tempData = new Dropdown.OptionData();
                tempData.text = Id_arr[k];
                Drd_IPList.options.Add(tempData);
            }
            Drd_IPList.captionText.text = Convert.ToString(dc.Tables[0].Rows[0][0]);
            sqlCon.Close();
        });
    }
}
