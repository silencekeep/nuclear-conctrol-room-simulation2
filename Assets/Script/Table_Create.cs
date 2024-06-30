using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System;
using System.IO;

public class Table_Create : MonoBehaviour
{
    public GameObject Row_Prefab;//表头预设
    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=ToolsDB;uid=sa;pwd=xmw19970505";
    public Dropdown Drd_IPList;
    public GameObject table;
    void Start()
    {
        //var button = GetComponent<Button>();
        //button.onClick.AddListener(OnClick);

        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
    }
    void Update()
    {
    }
    public void OnClick()
    {
        int count = table.transform.childCount;
        for (int i = 0; i < count ; i++)
        {
            Destroy(table.transform.GetChild(i).gameObject);
        }
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;
        SqlDataAdapter DC = new SqlDataAdapter("select ID,Operation,Force,Time from ToolOpreation where Tool='" + Drd_IPList.captionText.text + "'", sqlCon);


        System.Data.DataSet dc = new System.Data.DataSet();
        DC.Fill(dc);
        string[] Tool_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        string[] Operation_arr = new string[dc.Tables[0].Rows.Count];
        string[] Force_arr = new string[dc.Tables[0].Rows.Count];
        string[] Time_arr = new string[dc.Tables[0].Rows.Count];
        string[] ID_arr = new string[dc.Tables[0].Rows.Count];

        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的数据放进数组中
        {
            string ID = Convert.ToString(dc.Tables[0].Rows[i][0]);
            if (ID != null)

            {
                ID_arr[i] = ID;
            }

            string Operation = Convert.ToString(dc.Tables[0].Rows[i][1]);//将数据表中的动素序号ActID数据放进数组中
                                                                         //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
                                                                         //dc.Tables[0].Rows[i][0])表示取数据表中第一列所有数据
            if (Operation != null)
            {
                Operation_arr[i] = Operation;
            }

            string Force = Convert.ToString(dc.Tables[0].Rows[i][2]);//将数据表中的动素类别ActType数据放进数组中
            if (Force != null)
            {
                Force_arr[i] = Force;
            }

            string Time = Convert.ToString(dc.Tables[0].Rows[i][3]);
            if (Time != null)

            {
                Time_arr[i] = Time;
            }
            //string Description = Convert.ToString(dc.Tables[0].Rows[i][4]);
            // if (Description != null)
            // Description_arr[i] = Description;
            //string Path = Convert.ToString(dc.Tables[0].Rows[i][5]);
            //if (Path != null)
            // Path_arr[i] = Path;
        }

        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)//添加并修改预设的过程，将创建10行
        {
            //在Table下创建新的预设实例
            GameObject table = GameObject.Find("PiXYZ_Runtime_Import_Boiler_Plate(Clone)/Canvas/ToolsPopUp/Scroll View/Viewport/Table");
            GameObject row = Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("cell3").GetComponent<InputField>().text = ID_arr[i];
            row.transform.Find("cell0").GetComponent<InputField>().text = Operation_arr[i];
            row.transform.Find("cell1").GetComponent<InputField>().text = Force_arr[i];
            row.transform.Find("cell2").GetComponent<InputField>().text = Time_arr[i];
            row.transform.Find("Text").GetComponent<Text>().text = " ";
            row.transform.Find("Text/Update").GetComponent<Text>().text = "修改";
            row.transform.Find("Text/Delete").GetComponent<Text>().text = "删除";

            //row.transform.Find("ActID").GetComponent<Text>().text = ID_arr[i];
            //row.transform.Find("ActType").GetComponent<Text>().text = Operation_arr[i];
            //row.transform.Find("ActName").GetComponent<Text>().text = Force_arr[i];
            //row.transform.Find("ActDescription").GetComponent<Text>().text = Time_arr[i];
            //row.transform.Find("Text").GetComponent<Text>().text = " ";
            //row.transform.Find("Text/Update").GetComponent<Text>().text = "修改";
            //row.transform.Find("Text/Delete").GetComponent<Text>().text = "删除";

            // row.transform.Find("Delete").GetComponent<Text>().text = "删除";
            //row.transform.Find("ActTime").GetComponent<Text>().text = Time_arr[i];
            // row.transform.Find("ActDescription").GetComponent<Text>().text = Description_arr[i];
            // row.transform.Find("ActPath").GetComponent<Text>().text = Path_arr[i];
        }
        sqlCon.Close();
    }
}

   

