using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System;


public class ActCombManager : MonoBehaviour
{
    public GameObject CombRow_Prefab;
    public GameObject Combeditpanel;
    public GameObject Combeditokpanel;
    public GameObject Combaddpanel;
    public GameObject Combaddokpanel;
    //public GameObject exportokpanel;
    //public GameObject importpanel;
    //public GameObject importokpanel;
    public GameObject Combtable;
    //public GameObject querypanel;
    //public Dropdown QTypeDropdown;
    //public Dropdown QType1Dropdown;
    public static bool Combeditpanelsign;
    public static bool Combeditshowsign;
    public static bool CombAddActPathSign;
    public static bool CombEditActPathSign;
    public InputField AddCombIDInputField;
    public InputField AddCombNameInputField;
    public InputField AddCombTimeInputField;
    //public InputField AddCombTypeInputField;
    //public InputField AddType1InputField;
    public InputField AddCombDescriptionInputField;
    public InputField AddCombPathInputField;
    public InputField EditCombIDInputField;
    public InputField EditCombNameInputField;
    public InputField EditCombTimeInputField;
    //public InputField EditTypeInputField;
    //public InputField EditType1InputField;
    public InputField EditCombDescriptionInputField;
    public InputField EditCombPathInputField;
   // public static bool typechangesign;
    //public static bool type1changesign;
    //public GameObject Editpanel;

    private SqlConnection sqlCon;
    public static int querysign;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";

    // Start is called before the first frame update
    void Start()
    {
        
        Combeditpanelsign = false;
        Combeditshowsign = true;
    }


    //public void ActPanelClick()//选择动素管理模块
    //{
    //    ChoosePanel.SetActive(false);
    //    ActDataBase.SetActive(true);

    //}
    //public void ActCombDBClick()//选择动素组合库管理模块
    //{
    //    ChoosePanel.SetActive(false);
    //    ActCombDB.SetActive(true);
    //}


    //public void TypeChanged()//动素种类点击触发事件
    //{
    //    Combtypechangesign = true;
    //}
    //public void Type1Changed()//动作种类点击触发事件
    //{
    //    Combtype1changesign = true;
    //}

    public void EditPanelShow()
    {
        CombEditActPathSign = true;
        Combeditpanelsign = true;
    }
    public void EditPanelClose()
    {
        Combeditpanelsign = false;
    }
    public void AddPanelShow()
    {
        Combaddpanel.SetActive(true);
        CombAddActPathSign = true;
    }
    public void AddPanelClose()
    {
        Combaddpanel.SetActive(false);
    }
    //public void ConditionalQuery()
    //{
    //    querypanel.SetActive(true);

    //}
    //public void ConditionalQueryClose()
    //{
    //    querypanel.SetActive(false);

    //}
    //public void QYesButtonClicked()//条件查询按钮click事件
    //{
    //    querysign = 2;//若当前为条件查询界面则querysign = 2
    //    int count = Combtable.transform.childCount;
    //    for (int i = 0; i < count; i++)
    //    {
    //        Destroy(Combtable.transform.GetChild(i).gameObject);//删除所有“行预制体”
    //        // table.transform.GetChild(i).gameObject.SetActive(false);
    //    }
    //    //ConditionalQueryTable();
    //    UpdateTable();
    //    querypanel.SetActive(false);
    //}
    //public void ConditionalQueryTable()//显示动素条件查询结果（种类）
    //{
    //    sqlCon = new SqlConnection(sqlAddress);
    //    sqlCon.Open();
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.Connection = sqlCon;
    //    string Qtype = QTypeDropdown.captionText.text;
    //    string Qtype1 = QType1Dropdown.captionText.text;
    //    SqlDataAdapter DC;
    //    if (QTypeDropdown.captionText.text != "不限")
    //    {
    //        DC = new SqlDataAdapter("select * from actions where ActType='" + Qtype + "'", sqlCon);
    //    }
    //    else
    //    {
    //        DC = new SqlDataAdapter("select * from actions where ActGenType='" + Qtype1 + "'", sqlCon);
    //    }
    //    //SqlDataAdapter DC = new SqlDataAdapter("select * from actions where ActType='" + Qtype + "'", sqlCon);
    //    System.Data.DataSet dc = new System.Data.DataSet();
    //    DC.Fill(dc);
    //    string[] Id_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
    //    string[] Type_arr = new string[dc.Tables[0].Rows.Count];
    //    string[] Type1_arr = new string[dc.Tables[0].Rows.Count];
    //    string[] Name_arr = new string[dc.Tables[0].Rows.Count];
    //    string[] Time_arr = new string[dc.Tables[0].Rows.Count];
    //    string[] Description_arr = new string[dc.Tables[0].Rows.Count];
    //    string[] Path_arr = new string[dc.Tables[0].Rows.Count];
    //    for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的数据放进数组中
    //    {
    //        string Id = Convert.ToString(dc.Tables[0].Rows[i][0]);//将数据表中的动素序号ActID数据放进数组中
    //                                                              //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
    //                                                              //dc.Tables[0].Rows[i][7])表示取数据表中第一列所有数据
    //        if (Id != null)
    //            Id_arr[i] = Id;
    //        string Type = Convert.ToString(dc.Tables[0].Rows[i][1]);//将数据表中的动素类别ActType数据放进数组中
    //        if (Type != null)
    //            Type_arr[i] = Type;
    //        string Type1 = Convert.ToString(dc.Tables[0].Rows[i][6]);//将数据表中的动作类别ActGenType数据放进数组中
    //        if (Type != null)
    //            Type1_arr[i] = Type1;
    //        string Name = Convert.ToString(dc.Tables[0].Rows[i][2]);
    //        if (Name != null)
    //            Name_arr[i] = Name;
    //        string Time = Convert.ToString(dc.Tables[0].Rows[i][3]);
    //        if (Time != null)
    //            Time_arr[i] = Time;
    //        string Description = Convert.ToString(dc.Tables[0].Rows[i][4]);
    //        if (Description != null)
    //            Description_arr[i] = Description;
    //        string Path = Convert.ToString(dc.Tables[0].Rows[i][5]);
    //        if (Path != null)
    //            Path_arr[i] = Path;
    //    }

    //    for (int i = 0; i < dc.Tables[0].Rows.Count; i++)//添加并修改预设的过程，将创建10行
    //    {
    //        //在Table下创建新的预设实例
    //        GameObject table = GameObject.Find("Canvas/ActDatabase/MainPanel/Viewport/ActTable");
    //        GameObject row = Instantiate(CombRow_Prefab, table.transform.position, table.transform.rotation) as GameObject;
    //        row.name = "row" + (i + 1);
    //        row.transform.SetParent(table.transform);
    //        row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
    //        //设置预设实例中的各个子物体的文本内容
    //        row.transform.Find("ActID").GetComponent<Text>().text = Id_arr[i];
    //        row.transform.Find("ActType1").GetComponent<Text>().text = Type1_arr[i];
    //        row.transform.Find("ActType").GetComponent<Text>().text = Type_arr[i];
    //        row.transform.Find("ActName").GetComponent<Text>().text = Name_arr[i];
    //        row.transform.Find("ActTime").GetComponent<Text>().text = Time_arr[i];
    //        row.transform.Find("ActDescription").GetComponent<Text>().text = Description_arr[i];
    //        row.transform.Find("ActPath").GetComponent<Text>().text = Path_arr[i];
    //    }
    //    sqlCon.Close();
    //}
    public void AddYesButtonClicked()//数据库新增功能
    {
        CombAddActPathSign = false;
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        string ActInsert = "insert into actionCombine(actionCombineID,actionCombineName,actionCombineTime,actionCombineDescription,actionCombinePath) values ('" + AddCombIDInputField.text + "','" + AddCombNameInputField.text + "','" + AddCombTimeInputField.text + "','" + AddCombDescriptionInputField.text + "','" + AddCombPathInputField.text + "')";
        SqlCommand sqlCommand = new SqlCommand(ActInsert, sqlCon);
        sqlCommand.ExecuteNonQuery();
        sqlCon.Close();
        Combaddokpanel.SetActive(true);

    }
    public void AddOkYesButtonClicked()
    {
        Combaddokpanel.SetActive(false);
        Combaddpanel.SetActive(false);
        UpdateTable();
    }
        
    private void UpdateTable()
    {
        int count = Combtable.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(Combtable.transform.GetChild(i).gameObject);//删除所有“行预制体”           
        }        
        AllQuery();
    }
    
    public void EditYesButtonClicked()//数据库编辑功能
    {
        CombEditActPathSign = false;
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        string ActEdit = "update actionCombine set  actionCombineName='" + EditCombNameInputField.text + "', actionCombineTime='" + EditCombTimeInputField.text + "', actionCombineDescription = '" + EditCombDescriptionInputField.text + "',actionCombinePath = '" + EditCombPathInputField.text + "' where actionCombineID = '" + EditCombIDInputField.text + "'";
        SqlCommand sqlCommand = new SqlCommand(ActEdit, sqlCon);
        sqlCommand.ExecuteNonQuery();
        sqlCon.Close();
        Combeditokpanel.SetActive(true);
    }
    public void EditOkYesButtonClicked()
    {
        Combeditokpanel.SetActive(false);
        Combeditpanelsign = false;
        UpdateTable();
    }

    void EditShow()
    {
        Combeditshowsign = false;
        EditCombIDInputField.GetComponent<InputField>().text = CombRowScripts.RowActCombIDText;
        EditCombNameInputField.GetComponent<InputField>().text = CombRowScripts.RowActCombNameText;
        EditCombTimeInputField.GetComponent<InputField>().text = CombRowScripts.RowActCombTimeText;
        //EditTypeInputField.GetComponent<InputField>().text = RowScripts.RowActTypeText;
        //EditType1InputField.GetComponent<InputField>().text = RowScripts.RowActType1Text;
        EditCombPathInputField.GetComponent<InputField>().text = CombRowScripts.RowActCombPathText;
        EditCombDescriptionInputField.GetComponent<InputField>().text = CombRowScripts.RowActCombDescriptionText;
    }
    void AllQuery()//显示全部查询内容
    {
        querysign = 1;
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;
        SqlDataAdapter DC = new SqlDataAdapter("select * from actionCombine ", sqlCon);
        System.Data.DataSet dc = new System.Data.DataSet();
        DC.Fill(dc);
        string[] Id_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        //string[] Type_arr = new string[dc.Tables[0].Rows.Count];
        //string[] Type1_arr = new string[dc.Tables[0].Rows.Count];
        string[] Name_arr = new string[dc.Tables[0].Rows.Count];
        string[] Time_arr = new string[dc.Tables[0].Rows.Count];
        string[] Description_arr = new string[dc.Tables[0].Rows.Count];
        string[] Path_arr = new string[dc.Tables[0].Rows.Count];
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的数据放进数组中
        {
            string Id = Convert.ToString(dc.Tables[0].Rows[i][0]);//将数据表中的动素序号ActID数据放进数组中
                                                                  //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
                                                                  //dc.Tables[0].Rows[i][7])表示取数据表中第一列所有数据
            if (Id != null)
                Id_arr[i] = Id;
            //string Type = Convert.ToString(dc.Tables[0].Rows[i][1]);//将数据表中的动素类别ActType数据放进数组中
            //if (Type != null)
            //    Type_arr[i] = Type;
            //string Type1 = Convert.ToString(dc.Tables[0].Rows[i][6]);//将数据表中的动作类别ActGenType数据放进数组中
            //if (Type != null)
            //    Type1_arr[i] = Type1;
            string Name = Convert.ToString(dc.Tables[0].Rows[i][1]);
            if (Name != null)
                Name_arr[i] = Name;
            string Time = Convert.ToString(dc.Tables[0].Rows[i][2]);
            if (Time != null)
                Time_arr[i] = Time;
            string Description = Convert.ToString(dc.Tables[0].Rows[i][3]);
            if (Description != null)
                Description_arr[i] = Description;
            string Path = Convert.ToString(dc.Tables[0].Rows[i][4]);
            if (Path != null)
                Path_arr[i] = Path;
        }

        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)//添加并修改预设的过程，将创建10行
        {
            //在Table下创建新的预设实例
            GameObject table = Combtable;
            GameObject row = Instantiate(CombRow_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("ActCombID").GetComponent<Text>().text = Id_arr[i];
            //row.transform.Find("ActType1").GetComponent<Text>().text = Type1_arr[i];
            //row.transform.Find("ActType").GetComponent<Text>().text = Type_arr[i];
            row.transform.Find("ActCombName").GetComponent<Text>().text = Name_arr[i];
            row.transform.Find("ActCombTime").GetComponent<Text>().text = Time_arr[i];
            row.transform.Find("ActCombDescription").GetComponent<Text>().text = Description_arr[i];
            row.transform.Find("ActCombPath").GetComponent<Text>().text = Path_arr[i];
        }
    }
    void Update()
    {
        if (Combeditpanelsign == true)
        {
            Combeditpanel.SetActive(true);
            if (Combeditshowsign == true)
            {
                EditShow();
            }
        }
        else
        {
            Combeditpanel.SetActive(false);
        }
    }

}

