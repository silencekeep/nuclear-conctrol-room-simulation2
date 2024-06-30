using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System;
using System.IO;
public class ActionDataBaseScript : MonoBehaviour
{
    public Dropdown ActType1DropDown;
    public Dropdown ActTypeDropDown;
    public Dropdown ActDropDown;
    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";
    Dropdown dropdown;
    string Seletedtype;
    

    void Start()
    {
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        dropdown = this.GetComponent<Dropdown>();//获取下拉菜单控件          
        
    }


    // Update is called once per frame    
    void Update()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;
        

        Seletedtype = ActTypeDropDown.captionText.text;
        SqlDataAdapter DC = new SqlDataAdapter("select * from actions where ActType='" + Seletedtype + "' ", sqlCon);//and ActGenType = '" + Seletedtype1 + "'
        System.Data.DataSet dc = new System.Data.DataSet();
        DC.Fill(dc);
        string[] Id_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的一列数据放进数组中
        {
            string Id = Convert.ToString(dc.Tables[0].Rows[i][2]);//double
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
        sqlCon.Close();
        if (ActTypeDropDown.options.Count != 0)//ActDropDown.value == 0
        {
            if (ActDropDown.value == 0)
            {
                ActDropDown.captionText.text = dropdown.options[0].text;
            }
            if (ActTypeDropdown.ActTypeDropDownValueChanged == true)
            {
                ActDropDown.captionText.text = dropdown.options[0].text;
                ActTypeDropdown.ActTypeDropDownValueChanged = false;
            }
        }
    }
}
