using Aspose.Words.Tables;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using TMPro;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class RoleControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //初始化
    public void roleinition() {
        //添加右侧控件
        string sql = "select * from roles";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        GameObject table = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundLeft/Scroll View/Viewport/Content");
        for (int i = 0; i < table.transform.childCount; i++)
            Destroy(table.transform.GetChild(i).gameObject);
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            //在Table下创建新的预设实例
            GameObject row = GameObject.Instantiate(Resources.Load("ButtonRole"), table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.GetChild(0).Find("Text (TMP)").GetComponent<TMP_Text>().text = Convert.ToString(dc.Tables[0].Rows[i][0]);
        }
        //默认点了第一个按钮
        GameObject RoleName = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundRight/functionRow/function");
        RoleName.GetComponent<TMP_Text>().text = Convert.ToString(dc.Tables[0].Rows[0][0]);
        for (int i = 0; i < table.transform.childCount; i++)
        {
            table.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255f / 255f, 255 / 255f);
            table.transform.GetChild(i).GetChild(0).transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().color = new Color(0 / 255f, 0 / 255f, 255f / 255f, 255 / 255f);
        }
        //消除的物体计数还在
        GameObject button1 = table.transform.GetChild(table.transform.childCount - dc.Tables[0].Rows.Count).GetChild(0).gameObject;
        Debug.Log(table.transform.childCount);
        button1.GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 255f / 255f, 255 / 255f);
        button1.transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().color = new Color(255 / 255f, 255 / 255f, 255f / 255f, 255 / 255f);
        Toggele();
        GameObject error = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundLeft/error");
        error.SetActive(false);
    }
    //点击左侧列表按钮   
    public void roleButton() {
        GameObject error = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundLeft/error");
        error.SetActive(false);
        GameObject RoleName = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundRight/functionRow/function");
        RoleName.GetComponent<TMP_Text>().text = transform.GetChild(0).Find("Text (TMP)").GetComponent<TMP_Text>().text;      
        Toggele();
        GameObject table = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundLeft/Scroll View/Viewport/Content");
        for (int i = 0; i < table.transform.childCount; i++)
        {
            table.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255f / 255f, 255 / 255f);
            table.transform.GetChild(i).GetChild(0).transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().color = new Color(0 / 255f, 0 / 255f, 255f / 255f, 255 / 255f);
        }
        gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 255f / 255f, 255 / 255f);
        gameObject.transform.GetChild(0).Find("Text (TMP)").GetComponent<TMP_Text>().color = new Color(255 / 255f, 255 / 255f, 255f / 255f, 255 / 255f);
    }
    //点击左侧确认按钮   
    public void EnterButton()
    {
        GameObject error = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundLeft/error");
        error.SetActive(false);
        GameObject RoleName = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundRight/functionRow/function");
        string sql = "insert into roles values ('" + RoleName.GetComponent<TMP_Text>().text + "'";
        for (int i = 1; i < 9; i++)
        {
            string path = "2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundRight/functionRow (" + i + ")/Toggle";
            Toggle Toggle = GameObject.Find(path).GetComponent<Toggle>();
            if (Toggle.isOn)
            {
                sql += ",'" + 1 + "'";
            }
            else {
                sql += ",'" + 0 + "'";
            }
        }
        sql += ")";
        Debug.Log(sql);
        sqlconnect.Instance.sqladd(sql);
        roleinition();
    }
    //点击左侧新增按钮  
    public void addButton()
    {
        GameObject error = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundLeft/error");
        error.SetActive(false);
        GameObject nameInputField = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundLeft/backgroundLeft-2/NumberInputField (TMP)");
        string sql = "select * from roles where positon = '" + nameInputField.GetComponent<TMP_InputField>().text +"'";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        if (nameInputField.GetComponent<TMP_InputField>().text.Length == 0)
        {
            error.GetComponent<TMP_Text>().text = "请输入角色名";
            error.SetActive(true);
        }
        else if (dc.Tables[0].Rows.Count > 0){
            error.GetComponent<TMP_Text>().text = "角色名不可重复";
            error.SetActive(true);
        }
        else {
            //添加一个按钮
            GameObject table = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundLeft/Scroll View/Viewport/Content");
            GameObject row = GameObject.Instantiate(Resources.Load("ButtonRole"), table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (table.transform.childCount + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;
            row.transform.GetChild(0).Find("Text (TMP)").GetComponent<TMP_Text>().text = nameInputField.GetComponent<TMP_InputField>().text;
            //点了最后一个按钮
            GameObject RoleName = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundRight/functionRow/function");
            RoleName.GetComponent<TMP_Text>().text = nameInputField.GetComponent<TMP_InputField>().text;
            for (int i = 0; i < table.transform.childCount; i++)
            {
                table.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255f / 255f, 255 / 255f);
                table.transform.GetChild(i).transform.GetChild(0).Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().color = new Color(0 / 255f, 0 / 255f, 255f / 255f, 255 / 255f);
            }
            table.transform.GetChild(table.transform.childCount - 1).GetChild(0).gameObject.GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 255f / 255f, 255 / 255f);
            table.transform.GetChild(table.transform.childCount - 1).transform.GetChild(0).Find("Text (TMP)").gameObject.GetComponent<TMP_Text>().color = new Color(255 / 255f, 255 / 255f, 255f / 255f, 255 / 255f);
            Toggele();
        }
        
    }
    public void RowDeleteClick()//数据库删除功能
    {
        string role = this.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text;
        Destroy(this.transform.gameObject);
        string sql = "delete from roles where positon = '" + role + "'";
        sqlconnect.Instance.sqladd(sql);
        roleinition();
    }
    //右侧显示是否需要勾选
    private void Toggele()
    {        
        GameObject RoleName = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundRight/functionRow/function");
        string sql = "select * from roles where positon = '" + RoleName.GetComponent<TMP_Text>().text + "'";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        if (dc.Tables[0].Rows.Count == 1)
        {
            for (int i = 1; i < dc.Tables[0].Columns.Count; i++)
            {
                string path = "2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundRight/functionRow (" + i + ")/Toggle";
                Toggle Toggle = GameObject.Find(path).GetComponent<Toggle>();
                if ("1".Equals(Convert.ToString(dc.Tables[0].Rows[0][i]).Substring(0, 1)))
                {
                    Toggle.isOn = true;
                }
                else
                {
                    Toggle.isOn = false;
                }

            }
        }
        else {
            for (int i = 1; i < dc.Tables[0].Columns.Count; i++)
            {
                string path = "2DUI/Canvas/JurisdictionManagementPanel/roleControl/backgroundRight/functionRow (" + i + ")/Toggle";
                Toggle Toggle = GameObject.Find(path).GetComponent<Toggle>();
                Toggle.isOn = false;
            }

        }
        

    }
}
