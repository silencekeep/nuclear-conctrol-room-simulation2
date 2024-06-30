using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using UnityEngine.UI;
using System.Data;
using UnityEngine.SceneManagement;

public class LoginScripts : MonoBehaviour
{
    public InputField UserNameInputField;
    public InputField PassWordInputField;
    public GameObject ChoosePanel;
    public GameObject RoleManage;
    public GameObject LoginPanel;
    public GameObject LoginFailed;
    public Dropdown RoleDropdown;

    private SqlConnection sqlCon;    
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LoginClicked()
    {
        string username = UserNameInputField.text;
        string password = PassWordInputField.text;
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        string UserLogin = "select UserName,PassWord,UserType from users where UserName='" + username + "' and PassWord='" + password + "' and UserType = '" + RoleDropdown.captionText.text + "' ";
        SqlCommand sqlCommand = new SqlCommand(UserLogin, sqlCon);
        SqlDataAdapter uda = new SqlDataAdapter(sqlCommand);
        DataSet uds = new DataSet();
        int n = uda.Fill(uds, "register");
        if (n != 0 && RoleDropdown.captionText.text == "高级管理员")
        {
            LoginPanel.SetActive(false);
            RoleManage.SetActive(true);
            ChoosePanel.SetActive(false);
           
        }
        else if (n != 0 && RoleDropdown.captionText.text == "动素管理员")
        {
            LoginPanel.SetActive(false);
            ChoosePanel.SetActive(true);
            RoleManage.SetActive(false);
            
        }
        else if (n == 0)
        {
            LoginFailed.SetActive(true);
            
        }
        sqlCon.Close();      

    }

    public void LoginFailedPanelClose()
    {
        LoginFailed.SetActive(false);
    }

    public void ExitActDataBase()
    {
        SceneManager.LoadScene(1);
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }
}
