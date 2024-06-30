using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SQLite;
using System.Data;

public class SqliteAssist: MonoBehaviour
{
    private readonly string connectionString;
    string connectionDBString = "Data Source=" + System.Environment.CurrentDirectory + @"\SQLiteData.db;Version=3;";
    SQLiteConnection _connection;
    SQLiteCommand _command;
    public SqliteAssist(string dbPath)
    {
        // 设置SQLite连接字符串
        //connectionString = $"Data Source={dbPath};Version=3;";
    }

    public SqliteAssist()
    {
        _connection = new SQLiteConnection(connectionDBString);

    }

    /// <summary>
    /// 执行查询语句
    /// </summary>
    /// <param name="SQL语句"></param>
    /// <returns></returns>
    public SQLiteDataReader GetQueryResult(string SQL语句)
    {
        //SQLiteConnection connection = new SQLiteConnection(connectionDBString);
        _command = new SQLiteCommand(SQL语句, _connection);
        _connection.Open();

        SQLiteDataReader reader = _command.ExecuteReader();

        return reader;
    }

    public DataTable GetQueryResultByDataTable(string SQL语句)
    {
        _command = new SQLiteCommand(SQL语句, _connection);
        _connection.Open();
        SQLiteDataAdapter adapter = new SQLiteDataAdapter(_command);
        DataTable data = new DataTable();
        adapter.Fill(data);
        return data;
    }
    public DataSet GetQueryResultByDataSet(string SQL语句)
    {
        _command = new SQLiteCommand(SQL语句, _connection);
        _connection.Open();
        SQLiteDataAdapter adapter = new SQLiteDataAdapter(_command);
        DataSet data = new DataSet();
        adapter.Fill(data, "table");
        return data;
    }


    /// <summary>
    /// 执行INSERT、UPDATE、DELETE等不需要返回结果值的语句
    /// </summary>
    /// <param name="SQL语句"></param>
    /// <returns></returns>
    public int RecordChange(string SQL语句)
    {
        SQLiteConnection connection = new SQLiteConnection(connectionDBString);
        SQLiteCommand command = new SQLiteCommand(SQL语句, connection);
        connection.Open();
        int result = command.ExecuteNonQuery();
        return result;
    }

    // 执行查询并返回单个值
    public object getChangeCount(string query)
    {
        SQLiteConnection connection = new SQLiteConnection(connectionDBString);
        SQLiteCommand command = new SQLiteCommand(query, connection);
        connection.Open();
        object result = command.ExecuteScalar();
        return result;
    }

    //关闭数据库
    public void Close()
    {
        _connection.Close();
    }

}
