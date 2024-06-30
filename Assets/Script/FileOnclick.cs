using System.IO;
using System;
using UnityEngine;
using UnityEditor;
//using SFB;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Data;
using System.Data.SqlClient;

public class FileOnclick : MonoBehaviour
{
    //[MenuItem("Example/Import human")]
    public GameObject obj;
    Text text;
    //DataSet myds1 = new DataSet();
    //string sqlconn = "Server=.;Database=test;Trusted_Connection=SSPI";

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }


    private void OnClick()
    {
        //string sql1 = "select test1 from Table_1";
        //SqlConnection mycon1 = new SqlConnection(sqlconn);
        //mycon1.Open();
        //SqlDataAdapter myda1 = new SqlDataAdapter(sql1, sqlconn);
        //DataSet myds1 = new DataSet();
        //myda1.Fill(myds1);
        //string a = myds1.Tables[0].Rows[0][1].ToString();

        //text = GameObject.Find("Canvas/Text").GetComponent<Text>();
        //text.text = a;
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        //}
    }

}
