using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;

public class RoleRowScripts : MonoBehaviour
{
    public static string RowUserIDText;
    public static string RowUserNameText;
    public static string RowUserTypeText;
    public static string RowUserPassWordText;
    private SqlConnection sqlCon;
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";

    // Start is called before the first frame update
    void Start()
    {

    }

    public void RowEditClick()
    {
        Text RowUserID = transform.GetChild(0).GetComponent<Text>();
        RowUserIDText = RowUserID.text;

        Text RowUserName = transform.GetChild(2).GetComponent<Text>();
        RowUserNameText = RowUserName.text;

        Text RowUserPassWord = transform.GetChild(3).GetComponent<Text>();
        RowUserPassWordText = RowUserPassWord.text;

        Text RowUserType = transform.GetChild(1).GetComponent<Text>();
        RowUserTypeText = RowUserType.text;

        RoleManager.Usereditpanelsign = true;
        RoleManager.Usereditshowsign = true;
    }
    public void RowDeleteClick()//数据库删除功能
    {
        Text RowUserID = transform.GetChild(0).GetComponent<Text>();
        RowUserIDText = RowUserID.text;
        Destroy(transform.gameObject);
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        string UserDelete = "delete from users where UserID = '" + RowUserIDText + "'";

        SqlCommand sqlCommand = new SqlCommand(UserDelete, sqlCon);
        sqlCommand.ExecuteNonQuery();
        sqlCon.Close();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
