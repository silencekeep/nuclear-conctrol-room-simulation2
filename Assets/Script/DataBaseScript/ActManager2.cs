using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System;
using TMPro;

public class ActManager2 : MonoBehaviour
{
    //public GameObject LoginPanel;
    public GameObject ChoosePanel;
    public GameObject ActDataBase;
    public GameObject ActCombDB;
    public GameObject Row_Prefab, Row_Prefab2;
    public GameObject editpanel;
    public GameObject editokpanel;
    public GameObject addpanel;
    public GameObject addokpanel;
    //public GameObject exportokpanel;
    public GameObject importpanel;
    public GameObject importokpanel;
    public GameObject table1, table2;
    public GameObject querypanel, querypanel2;
    public Dropdown QTypeDropdown;
    public Dropdown QType1Dropdown;
    public Dropdown QType2Dropdown;
    public static bool editpanelsign;
    public static bool editshowsign;
    public static bool AddActPathSign;
    // public static bool EditActPathSign;
    public InputField nameIputField_Standing, height_StandingInputField, seegle_StandingInputField, shoulder_StandingInputField, fingertips_StandingInputField,
                          stretch_StandingInputField, axisDistance_StandingInputField, centralAxisToEye_StandingInputField, eyeToSide_StandingInputField, shoulderToSide_StandingInputField,
         nameIputField_Sitting, belowTheKnee_SittingInputField, heightAboveChair_SittingInputField, seegleAboveChair_SittingInputField,
                      shoulderAboveChair_SittingInputField, stretch_SittingInputField, thigh_SittingInputField, hipToKneeMesial_SittingInputField, kneeHeight_SittingInputField,
                      axisDistance_SittingInputField, centralAxisToEye_SittingInputField, seegleHeight_SittingInputField, shoulderHeight_SittingInputField,
                      hipToKnee_SittingInputField, foot_SittingInputField, bodyToToes_SittingInputField, eyeToSide_SittingInputField, shoulderToSide_SittingInputField;
    public GameObject Standing, Sitting;
    public Dropdown posSelect;

    public static bool typechangesign;
    public static bool type1changesign;
    //public GameObject Editpanel;



    //private SqlConnection sqlCon;
    public static int querysign;
    //private string sqlAddress = "server=127.0.0.1;database=NuclearControlRoomItemDB;uid=sa;pwd=123456";

    // Start is called before the first frame update
    void Start()
    {
        editpanelsign = false;
        editshowsign = true;
        table1 = GameObject.Find("Canvas/CaiBiaoPanel/MainPanel/Viewport/ActTable");
        //table2 = GameObject.Find("Canvas/ActDatabase/MainPanel2/Viewport/ActTable");
    }

    //public void ActDBtoLogin()
    //{
    //    ActDataBase.SetActive(false);
    //    LoginPanel.SetActive(true);
    //}

    //public void ActCombDBtoLogin()
    //{
    //    ActCombDB.SetActive(false);
    //    LoginPanel.SetActive(true);
    //}


    public void ActPanelClick()//选择动素管理模块
    {
        ChoosePanel.SetActive(false);
        ActDataBase.SetActive(true);

    }
    public void ActCombDBClick()//选择动素组合库管理模块
    {
        ChoosePanel.SetActive(false);
        ActCombDB.SetActive(true);
    }


    public void TypeChanged()//动素种类点击触发事件
    {
        typechangesign = true;
    }
    public void Type1Changed()//动作种类点击触发事件    项目条款点击触发事件
    {
        type1changesign = true;
    }


    public void EditPanelShow()
    {
        //EditActPathSign = true;
        editpanelsign = true;
    }
    public void EditPanelClose()
    {
        editpanelsign = false;
    }

    public void AddPanelShow()
    {
        addpanel.SetActive(true);
        AddActPathSign = true;
    }
    public void AddPanelClose()
    {
        addpanel.SetActive(false);
    }
    public void ConditionalQuery()
    {
        querypanel.SetActive(true);

    }

    public void ConditionalQuery2()
    {
        querypanel2.SetActive(true);

    }


    public void ConditionalQueryClose()
    {
        querypanel.SetActive(false);

    }
    public void ConditionalQueryClose2()
    {
        querypanel2.SetActive(false);
       // querypanel.SetActive(false);

    }

    public void QYesButtonClicked()//条件查询按钮click事件
    {
        querysign = 3;//若当前为条件查询界面则querysign = 3

        //if (posSelect.value == 0)
        //{
            int count = table1.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Destroy(table1.transform.GetChild(i).gameObject);// 删除所有“行预制体”
                // table.transform.GetChild(i).gameObject.SetActive(false);
            }
            ConditionalQueryTable1();

        //}
        //else
        //{
        //    if (posSelect.value == 1)
        //    {
        //        int count = table2.transform.childCount;
        //        for (int i = 0; i < count; i++)
        //        {
        //            Destroy(table2.transform.GetChild(i).gameObject);//删除所有“行预制体”
        //            // table.transform.GetChild(i).gameObject.SetActive(false);
        //        }
        //        ConditionalQueryTable2();
        //    }
        //}

        //UpdateTable();
        querypanel.SetActive(false);
    }

    public void QYesButtonClicked2()//条件查询按钮click事件
    {
        querysign = 3;//若当前为条件查询界面则querysign = 3

        //if (posSelect.value == 0)
        //{
        int count = table1.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(table1.transform.GetChild(i).gameObject);// 删除所有“行预制体”
                                                             // table.transform.GetChild(i).gameObject.SetActive(false);
        }
        ConditionalQueryTable2();

        //}
        //else
        //{
        //    if (posSelect.value == 1)
        //    {
        //        int count = table2.transform.childCount;
        //        for (int i = 0; i < count; i++)
        //        {
        //            Destroy(table2.transform.GetChild(i).gameObject);//删除所有“行预制体”
        //            // table.transform.GetChild(i).gameObject.SetActive(false);
        //        }
        //        ConditionalQueryTable2();
        //    }
        //}

        //UpdateTable();
        querypanel2.SetActive(false);
        querypanel.SetActive(false);
    }


    public void ConditionalQueryTable1()//按标准种类查询   显示动素条件查询结果（种类）    
    {
        //int count = table1.transform.childCount;
        //for (int i = 0; i < count; i++)
        //{
        //    Destroy(table1.transform.GetChild(i).gameObject);
        //    // table.transform.GetChild(i).gameObject.SetActive(false);
        //}

        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        SqlCommand cmd = new SqlCommand();
        //cmd.Connection = sqlCon;
        //string Qtype = QTypeDropdown.captionText.text;
        string Qtype1 = QType1Dropdown.captionText.text;
        //SqlDataAdapter DC;
        //if (QTypeDropdown.captionText.text != "不限")
        //{
        //    DC = new SqlDataAdapter("select * from actions where ActType='" + Qtype + "'", sqlCon);            
        //}
        //else
        //{
        //    DC = new SqlDataAdapter("select * from actions where ActGenType='" + Qtype1 + "'", sqlCon);
        //}


        System.Data.DataSet dc = sqlconnect.Instance.sqlquery("select * from standard where standardname='" + Qtype1 + "'");
        //DC = new SqlDataAdapter("select * from zhanzi where percentage='" + Qtype1 + "'", sqlCon);

        //System.Data.DataSet dc = new System.Data.DataSet();
        //DC.Fill(dc);

        string[] standard_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        string[] item_arr = new string[dc.Tables[0].Rows.Count];
        string[] type_arr = new string[dc.Tables[0].Rows.Count];
        string[] type1_arr = new string[dc.Tables[0].Rows.Count];
        string[] content_arr = new string[dc.Tables[0].Rows.Count];
        string[] upper_arr = new string[dc.Tables[0].Rows.Count];
        string[] lower_arr = new string[dc.Tables[0].Rows.Count];
        


        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的数据放进数组中
        {
            string standard = Convert.ToString(dc.Tables[0].Rows[i][0]);//将数据表中的动素序号ActID数据放进数组中
                                                                        //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
                                                                        //dc.Tables[0].Rows[i][0])表示取数据表中第一列所有数据
            if (standard != null)
                standard_arr[i] = standard;
            string item = Convert.ToString(dc.Tables[0].Rows[i][1]);//将数据表中的动素类别ActType数据放进数组中
            if (item != null)
                item_arr[i] = item;
            string type = Convert.ToString(dc.Tables[0].Rows[i][2]);//将数据表中的动作类别ActGenType数据放进数组中
            if (type != null)
                type_arr[i] = type;
            string type1 = Convert.ToString(dc.Tables[0].Rows[i][3]);//将数据表中的动作类别ActGenType数据放进数组中
            if (type1 != null)
                type1_arr[i] = type1;
            string content = Convert.ToString(dc.Tables[0].Rows[i][4]);
            if (content != null)
                content_arr[i] = content;
            string upper = Convert.ToString(dc.Tables[0].Rows[i][5]);
            if (upper != null)
                upper_arr[i] = upper;
            string lower = Convert.ToString(dc.Tables[0].Rows[i][6]);
            if (lower != null)
                lower_arr[i] = lower;
            


        }

        GameObject row0 = Instantiate(Row_Prefab, table1.transform.position, table1.transform.rotation) as GameObject;
        row0.name = "row0";
        row0.transform.SetParent(table1.transform);
        row0.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
                                                //设置预设实例中的各个子物体的文本内容
        row0.transform.Find("Toggle").gameObject.SetActive(false);
        row0.transform.Find("kong").gameObject.SetActive(true);
        row0.transform.Find("standard").GetComponent<Text>().text = "标准";
        row0.transform.Find("item").GetComponent<Text>().text = "条目";
        row0.transform.Find("type").GetComponent<Text>().text = "盘台类别";
        row0.transform.Find("type1").GetComponent<Text>().text = "评估类别";
        row0.transform.Find("content").GetComponent<Text>().text = "内容";
        GameObject upperLabel = row0.transform.Find("upperLabel").gameObject;
        upperLabel.SetActive(true);
        upperLabel.GetComponent<Text>().text = "上限";
        row0.transform.Find("upper").gameObject.SetActive(false);
        GameObject lowerLabel = row0.transform.Find("lowerLabel").gameObject;
        lowerLabel.SetActive(true);
        lowerLabel.GetComponent<Text>().text = "下限";
        row0.transform.Find("lower").gameObject.SetActive(false);



        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)//添加并修改预设的过程，将创建10行
        {

            //在Table下创建新的预设实例
            GameObject table1 = GameObject.Find("Canvas/CaiBiaoPanel/MainPanel/Viewport/ActTable");
            GameObject row = Instantiate(Row_Prefab, table1.transform.position, table1.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table1.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("standard").GetComponent<Text>().text = standard_arr[i];
            row.transform.Find("item").GetComponent<Text>().text = item_arr[i];
            row.transform.Find("type").GetComponent<Text>().text = type_arr[i];
            row.transform.Find("type1").GetComponent<Text>().text = type1_arr[i];
            row.transform.Find("content").GetComponent<Text>().text = content_arr[i];
            row.transform.Find("upper").GetComponent<TMP_InputField>().text = upper_arr[i];
            row.transform.Find("lower").GetComponent<TMP_InputField>().text = lower_arr[i];
           
            

        }
        //sqlCon.Close();
    }

    public void ConditionalQueryTable2()//按条目查询   显示动素条件查询结果（种类）    
    {
        //int count = table1.transform.childCount;
        //for (int i = 0; i < count; i++)
        //{
        //    Destroy(table1.transform.GetChild(i).gameObject);
        //    // table.transform.GetChild(i).gameObject.SetActive(false);
        //}




        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        SqlCommand cmd = new SqlCommand();
        //cmd.Connection = sqlCon;
        string Qtype = QType1Dropdown.captionText.text;
        string Qtype1 = QType2Dropdown.captionText.text;
        //SqlDataAdapter DC;
        //if (QTypeDropdown.captionText.text != "不限")
        //{
        //    DC = new SqlDataAdapter("select * from actions where ActType='" + Qtype + "'", sqlCon);            
        //}
        // else if (QTypeDropdown2.captionText.text != "不限")
        // {
        //  DC = new SqlDataAdapter("select * from actions where ActType='" + Qtype + "'", sqlCon); 
        //}
        //else
        //{
        //    DC = new SqlDataAdapter("select * from actions where ActGenType='" + Qtype1 + "'", sqlCon);
        //}

        //更改
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery("select * from standard where type='" + Qtype1 + "'AND standardname='" + Qtype + "'");

        //DC = new SqlDataAdapter("select * from zuozi where percentage='" + Qtype1 + "'", sqlCon);

        //System.Data.DataSet dc = new System.Data.DataSet();
        //DC.Fill(dc);
        string[] standard_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        string[] item_arr = new string[dc.Tables[0].Rows.Count];
        string[] type_arr = new string[dc.Tables[0].Rows.Count];
        string[] type1_arr = new string[dc.Tables[0].Rows.Count];
        string[] content_arr = new string[dc.Tables[0].Rows.Count];
        string[] upper_arr = new string[dc.Tables[0].Rows.Count];
        string[] lower_arr = new string[dc.Tables[0].Rows.Count];

        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的数据放进数组中
        {
            string standard = Convert.ToString(dc.Tables[0].Rows[i][0]);//将数据表中的动素序号ActID数据放进数组中
                                                                        //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
                                                                        //dc.Tables[0].Rows[i][0])表示取数据表中第一列所有数据
            if (standard != null)
                standard_arr[i] = standard;
            string item = Convert.ToString(dc.Tables[0].Rows[i][1]);//将数据表中的动素类别ActType数据放进数组中
            if (item != null)
                item_arr[i] = item;
            string type = Convert.ToString(dc.Tables[0].Rows[i][2]);//将数据表中的动作类别ActGenType数据放进数组中
            if (type != null)
                type_arr[i] = type;
            string type1 = Convert.ToString(dc.Tables[0].Rows[i][3]);//将数据表中的动作类别ActGenType数据放进数组中
            if (type1 != null)
                type1_arr[i] = type1;
            string content = Convert.ToString(dc.Tables[0].Rows[i][4]);
            if (content != null)
                content_arr[i] = content;
            string upper = Convert.ToString(dc.Tables[0].Rows[i][5]);
            if (upper != null)
                upper_arr[i] = upper;
            string lower = Convert.ToString(dc.Tables[0].Rows[i][6]);
            if (lower != null)
                lower_arr[i] = lower;

        }

        GameObject row0 = Instantiate(Row_Prefab, table1.transform.position, table1.transform.rotation) as GameObject;
        row0.name = "row0";
        row0.transform.SetParent(table1.transform);
        row0.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
                                                //设置预设实例中的各个子物体的文本内容
        row0.transform.Find("Toggle").gameObject.SetActive(false);
        row0.transform.Find("kong").gameObject.SetActive(true);
        row0.transform.Find("standard").GetComponent<Text>().text = "标准";
        row0.transform.Find("item").GetComponent<Text>().text = "条目";
        row0.transform.Find("type").GetComponent<Text>().text = "盘台类别";
        row0.transform.Find("type1").GetComponent<Text>().text = "评估类别";
        row0.transform.Find("content").GetComponent<Text>().text = "内容";
        GameObject upperLabel = row0.transform.Find("upperLabel").gameObject;
        upperLabel.SetActive(true);
        upperLabel.GetComponent<Text>().text = "上限";
        row0.transform.Find("upper").gameObject.SetActive(false);
        GameObject lowerLabel = row0.transform.Find("lowerLabel").gameObject;
        lowerLabel.SetActive(true);
        lowerLabel.GetComponent<Text>().text = "下限";
        row0.transform.Find("lower").gameObject.SetActive(false);


        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)//添加并修改预设的过程，将创建10行
        {

            //在Table下创建新的预设实例
            GameObject table1 = GameObject.Find("Canvas/CaiBiaoPanel/MainPanel/Viewport/ActTable");
            GameObject row = Instantiate(Row_Prefab, table1.transform.position, table1.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table1.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("standard").GetComponent<Text>().text = standard_arr[i];
            row.transform.Find("item").GetComponent<Text>().text = item_arr[i];
            row.transform.Find("type").GetComponent<Text>().text = type_arr[i];
            row.transform.Find("type1").GetComponent<Text>().text = type1_arr[i];
            row.transform.Find("content").GetComponent<Text>().text = content_arr[i];
            row.transform.Find("upper").GetComponent<TMP_InputField>().text = upper_arr[i];
            row.transform.Find("lower").GetComponent<TMP_InputField>().text = lower_arr[i];
        }

    }


    //public void AddYesButtonClicked()//数据库新增功能
    //{
    //    AddActPathSign = false;
    //    sqlCon = new SqlConnection(sqlAddress);
    //    sqlCon.Open();
    //    string ActInsert = "insert into actions(ActBM,ActType,ActName,ActTime,ActDescription,ActPath,ActGenType) values ('" + AddIDInputField.text + "','" + AddTypeInputField.text + "','" + AddNameInputField.text + "','" + AddTimeInputField.text + "','" + AddDescriptionInputField.text + "','" + AddPathInputField.text + "','" + AddType1InputField.text + "')";        
    //    SqlCommand sqlCommand = new SqlCommand(ActInsert, sqlCon);        
    //    sqlCommand.ExecuteNonQuery();
    //    sqlCon.Close();
    //    addokpanel.SetActive(true);

    //}

    public void AddOkYesButtonClicked()
    {
        addokpanel.SetActive(false);
        addpanel.SetActive(false);
        //UpdateTable(); //注释
    }

    public void ImportPanelShow()
    {
        importpanel.SetActive(true);
    }

    public void ImportPanelClose()
    {
        importpanel.SetActive(false);
    }

    public void ImportOkYesButtonClicked()
    {
        importokpanel.SetActive(false);
        importpanel.SetActive(false);
    }

    //private void UpdateTable()
    //{

    //    if (querysign == 1)//当前在全部查询界面
    //    {
    //        AllQuery1();
    //    }
    //    else if (querysign == 2)//当前在全部查询界面
    //    {
    //        AllQuery2();
    //    }
    //    else if (querysign == 3)//当前在条件查询界面
    //    {
    //        ConditionalQueryTable();
    //    }        
    //}


    //public void ExportOkYesButtonClicked()//数据库导出功能
    //{
    //    exportokpanel.SetActive(false);       
    //}

    public void EditYesButtonClicked1()//数据库编辑功能  站姿
    {
        //EditActPathSign = false;
        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        string ActEdit = "update zhanzi set height='" + height_StandingInputField.text + "', seegle='" + seegle_StandingInputField.text + "', shoulder='" + shoulder_StandingInputField.text + "', fingertips = '" + fingertips_StandingInputField.text + "', stretch = '" + stretch_StandingInputField.text + "', axisDistance='" + axisDistance_StandingInputField.text + "' , centralAxisToEye = '" + centralAxisToEye_StandingInputField.text + "' , eyeToSide='" + eyeToSide_StandingInputField.text + "', shoulderToSide='" + shoulderToSide_StandingInputField.text + "'  where percentage = '" + nameIputField_Standing.text + "'";
        sqlconnect.Instance.sqladd(ActEdit);
        //SqlCommand sqlCommand = new SqlCommand(ActEdit, sqlCon);
        //sqlCommand.ExecuteNonQuery();
        //sqlCon.Close();
        editokpanel.SetActive(true);
    }

    public void EditYesButtonClicked2()//数据库编辑功能  坐姿
    {
        // EditActPathSign = false;
        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        string ActEdit = "update zuozi set belowTheKnee='" + belowTheKnee_SittingInputField.text + "', heightAboveChair='" + heightAboveChair_SittingInputField.text + "', seegleAboveChair='" + seegleAboveChair_SittingInputField.text + "', shoulderAboveChair = '" + shoulderAboveChair_SittingInputField.text + "', stretch = '" + stretch_SittingInputField.text + "', thigh='" + thigh_SittingInputField.text + "', hipToKneeMesial = '" + hipToKneeMesial_SittingInputField.text +
            "', kneeHeight = '" + kneeHeight_SittingInputField.text + "', axisDistance = '" + axisDistance_SittingInputField.text + "', centralAxisToEye = '" + centralAxisToEye_SittingInputField.text + "', seegleHeight = '" + seegleHeight_SittingInputField.text + "', shoulderHeight = '" + shoulderHeight_SittingInputField.text + "', hipToKnee = '" + hipToKnee_SittingInputField.text + "',  foot = '" + foot_SittingInputField.text +
           "', bodyToToes = '" + bodyToToes_SittingInputField.text + "', eyeToSide = '" + eyeToSide_SittingInputField.text + "', shoulderToSide = '" + shoulderToSide_SittingInputField.text + "'  where percentage = '" + nameIputField_Sitting.text + "'";
        sqlconnect.Instance.sqladd(ActEdit);
        //SqlCommand sqlCommand = new SqlCommand(ActEdit, sqlCon);
        //sqlCommand.ExecuteNonQuery();
        //sqlCon.Close();
        editokpanel.SetActive(true);
    }

    public void EditYesButtonClicked()
    {
        if (posSelect.value == 0)
        {
            EditYesButtonClicked1();
        }
        else
        {
            if (posSelect.value == 1)
            {
                EditYesButtonClicked2();
            }
        }
    }

    public void EditOkYesButtonClicked()
    {
        editokpanel.SetActive(false);
        editpanelsign = false;
        //UpdateTable();
        if (posSelect.value == 0)
        {
            int count = table1.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                Destroy(table1.transform.GetChild(i).gameObject);
                // table.transform.GetChild(i).gameObject.SetActive(false);
            }
            AllQuery1();

        }
        else
        {
            if (posSelect.value == 1)
            {
                int count = table2.transform.childCount;
                for (int i = 0; i < count; i++)
                {
                    Destroy(table2.transform.GetChild(i).gameObject);
                    // table.transform.GetChild(i).gameObject.SetActive(false);
                }
                AllQuery2();
            }
        }
    }




    void EditShow1()
    {
        editshowsign = false;
        height_StandingInputField.GetComponent<InputField>().text = RowScripts.height_StandingText;
        seegle_StandingInputField.GetComponent<InputField>().text = RowScripts.seegle_StandingText;
        shoulder_StandingInputField.GetComponent<InputField>().text = RowScripts.shoulder_StandingText;
        fingertips_StandingInputField.GetComponent<InputField>().text = RowScripts.fingertips_StandingText;
        stretch_StandingInputField.GetComponent<InputField>().text = RowScripts.stretch_StandingText;
        axisDistance_StandingInputField.GetComponent<InputField>().text = RowScripts.axisDistance_StandingText;
        centralAxisToEye_StandingInputField.GetComponent<InputField>().text = RowScripts.centralAxisToEye_StandingText;
        eyeToSide_StandingInputField.GetComponent<InputField>().text = RowScripts.eyeToSide_StandingText;
        shoulderToSide_StandingInputField.GetComponent<InputField>().text = RowScripts.shoulderToSide_StandingText;
        nameIputField_Standing.GetComponent<InputField>().text = RowScripts.percentage_StandingText;
    }

    void EditShow2()
    {
        editshowsign = false;
        belowTheKnee_SittingInputField.GetComponent<InputField>().text = RowScripts.belowTheKnee_SittingText;
        heightAboveChair_SittingInputField.GetComponent<InputField>().text = RowScripts.heightAboveChair_SittingText;
        seegleAboveChair_SittingInputField.GetComponent<InputField>().text = RowScripts.seegleAboveChair_SittingText;
        shoulderAboveChair_SittingInputField.GetComponent<InputField>().text = RowScripts.shoulderAboveChair_SittingText;
        stretch_SittingInputField.GetComponent<InputField>().text = RowScripts.stretch_SittingText;
        thigh_SittingInputField.GetComponent<InputField>().text = RowScripts.thigh_SittingText;
        hipToKneeMesial_SittingInputField.GetComponent<InputField>().text = RowScripts.hipToKneeMesial_SittingText;
        kneeHeight_SittingInputField.GetComponent<InputField>().text = RowScripts.kneeHeight_SittingText;
        axisDistance_SittingInputField.GetComponent<InputField>().text = RowScripts.axisDistance_SittingText;
        centralAxisToEye_SittingInputField.GetComponent<InputField>().text = RowScripts.centralAxisToEye_SittingText;
        seegleHeight_SittingInputField.GetComponent<InputField>().text = RowScripts.seegleHeight_SittingText;
        shoulderHeight_SittingInputField.GetComponent<InputField>().text = RowScripts.shoulderHeight_SittingText;
        hipToKnee_SittingInputField.GetComponent<InputField>().text = RowScripts.hipToKnee_SittingText;
        foot_SittingInputField.GetComponent<InputField>().text = RowScripts.foot_SittingText;
        bodyToToes_SittingInputField.GetComponent<InputField>().text = RowScripts.bodyToToes_SittingText;
        eyeToSide_SittingInputField.GetComponent<InputField>().text = RowScripts.eyeToSide_SittingText;
        shoulderToSide_SittingInputField.GetComponent<InputField>().text = RowScripts.shoulderToSide_SittingText;
        nameIputField_Sitting.GetComponent<InputField>().text = RowScripts.percentage_SittingText;

    }



    void AllQuery1()//显示全部查询内容  站姿
    {
        querysign = 1;

        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        SqlCommand cmd = new SqlCommand();
        //cmd.Connection = sqlCon;
        //SqlDataAdapter DC = new SqlDataAdapter("select * from zhanzi ", sqlCon);

        System.Data.DataSet dc = sqlconnect.Instance.sqlquery("select * from zhanzi ");

        //System.Data.DataSet dc = new System.Data.DataSet();
        //DC.Fill(dc);
        string[] height_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        string[] seegle_arr = new string[dc.Tables[0].Rows.Count];
        string[] shoulder_arr = new string[dc.Tables[0].Rows.Count];
        string[] fingertips_arr = new string[dc.Tables[0].Rows.Count];
        string[] stretch_arr = new string[dc.Tables[0].Rows.Count];
        string[] axisDistance_arr = new string[dc.Tables[0].Rows.Count];
        string[] centralAxisToEye_arr = new string[dc.Tables[0].Rows.Count];
        string[] eyeToSide_arr = new string[dc.Tables[0].Rows.Count];
        string[] shoulderToSide_arr = new string[dc.Tables[0].Rows.Count];
        string[] percentage_arr = new string[dc.Tables[0].Rows.Count];


        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的数据放进数组中
        {
            string height = Convert.ToString(dc.Tables[0].Rows[i][0]);//将数据表中的动素序号ActID数据放进数组中
                                                                      //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
                                                                      //dc.Tables[0].Rows[i][0])表示取数据表中第一列所有数据
            if (height != null)
                height_arr[i] = height;
            string seegle = Convert.ToString(dc.Tables[0].Rows[i][1]);//将数据表中的动素类别ActType数据放进数组中
            if (seegle != null)
                seegle_arr[i] = seegle;
            string shoulder = Convert.ToString(dc.Tables[0].Rows[i][2]);//将数据表中的动作类别ActGenType数据放进数组中
            if (shoulder != null)
                shoulder_arr[i] = shoulder;
            string fingertips = Convert.ToString(dc.Tables[0].Rows[i][3]);
            if (fingertips != null)
                fingertips_arr[i] = fingertips;
            string stretch = Convert.ToString(dc.Tables[0].Rows[i][4]);
            if (stretch != null)
                stretch_arr[i] = stretch;
            string axisDistance = Convert.ToString(dc.Tables[0].Rows[i][5]);
            if (axisDistance != null)
                axisDistance_arr[i] = axisDistance;
            string centralAxisToEye = Convert.ToString(dc.Tables[0].Rows[i][6]);
            if (centralAxisToEye != null)
                centralAxisToEye_arr[i] = centralAxisToEye;
            string eyeToSide = Convert.ToString(dc.Tables[0].Rows[i][7]);
            if (eyeToSide != null)
                eyeToSide_arr[i] = eyeToSide;
            string shoulderToSide = Convert.ToString(dc.Tables[0].Rows[i][8]);
            if (shoulderToSide != null)
                shoulderToSide_arr[i] = shoulderToSide;
            string percentage = Convert.ToString(dc.Tables[0].Rows[i][9]);
            if (percentage != null)
                percentage_arr[i] = percentage;

        }


        GameObject row0 = Instantiate(Row_Prefab, table1.transform.position, table1.transform.rotation) as GameObject;
        row0.name = "row0";
        row0.transform.SetParent(table1.transform);
        row0.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
                                                //设置预设实例中的各个子物体的文本内容
        row0.transform.Find("height_Standing").GetComponent<Text>().text = "身高";
        row0.transform.Find("seegle_Standing").GetComponent<Text>().text = "眼至地面高";
        row0.transform.Find("shoulder_Standing").GetComponent<Text>().text = "肩高";
        row0.transform.Find("fingertips_Standing").GetComponent<Text>().text = "指尖至地面高";
        row0.transform.Find("stretch_Standing").GetComponent<Text>().text = "功能伸展范围";
        row0.transform.Find("axisDistance_Standing").GetComponent<Text>().text = "人体轴线至台边距离";
        row0.transform.Find("centralAxisToEye_Standing").GetComponent<Text>().text = "人体中心轴至眼距离";
        row0.transform.Find("eyeToSide_Standing").GetComponent<Text>().text = "眼至盘台前沿";
        row0.transform.Find("shoulderToSide_Standing").GetComponent<Text>().text = "肩至盘台前沿";
        row0.transform.Find("percentage_Standing").GetComponent<Text>().text = "项目适用条款";
        row0.transform.Find("ActEdit").GetComponent<Text>().text = "操作";
        row0.transform.Find("ActDelete").GetComponent<Text>().text = null;


        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)//添加并修改预设的过程，将创建10行
        {

            //在Table下创建新的预设实例
            GameObject table1 = GameObject.Find("Canvas/ActDatabase/MainPanel/Viewport/ActTable");
            GameObject row = Instantiate(Row_Prefab, table1.transform.position, table1.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table1.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("height_Standing").GetComponent<Text>().text = height_arr[i];
            row.transform.Find("seegle_Standing").GetComponent<Text>().text = seegle_arr[i];
            row.transform.Find("shoulder_Standing").GetComponent<Text>().text = shoulder_arr[i];
            row.transform.Find("fingertips_Standing").GetComponent<Text>().text = fingertips_arr[i];
            row.transform.Find("stretch_Standing").GetComponent<Text>().text = stretch_arr[i];
            row.transform.Find("axisDistance_Standing").GetComponent<Text>().text = axisDistance_arr[i];
            row.transform.Find("centralAxisToEye_Standing").GetComponent<Text>().text = centralAxisToEye_arr[i];
            row.transform.Find("eyeToSide_Standing").GetComponent<Text>().text = eyeToSide_arr[i];
            row.transform.Find("shoulderToSide_Standing").GetComponent<Text>().text = shoulderToSide_arr[i];
            row.transform.Find("percentage_Standing").GetComponent<Text>().text = percentage_arr[i];

        }
    }
    void AllQuery2()//显示全部查询内容  坐姿
    {
        querysign = 2;


        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        SqlCommand cmd = new SqlCommand();
        //cmd.Connection = sqlCon;
        //SqlDataAdapter DC = new SqlDataAdapter("select * from zuozi ", sqlCon);
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery("select * from zuozi ");

        //System.Data.DataSet dc = new System.Data.DataSet();
        //DC.Fill(dc);
        string[] belowTheKnee_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        string[] heightAboveChair_arr = new string[dc.Tables[0].Rows.Count];
        string[] seegleAboveChair_arr = new string[dc.Tables[0].Rows.Count];
        string[] shoulderAboveChair_arr = new string[dc.Tables[0].Rows.Count];
        string[] stretch_arr = new string[dc.Tables[0].Rows.Count];
        string[] thigh_arr = new string[dc.Tables[0].Rows.Count];
        string[] hipToKneeMesial_arr = new string[dc.Tables[0].Rows.Count];
        string[] kneeHeight_arr = new string[dc.Tables[0].Rows.Count];
        string[] axisDistance_arr = new string[dc.Tables[0].Rows.Count];
        string[] centralAxisToEye_arr = new string[dc.Tables[0].Rows.Count];
        string[] seegleHeight_arr = new string[dc.Tables[0].Rows.Count];
        string[] shoulderHeight_arr = new string[dc.Tables[0].Rows.Count];
        string[] hipToKnee_arr = new string[dc.Tables[0].Rows.Count];
        string[] foot_arr = new string[dc.Tables[0].Rows.Count];
        string[] bodyToToes_arr = new string[dc.Tables[0].Rows.Count];
        string[] eyeToSide_arr = new string[dc.Tables[0].Rows.Count];
        string[] shoulderToSide_arr = new string[dc.Tables[0].Rows.Count];
        string[] percentage_arr = new string[dc.Tables[0].Rows.Count];


        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的数据放进数组中
        {
            string belowTheKnee = Convert.ToString(dc.Tables[0].Rows[i][0]);//将数据表中的动素序号ActID数据放进数组中
                                                                            //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
                                                                            //dc.Tables[0].Rows[i][0])表示取数据表中第一列所有数据
            if (belowTheKnee != null)
                belowTheKnee_arr[i] = belowTheKnee;
            string heightAboveChair = Convert.ToString(dc.Tables[0].Rows[i][1]);//将数据表中的动素类别ActType数据放进数组中
            if (heightAboveChair != null)
                heightAboveChair_arr[i] = heightAboveChair;
            string seegleAboveChair = Convert.ToString(dc.Tables[0].Rows[i][2]);//将数据表中的动作类别ActGenType数据放进数组中
            if (seegleAboveChair != null)
                seegleAboveChair_arr[i] = seegleAboveChair;
            string shoulderAboveChair = Convert.ToString(dc.Tables[0].Rows[i][3]);
            if (shoulderAboveChair != null)
                shoulderAboveChair_arr[i] = shoulderAboveChair;
            string stretch = Convert.ToString(dc.Tables[0].Rows[i][4]);
            if (stretch != null)
                stretch_arr[i] = stretch;
            string thigh = Convert.ToString(dc.Tables[0].Rows[i][5]);
            if (thigh != null)
                thigh_arr[i] = thigh;
            string hipToKneeMesial = Convert.ToString(dc.Tables[0].Rows[i][6]);
            if (hipToKneeMesial != null)
                hipToKneeMesial_arr[i] = hipToKneeMesial;
            string kneeHeight = Convert.ToString(dc.Tables[0].Rows[i][7]);
            if (kneeHeight != null)
                kneeHeight_arr[i] = kneeHeight;
            string axisDistance = Convert.ToString(dc.Tables[0].Rows[i][8]);
            if (axisDistance != null)
                axisDistance_arr[i] = axisDistance;
            string centralAxisToEye = Convert.ToString(dc.Tables[0].Rows[i][9]);
            if (centralAxisToEye != null)
                centralAxisToEye_arr[i] = centralAxisToEye;
            string seegleHeight = Convert.ToString(dc.Tables[0].Rows[i][10]);
            if (seegleHeight != null)
                seegleHeight_arr[i] = seegleHeight;
            string shoulderHeight = Convert.ToString(dc.Tables[0].Rows[i][11]);
            if (shoulderHeight != null)
                shoulderHeight_arr[i] = shoulderHeight;
            string hipToKnee = Convert.ToString(dc.Tables[0].Rows[i][12]);
            if (hipToKnee != null)
                hipToKnee_arr[i] = hipToKnee;
            string foot = Convert.ToString(dc.Tables[0].Rows[i][13]);
            if (foot != null)
                foot_arr[i] = foot;
            string bodyToToes = Convert.ToString(dc.Tables[0].Rows[i][14]);
            if (bodyToToes != null)
                bodyToToes_arr[i] = bodyToToes;
            string eyeToSide = Convert.ToString(dc.Tables[0].Rows[i][15]);
            if (eyeToSide != null)
                eyeToSide_arr[i] = eyeToSide;
            string shoulderToSide = Convert.ToString(dc.Tables[0].Rows[i][16]);
            if (shoulderToSide != null)
                shoulderToSide_arr[i] = shoulderToSide;
            string percentage = Convert.ToString(dc.Tables[0].Rows[i][17]);
            if (percentage != null)
                percentage_arr[i] = percentage;

        }


        GameObject row0 = Instantiate(Row_Prefab2, table2.transform.position, table2.transform.rotation) as GameObject;
        row0.name = "row0";
        row0.transform.SetParent(table2.transform);
        row0.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
                                                //设置预设实例中的各个子物体的文本内容
        row0.transform.Find("belowTheKnee_Sitting").GetComponent<Text>().text = "腿部弯曲部位的高度";
        row0.transform.Find("heightAboveChair_Sitting").GetComponent<Text>().text = "椅面以上的身高";
        row0.transform.Find("seegleAboveChair_Sitting").GetComponent<Text>().text = "椅面以上的眼高";
        row0.transform.Find("shoulderAboveChair_Sitting").GetComponent<Text>().text = "椅面以上的肩高";
        row0.transform.Find("stretch_Sitting").GetComponent<Text>().text = "功能伸展范围";
        row0.transform.Find("thigh_Sitting").GetComponent<Text>().text = "大腿的净高度";
        row0.transform.Find("hipToKneeMesial_Sitting").GetComponent<Text>().text = "臀部至膝弯内侧的距离";
        row0.transform.Find("kneeHeight_Sitting").GetComponent<Text>().text = "膝的高度";
        row0.transform.Find("axisDistance_Sitting").GetComponent<Text>().text = "人体轴线至台边距离";
        row0.transform.Find("centralAxisToEye_Sitting").GetComponent<Text>().text = "人体中心轴至眼距离";
        row0.transform.Find("seegleHeight_Sitting").GetComponent<Text>().text = "坐姿眼高";
        row0.transform.Find("shoulderHeight_Sitting").GetComponent<Text>().text = "坐姿肩高";
        row0.transform.Find("hipToKnee_Sitting").GetComponent<Text>().text = "臀膝距";
        row0.transform.Find("foot_Sitting").GetComponent<Text>().text = "脚长";
        row0.transform.Find("bodyToToes_Sitting").GetComponent<Text>().text = "身体前端至脚尖距离";
        row0.transform.Find("eyeToSide_Sitting").GetComponent<Text>().text = "眼至盘台前沿";
        row0.transform.Find("shoulderToSide_Sitting").GetComponent<Text>().text = "肩至盘台前沿";
        row0.transform.Find("percentage_Sitting").GetComponent<Text>().text = "项目适用条款";
        row0.transform.Find("ActEdit").GetComponent<Text>().text = "操作";
        row0.transform.Find("ActDelete").GetComponent<Text>().text = null;


        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)//添加并修改预设的过程，将创建10行
        {

            //在Table下创建新的预设实例
            GameObject table2 = GameObject.Find("Canvas/ActDatabase/MainPanel2/Viewport/ActTable");
            GameObject row = Instantiate(Row_Prefab2, table2.transform.position, table2.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table2.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("belowTheKnee_Sitting").GetComponent<Text>().text = belowTheKnee_arr[i];
            row.transform.Find("heightAboveChair_Sitting").GetComponent<Text>().text = heightAboveChair_arr[i];
            row.transform.Find("seegleAboveChair_Sitting").GetComponent<Text>().text = seegleAboveChair_arr[i];
            row.transform.Find("shoulderAboveChair_Sitting").GetComponent<Text>().text = shoulderAboveChair_arr[i];
            row.transform.Find("stretch_Sitting").GetComponent<Text>().text = stretch_arr[i];
            row.transform.Find("thigh_Sitting").GetComponent<Text>().text = thigh_arr[i];
            row.transform.Find("hipToKneeMesial_Sitting").GetComponent<Text>().text = hipToKneeMesial_arr[i];
            row.transform.Find("kneeHeight_Sitting").GetComponent<Text>().text = kneeHeight_arr[i];
            row.transform.Find("axisDistance_Sitting").GetComponent<Text>().text = axisDistance_arr[i];
            row.transform.Find("centralAxisToEye_Sitting").GetComponent<Text>().text = centralAxisToEye_arr[i];
            row.transform.Find("seegleHeight_Sitting").GetComponent<Text>().text = seegleHeight_arr[i];
            row.transform.Find("shoulderHeight_Sitting").GetComponent<Text>().text = shoulderHeight_arr[i];
            row.transform.Find("hipToKnee_Sitting").GetComponent<Text>().text = hipToKnee_arr[i];
            row.transform.Find("foot_Sitting").GetComponent<Text>().text = foot_arr[i];
            row.transform.Find("bodyToToes_Sitting").GetComponent<Text>().text = bodyToToes_arr[i];
            row.transform.Find("eyeToSide_Sitting").GetComponent<Text>().text = eyeToSide_arr[i];
            row.transform.Find("shoulderToSide_Sitting").GetComponent<Text>().text = shoulderToSide_arr[i];
            row.transform.Find("percentage_Sitting").GetComponent<Text>().text = percentage_arr[i];


        }
    }

    void Update()
    {
        //if (editpanelsign == true)
        //{

        //    //if (posSelect.value == 0)
        //    //{
        //    //    editpanel.SetActive(true);
        //    //    Standing.SetActive(true);
        //    //    Sitting.SetActive(false);
        //    //}
        //    //else
        //    //{
        //    //    if (posSelect.value == 1)
        //    //    {
        //    //        editpanel.SetActive(true);
        //    //        Sitting.SetActive(true);
        //    //        Standing.SetActive(false);
        //    //    }
        //    //}
        //    if (editshowsign == true)
        //    {
        //        //if (posSelect.value == 0)
        //        //{
        //            EditShow1();
        //        //}
        //        //else
        //        //{
        //        //    if (posSelect.value == 1)
        //        //    {
        //        //        EditShow2();
        //        //    }
        //        //}

        //    }
        //}
        //else
        //{
        //    editpanel.SetActive(false);
        //}
    }


    //
    public void ActDatabaseClose()
    {
        ActDataBase.SetActive(false);
    }
}

