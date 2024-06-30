using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;
using Button = UnityEngine.UI.Button;
using Application = UnityEngine.Application;

public class Add_SQL222 : MonoBehaviour
{
    //private SqlConnection sqlCon;
    //private string sqlAddress = "server=10.23.140.254;database=ToolsDB;uid=sa;pwd=xmw19970505";
    //public InputField ifd1, ifd2, ifd3;
    //// Use this for initialization

    void Start()
    {
        //var button = GetComponent<Button>();
        //button.onClick.AddListener(OnClick);
        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        //SqlCommand cmd = new SqlCommand();
        //cmd.Connection = sqlCon;
        ////OpenFileDialog form = new OpenFileDialog();
        ////form.InitialDirectory = UnityEngine.Application.dataPath;
        ////if (form.ShowDialog() == DialogResult.OK)
        //// {
        ////ifd3.text = form.FileName;
        ////ifd3.text = Path.GetFileNameWithoutExtension(form.FileName);
        //// }
        //string juzi = "insert into ToolPath(ToolID,ToolName,SPEC,FileName) values ((select case when max(ToolID) is null then 1 else max(ToolID)+1 end from ToolPath),'" + ifd1.text + "','" + ifd2.text + "','/Tools/" + ifd3.text + "')";


        //SqlDataAdapter DC = new SqlDataAdapter(juzi, sqlCon);
        //System.Data.DataSet dc = new System.Data.DataSet();
        //DC.Fill(dc);

        //sqlCon.Close();
    }
}