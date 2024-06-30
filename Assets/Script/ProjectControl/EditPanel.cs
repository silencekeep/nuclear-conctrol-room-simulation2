using I18N.Common;
using NPOI.SS.Formula.PTG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditPanel : MonoBehaviour
{
    public TMP_Text NumberText;//项目编号
    public TMP_InputField NameInput;//项目名称
    public TMP_Dropdown StandUpDrop;//站姿上限下拉框
    public TMP_Dropdown StandDownDrop;//站姿下限下拉框
    public TMP_Dropdown SitUpDrop;//坐姿上限下拉框
    public TMP_Dropdown SitDownDrop;//坐姿下限下拉框 
    public GameObject editpanel;
    public GameObject querypanel;
    public ProjectTableCreate PTC;  
    public GameObject error;//错误提示

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void inition()
    {
        //获取该数据的值
        string ProjectNumber = NumberText.text;
        string sql = "select * from ProjectData where Number = '" + ProjectNumber + "'";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        NameInput.text = dc.Tables[0].Rows[0][1].ToString().Trim();
        string StandUpText = dc.Tables[0].Rows[0][2].ToString().Trim();
        string StandDownText = dc.Tables[0].Rows[0][3].ToString().Trim();
        string SitUpText = dc.Tables[0].Rows[0][4].ToString().Trim();
        string SitDownText = dc.Tables[0].Rows[0][5].ToString().Trim();
        int StandUpValue = 0;
        int StandDownValue = 0;
        int SitUpValue = 0;
        int SitDownValue = 0;
        //下拉列表初始化
        sql = "select * from zhanzi";
        dc = sqlconnect.Instance.sqlquery(sql);
        List<TMP_Dropdown.OptionData> listOptions = new List<TMP_Dropdown.OptionData>();
        listOptions.Add(new TMP_Dropdown.OptionData("不限"));
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string dataposition = dc.Tables[0].Rows[i][9].ToString().Trim();
            listOptions.Add(new TMP_Dropdown.OptionData(dataposition));
            //通过值来获取下拉框的value，以便后续修改下拉框的初始值，因为前面有个“不限”，所以顺序是i+1（或许有简便的方法，暂没找到）
            if (dataposition == StandUpText) 
            {
                StandUpValue = i + 1;
            }
            if (dataposition == StandDownText)
            {
                StandDownValue = i + 1;
            }
        }
        StandUpDrop.ClearOptions();
        StandUpDrop.AddOptions(listOptions);
        StandDownDrop.ClearOptions();
        StandDownDrop.AddOptions(listOptions);

        //坐姿列表初始化
        sql = "select * from zuozi";
        dc = sqlconnect.Instance.sqlquery(sql);
        listOptions = new List<TMP_Dropdown.OptionData>();
        listOptions.Add(new TMP_Dropdown.OptionData("不限"));
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string dataposition = dc.Tables[0].Rows[i][17].ToString().Trim();
            listOptions.Add(new TMP_Dropdown.OptionData(dataposition));
            //通过值来获取下拉框的value，以便后续修改下拉框的初始值，因为前面有个“不限”，所以顺序是i+1（或许有简便的方法，暂没找到）
            if (dataposition == SitUpText)
            {
                SitUpValue = i + 1;
            }
            if (dataposition == SitDownText)
            {
                SitDownValue = i + 1;
            }
        }
        SitUpDrop.ClearOptions();
        SitUpDrop.AddOptions(listOptions);
        SitDownDrop.ClearOptions();
        SitDownDrop.AddOptions(listOptions);
        //下拉框初始值赋值
        StandUpDrop.value = StandUpValue;
        StandDownDrop.value = StandDownValue;
        SitUpDrop.value = SitUpValue;
        SitDownDrop.value = SitDownValue;
        //两行数据初始化
        StandUpDropChange();
        StandDownDropChange();
        SitUpDropChange();
        SitDownDropChange();
        //错误提示初始化
        error.SetActive(false);
    }
    public void EditButton() 
    {
        string Number = NumberText.text;
        string Name = NameInput.text;
        string StandUp = StandUpDrop.options[StandUpDrop.value].text;
        string StandDown = StandDownDrop.options[StandDownDrop.value].text;
        string SitUp = SitUpDrop.options[SitUpDrop.value].text;
        string SitDown = SitDownDrop.options[SitDownDrop.value].text;

        if (yanzheng(Name))
        {
            string sql = "update ProjectData set name = '" + Name + "' ,StandUp = '" + StandUp + "' , StandDown = '" + StandDown + "' , SitUp = '" + SitUp + "' , SitDown = '" + SitDown + "' where number = '" + Number + "'";
            Debug.Log(sql);
            sqlconnect.Instance.sqladd(sql);
            BackButton();
        }     
    }
    public void BackButton()
    {
        editpanel.SetActive(false);
        querypanel.SetActive(true);
        PTC.inition();
    }
    //站姿上限下拉框值改变
    public void StandUpDropChange()
    {
        string path = "2DUI/Canvas/ProjectManagementPanel/EditPanel/ShowData/zhanzi/Scroll View/Viewport/Content/StandUp";
        string DatabaseName = "zhanzi";
        string percentage = StandUpDrop.options[StandUpDrop.value].text;
        int n = 10;
        DropChange(path, DatabaseName, percentage, n);
    }
    //站姿下限下拉框值改变
    public void StandDownDropChange()
    {
        string path = "2DUI/Canvas/ProjectManagementPanel/EditPanel/ShowData/zhanzi/Scroll View/Viewport/Content/StandDown";
        string DatabaseName = "zhanzi";
        string percentage = StandDownDrop.options[StandDownDrop.value].text;
        int n = 10;
        DropChange(path, DatabaseName, percentage, n);
    }
    //坐姿上限下拉框值改变
    public void SitUpDropChange()
    {
        string path = "2DUI/Canvas/ProjectManagementPanel/EditPanel/ShowData/zuozi/Scroll View/Viewport/Content/SitUp";
        string DatabaseName = "zuozi";
        string percentage = SitUpDrop.options[SitUpDrop.value].text;
        int n = 18;
        DropChange(path, DatabaseName, percentage, n);
    }
    //坐姿下限下拉框值改变
    public void SitDownDropChange()
    {
        string path = "2DUI/Canvas/ProjectManagementPanel/EditPanel/ShowData/zuozi/Scroll View/Viewport/Content/SitDown";
        string DatabaseName = "zuozi";
        string percentage = SitDownDrop.options[SitDownDrop.value].text;
        int n = 18;
        DropChange(path, DatabaseName, percentage, n);
    }
    //改变row的值
    public void DropChange(string path, string DatabaseName, string percentage, int n)
    {
        Debug.Log(path);
        GameObject row = GameObject.Find(path);
        if (percentage == "不限")
        {
            for (int i = 0; i < n; i++)
            {
                row.gameObject.transform.GetChild(i).GetComponent<Text>().text = "";
            }
        }
        else
        {
            string sql = "select * from " + DatabaseName + " where percentage = '" + percentage + "'";
            Debug.Log(sql);
            System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
            for (int i = 0; i < n; i++)
            {
                row.gameObject.transform.GetChild(i).GetComponent<Text>().text = Convert.ToString(dc.Tables[0].Rows[0][i]);
            }
        }
    }
    public bool yanzheng(string name)
    {
        if (name == "")
        {
            error.GetComponent<TMP_Text>().text = "项目名称不能为空";
            error.SetActive(true);
            return false;
        }
        return true;
    }
}
