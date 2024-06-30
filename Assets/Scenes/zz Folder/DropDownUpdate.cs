using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Data.SqlClient;



public class DropDownUpdate : MonoBehaviour

{

    public Dropdown dropDown;
    // Start is called before the first frame update
    void Start()
    {
        Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        Dropdown dropdown = dropDown;
        string sql = "select percentage from zuozi" ;
        SqliteAssist sqliteAssist = new SqliteAssist();
        DataSet dc = sqliteAssist.GetQueryResultByDataSet(sql);

        string[] Id_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的一列数据放进数组中
        {
            string Id = Convert.ToString(dc.Tables[0].Rows[i][0]);//double
            if (Id != null)
                Id_arr[i] = Id;
            //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
            //dc.Tables[0].Rows[i][0])表示取数据表中第一列所有数据
        }
        dropdown.options.Clear();
        Dropdown.OptionData tempData;
        tempData = new Dropdown.OptionData();
        tempData.text = "场景";
        dropdown.options.Add(tempData);
        for (int k = 0; k < dc.Tables[0].Rows.Count; k++)   //将数组中的数据取出显示在下拉列表中
        {
            tempData = new Dropdown.OptionData();
            tempData.text = Id_arr[k];
            dropdown.options.Add(tempData);
        }
    }
}
