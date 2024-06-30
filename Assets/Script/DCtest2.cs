using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SQLite;
using UnityEngine.UI;
using System.Text;
using System;
using System.Data;

public class DCtest2 : MonoBehaviour
{
    SQLiteConnection m_dbConnection;
    [SerializeField] Button button;
    //创建一个连接到指定数据库
    private void Start()
    {
        button.onClick.AddListener(Onclick2);
    }
    void connectToDatabase()
    {
        string str1 = "Data Source=" + System.Environment.CurrentDirectory + @"\SQLiteData.db;Version=3;";
        //string str2 = str1.Replace(@"\", "//"); //不需要转
        string str3 = "str3: Data Source=C://Users//CXT//Desktop//group//SQLiteData.db;Version=3;";
        string str4 = "Data Source=C://Users//CXT//Desktop//group//SQLiteData.db;Version=3;";
        //print(str1);
        //print(str3);
        m_dbConnection = new SQLiteConnection(str1);//Data Source=D://sqlite//demo.db与数据库文件对应
        m_dbConnection.Open();
    }

    //使用sql查询语句，并显示结果
    void Update()
    {
        //connectToDatabase();
        //string sql = "select * from AllWorkAreaTable";
        //SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
        //SQLiteDataReader reader = command.ExecuteReader();
        //while (reader.Read())
        //{
        //    print(reader[0]);
        //}
        //print("Data Source=C://Users//CXT//Desktop//group//SQLiteData.db;Version=3;");
        //print("Data Source=" + System.Environment.CurrentDirectory + "//SQLiteData.db;Version=3;");
        //SqliteAssist sa = new SqliteAssist();
        //sa.
    }

    void onclick()
    {
        string sql = "select * from AllWorkAreaTable";
        SqliteAssist sa = new SqliteAssist();
        SQLiteDataReader reader =  sa.GetQueryResult(sql);
        //foreach (var item in reader)
        //{
        //    print(item.ToString());
        //}
        StringBuilder sb = new StringBuilder();

        
        if (reader.HasRows)//如果有数据
        {
            List<string[]> strList = new List<string[]>();
            while (reader.Read())
            {
                print(reader[0]);
                String[] userIdArr = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++) //逐个字段的遍历
                {
                    sb.AppendFormat("{0},", reader[i].ToString());//字段之间用 |连接
                    //print(sb.ToString());
                    userIdArr = sb.ToString().Split(',');
                    
                }
                print(userIdArr.ToString());
                strList.Add(userIdArr);
                sb.Append("\r\n");//每一行数据换行                
            }

            string[,] arr = new string[strList.Count, reader.FieldCount];
            for (int i = 0; i < strList.Count; i++)
            {
                for (int j = 0; j < reader.FieldCount; j++)
                {
                    arr[i, j] = strList[i][j];
                }
            }
            print(arr);
        }        
    }

    void Onclick2()
    {
        string sql = "select * from AllWorkAreaTable";
        SqliteAssist sa = new SqliteAssist();
        DataSet dc = sa.GetQueryResultByDataSet(sql);
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < dc.Tables[0].Columns.Count; j++)
            {
                print(dc.Tables[0].Rows[i][j].ToString());
            }
            
        }
    }

    void GetInfoFromReader()
    {
        //StringBuilder sb = new StringBuilder();
        //if (dr.HasRows)//如果有数据
        //{
        //    while (dr.Read())
        //    {
        //        for (int i = 0; i < dr.FieldCount; i++) //逐个字段的遍历
        //        {
        //            sb.AppendFormat("{0}|", dr[i]);//字段之间用 |连接
        //        }
        //        sb.Append("\r\n");//每一行数据换行
        //    }
        //}

    }
}
