using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System;


public class RoleManager : MonoBehaviour
{
    public GameObject Rrow_Prefab;
    public GameObject editUserpanel;
    public GameObject editokpanel;
    public GameObject addUserpanel;
    public GameObject addokpanel;    
    public GameObject Userquerypanel;
    public GameObject table;
    public GameObject LoginPanel;
    public GameObject RoleManagerPanel;

    public static int Userquerysign;
    public static bool Usereditpanelsign;
    public static bool Usereditshowsign;
 
    public Dropdown QUserTypeDropdown;

    public InputField AddUserIDInputField;
    public InputField AddUserpasswordInputField;
    public InputField AddUsernameInputField;
    public Dropdown   AddUserTypeDropdown;

    public InputField EditUserIDInputField;
    public InputField EditUsernameInputField;
    public Dropdown   EditUserTypeDropdown;
    public InputField EditUserpasswordInputField;

    private SqlConnection sqlCon;
    
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";

    // Start is called before the first frame update
    void Start()
    {
        Usereditpanelsign = false;
        Usereditshowsign = true;        
    }

    public void RoleExitImageClicked()
    {
        RoleManagerPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }
    public void EditPanelShow()
    {        
        Usereditpanelsign = true;
    }
    public void EditPanelClose()
    {
        Usereditpanelsign = false;
    }
    public void AddPanelShow()
    {
        addUserpanel.SetActive(true);       
    }
    public void AddPanelClose()
    {
        addUserpanel.SetActive(false);
    }
    public void ConditionalQuery()
    {
        Userquerypanel.SetActive(true);
    }
    public void ConditionalQueryClose()
    {
        Userquerypanel.SetActive(false);
    }
    public void QYesButtonClicked()//条件查询按钮click事件
    {
        Userquerysign = 2;//若当前为条件查询界面则querysign = 2
        int count = table.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(table.transform.GetChild(i).gameObject);//删除所有“行预制体”        
        }        
        UpdateTable();
        Userquerypanel.SetActive(false);
    }
    public void ConditionalQueryTable()//显示动素条件查询结果（种类）
    {
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;
        string Qtype = QUserTypeDropdown.captionText.text;
        SqlDataAdapter DC = new SqlDataAdapter("select * from users where UserType='" + Qtype + "'", sqlCon);


        System.Data.DataSet dc = new System.Data.DataSet();
        DC.Fill(dc);
        string[] Id_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        string[] Type_arr = new string[dc.Tables[0].Rows.Count];
        string[] Name_arr = new string[dc.Tables[0].Rows.Count];
        string[] PassWord_arr = new string[dc.Tables[0].Rows.Count];
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的数据放进数组中
        {
            string Id = Convert.ToString(dc.Tables[0].Rows[i][0]);//将数据表中的动素序号ActID数据放进数组中
                                                                  //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
                                                                  //dc.Tables[0].Rows[i][0])表示取数据表中第一列所有数据
            if (Id != null)
                Id_arr[i] = Id;
            string Type = Convert.ToString(dc.Tables[0].Rows[i][3]);//将数据表中的动素类别ActType数据放进数组中
            if (Type != null)
                Type_arr[i] = Type;
            string Name = Convert.ToString(dc.Tables[0].Rows[i][1]);
            if (Name != null)
                Name_arr[i] = Name;
            string PassWord = Convert.ToString(dc.Tables[0].Rows[i][2]);
            if (PassWord != null)
                PassWord_arr[i] = PassWord;

        }

        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)//添加并修改预设的过程，将创建dc.Tables[0].Rows.Count行
        {
            //在Table下创建新的预设实例
            GameObject table = GameObject.Find("Canvas/RoleManage/MainPanel/Viewport/RTable");
            GameObject row = Instantiate(Rrow_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("RID").GetComponent<Text>().text = Id_arr[i];
            row.transform.Find("RType").GetComponent<Text>().text = Type_arr[i];
            row.transform.Find("RName").GetComponent<Text>().text = Name_arr[i];
            row.transform.Find("RPassWord").GetComponent<Text>().text = PassWord_arr[i];
        }
        sqlCon.Close();
    }
    public void AddUsersYesButtonClicked()//新增管理员
    {
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        string UserInsert = "insert into users(UserID,UserName,PassWord,UserType) values ('" + AddUserIDInputField.text + "','" + AddUsernameInputField.text + "','" + AddUserpasswordInputField.text + "','" + AddUserTypeDropdown.captionText.text + "')";
        SqlCommand sqlCommand = new SqlCommand(UserInsert, sqlCon);
        sqlCommand.ExecuteNonQuery();
        sqlCon.Close();
        addokpanel.SetActive(true);
        Userquerysign = 1;
    }
    public void AddOkYesButtonClicked()
    {
        addokpanel.SetActive(false);
        addUserpanel.SetActive(false);
        UpdateTable();
    }
    private void UpdateTable()
    {
        int count = table.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(table.transform.GetChild(i).gameObject);//删除所有“行预制体”           
        }
        if (Userquerysign == 1)//当前在全部查询界面
        {
            AllQuery();
        }
        else if (Userquerysign == 2)//当前在条件查询界面
        {
            ConditionalQueryTable();
        }
    }    
    public void EditYesButtonClicked()//数据库编辑功能
    {        
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        string UserEdit = "update users set UserType='" + EditUserTypeDropdown.captionText.text + "', UserName='" + EditUsernameInputField.text + "', PassWord = '" + EditUserpasswordInputField.text + "'where UserID = '" + EditUserIDInputField.text + "'";
        SqlCommand sqlCommand = new SqlCommand(UserEdit, sqlCon);
        sqlCommand.ExecuteNonQuery();
        sqlCon.Close();
        editokpanel.SetActive(true);
    }
    public void EditOkYesButtonClicked()
    {
        editokpanel.SetActive(false);
        Usereditpanelsign = false;
        Userquerysign = 1;
        UpdateTable();
    }
    void EditShow()
    {
        Usereditshowsign = false;
        EditUserIDInputField.GetComponent<InputField>().text = RoleRowScripts.RowUserIDText;
        EditUsernameInputField.GetComponent<InputField>().text = RoleRowScripts.RowUserNameText;
        EditUserTypeDropdown.captionText.text = RoleRowScripts.RowUserTypeText;        
        EditUserpasswordInputField.GetComponent<InputField>().text = RoleRowScripts.RowUserPassWordText;
       
    }
    void AllQuery()//显示全部查询内容
    {
        Userquerysign = 1;
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;
        SqlDataAdapter DC = new SqlDataAdapter("select * from users ", sqlCon);
        System.Data.DataSet dc = new System.Data.DataSet();
        DC.Fill(dc);
        string[] Id_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        string[] Type_arr = new string[dc.Tables[0].Rows.Count];
        string[] Name_arr = new string[dc.Tables[0].Rows.Count];
        string[] PassWord_arr = new string[dc.Tables[0].Rows.Count];       
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的数据放进数组中
        {
            string Id = Convert.ToString(dc.Tables[0].Rows[i][0]);//将数据表中的动素序号ActID数据放进数组中
                                                                  //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
                                                                  //dc.Tables[0].Rows[i][0])表示取数据表中第一列所有数据
            if (Id != null)
                Id_arr[i] = Id;
            string Type = Convert.ToString(dc.Tables[0].Rows[i][3]);//将数据表中的动素类别ActType数据放进数组中
            if (Type != null)
                Type_arr[i] = Type;
            string Name = Convert.ToString(dc.Tables[0].Rows[i][1]);
            if (Name != null)
                Name_arr[i] = Name;
            string PassWord = Convert.ToString(dc.Tables[0].Rows[i][2]);
            if (PassWord != null)
                PassWord_arr[i] = PassWord;         
           
        }

        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)//添加并修改预设的过程，将创建dc.Tables[0].Rows.Count行
        {
            //在Table下创建新的预设实例
            GameObject table = GameObject.Find("Canvas/RoleManage/MainPanel/Viewport/RTable");
            GameObject row = Instantiate(Rrow_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("RID").GetComponent<Text>().text = Id_arr[i];
            row.transform.Find("RType").GetComponent<Text>().text = Type_arr[i];
            row.transform.Find("RName").GetComponent<Text>().text = Name_arr[i];
            row.transform.Find("RPassWord").GetComponent<Text>().text = PassWord_arr[i];
        }
    }
    void Update()
    {
        if (Usereditpanelsign == true)
        {
            editUserpanel.SetActive(true);
            if (Usereditshowsign == true)
            {
                EditShow();     
            }
        }
        else
        {
            editUserpanel.SetActive(false);
        }
    }

}


