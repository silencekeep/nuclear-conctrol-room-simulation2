using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System;
using System.IO;

public class Delete_toolsDB : MonoBehaviour
{
    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";
    Dropdown.OptionData m_TempData;
    public Dropdown Drd_IPList;

    // Use this for initialization

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;

        SqlDataAdapter DC = new SqlDataAdapter("delete from ToolPath where ToolName='" + Drd_IPList.captionText.text+"'", sqlCon);
        System.Data.DataSet dc = new System.Data.DataSet();
        DC.Fill(dc);

        int a = Drd_IPList.value;
        m_TempData = Drd_IPList.options[a];
        Drd_IPList.options.Remove(m_TempData);
        Drd_IPList.captionText.text = "";

        sqlCon.Close();
    }
}