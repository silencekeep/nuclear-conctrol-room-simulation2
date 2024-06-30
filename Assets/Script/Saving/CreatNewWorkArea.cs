using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CNCC.Models;
using CNCC.Panels;
using CNCC.Saving;
using UnityEngine.Events;
using System;
using System.Data;

public class CreatNewWorkArea : MonoBehaviour
{
    [Header("UI_InputText")]
    [SerializeField] TMP_InputField workAreaName;//用户名输入框
    [SerializeField] TMP_InputField lengthText;
    [SerializeField] TMP_InputField widthText;
    [Header("UI_Button")]
    [SerializeField] Button comfireButton;
    [SerializeField] Button cancelButton;
    [Header("厂房围墙")]
    [SerializeField] GameObject Wall_x0;
    [SerializeField] GameObject Wall_z0;
    [SerializeField] GameObject Wall_x;
    [SerializeField] GameObject Wall_z;


    [Header("厂房生成事件")]
    public UnityEvent OnNewWorkAreaCreated;

    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=NuclearControlRoomItemDB;uid=sa;pwd=123456";
    private string tableName = "";
    private string sql = "";
    private float length = 0;
    private float width = 0;
    private float height = 10;
    // Start is called before the first frame update
    void Start()
    {
        comfireButton.onClick.AddListener(creatNewWorkArea);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void creatNewWorkArea()
    {
        //输入合法检查
        if (!float.TryParse(lengthText.text, out length) || !float.TryParse(widthText.text, out width))
        {
            print("输入的参数有误");
            return;
        }
        if (lengthText.text == null || widthText.text == null)
        {
            return;
        }
        GetInPut();
        ClearOutAllPrefabs();
        if (IsWrokAreaNameRepeat(tableName)) return;

        //数据库连接
        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        SqliteAssist sqliteAssist = new SqliteAssist();
        string sqlstr = "create table " + tableName + " (Type varchar(50) DEFAULT NULL, Name varchar(50) DEFAULT NULL, Position varchar(50) DEFAULT NULL, Rotation varchar(50) DEFAULT NULL, Scale varchar(50) DEFAULT NULL)";
        print(sqlstr);
        sqliteAssist.RecordChange(sqlstr);
        //SqlDataAdapter sda = new SqlDataAdapter(sqlstr, sqlCon);

        //System.Data.DataSet ds = new System.Data.DataSet();
        //sda.Fill(ds, "table");


        AddNewWorkAreaToDatabase(tableName, length, width);
        SetWorkAreaWall();
        ResetList();
        SavingSystem.CurrentWorkAreaName = tableName;
        OnNewWorkAreaCreated.Invoke();
        this.gameObject.SetActive(false);
        sqliteAssist.Close();
    }

    void GetInPut()
    {
        tableName = workAreaName.text;
    }

    //添加新表到创建的表当中，包括厂房名称，创建时间、修改时间、操作员等）
    void AddNewWorkAreaToDatabase(string newWorkAreaName, float length, float width)
    {
        //SqlConnection newsqlCon = new SqlConnection(sqlAddress);
        //newsqlCon.Open();
        // SqlDataAdapter sda = new SqlDataAdapter("INSERT INTO AllWorkAreaTable VALUES ('" + newWorkAreaName + "', '" + System.DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss") + "', '" + System.DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss") + "', '" + "操作员" + "', '" + length + "', '" + width + "')", newsqlCon);
        //System.Data.DataSet ds = new System.Data.DataSet();
        //sda.Fill(ds, "table");
        //newsqlCon.Close();
        string sql = "INSERT INTO AllWorkAreaTable VALUES ('" + newWorkAreaName + "', '" + System.DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss") + "', '" + System.DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss") + "', '" + SavingSystem.CurrentUserName + "', '" + length + "', '" + width + "')";
        SqliteAssist sqliteAssist = new SqliteAssist();
        sqliteAssist.RecordChange(sql);
        sqliteAssist.Close();
    }

    void ClearOutAllPrefabs()
    {
        foreach (Model human in Model.createdModel)
        {
            Destroy(human.gameObject);
        }
        Model.createdModel.Clear();

        foreach (BUP bup in BUP.AllBUPs)
        {
            Destroy(bup.gameObject);
        }
        BUP.AllBUPs.Clear();

        foreach (ECP_N ECP_N in ECP_N.ALLECP_Ns)
        {
            Destroy(ECP_N.gameObject);
        }
        ECP_N.ALLECP_Ns.Clear();

        foreach (ECP_S ECP_S in ECP_S.ALLECP_Ss)
        {
            Destroy(ECP_S.gameObject);
        }
        ECP_S.ALLECP_Ss.Clear();

        foreach (LargeScreen LargeScreen in LargeScreen.AllLargeScreen)
        {
            Destroy(LargeScreen.gameObject);
        }
        LargeScreen.AllLargeScreen.Clear();

        foreach (LargeScreen45Assembly LargeScreen45Assembly in LargeScreen45Assembly.AllLargeScreen45Assembly)
        {
            Destroy(LargeScreen45Assembly.gameObject);
        }
        LargeScreen45Assembly.AllLargeScreen45Assembly.Clear();

        foreach (OWP_corner OWP_corner in OWP_corner.AllOWP_corner)
        {
            Destroy(OWP_corner.gameObject);
        }
        OWP_corner.AllOWP_corner.Clear();

        foreach (OWP_three OWP_three in OWP_three.AllOWP_three)
        {
            Destroy(OWP_three.gameObject);
        }
        OWP_three.AllOWP_three.Clear();

        foreach (OWP_two OWP_two in OWP_two.AllOWP_two)
        {
            Destroy(OWP_two.gameObject);
        }
        OWP_two.AllOWP_two.Clear();

        foreach (SVDU SVDU in SVDU.AllSVDU)
        {
            Destroy(SVDU.gameObject);
        }
        SVDU.AllSVDU.Clear();

        foreach (Chair Chair in Chair.ALLChairs)
        {
            Destroy(Chair.gameObject);
        }
        Chair.ALLChairs.Clear();

        foreach (Panel panel in Panel.AllPanels)
        {
            Destroy(panel.gameObject);
        }
        Panel.AllPanels.Clear();
    }

    bool IsWrokAreaNameRepeat(string newWorkAreaName)
    {
        //SqlConnection newsqlCon = new SqlConnection(sqlAddress);
        //newsqlCon.Open();
        //SqlDataAdapter sda = new SqlDataAdapter("select WorkAreaName from AllWorkAreaTable", newsqlCon);
        //System.Data.DataSet ds = new System.Data.DataSet();
        //sda.Fill(ds, "table");
        string sql = "select WorkAreaName from AllWorkAreaTable";
        SqliteAssist sqliteAssist = new SqliteAssist();
        DataSet ds =  sqliteAssist.GetQueryResultByDataSet(sql);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string oldName = ds.Tables[0].Rows[i][0].ToString().Trim();
            if (oldName.Equals(newWorkAreaName))
            {
                print("该厂房名称已存在");
                //newsqlCon.Close();
                sqliteAssist.Close();
                return true;
            }
        }
        //newsqlCon.Close();
        sqliteAssist.Close();
        return false;
    }

    private void ResetList()
    {
        Model.createdModel.Clear();
        Panel.AllPanels.Clear();
        BUP.AllBUPs.Clear();
        BUP90Assembly.AllBUP90s.Clear();
        ECP_N.ALLECP_Ns.Clear();
        ECP_S.ALLECP_Ss.Clear();
        LargeScreen.AllLargeScreen.Clear();
        LargeScreen45Assembly.AllLargeScreen45Assembly.Clear();
        OWP_corner.AllOWP_corner.Clear();
        OWP_three.AllOWP_three.Clear();
        OWP_two.AllOWP_two.Clear();
        SVDU.AllSVDU.Clear();
        Chair.ALLChairs.Clear();
    }
    #region 设置厂房面积
    void SetWorkAreaWall()
    {
        Setx0();
        Setz0();
        Setx();
        Setz();
    }

    void Setx0()
    {
        Wall_x0.transform.position = new Vector3(0, height / 2, width / 2);
        Wall_x0.transform.localScale = new Vector3((float)0.01, height, width);
    }

    void Setz0()
    {
        Wall_z0.transform.position = new Vector3(length / 2, height / 2, 0);
        Wall_z0.transform.localScale = new Vector3(length, height, (float)0.01);
    }

    void Setx()
    {
        Wall_x.transform.position = new Vector3(length, height / 2, width / 2);
        Wall_x.transform.localScale = new Vector3((float)0.01, height, width);
    }

    void Setz()
    {
        Wall_z.transform.position = new Vector3(length / 2, height / 2, width);
        Wall_z.transform.localScale = new Vector3(length, height, (float)0.01);
    }
    #endregion

}
