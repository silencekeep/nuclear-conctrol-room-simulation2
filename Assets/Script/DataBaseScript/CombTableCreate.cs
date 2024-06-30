using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Data.SqlClient;
using System;

public class CombTableCreate : MonoBehaviour
{
    public GameObject CombRow_Prefab;//表头预设
    public GameObject ActCombTable;
    private SqlConnection sqlCon;
    //public GameObject table;
    GameObject table;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";
    void Start()
    {
        table = ActCombTable;// GameObject.Find("ActCombTable");
        AllDataTableShow();
    }
    public void AllDataTableShow()
    {
        //ActManager.querysign = 1;
        int count = table.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(table.transform.GetChild(i).gameObject);            
        }

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
                                                                  //dc.Tables[0].Rows[i][0])表示取数据表中第一列所有数据
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
            GameObject table = ActCombTable;
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

}

