using MiniJSON;
using System.Collections.Generic;
using System.Data.SqlClient;
using TMPro;
using UnityEngine;

using UnityEngine.UI;
using System;
using UnityEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

public class ADDpanel : MonoBehaviour
{
    public TMP_InputField Numberinput;//项目编号
    public TMP_InputField Nameinput;//项目名称
    public TMP_Dropdown StandUpDrop;//站姿上限下拉框
    public TMP_Dropdown StandDownDrop;//站姿下限下拉框
    public TMP_Dropdown SitUpDrop;//坐姿上限下拉框
    public TMP_Dropdown SitDownDrop;//坐姿下限下拉框 
    public GameObject addpanel;
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
        //站姿列表初始化
        string sql = "select * from zhanzi";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        List<TMP_Dropdown.OptionData> listOptions = new List<TMP_Dropdown.OptionData>();
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
        //工程编号、名称初始化
        Numberinput.text = "";
        Nameinput.text = "";
        //两行数据初始化
        StandUpDropChange();
        StandDownDropChange();
        SitUpDropChange();
        SitDownDropChange();
        //错误提示初始化
        error.SetActive(false);
    }
    //按下增加按钮
    public void addButton()
    {
        string Number = Numberinput.text;
        string Name = Nameinput.text;
        string StandUp = StandUpDrop.options[StandUpDrop.value].text;
        string StandDown = StandDownDrop.options[StandDownDrop.value].text;
        string SitUp = SitUpDrop.options[SitUpDrop.value].text;
        string SitDown = SitDownDrop.options[SitDownDrop.value].text;
        //验证
        if (yanzheng(Number, Name))
        {
            string sql = "insert into projectdata values ('" + Number + "', '" + Name + "', '" + StandUp + "','" + StandDown + "','" + SitUp + "','" + SitDown + "')";
            Debug.Log(sql);
            sqlconnect.Instance.sqladd(sql);
            BackButton();
        }
    }
    //按下返回按钮
    public void BackButton()
    {
        addpanel.SetActive(false);
        querypanel.SetActive(true);
        PTC.inition();
    }
    //按下修改人体尺寸按钮
    public void EditHumanButton()
    {
    //    DataDetailPanel.SetActive(true);
    //    //判断按下的是哪个按钮
    //    string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
    //    string openpath;
    //    string closepath;
    //    TMP_Dropdown Dropdown;
    //    if (name.Contains("Stand"))
    //    {
    //        openpath = "2DUI/Canvas/ProjectManagementPanel/DataDetailPanel/Standing";
    //        closepath = "2DUI/Canvas/ProjectManagementPanel/DataDetailPanel/sitting";
    //        if (name.Contains("StandUp"))
    //        {
    //            Dropdown = GameObject.Find("2DUI/Canvas/ProjectManagementPanel/AddPanel/zujian/StandUp/StandUpDropdown").GetComponent<TMP_Dropdown>();
    //        }
    //        else
    //        {
    //             Dropdown = GameObject.Find("2DUI/Canvas/ProjectManagementPanel/AddPanel/zujian/StandDown/StandDownDropdown").GetComponent<TMP_Dropdown>(); 
    //        }
    //    }
    //    else 
    //    {
    //        openpath = "2DUI/Canvas/ProjectManagementPanel/DataDetailPanel/sitting";
    //        closepath = "2DUI/Canvas/ProjectManagementPanel/DataDetailPanel/Standing";
    //        if (name.Contains("SitUp"))
    //        {
    //            Dropdown = GameObject.Find("2DUI/Canvas/ProjectManagementPanel/AddPanel/zujian/SitUp/SitUpDropdown").GetComponent<TMP_Dropdown>();
    //        }
    //        else
    //        {
    //            Dropdown = GameObject.Find("2DUI/Canvas/ProjectManagementPanel/AddPanel/zujian/SitDown/SitDownDropdown").GetComponent<TMP_Dropdown>();
    //        }
    //    }
    //    //设值
    //    GameObject openGameobject = GameObject.Find(openpath);
    //    GameObject closeGameobject = GameObject.Find(closepath);
    //    openGameobject.SetActive(true);
    //    closeGameobject.SetActive(false);
    //    Text xiangmutiaokuanText = GameObject.Find(openpath + "/nameText").GetComponent<Text>();
    //    xiangmutiaokuanText.text = Dropdown.options[Dropdown.value].text;
    //    DataDetailPanel DDP = DataDetailPanel.GetComponent<DataDetailPanel>();
    //    DDP.inition();
    }



    //站姿上限下拉框值改变
    public void StandUpDropChange() 
    {
        string path = "2DUI/Canvas/ProjectManagementPanel/AddPanel/ShowData/zhanzi/Scroll View/Viewport/Content/StandUp";
        string DatabaseName = "zhanzi";
        string percentage = StandUpDrop.options[StandUpDrop.value].text;
        int n = 10;
        DropChange(path, DatabaseName, percentage, n);
    }
    //站姿下限下拉框值改变
    public void StandDownDropChange()
    {
        string path = "2DUI/Canvas/ProjectManagementPanel/AddPanel/ShowData/zhanzi/Scroll View/Viewport/Content/StandDown";
        string DatabaseName = "zhanzi";
        string percentage = StandDownDrop.options[StandDownDrop.value].text;
        int n = 10;
        DropChange(path, DatabaseName, percentage, n);
    }
    //坐姿上限下拉框值改变
    public void SitUpDropChange()
    {
        string path = "2DUI/Canvas/ProjectManagementPanel/AddPanel/ShowData/zuozi/Scroll View/Viewport/Content/SitUp";
        string DatabaseName = "zuozi";
        string percentage = SitUpDrop.options[SitUpDrop.value].text;
        int n = 18;
        DropChange(path, DatabaseName, percentage, n);
    }
    //坐姿下限下拉框值改变
    public void SitDownDropChange()
    {
        string path = "2DUI/Canvas/ProjectManagementPanel/AddPanel/ShowData/zuozi/Scroll View/Viewport/Content/SitDown";
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
    //验证是否可以增加该条数据
    public bool yanzheng(string Number, string Name)
    {
        if (Number == "")
        {
            error.GetComponent<TMP_Text>().text = "项目编号不能为空";
            error.SetActive(true);
            return false;
        }
        else if (Name == "")
        {
            error.GetComponent<TMP_Text>().text = "项目名称不能为空";
            error.SetActive(true);
            return false;
        }
        else
        {
            string sql = "select* from ProjectData";
            System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
            for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
            {
                string datause = dc.Tables[0].Rows[i][0].ToString().Trim();
                if (datause.Equals(Number))
                {
                    error.GetComponent<TMP_Text>().text = "项目编号已存在";
                    error.SetActive(true);
                    return false; ;
                }
            }
        }
        return true;
    }
}
