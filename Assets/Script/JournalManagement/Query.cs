using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Query : MonoBehaviour
{

    public GameObject QueryPanel;//查询面板
    public GameObject CalendarPrefab;//日历预制体
    public GameObject table;//表格的父节点
    public GameObject Row_Prefab;//表格数据行预设 


    public TMP_InputField numberinput;//项目编号
    public TMP_Dropdown TableDrop;//表下拉框
    public TMP_Dropdown OperationDrop;//操作下拉框
    public TMP_Dropdown PeopleDrop;//操作人员下拉框
    public TMP_Text TimeInput;//起止时间
    // Start is called before the first frame update

    void Start()
    {
        //inition();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void inition()
    {        
       

        //表列表初始化
        TableDrop.ClearOptions();
        string sql = "SELECT name FROM sqlite_master WHERE type='table' order by name";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        List<TMP_Dropdown.OptionData> listOptions = new List<TMP_Dropdown.OptionData>();
        listOptions.Add(new TMP_Dropdown.OptionData("全部"));
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string dataposition = dc.Tables[0].Rows[i][0].ToString().Trim();
            listOptions.Add(new TMP_Dropdown.OptionData(dataposition));

        }
        TableDrop.AddOptions(listOptions);
        //表操作列表初始化
        OperationDrop.ClearOptions();
        listOptions = new List<TMP_Dropdown.OptionData>();
        listOptions.Add(new TMP_Dropdown.OptionData("全部"));
        listOptions.Add(new TMP_Dropdown.OptionData("Insert"));
        listOptions.Add(new TMP_Dropdown.OptionData("Delete"));
        listOptions.Add(new TMP_Dropdown.OptionData("Update"));
        OperationDrop.AddOptions(listOptions);
        //操作人员列表初始化
        PeopleDrop.ClearOptions();
        sql = "select * from UseTable"; ;
        dc = sqlconnect.Instance.sqlquery(sql);
        listOptions = new List<TMP_Dropdown.OptionData>();
        listOptions.Add(new TMP_Dropdown.OptionData("全部"));
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string dataposition = dc.Tables[0].Rows[i][0].ToString().Trim();
            listOptions.Add(new TMP_Dropdown.OptionData(dataposition));

        }
        PeopleDrop.AddOptions(listOptions);
        //参数初始化
        numberinput.text = "";
        TimeInput.text = "";
        //全部查询       
        AllDataTableShow();
    }
    //添加日历
    public void calendar()
    {
        GameObject CalendarObject = Instantiate(CalendarPrefab, QueryPanel.transform.position, QueryPanel.transform.rotation) as GameObject;
        CalendarObject.transform.SetParent(QueryPanel.transform);
        CalendarObject.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
    }
    public void SelectButton()
    {
        string Number = numberinput.text;
        string Table = TableDrop.options[TableDrop.value].text;
        string Operation = OperationDrop.options[OperationDrop.value].text;
        string People = PeopleDrop.options[PeopleDrop.value].text;
        string Time = TimeInput.text;
        string sql = "select * from JournalTable";
        if (Table != "全部")
        {
            sql = sql + " where TableName = '" + Table + "'";
        }
        else
        {
            sql = sql + " where TableName IS NOT NULL ";
        }
        if (Number != "")
        {
            sql = sql + "and Number like '%" + Number + "%'";
        }
        if (Operation != "全部")
        {
            sql = sql + "and Operation like '%" + Operation + "%'";
        }
        if (People != "全部")
        {
            sql = sql + "and Person = '" + People + "'";
        }
        if (Time != "")
        {
            string[] Times = Time.Split('-');
            string StartTime = Times[0].Replace(" ", "");
            string EndTime = Times[1].Replace(" ", "");
            sql = sql + "and Time > '" + StartTime + "'";
            sql = sql + "and Time < '" + EndTime + "'";
        }
        sql = sql + " order by Time desc";
        Debug.Log(sql);
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        showdata(dc);
    }
    //全部查询
    public void AllDataTableShow()
    {
        string sql = "select * from JournalTable order by Time desc";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        showdata(dc);
    }
    //表格显示数据
    public void showdata(System.Data.DataSet dc)
    {
        for (int i = 0; i < table.transform.childCount; i++)
            Destroy(table.transform.GetChild(i).gameObject);
        string[] Number_arr = new string[dc.Tables[0].Rows.Count];
        string[] Table_arr = new string[dc.Tables[0].Rows.Count];
        string[] Operation_arr = new string[dc.Tables[0].Rows.Count];
        string[] Person_arr = new string[dc.Tables[0].Rows.Count];
        string[] Time_arr = new string[dc.Tables[0].Rows.Count];
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的数据放进数组中
        {
            string number = Convert.ToString(dc.Tables[0].Rows[i][0]);
            if (number != null)
                Number_arr[i] = number;
            string Table = Convert.ToString(dc.Tables[0].Rows[i][1]);
            if (Table != null)
                Table_arr[i] = Table;
            string Operation = Convert.ToString(dc.Tables[0].Rows[i][2]);
            if (Operation != null)
                Operation_arr[i] = Operation;
            string Person = Convert.ToString(dc.Tables[0].Rows[i][4]);
            if (Person != null)
                Person_arr[i] = Person;
            string Time = Convert.ToString(dc.Tables[0].Rows[i][3]);
            if (Time != null)
                Time_arr[i] = Time.Substring(0, Time.Length - 4);

        }
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)//添加并修改预设的过程，将创建10行
        {
            //在Table下创建新的预设实例
            GameObject row = Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("data").GetComponent<TMP_Text>().text = (i + 1).ToString();
            row.transform.Find("number").GetComponent<TMP_Text>().text = Number_arr[i];
            row.transform.Find("table").GetComponent<TMP_Text>().text = Table_arr[i];
            row.transform.Find("operation").GetComponent<TMP_Text>().text = Operation_arr[i];
            row.transform.Find("person").GetComponent<TMP_Text>().text = Person_arr[i];
            row.transform.Find("time").GetComponent<TMP_Text>().text = Time_arr[i];
        }
    }
}
