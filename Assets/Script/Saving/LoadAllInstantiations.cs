using CNCC.Models;
using CNCC.Panels;
using CNCC.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

 
public class LoadAllInstantiations : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button LoadButton;
    [SerializeField] Button Reset;
    [SerializeField] TMP_Dropdown WorkAreaDropdown;
    [SerializeField] TMP_Text LeghthText;
    [SerializeField] TMP_Text WidthText;
    [SerializeField] TMP_Text CreatedTimeText;
    [SerializeField] TMP_Text ModifiedTimeText;
    [SerializeField] TMP_Text OperatorText;

    [Header("生成位置")]
    [SerializeField] Transform generateOriginalPosition;
    [SerializeField] Transform HumangenerateOriginalPosition;
    [Header("Prefab")]
    [SerializeField] GameObject Man;
    [SerializeField] GameObject Woman;
    [SerializeField] GameObject BUPpanel;
    [SerializeField] GameObject BUP90Assemblypanel;
    [SerializeField] GameObject ECP_Npanel;
    [SerializeField] GameObject ECP_Spanel;
    [SerializeField] GameObject LargeScreenpanel;
    [SerializeField] GameObject LargeScreen45Assemblypanel;
    [SerializeField] GameObject OWP_cornerpanel;
    [SerializeField] GameObject OWP_threepanel;
    [SerializeField] GameObject OWP_twopanel;
    [SerializeField] GameObject SVDUpanel;
    [SerializeField] GameObject ChairPanel;
    [Header("厂房围墙")]
    [SerializeField] GameObject Wall_x0;
    [SerializeField] GameObject Wall_z0;
    [SerializeField] GameObject Wall_x;
    [SerializeField] GameObject Wall_z;

    [Header("厂房生成事件")]
    public UnityEvent SaveOldWorkArea;
    public UnityEvent OnNewWorkAreaLoad;
    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=NuclearControlRoomItemDB;uid=sa;pwd=123456";
    //private string workArea = "";
    GameObject generatedCharacters;
    GameObject generatedPanel;
    private float length = 0;
    private float width = 0;
    private float height = 10;
    // Start is called before the first frame update
    private void Awake()
    {
        //this.gameObject.SetActive(true);
    }
    void Start()
    {

        LoadButton.onClick.AddListener(LoadAllObject);
        GetWorkAreaInfor();

        //程序开始初始化，最后可删
        Init();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        print("当前人物数量" + Model.createdModel.Count);
    }
    private void OnEnable()
    {
        GetWorkAreaInfor();
    }

    void LoadAllObject()
    {
        //if (SavingSystem.CurrentWorkAreaName!="")
        //{
        //    SaveOldWorkArea.Invoke();
        //}
        ClearOutAllPrefabs();
        SetWorkAreaWall();
        OnNewWorkAreaLoad.Invoke();
        InstantiateAllObjects(SavingSystem.CurrentWorkAreaName);
        LoadHumanInfo(SavingSystem.CurrentWorkAreaName);
        this.gameObject.SetActive(false);
    }

    //加载所有厂房信息
    void InstantiateAllObjects(string workArea)
    {
        if (workArea.Equals(null) || workArea == "")
        {
            print("选择的厂房为空");
            return;
        }
        string sql = "select * from " + workArea;
        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        //DataSet dc = sqlquery(sql);
        SqliteAssist sqliteAssist = new SqliteAssist();
        DataSet dc = sqliteAssist.GetQueryResultByDataSet(sql);
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string type = dc.Tables[0].Rows[i][0].ToString().Trim();
            // print(dc.Tables[0].Rows[i][2]+" "+ dc.Tables[0].Rows[i][3]+ " "+ dc.Tables[0].Rows[i][4]);
            switch (type)
            {
                case "人物男":
                    generatedCharacters = Instantiate(Man, HumangenerateOriginalPosition);
                    generatedCharacters.GetComponent<Model>().Name = dc.Tables[0].Rows[i][1].ToString().Trim();
                    generatedCharacters.GetComponent<Model>().gender = "男";
                    SetPosition(dc, i, generatedCharacters);
                    Model.createdModel.Add(generatedCharacters.GetComponent<Model>());
                    break;
                case "人物女":
                    generatedCharacters = Instantiate(Woman, HumangenerateOriginalPosition);
                    generatedCharacters.GetComponent<Model>().Name = dc.Tables[0].Rows[i][1].ToString().Trim();
                    generatedCharacters.GetComponent<Model>().gender = "女";
                    SetPosition(dc, i, generatedCharacters);
                    Model.createdModel.Add(generatedCharacters.GetComponent<Model>());
                    break;
                case "BUP":
                    generatedPanel = Instantiate(BUPpanel, generateOriginalPosition);
                    generatedPanel.gameObject.GetComponent<BUP>().ID = dc.Tables[0].Rows[i][1].ToString().Trim();
                    SetPosition(dc, i, generatedPanel);
                    BUP.AllBUPs.Add(generatedPanel.GetComponent<BUP>());
                    Panel.AllPanels.Add(generatedPanel.GetComponent<BUP>());
                    break;
                case "BUP90Assembly":
                    generatedPanel = Instantiate(BUP90Assemblypanel, generateOriginalPosition);
                    generatedPanel.gameObject.GetComponent<BUP90Assembly>().ID = dc.Tables[0].Rows[i][1].ToString().Trim();
                    SetPosition(dc, i, generatedPanel);
                    BUP90Assembly.AllBUP90s.Add(generatedPanel.GetComponent<BUP90Assembly>());
                    Panel.AllPanels.Add(generatedPanel.GetComponent<BUP90Assembly>());
                    break;
                case "ECP_N":
                    generatedPanel = Instantiate(ECP_Npanel, generateOriginalPosition);
                    generatedPanel.gameObject.GetComponent<ECP_N>().ID = dc.Tables[0].Rows[i][1].ToString().Trim();
                    SetPosition(dc, i, generatedPanel);
                    ECP_N.ALLECP_Ns.Add(generatedPanel.GetComponent<ECP_N>());
                    Panel.AllPanels.Add(generatedPanel.GetComponent<ECP_N>());
                    break;
                case "ECP_S":
                    generatedPanel = Instantiate(ECP_Spanel, generateOriginalPosition);
                    generatedPanel.gameObject.GetComponent<ECP_S>().ID = dc.Tables[0].Rows[i][1].ToString().Trim();
                    SetPosition(dc, i, generatedPanel);
                    ECP_S.ALLECP_Ss.Add(generatedPanel.GetComponent<ECP_S>());
                    Panel.AllPanels.Add(generatedPanel.GetComponent<ECP_S>());
                    break;
                case "LargeScreen":
                    generatedPanel = Instantiate(LargeScreenpanel, generateOriginalPosition);
                    generatedPanel.gameObject.GetComponent<LargeScreen>().ID = dc.Tables[0].Rows[i][1].ToString().Trim();
                    SetPosition(dc, i, generatedPanel);
                    LargeScreen.AllLargeScreen.Add(generatedPanel.GetComponent<LargeScreen>());
                    Panel.AllPanels.Add(generatedPanel.GetComponent<LargeScreen>());
                    break;
                case "LargeScreen45Assembly":
                    generatedPanel = Instantiate(LargeScreen45Assemblypanel, generateOriginalPosition);
                    generatedPanel.gameObject.GetComponent<LargeScreen45Assembly>().ID = dc.Tables[0].Rows[i][1].ToString().Trim();
                    SetPosition(dc, i, generatedPanel);
                    LargeScreen45Assembly.AllLargeScreen45Assembly.Add(generatedPanel.GetComponent<LargeScreen45Assembly>());
                    Panel.AllPanels.Add(generatedPanel.GetComponent<LargeScreen45Assembly>());
                    break;
                case "OWP_corner":
                    generatedPanel = Instantiate(OWP_cornerpanel, generateOriginalPosition);
                    generatedPanel.gameObject.GetComponent<OWP_corner>().ID = dc.Tables[0].Rows[i][1].ToString().Trim();
                    SetPosition(dc, i, generatedPanel);
                    //generatedPanel.gameObject.GetComponent<OWP_corner>().monitorMiddle.SetActive(SetMonitorShowHide(dc, i)[1]);
                    OWP_corner.AllOWP_corner.Add(generatedPanel.GetComponent<OWP_corner>());
                    Panel.AllPanels.Add(generatedPanel.GetComponent<OWP_corner>());
                    break;
                case "OWP_three":
                    generatedPanel = Instantiate(OWP_threepanel, generateOriginalPosition);
                    generatedPanel.gameObject.GetComponent<OWP_three>().ID = dc.Tables[0].Rows[i][1].ToString().Trim();
                    SetPosition(dc, i, generatedPanel);
                    //generatedPanel.gameObject.GetComponent<OWP_three>().monitorLeft.SetActive(SetMonitorShowHide(dc, i)[0]);
                    //generatedPanel.gameObject.GetComponent<OWP_three>().monitorMiddle.SetActive(SetMonitorShowHide(dc, i)[1]);
                    //generatedPanel.gameObject.GetComponent<OWP_three>().monitorRight.SetActive(SetMonitorShowHide(dc, i)[2]);
                    OWP_three.AllOWP_three.Add(generatedPanel.GetComponent<OWP_three>());
                    Panel.AllPanels.Add(generatedPanel.GetComponent<OWP_three>());
                    break;
                case "OWP_two":
                    generatedPanel = Instantiate(OWP_twopanel, generateOriginalPosition);
                    generatedPanel.gameObject.GetComponent<OWP_two>().ID = dc.Tables[0].Rows[i][1].ToString().Trim();
                    SetPosition(dc, i, generatedPanel);
                    //generatedPanel.gameObject.GetComponent<OWP_two>().monitorLeft.SetActive(SetMonitorShowHide(dc, i)[0]);
                    //generatedPanel.gameObject.GetComponent<OWP_two>().monitorRight.SetActive(SetMonitorShowHide(dc, i)[2]);
                    OWP_two.AllOWP_two.Add(generatedPanel.GetComponent<OWP_two>());
                    Panel.AllPanels.Add(generatedPanel.GetComponent<OWP_two>());
                    break;
                case "SVDU":
                    generatedPanel = Instantiate(SVDUpanel, generateOriginalPosition);
                    generatedPanel.gameObject.GetComponent<SVDU>().ID = dc.Tables[0].Rows[i][1].ToString().Trim();
                    SetPosition(dc, i, generatedPanel);
                    SVDU.AllSVDU.Add(generatedPanel.GetComponent<SVDU>());
                    Panel.AllPanels.Add(generatedPanel.GetComponent<SVDU>());
                    break;

                case "Chair":
                    generatedPanel = Instantiate(ChairPanel, generateOriginalPosition);
                    generatedPanel.gameObject.GetComponent<Chair>().ID = dc.Tables[0].Rows[i][1].ToString().Trim();
                    SetPosition(dc, i, generatedPanel);
                    Chair.ALLChairs.Add(generatedPanel.GetComponent<Chair>());
                    Panel.AllPanels.Add(generatedPanel.GetComponent<Chair>());
                    break;
            }
        }
        print("所有物体已加载");
        sqliteAssist.Close();
    }

    //加载所有人物信息
    void LoadHumanInfo(string tableName)
    {
        string sql = "select * from HumanTable Where WorkAreaName = '" + tableName + "'";
        print(sql);
        //System.Data.DataSet dc = sqlquery(sql);   //
        SqliteAssist sqliteAssist = new SqliteAssist();
        DataSet dc = sqliteAssist.GetQueryResultByDataSet(sql);
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < Model.createdModel.Count; j++)
            {
                if (dc.Tables[0].Rows[i][1].ToString().Equals(Model.createdModel[j].Name))
                {
                    List<Vector3> DBJointInfo = new List<Vector3>();
                    for (int k = 2; k < dc.Tables[0].Columns.Count; k++)
                    {
                        DBJointInfo.Add(ParseStoV3(dc.Tables[0].Rows[i][k].ToString()));
                    }
                    Model.createdModel[j].SetJointInfo(DBJointInfo);
                }
            }
        }
        //string sqlDelete = "DELETE FROM HumanTable Where WorkAreaName ='" + tableName +  "'";
        //sqliteAssist.RecordChange(sqlDelete);
        sqliteAssist.Close();
    }

    private void SetPosition(DataSet dc, int i, GameObject obj)
    {
        obj.transform.localPosition = ParseStoV3(dc.Tables[0].Rows[i][2].ToString());
        obj.transform.localEulerAngles = ParseStoV3(dc.Tables[0].Rows[i][3].ToString());
        obj.transform.localScale = ParseStoV3(dc.Tables[0].Rows[i][4].ToString());
    }

    private static bool[] SetMonitorShowHide(DataSet dc, int i)
    {
        bool[] boolGroup = new bool[3];
        string vector3 = dc.Tables[0].Rows[i][5].ToString();//.Replace("(", "").Replace(")", "");
        string[] s = vector3.Split(',');
        boolGroup[0] = Convert.ToBoolean(s[0]);
        boolGroup[1] = Convert.ToBoolean(s[1]);
        boolGroup[2] = Convert.ToBoolean(s[2]);
        return boolGroup;
    }
    //private DataSet sqlquery(string sql)
    //{

    //    SqlConnection sqlCon = new SqlConnection(sqlAddress);
    //    SqlCommand cmd = new SqlCommand();
    //    //cmd.Connection = sqlCon;
    //    SqlDataAdapter DC = new SqlDataAdapter(sql, sqlCon);
    //    System.Data.DataSet dc = new System.Data.DataSet();
    //    DC.Fill(dc, "table");
    //    return dc;
    //}

    private static Vector3 ParseStoV3(string vector3)
    {
        vector3 = vector3.Replace("(", "").Replace(")", "");
        string[] s = vector3.Split(',');
        return new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
    }



    void GetWorkAreaInfor()
    {
        //数据库初始化
        //SqlConnection sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        //下拉列表初始化
        WorkAreaDropdown.ClearOptions();
        string sql = "select * from AllWorkAreaTable";
        //System.Data.DataSet dc = sqlquery(sql);
        SqliteAssist sqliteAssist = new SqliteAssist();
        DataSet dc = sqliteAssist.GetQueryResultByDataSet(sql);
        List<TMP_Dropdown.OptionData> listOptions = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string dataposition = dc.Tables[0].Rows[i][0].ToString().Trim();
            listOptions.Add(new TMP_Dropdown.OptionData(dataposition));

        }
        WorkAreaDropdown.AddOptions(listOptions);
        ShowMessage();
        sqliteAssist.Close();
    }

    public void ShowMessage()
    {
        int index = WorkAreaDropdown.value;
        string currentName = WorkAreaDropdown.options[index].text;
        //SqlConnection sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        string sql = "select * from AllWorkAreaTable";
        //System.Data.DataSet dc = sqlquery(sql);
        SqliteAssist sqliteAssist = new SqliteAssist();
        DataSet dc = sqliteAssist.GetQueryResultByDataSet(sql);
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string existsName = dc.Tables[0].Rows[i][0].ToString().Trim();
            if (existsName == "")
            {
                return;
            }
            if (existsName.Equals(currentName))
            {
                LeghthText.text = dc.Tables[0].Rows[i][4].ToString();
                WidthText.text = dc.Tables[0].Rows[i][5].ToString();
                CreatedTimeText.text = dc.Tables[0].Rows[i][1].ToString();
                ModifiedTimeText.text = dc.Tables[0].Rows[i][2].ToString();
                OperatorText.text = dc.Tables[0].Rows[i][3].ToString();
                length = float.Parse(dc.Tables[0].Rows[i][4].ToString());
                width = float.Parse(dc.Tables[0].Rows[i][5].ToString());

                if (WorkAreaDropdown.options.Count != 0 && !SavingSystem.CurrentWorkAreaName.Equals(WorkAreaDropdown.options[WorkAreaDropdown.value].text))
                {
                    SavingSystem.CurrentWorkAreaName = currentName;
                }
                else
                {
                    SavingSystem.CurrentWorkAreaName = "";
                }
                //sqlCon.Close();
                sqliteAssist.Close();
                return;
            }
        }
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

    //判断字符串是否为空
    bool StrIsNull(string str)
    {
        if (str.Equals(null) || str == "")
        {
            return true;
        }

        return false;
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

    void Init()
    {
        SavingSystem.CurrentWorkAreaName = "PDF样例厂房";//TEST厂房
        //SavingSystem.CurrentWorkAreaName = "TEST厂房";
        LoadAllObject();
    }
}
