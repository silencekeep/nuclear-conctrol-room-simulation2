using UnityEngine;
using System.Data.SqlClient;
using System;
using TMPro;
using System.Collections.Generic;


public class ProjectTableCreate : MonoBehaviour
{
    //public GameObject table;
    public GameObject Row_Prefab;//表格数据行预设 
    public GameObject table;//表格的父节点
    public TMP_InputField numberinput;//项目编号
    public TMP_Dropdown StandUpDrop;//坐姿上限下拉框
    public TMP_Dropdown StandDownDrop;//坐姿下限下拉框
    public TMP_InputField nameinput;//项目名称
    public TMP_Dropdown SitUpDrop;//坐姿上限下拉框
    public TMP_Dropdown SitDownDrop;//坐姿下限下拉框
    public GameObject QueryPanel;//查询面板
    public GameObject addPanel;//添加面板
    public ADDpanel Ap;//添加脚本

    void Start()
    {
        
        //inition();
         
    }
    public void inition()
    {
        //站姿列表初始化
        string sql = "select * from zhanzi";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        List<TMP_Dropdown.OptionData> listOptions = new List<TMP_Dropdown.OptionData>();
        listOptions.Add(new TMP_Dropdown.OptionData("全部"));
        listOptions.Add(new TMP_Dropdown.OptionData("不限"));
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string dataposition = dc.Tables[0].Rows[i][9].ToString().Trim();
            listOptions.Add(new TMP_Dropdown.OptionData(dataposition));

        }      
        StandUpDrop.ClearOptions();
        StandUpDrop.AddOptions(listOptions);
        StandDownDrop.ClearOptions();
        StandDownDrop.AddOptions(listOptions);
        //坐姿列表初始化
        sql = "select * from zuozi";
        dc = sqlconnect.Instance.sqlquery(sql);
        listOptions = new List<TMP_Dropdown.OptionData>();
        listOptions.Add(new TMP_Dropdown.OptionData("全部"));
        listOptions.Add(new TMP_Dropdown.OptionData("不限"));
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string dataposition = dc.Tables[0].Rows[i][17].ToString().Trim();
            listOptions.Add(new TMP_Dropdown.OptionData(dataposition));

        }     
        SitUpDrop.ClearOptions();
        SitUpDrop.AddOptions(listOptions);
        SitDownDrop.ClearOptions();
        SitDownDrop.AddOptions(listOptions);
        //参数初始化
        numberinput.text = "";
        nameinput.text = "";
        //全部查询       
        AllDataTableShow();
    }
    //全部查询
    public void AllDataTableShow()
    {
        string sql = "select * from ProjectData";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        showdata(dc);
    }
    //显示添加界面
    public void addproject()
    {
        QueryPanel.SetActive(false);
        addPanel.SetActive(true);
        Ap.inition();
    }
    //查询按钮
    public void SelectButton() 
    {
        string Number = numberinput.text;
        string Name = nameinput.text;
        string StandUp = StandUpDrop.options[StandUpDrop.value].text;
        string StandDown = StandDownDrop.options[StandDownDrop.value].text;
        string SitUp = SitUpDrop.options[SitUpDrop.value].text;
        string SitDown = SitDownDrop.options[SitDownDrop.value].text;
        string sql = "select * from ProjectData";
        if (StandUp != "全部")
        {
            sql = sql + " where StandUp = '" + StandUp + "'";
        }
        else 
        {
            sql = sql + " where StandUp IS NOT NULL ";
        }
        if (Number != "")
        {
            sql = sql + "and number like '%" + Number + "%'";
        }
        if (Name != "") 
        {
            sql = sql + "and name like '%" + Name + "%'";
        }
        if (StandDown != "全部")
        {
            sql = sql + "and StandDown = '" + StandDown + "'";
        }
        if (SitUp != "全部")
        {
            sql = sql + "and SitUp = '" + SitUp + "'";
        }
        if (SitDown != "全部")
        {
            sql = sql + "and SitDown = '" + SitDown + "'";
        }
        Debug.Log(sql);
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        showdata(dc);
    }
    //表格显示数据
    public void showdata(System.Data.DataSet dc) 
    {
        for (int i = 0; i < table.transform.childCount; i++)
            Destroy(table.transform.GetChild(i).gameObject);
        string[] Number_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        string[] Name_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        string[] StandUp_arr = new string[dc.Tables[0].Rows.Count];
        string[] StandDown_arr = new string[dc.Tables[0].Rows.Count];
        string[] SitUp_arr = new string[dc.Tables[0].Rows.Count];
        string[] SitDown_arr = new string[dc.Tables[0].Rows.Count];
        Debug.Log(dc.Tables[0].Rows.Count);
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的数据放进数组中
        {
            string number = Convert.ToString(dc.Tables[0].Rows[i][0]);
            if (number != null)
                Number_arr[i] = number;
            string name = Convert.ToString(dc.Tables[0].Rows[i][1]);
            if (name != null)
                Name_arr[i] = name;
            string StandUp = Convert.ToString(dc.Tables[0].Rows[i][2]);
            if (StandUp != null)
                StandUp_arr[i] = StandUp;
            string StandDown = Convert.ToString(dc.Tables[0].Rows[i][3]);
            if (StandDown != null)
                StandDown_arr[i] = StandDown;
            string SitUp = Convert.ToString(dc.Tables[0].Rows[i][4]);
            if (SitUp != null)
                SitUp_arr[i] = SitUp;
            string SitdDown = Convert.ToString(dc.Tables[0].Rows[i][5]);
            if (SitdDown != null)
                SitDown_arr[i] = SitdDown;

        }

        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)//添加并修改预设的过程，将创建10行
        {
            //在Table下创建新的预设实例
            GameObject row = Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("Number").GetComponent<TMP_Text>().text = Number_arr[i];
            row.transform.Find("Name").GetComponent<TMP_Text>().text = Name_arr[i];
            row.transform.Find("StandUp").GetComponent<TMP_Text>().text = StandUp_arr[i];
            row.transform.Find("StandDown").GetComponent<TMP_Text>().text = StandDown_arr[i];
            row.transform.Find("SitUp").GetComponent<TMP_Text>().text = SitUp_arr[i];
            row.transform.Find("SitDown").GetComponent<TMP_Text>().text = SitDown_arr[i];
        }
    }
}

