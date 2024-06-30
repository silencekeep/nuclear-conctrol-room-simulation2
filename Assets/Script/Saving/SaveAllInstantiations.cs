using CNCC.Saving;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;
using CNCC.Models;
using CNCC.Panels;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text;
using System.Data;

public class SaveAllInstantiations : MonoBehaviour
{
    [Header("UI_Button")]
    [SerializeField] Button Save;
    [SerializeField] Button Reset;
    [Header("厂房围墙")]
    [SerializeField] GameObject Wall_x0;
    [SerializeField] GameObject Wall_z0;
    [SerializeField] GameObject Wall_x;
    [SerializeField] GameObject Wall_z;
    [Header("厂房保存")]
    public UnityEvent OnNewWorkAreaSave;
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
        Save.onClick.AddListener(SaveClick);
        //tableName = "表1";
    }

    private void OnEnable()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.S))
        {
            SaveClick();
        }
    }

    public void SaveClick()
    {
        SaveAll();
        print("所有物体已保存");
        OnNewWorkAreaSave.Invoke();
        this.gameObject.SetActive(false);
    }

    void SaveAll()
    {
        if (!tableName.Equals(SavingSystem.CurrentWorkAreaName))
        {
            tableName = SavingSystem.CurrentWorkAreaName;

        }
        string sql = "delete from " + tableName;
        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        //SqlDataAdapter sda = new SqlDataAdapter("delete from " + tableName, sqlCon);
        //System.Data.DataSet ds = new System.Data.DataSet();
        //sda.Fill(ds, "table");
        SqliteAssist sqliteAssist = new SqliteAssist();
        sqliteAssist.RecordChange(sql);
        GetAllHumanInstantiations();
        GetAllPanelInstantiations();
        GetAreaInfo();
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

    void GetAllHumanInstantiations()
    {
        string sql = "select * from HumanTable where WorkAreaName = '" + tableName + "'";
        SqliteAssist sqliteAssist = new SqliteAssist();
        DataSet dc = sqliteAssist.GetQueryResultByDataSet(sql);
        string sqlDeleteHUmanInfo = "DELETE FROM HumanTable Where WorkAreaName ='" + tableName + "'";
        sqliteAssist.RecordChange(sqlDeleteHUmanInfo);
        //System.Data.DataSet dc = Sqlquery(sql);
        foreach (Model human in Model.createdModel)
        {
            sql = "INSERT INTO " + tableName + " VALUES ('人物" + human.gender + "', '" + human.Name + "','" + human.gameObject.transform.position.ToString("F4") + "','" + human.gameObject.transform.eulerAngles + "','" + human.gameObject.transform.localScale + "')";
            //print(sql);
            //Sqladd(sql);
            sqliteAssist.RecordChange(sql);

            //插入到人物表当中

            if (IsRepeat(tableName, human.Name, dc))
            {
                string sqlDelete = "DELETE FROM HumanTable Where WorkAreaName ='" + tableName + "' AND Name ='" + human.Name + "'";
                //Sqladd(sqlDelete);
                sqliteAssist.RecordChange(sqlDelete);
            }

            StringBuilder res = new StringBuilder();//StringBuilder
            List<Vector3> vector3s = human.GetJointInfo();
            res.Append("INSERT INTO HumanTable VALUES ('");
            res.Append(tableName);
            res.Append("','");
            res.Append(human.Name);
            res.Append("','");
            for (int i = 0; i < vector3s.Count; i++)
            {
                res.Append(vector3s[i]);
                if (i != vector3s.Count - 1)
                {
                    res.Append("','");
                }
            }
            res.Append("')");
            //Sqladd(res.ToString());
            sqliteAssist.RecordChange(res.ToString());
        }
        sqliteAssist.Close();
    }

    void GetAllPanelInstantiations()
    {
        SqliteAssist sqliteAssist = new SqliteAssist();
        foreach (BUP bup in BUP.AllBUPs)
        {
            sql = "INSERT INTO " + tableName + " VALUES ('BUP" + "', '" + bup.ID + "', '" + bup.gameObject.transform.position.ToString("F4") + "', '" + bup.gameObject.transform.eulerAngles + "', '" + bup.gameObject.transform.localScale + "')";
            //Sqladd(sql);
            sqliteAssist.RecordChange(sql);

        }

        foreach (BUP90Assembly bup90 in BUP90Assembly.AllBUP90s)
        {
            sql = "INSERT INTO " + tableName + " VALUES ('BUP90Assembly" + "', '" + bup90.ID + "', '" + bup90.gameObject.transform.position.ToString("F4") + "', '" + bup90.gameObject.transform.eulerAngles + "', '" + bup90.gameObject.transform.localScale + "')";
            //Sqladd(sql);
            sqliteAssist.RecordChange(sql);
        }

        foreach (ECP_N ECP_N in ECP_N.ALLECP_Ns)
        {
            sql = "INSERT INTO " + tableName + " VALUES ('ECP_N" + "', '" + ECP_N.ID + "','" + ECP_N.gameObject.transform.position.ToString("F4") + "','" + ECP_N.gameObject.transform.eulerAngles + "','" + ECP_N.gameObject.transform.localScale + "')";
            //Sqladd(sql);
            sqliteAssist.RecordChange(sql);
        }

        foreach (ECP_S ECP_S in ECP_S.ALLECP_Ss)
        {
            sql = "INSERT INTO " + tableName + " VALUES ('ECP_S" + "', '" + ECP_S.ID + "','" + ECP_S.gameObject.transform.position.ToString("F4") + "','" + ECP_S.gameObject.transform.eulerAngles + "','" + ECP_S.gameObject.transform.localScale + "')";
            //Sqladd(sql);
            sqliteAssist.RecordChange(sql);
        }

        foreach (LargeScreen LargeScreen in LargeScreen.AllLargeScreen)
        {
            sql = "INSERT INTO " + tableName + " VALUES ('LargeScreen" + "', '" + LargeScreen.ID + "','" + LargeScreen.gameObject.transform.position.ToString("F4") + "','" + LargeScreen.gameObject.transform.eulerAngles + "','" + LargeScreen.gameObject.transform.localScale + "')";
            //Sqladd(sql);
            sqliteAssist.RecordChange(sql);
        }

        foreach (LargeScreen45Assembly LargeScreen45Assembly in LargeScreen45Assembly.AllLargeScreen45Assembly)
        {
            sql = "INSERT INTO " + tableName + " VALUES ('LargeScreen45Assembly" + "', '" + LargeScreen45Assembly.ID + "','" + LargeScreen45Assembly.gameObject.transform.position.ToString("F4") + "','" + LargeScreen45Assembly.gameObject.transform.eulerAngles + "','" + LargeScreen45Assembly.gameObject.transform.localScale + "')";
            //Sqladd(sql);
            sqliteAssist.RecordChange(sql);
        }

        foreach (OWP_corner OWP_corner in OWP_corner.AllOWP_corner)
        {
            sql = "INSERT INTO " + tableName + " VALUES ('OWP_corner" + "', '" + OWP_corner.ID + "','" + OWP_corner.gameObject.transform.position.ToString("F4") + "','" + OWP_corner.gameObject.transform.eulerAngles + "','" + OWP_corner.gameObject.transform.localScale + "')";
            //Sqladd(sql);
            sqliteAssist.RecordChange(sql);
        }

        foreach (OWP_three OWP_three in OWP_three.AllOWP_three)
        {
            sql = "INSERT INTO " + tableName + " VALUES ('OWP_three" + "', '" + OWP_three.ID + "','" + OWP_three.gameObject.transform.position.ToString("F4") + "','" + OWP_three.gameObject.transform.eulerAngles + "','" + OWP_three.gameObject.transform.localScale + "')";
           //Sqladd(sql);
            sqliteAssist.RecordChange(sql);
        }

        foreach (OWP_two OWP_two in OWP_two.AllOWP_two)
        {
            sql = "INSERT INTO " + tableName + " VALUES ('OWP_two" + "', '" + OWP_two.ID + "','" + OWP_two.gameObject.transform.position.ToString("F4") + "','" + OWP_two.gameObject.transform.eulerAngles + "','" + OWP_two.gameObject.transform.localScale + "')";
            //Sqladd(sql);
            sqliteAssist.RecordChange(sql);
        }

        foreach (SVDU SVDU in SVDU.AllSVDU)
        {
            sql = "INSERT INTO " + tableName + " VALUES ('SVDU" + "', '" + SVDU.ID + "','" + SVDU.gameObject.transform.position.ToString("F4") + "','" + SVDU.gameObject.transform.eulerAngles + "','" + SVDU.gameObject.transform.localScale + "')";
            //Sqladd(sql);
            sqliteAssist.RecordChange(sql);
        }

        foreach (Chair Chair in Chair.ALLChairs)
        {
            sql = "INSERT INTO " + tableName + " VALUES ('Chair" + "', '" + Chair.ID + "','" + Chair.gameObject.transform.position.ToString("F4") + "','" + Chair.gameObject.transform.eulerAngles + "','" + Chair.gameObject.transform.localScale + "')";
            //Sqladd(sql);
            sqliteAssist.RecordChange(sql);
        }
        sqliteAssist.Close();
    }

    #region 获取当前厂房尺寸厂房面积
    void GetAreaInfo()
    {
        length = Wall_z0.transform.localScale.x;
        width = Wall_x0.transform.localScale.z;
        sql = "UPDATE AllWorkAreaTable SET length ='" + length + "', width = '" + width + "', ModifiedTime = '" + System.DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss") + "'" + " WHERE WorkAreaName ='" + tableName + "'";
        SqliteAssist sqliteAssist = new SqliteAssist();
        sqliteAssist.RecordChange(sql);
        //Sqladd(sql);
        print("保存信息修改");
        sqliteAssist.Close();
    }

    bool IsRepeat(string roomName, string objName, System.Data.DataSet dc)
    {
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string DBRoomName = dc.Tables[0].Rows[i][0].ToString().Trim();
            string DBobjName = dc.Tables[0].Rows[i][1].ToString().Trim();
            if (roomName.Equals(DBRoomName) && objName.Equals(DBobjName))
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}
