﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System;
using System.IO;

public class ActTypeDropdown : MonoBehaviour
{
    public Dropdown ActType1DropDown;
    public Dropdown ActTypeDropDown;
    string Seletedtype1;
    private SqlConnection sqlCon;
    public static bool ActTypeDropDownValueChanged;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";
    Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        ActTypeDropDownValueChanged = false;
    }
    public void ClaerOnDropDownValueChanged()//当动素种类下拉框值改变时调用(清除动素选择的内容
    {
        ActTypeDropDownValueChanged = true;
    }

    // Update is called once per frame
    void Update()
    {
        dropdown = this.GetComponent<Dropdown>();//获取下拉菜单控件
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;

        Seletedtype1 = ActType1DropDown.captionText.text;
        SqlDataAdapter DC = new SqlDataAdapter("select distinct ActType from actions  where ActGenType = '" + Seletedtype1 + "'", sqlCon);
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
        dropdown.options.Clear();
        Dropdown.OptionData tempData;
        for (int k = 0; k < dc.Tables[0].Rows.Count; k++)   //将数组中的数据取出显示在下拉列表中
        {
            tempData = new Dropdown.OptionData();
            tempData.text = Id_arr[k];
            dropdown.options.Add(tempData);
        }
        if (ActTypeDropDown.value == 0 && ActTypeDropDown.options.Count>0)
        {            
            ActTypeDropDown.captionText.text = dropdown.options[0].text;
        }
        if (ActGenTypeDropDown.ActType1DropDownValueChanged == true)
        {
            ActTypeDropDown.captionText.text =  dropdown.options[0].text;
            ActGenTypeDropDown.ActType1DropDownValueChanged = false;
        }
    }
}
