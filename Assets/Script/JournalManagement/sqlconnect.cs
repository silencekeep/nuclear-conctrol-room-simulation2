using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SQLite;
using System.Data;

public class sqlconnect : gSingleton<sqlconnect>
{
    //private static string sqlAddress1 = "server=" + service + ";database=NuclearControlRoomItemDB;uid=sa;pwd=123456";
    string connectionDBString = "Data Source=" + System.Environment.CurrentDirectory + @"\SQLiteData.db;Version=3;";



    //数据库查询
    public System.Data.DataSet sqlquery(string sql)
    {
        // string service = "127.0.0.1"; 
        // if (Gamedata.Instance)
        // {
        //     service = Gamedata.Instance.Ip;
        // }
        // string sqlAddress = "server=" + service + ";database=NuclearControlRoomItemDB;uid=sa;pwd=123456";
        //SQLiteConnection sqlCon = new SQLiteConnection("Data Source=" + System.Environment.CurrentDirectory + @"\SQLiteData.db;Version=3;");
        //sqlCon.Open();
        ////SqlCommand cmd = new SqlCommand();
        ////cmd.Connection = sqlCon;
        //SQLiteDataAdapter DC = new SQLiteDataAdapter(sql, sqlCon);
        //DataTable dc = new DataTable();
        //DC.Fill(dc, "table");
        //sqlCon.Close();
        //return dc;
        SQLiteConnection _connection = new SQLiteConnection(connectionDBString);
        SQLiteCommand _command = new SQLiteCommand(sql, _connection);
        _connection.Open();
        SQLiteDataAdapter adapter = new SQLiteDataAdapter(_command);
        DataSet data = new DataSet();
        adapter.Fill(data, "table");
        _connection.Close();
        return data;
    }
    //数据库增加
    public void Add(string sql)
    {
        //string service = "127.0.0.1";
        //if (Gamedata.Instance)
        //{
        //    service = Gamedata.Instance.Ip;
        //}
        //string sqlAddress = "server=" + service + ";database=NuclearControlRoomItemDB;uid=sa;pwd=123456";
        //SqlConnection sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        ////SqlCommand cmd = new SqlCommand();
        ////cmd.Connection = sqlCon;
        //SqlDataAdapter DC = new SqlDataAdapter(sql, sqlCon);
        //System.Data.DataSet dc = new System.Data.DataSet();
        //DC.Fill(dc, "table");
        SQLiteConnection _connection = new SQLiteConnection(connectionDBString);
        SQLiteCommand _command = new SQLiteCommand(sql, _connection);
        _connection.Open();
        _command.ExecuteNonQuery();
        //DataSet data = new DataSet();
        //adapter.Fill(data, "table");
        _connection.Close();
        //sqlCon.Close();
    }
    //日志管理
    public void sqladd(string sql)
    {
        Add(sql);
        //寻找数据库和操作
        string[] words = sql.Split(' ');
        if (words.Length > 2) {
            string TableName = words[2];
            string operation = words[0].Substring(0, 1).ToUpper() + words[0].Substring(1).ToLower();
            if ("Update".Equals(operation))
            {
                TableName = words[1];
            }
            //处理日志编号
            string now = DateTime.Now.ToString("yyyyMMdd");
            sql = "select * from JournalTable where Number like '%" + now + "%'";
            Debug.Log(sql);
            System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
            int n = dc.Tables[0].Rows.Count + 1;
            string Number = n.ToString();
            while (Number.Length < 4)
            {
                Number = "0" + Number;
            }
            Number = now + Number;
            //添加数据
            string time = DateTime.Now.ToString();
            string person = "yingyuan";
            if (Gamedata.Instance)
            {
                person = Gamedata.Instance.people;
            }           
            sql = "insert into JournalTable values ('" + Number + "', '" + TableName + "', '" + operation + "','" + time + "','" + person + "')";
            Add(sql);
        }       
    }
}
