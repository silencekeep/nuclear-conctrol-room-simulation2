using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using TMPro;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityScript.Steps;

public class receive : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;
    public GameObject Button5;
    public GameObject Button6;
    public GameObject Button7;
    public GameObject Button8;
    public Text use;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void inition() 
    {
        string location = "高级管理员";
        string people = "yingyuan";
        if (Gamedata.Instance) {
            location = Gamedata.Instance.location;
            people = Gamedata.Instance.people;
        }       
        use.text = people;
        Button1.SetActive(true);
        Button2.SetActive(true);
        Button3.SetActive(true);
        Button4.SetActive(true);
        Button5.SetActive(true);
        Button6.SetActive(true);
        Button7.SetActive(true);
        Button8.SetActive(true);
        //权限管理
        string sql = "select * from roles where positon = '" + location + "'";
        Debug.Log(sql);
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        
        if (dc.Tables[0].Rows[0][1].ToString().Trim() == "0")
        {
            Button1.SetActive(false);
        }
        if (dc.Tables[0].Rows[0][2].ToString().Trim() == "0")
        {
            Button2.SetActive(false);
        }
        if (dc.Tables[0].Rows[0][3].ToString().Trim() == "0")
        {
            Button3.SetActive(false);
        }
        if (dc.Tables[0].Rows[0][4].ToString().Trim() == "0")
        {
            Button4.SetActive(false);
        }
        if (dc.Tables[0].Rows[0][5].ToString().Trim() == "0")
        {
            Button5.SetActive(false);
        }
        if (dc.Tables[0].Rows[0][6].ToString().Trim() == "0")
        {
            Button6.SetActive(false);
        }
        if (dc.Tables[0].Rows[0][7].ToString().Trim() == "0")
        {
            Button7.SetActive(false);
        }
        if (dc.Tables[0].Rows[0][8].ToString().Trim() == "0")
        {
            Button8.SetActive(false);
        }
    }
}
