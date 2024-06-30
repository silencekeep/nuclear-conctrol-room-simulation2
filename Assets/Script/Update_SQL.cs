using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System;
using System.IO;

public class Update_SQL : MonoBehaviour
{
    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";
    public Dropdown Drd_IPList, Drd_IPList1;
    public InputField ifd1,ifd2;
    String abc,abc1,abc2;
 
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
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;
      
        string juzi = "Update ToolPath set ToolName='"+ifd1.text+ "',SPEC='" + ifd2.text + "'where ToolName='"+ Drd_IPList.captionText.text+"' and SPEC='"+Drd_IPList1.captionText.text+"'";


        SqlDataAdapter DC = new SqlDataAdapter(juzi, sqlCon);
        System.Data.DataSet dc = new System.Data.DataSet();
        DC.Fill(dc);

        sqlCon.Close();
    }

    public void OnClick1()
    {
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;

        string OperationId;
        OperationId = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.Find("cell3").GetComponent<Text>().text;

        abc = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.Find("cell0").GetComponent<InputField>().text;
        abc1 = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.Find("cell1").GetComponent<InputField>().text;
        abc2 = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.Find("cell2").GetComponent<InputField>().text;

        string juzi1 = "Update ToolOperation set Operation='" + abc + "',Force='" + abc1 + "',Time='" + abc2 + "' where ID = '" + OperationId +"'";


        SqlDataAdapter DC = new SqlDataAdapter(juzi1, sqlCon);
        System.Data.DataSet dc = new System.Data.DataSet();
        DC.Fill(dc);

        sqlCon.Close();
    }
}
