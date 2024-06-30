using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectShow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
    public static void inition()
    {
        string sql = "select * from ProjectTable";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        GameObject ProjectTable = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/ProjcetControl/ProjectTable/Viewport/Content");
        for (int i = 0; i < ProjectTable.transform.childCount; i++)
            Destroy(ProjectTable.transform.GetChild(i).gameObject);
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            //在Table下创建新的预设实例
            GameObject row = GameObject.Instantiate(Resources.Load("ButtonProject"), ProjectTable.transform.position, ProjectTable.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(ProjectTable.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.GetChild(0).Find("Text (TMP)").GetComponent<TMP_Text>().text = Convert.ToString(dc.Tables[0].Rows[i][0]);
        }
        //默认点了第一个按钮
        GameObject ProjcectName = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/ProjcetControl/ProjectName");
        ProjcectName.GetComponent<TMP_Text>().text = Convert.ToString(dc.Tables[0].Rows[0][0]);
        for (int i = 0; i < ProjectTable.transform.childCount; i++)
        {
            ProjectTable.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255f / 255f, 255 / 255f);
            ProjectTable.transform.GetChild(i).GetChild(0).transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().color = new Color(0 / 255f, 0 / 255f, 255f / 255f, 255 / 255f);
        }
        //消除的物体计数还在
        GameObject button1 = ProjectTable.transform.GetChild(ProjectTable.transform.childCount - dc.Tables[0].Rows.Count).GetChild(0).gameObject;
        Debug.Log(ProjectTable.transform.childCount);
        button1.GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 255f / 255f, 255 / 255f);
        button1.transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().color = new Color(255 / 255f, 255 / 255f, 255f / 255f, 255 / 255f);
        PutToggle();
    }
    //右侧是否需要勾选

    //点击左侧按钮
    public void ProjectButton()
    {
        GameObject ProjectName = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/ProjcetControl/ProjectName");
        ProjectName.GetComponent<TMP_Text>().text = transform.GetChild(0).Find("Text (TMP)").GetComponent<TMP_Text>().text;
        PutToggle();
        GameObject ProjectTable = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/ProjcetControl/ProjectTable/Viewport/Content");
        for (int i = 0; i < ProjectTable.transform.childCount; i++)
        {
            ProjectTable.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255f / 255f, 255 / 255f);
            ProjectTable.transform.GetChild(i).GetChild(0).transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().color = new Color(0 / 255f, 0 / 255f, 255f / 255f, 255 / 255f);
        }
        gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 255f / 255f, 255 / 255f);
        gameObject.transform.GetChild(0).Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255 / 255f, 255 / 255f, 255f / 255f, 255 / 255f);
    }
    //点击删除按钮功能
    public void deleteButton()
    {
        string Name = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/ProjcetControl/ProjectName").GetComponent<TMP_Text>().text;
        string sql = "delete from ProjectTable where Name = '" + Name + "'";
        sqlconnect.Instance.sqladd(sql);
        inition();
    }
    //点击左侧修改按钮  
    public void updateButton()
    {
        //获取列的数量
        string sql = "select * from standard";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        Dictionary<string, string> dic = GetToggle(dc);
        //写sql语句
        GameObject ProjectName = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/ProjcetControl/ProjectName");
        sql = "Update ProjectTable set ";
        for (int i = 0; i < dc.Tables[0].Rows.Count - 1; i++)
        {
            string item = Convert.ToString(dc.Tables[0].Rows[i]["item"]);
            sql += "[" + item + "] = '" + dic[item] + "', ";

        }
        string itemLast = Convert.ToString(dc.Tables[0].Rows[dc.Tables[0].Rows.Count - 1]["item"]);
        sql += "[" + itemLast + "] = '" + dic[itemLast] + "' ";
        sql += "where Name = '" + ProjectName.GetComponent<TMP_Text>().text + "'";
        Debug.Log(sql);
        sqlconnect.Instance.sqladd(sql);
        inition();
    }
    //点击左侧新增按钮  
    public void addButton()
    {
        GameObject addPanel = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/ProjectAddPanel");
        addPanel.SetActive(true);
        GameObject NameInput = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/ProjectAddPanel/Name");
        NameInput.GetComponent<TMP_InputField>().text = "";
        GameObject error = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/ProjectAddPanel/error");
        error.SetActive(false);


    }
    //点击addPanel确认按钮  
    public void enterButton()
    {
        GameObject error = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/ProjectAddPanel/error");
        GameObject NameInput = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/ProjectAddPanel/Name");
        string name = NameInput.GetComponent<TMP_InputField>().text;
        if (NameInput.GetComponent<TMP_InputField>().text.Length == 0)
        {
            error.SetActive(true);
            error.GetComponent<TMP_Text>().text = "项目名不能为空";
            return;
        }
        string sql = "select * from ProjectTable where Name = '" + name + "'";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        if (dc.Tables[0].Rows.Count > 0)
        {
            error.SetActive(true);
            error.GetComponent<TMP_Text>().text = "项目名重复";
            return;

        }
        //获取列的数量
        sql = "select * from standard";
        dc = sqlconnect.Instance.sqlquery(sql);
        Dictionary<string, string> dic = GetToggle(dc);
        //写sql语句
        sql = "insert into ProjectTable values ('" + name + "'";
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string item = Convert.ToString(dc.Tables[0].Rows[i]["item"]);
            sql += ", '" + dic[item] + "'";

        }
        sql += ")";
        Debug.Log(sql);
        sqlconnect.Instance.sqladd(sql);
        CancelButton();
        inition();
    }
    //点击addPanel取消按钮  
    public void CancelButton()
    {
        GameObject addPanel = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/ProjectAddPanel");
        addPanel.SetActive(false);
    }
    //修改右侧表格显示情况
    private static void PutToggle()
    {
        GameObject ProjcectName = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/ProjcetControl/ProjectName");
        string sql = "select * from ProjectTable where Name = '" + ProjcectName.GetComponent<TMP_Text>().text + "'";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        GameObject table = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/MainPanel/Viewport/ActTable");
        for (int i = 1; i < table.transform.childCount; i++)
        {
            GameObject Row = table.transform.GetChild(i).gameObject;
            Toggle Toggle = Row.transform.Find("Toggle").GetComponent<Toggle>();
            string name = Row.transform.Find("item").GetComponent<Text>().text;
            if ("Uncheck".Equals(Convert.ToString(dc.Tables[0].Rows[0][name])))
            {
                Toggle.isOn = false;
            }
            else
            {
                Toggle.isOn = true;
                string[] words = (Convert.ToString(dc.Tables[0].Rows[0][name])).Split('+');
                if (words.Length == 2)
                {
                    if (!"upper".Equals(words[0]))
                    {
                        Row.transform.Find("upper").GetComponent<TMP_InputField>().text = words[0];

                    }
                    if (!"lower".Equals(words[1]))
                    {
                        Row.transform.Find("lower").GetComponent<TMP_InputField>().text = words[1];

                    }
                }

            }
        }
    }
    //获取右侧表格显示情况
    private static Dictionary<string, string> GetToggle(System.Data.DataSet dc)
    {
        //创建一个键值对，用来存放表格数据
        Dictionary<string, string> dic = new Dictionary<string, string>();
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            Debug.Log(i.ToString() + "/" + dc.Tables[0].Rows.Count.ToString() + Convert.ToString(dc.Tables[0].Rows[i]["item"]));
            dic.Add(Convert.ToString(dc.Tables[0].Rows[i]["item"]), "Uncheck");

        }
        //根据实际情况进行修改键值对    
        GameObject table = GameObject.Find("2DUI/Canvas/CaiBiaoPanel/MainPanel/Viewport/ActTable");
        for (int i = 1; i < table.transform.childCount; i++)
        {
            GameObject Row = table.transform.GetChild(i).gameObject;
            Toggle Toggle = Row.transform.Find("Toggle").GetComponent<Toggle>();
            if (Toggle.isOn)
            {
                string liename = Row.transform.Find("item").GetComponent<Text>().text;
                string upper = Row.transform.Find("upper").GetComponent<TMP_InputField>().text;
                string lower = Row.transform.Find("lower").GetComponent<TMP_InputField>().text;
                if (upper.Length == 0)
                {
                    upper = "upper";
                }
                if (lower.Length == 0)
                {
                    lower = "lower";
                }
                dic[liename] = upper + "+" + lower;
            }
        }
        return dic;

    }
}
