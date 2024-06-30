using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Data.SqlClient;


public class DCtest : MonoBehaviour
{
    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=NuclearControlRoomItemDB;uid=sa;pwd=123456";//连接数据库所需参数，可自行更改
    private string str;
    // private SqlConnection sqlCon;
    public void SqlSelect()
    {
        sqlCon = new SqlConnection(sqlAddress); //连接对象
        SqlDataAdapter sda = new SqlDataAdapter("select * from L810", sqlCon);//创建命令对象

        //实例化数据集，并写入查询到的数据
        System.Data.DataSet ds = new System.Data.DataSet();
        sda.Fill(ds, "table");

        //按行和列打印出数据
        for (int i = 0; i < 2; i++)
        {

            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
            {
                str += ds.Tables[0].Rows[i][j].ToString().Trim() + "    ";
                if (j == ds.Tables[0].Columns.Count - 1)
                {
                    print(str);
                    str = "";
                }
            }
        }
    }
}
