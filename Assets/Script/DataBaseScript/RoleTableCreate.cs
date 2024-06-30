using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Data.SqlClient;
using System;

public class RoleTableCreate : MonoBehaviour
{
    public GameObject Rrow_Prefab;//表头预设
    private SqlConnection sqlCon;
    //public GameObject table;
    GameObject table;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";
    void Start()
    {
        table = GameObject.Find("RTable");
        AllDataTableShow();
    }
    public void AllDataTableShow()
    {
        //RoleManager.querysign = 1;
        int count = table.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(table.transform.GetChild(i).gameObject);
            // table.transform.GetChild(i).gameObject.SetActive(false);
        }

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

}

