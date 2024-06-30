using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System;
using System.IO;

public class ToolImport : MonoBehaviour
{
    // Start is called before the first frame update
    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";
    public Dropdown dp1, dp2;
    /*public*/ CADLoader ToolcadLoader;
    string path;
    int meshQuality=1;
    float my_scale = 1f;
    bool zUp=true;
    bool rightHanded=true;
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        ToolcadLoader = new CADLoader();
        //cadLoader.ImportEnded += CADImportEnded;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;

        SqlDataAdapter DC = new SqlDataAdapter("select FileName from ToolPath where ToolName ='"+ dp1.captionText.text+"'and SPEC='"+ dp2.captionText.text+"'", sqlCon);
        System.Data.DataSet dc = new System.Data.DataSet();
        DC.Fill(dc);

        //string[] Id_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        //for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的一列数据放进数组中
        //{
         string Id = Convert.ToString(dc.Tables[0].Rows[0][0]);//double
        //    if (Id != null)
        //        Id_arr[i] = Id;
        //    //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
        //    //dc.Tables[0].Rows[i][0])表示取数据表中第一列所有数据
        //}
        
        sqlCon.Close();

        path = Application.dataPath + Id;

        ToolcadLoader.cadFileName = path;
        ToolcadLoader.LoadCAD(true, -1, meshQuality, my_scale, zUp, rightHanded);
    }

    private void CADImportEnded()
    {
        //Loading_text.text = "Loading complete ! (in " + (int)(cadLoader.CadImportTiming) + " seconds)\n" + Path.GetFileName(cadLoader.cadFileName);
        //m_ProgressBar.gameObject.SetActive(false);
        //m_ProgressBarFull.gameObject.SetActive(false);
        //finish_button.gameObject.SetActive(true);
        Console.WriteLine("hahaha");
    }
}
