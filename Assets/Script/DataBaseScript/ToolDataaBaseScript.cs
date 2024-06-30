using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System;
using System.IO;

public class ToolDataaBaseScript : MonoBehaviour
{
    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";
    Dropdown dropdown;
    // Use this for initialization
    // Start is called before the first frame update
    void Start()
    {
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
    }
    // Update is called once per frame
    void Update()
    {
        dropdown = this.GetComponent<Dropdown>();//获取下拉菜单控件


        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;


        SqlDataAdapter DC = new SqlDataAdapter("select * from tools ", sqlCon);
        System.Data.DataSet dc = new System.Data.DataSet();
        DC.Fill(dc);
        string[] Id_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的一列数据放进数组中
        {
            string Id = Convert.ToString(dc.Tables[0].Rows[i][1]);//设置读取那哪一列的数据
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

        //将选中的下拉框数据存放在数据库中
        //string DD = dropdown.captionText.text;
        //cmd.CommandText = "update plc_database set 盒子编号 =@DD where id=1";
        //SqlParameter p1 = cmd.Parameters.Add("DD", SqlDbType.VarString);
        //p1.Value = DD;
        //cmd.ExecuteNonQuery();
        sqlCon.Close();
        //Debug.Log("下拉框数据：" + dropdown.captionText.text);
    }
}

