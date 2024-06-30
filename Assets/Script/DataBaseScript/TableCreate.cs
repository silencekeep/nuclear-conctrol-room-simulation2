using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Data.SqlClient;
using System;

public class TableCreate : MonoBehaviour
{
    public GameObject Row_Prefab, Row_Prefab2;//表头预设
    //private SqlConnection sqlCon;
    //public GameObject table;
    GameObject table1,table2;
    public GameObject MainPanel, MainPanel2;
    public Dropdown posSelect;


    //private string sqlAddress = "server=127.0.0.1;database=NuclearControlRoomItemDB;uid=sa;pwd=123456";
    void Start()
    {
        table1 = GameObject.Find("Canvas/ActDatabase/MainPanel/Viewport/ActTable");
        table2 = GameObject.Find("Canvas/ActDatabase/MainPanel2/Viewport/ActTable");
        AllDataTableShow();
        AllDataTableShow2();
    }

    public void selectMethod()
    {
        if (posSelect.value == 0)
        {
            MainPanel.SetActive(true);
            MainPanel2.SetActive(false);
            AllDataTableShow();
        }
        else
        {
            if (posSelect.value == 1)
            {
                MainPanel2.SetActive(true);
                MainPanel.SetActive(false);
                AllDataTableShow2();
            }
        }
    }



    public void AllDataTableShow()//站姿
    {
        ActManager.querysign = 1;
        int count = table1.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(table1.transform.GetChild(i).gameObject);
            // table.transform.GetChild(i).gameObject.SetActive(false);
        }

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

    public void AllDataTableShow2()//坐姿
    {
        ActManager.querysign = 2;
        int count = table2.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(table2.transform.GetChild(i).gameObject);
            // table.transform.GetChild(i).gameObject.SetActive(false);
        }

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



}

