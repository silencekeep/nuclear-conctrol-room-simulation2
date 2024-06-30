using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System;
using System.IO;

public class Update_Operation : MonoBehaviour
{
    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";
    private string abc, abc1, abc2,opid;
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        opid = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.Find("cell3").GetComponent<InputField>().text;
        abc = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.Find("cell0").GetComponent<InputField>().text;
        abc1 = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.Find("cell1").GetComponent<InputField>().text;
        abc2 = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.Find("cell2").GetComponent<InputField>().text;

        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;

        string juzi1 = "Update ToolOpreation set Operation='" + abc + "',Force='" + abc1 + "',Time='" + abc2 + "' where ID = '" + opid + "'";

        SqlDataAdapter DC = new SqlDataAdapter(juzi1, sqlCon);
        System.Data.DataSet dc = new System.Data.DataSet();
        DC.Fill(dc);

        sqlCon.Close();
    }
}
