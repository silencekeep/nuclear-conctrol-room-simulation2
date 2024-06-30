using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System;
using System.IO;

public class AddTypeDropDown : MonoBehaviour
{
    public Dropdown AddTypeDropdown;
    public InputField AddTypeInputField;
    public Dropdown AddType1Dropdown;
    //public InputField AddType1InputField;
    private SqlConnection sqlCon;
    private string Seletedtype1;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";
    Dropdown dropdown;
    private bool firstload;
    // Start is called before the first frame update
    void Start()
    {
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        firstload=true;
}

    public void TypeTextFilled()//添加动素界面选择动素种类时调用
    {
        AddTypeInputField.text = AddTypeDropdown.captionText.text;
    }
    
    // Update is called once per frame
    void Update()
    {
        dropdown = this.GetComponent<Dropdown>();//获取下拉菜单控件
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;
        Seletedtype1 = AddType1Dropdown.captionText.text;
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
        if (AddTypeDropdown.value == 0 && AddType1Dropdown.captionText.text!="")
        {
            AddTypeDropdown.captionText.text = dropdown.options[0].text;
            if (firstload == true || AddType1Dropdown.value != 0)
            {
                firstload = false;
                AddTypeInputField.text = AddTypeDropdown.captionText.text;
            }
        }
    }
}
