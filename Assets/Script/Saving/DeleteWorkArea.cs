using CNCC.Models;
using CNCC.Panels;
using CNCC.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DeleteWorkArea : MonoBehaviour
{
    [Header("UI_Button")]
    [SerializeField] Button Delete;
    [SerializeField] Button Reset;

    [Header("厂房保存")]
    public UnityEvent OnNewWorkAreaDelete;

    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=NuclearControlRoomItemDB;uid=sa;pwd=123456";
    private string tableName = "";
    private string sql = "";
    void Start()
    {
        Delete.onClick.AddListener(DeleteOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeleteOnClick()
    {
        DeleteCurrentScene();
        OnNewWorkAreaDelete.Invoke();
        this.gameObject.SetActive(false);
    }

    private void DeleteCurrentScene()
    {
        DeleteAll();
        tableName = string.Empty;
        SavingSystem.CurrentWorkAreaName = string.Empty;
    }

    void DeleteAll()
    {
        if (SavingSystem.CurrentWorkAreaName == "")
        {
            return;
        }
        if (!tableName.Equals(SavingSystem.CurrentWorkAreaName))
        {
            tableName = SavingSystem.CurrentWorkAreaName;

        }
        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        SqliteAssist sqliteAssist = new SqliteAssist();
        sqliteAssist.RecordChange("drop table " + tableName);
        //SqlDataAdapter sda = new SqlDataAdapter("drop table " + tableName, sqlCon);
        //System.Data.DataSet ds = new System.Data.DataSet();
        //sda.Fill(ds, "table");
        DeleteDBInfo();
        ClearOutAllPrefabs();
        //sqlCon.Close();
    }

    public void ClearAll()
    {
        ClearOutAllPrefabs();
        print("清场");
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

        foreach (BUP90Assembly bup90 in BUP90Assembly.AllBUP90s)
        {
            Destroy(bup90.gameObject);
        }
        BUP90Assembly.AllBUP90s.Clear();

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

    void DeleteDBInfo()
    {
        sql = "delete from AllWorkAreaTable where WorkAreaName = '" + tableName +"'";
        string sqlHuman = "delete from HumanTable where WorkAreaName = '" + tableName + "'";
        print(sql);
        //Sqladd(sql);
        SqliteAssist sqliteAssist = new SqliteAssist();
        sqliteAssist.RecordChange(sql);
        sqliteAssist.RecordChange(sqlHuman);
        sqliteAssist.Close();
    }

    //public void Sqladd(string sql)
    //{
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.Connection = sqlCon;
    //    SqlDataAdapter DC = new SqlDataAdapter(sql, sqlCon);
    //    System.Data.DataSet dc = new System.Data.DataSet();
    //    DC.Fill(dc, "table");
    //}
    //数据库查询
    //public System.Data.DataSet Sqlquery(string sql)
    //{
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.Connection = sqlCon;
    //    SqlDataAdapter DC = new SqlDataAdapter(sql, sqlCon);
    //    System.Data.DataSet dc = new System.Data.DataSet();
    //    DC.Fill(dc, "table");
    //    return dc;
    //}
}
