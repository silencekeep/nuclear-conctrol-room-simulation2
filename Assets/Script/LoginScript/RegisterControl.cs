using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterControl : MonoBehaviour
{
    public TMP_Text textLogin;
    public TMP_Text textRegister;
    public Image ImageLogin;
    public Image ImageRegister;
    public TMP_InputField userText;//用户名输入框
    public TMP_InputField passwordText;//密码输入框
    public TMP_InputField passwordText2;//密码输入框2
    public TMP_Dropdown positionDropdown;//职位下拉框
    public GameObject error;//错误提示

    //判断是在登录界面还是注册界面
    private int logres = 0;
    // Start is called before the first frame update
    void Start()
    {
        //下拉列表初始化
        positionDropdown.ClearOptions();
        string sql = "select * from roles";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        List<TMP_Dropdown.OptionData> listOptions = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string dataposition = dc.Tables[0].Rows[i][0].ToString().Trim();
            listOptions.Add(new TMP_Dropdown.OptionData(dataposition));

        }
        positionDropdown.AddOptions(listOptions);
        LoginButton();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //登录按钮
    public void LoginButton()
    {
        error.SetActive(false);
        passwordText2.gameObject.SetActive(false);
        positionDropdown.gameObject.SetActive(false);
        userText.gameObject.transform.localPosition = new Vector3(0f, -70f, 0f);
        passwordText.gameObject.transform.localPosition = new Vector3(0f, -130f, 0f);
        textLogin.color = new Color(0 / 255f, 0 / 255f, 255 / 255f, 255 / 255f);
        textRegister.color = new Color(0 / 255f, 0 / 255f,0 / 255f, 255 / 255f);
        ImageLogin.color = new Color(0 / 255f, 0 / 255f, 255 / 255f, 255 / 255f);
        ImageRegister.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 0 / 255f);
        userText.text = "";
        passwordText.text = "";
        passwordText2.text = "";
        logres = 0;
    }
    //注册按钮
    public void RegisterButton()
    {
        error.SetActive(false);
        passwordText2.gameObject.SetActive(true);
        positionDropdown.gameObject.SetActive(true);
        userText.gameObject.transform.localPosition = new Vector3(0f, -30f, 0f);
        passwordText.gameObject.transform.localPosition = new Vector3(0f, -75f, 0f);
        passwordText2.gameObject.transform.localPosition = new Vector3(0f, -120f, 0f);
        positionDropdown.gameObject.transform.localPosition = new Vector3(0f, -165f, 0f);
        textLogin.color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
        textRegister.color = new Color(0 / 255f, 0 / 255f, 255 / 255f, 255 / 255f);
        ImageLogin.color = new Color (255f / 255f, 255f / 255f, 255f / 255f, 0 / 255f);
        ImageRegister.color = new Color(0 / 255f, 0 / 255f, 255 / 255f, 255 / 255f);
        userText.text = "";
        passwordText.text = "";
        passwordText2.text = "";
        logres = 1;
    }
    //确认按钮
    public bool EnterButton()
    {
        bool b;
        error.SetActive(false);
        string use = userText.text;
        string password = passwordText.text;
        if (logres == 0)
        {
            b = login(use, password);
        }
        else 
        {
            string password2 = passwordText2.text;
            var idx = positionDropdown.value;
            string position = positionDropdown.options[idx].text;
            b = register(use, password, password2, position);
            
        }
        return b;
        
    }



    //登录功能
    public bool login(string use, string password)
    {     
        //获取数据库数据
        string sql = "select * from UseTable";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        //验证用户名和密码
        bool b = true;
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string datause = dc.Tables[0].Rows[i][0].ToString().Trim();
            if (datause.Equals(use))
            {
                string datapassword = dc.Tables[0].Rows[i][1].ToString().Trim();
                Debug.Log("用户名正确");
                Debug.Log(datapassword);
                Debug.Log(password);
                if (datapassword.Equals(password))
                {
                    Debug.Log("密码正确");
                    error.SetActive(true);
                    error.GetComponent<TMP_Text>().color = new Color(25 / 255f, 25 / 255f, 112 / 255f, 255 / 255f);
                    error.GetComponent<TMP_Text>().text = "正在登录，请稍候";
                    userText.text = "";
                    passwordText.text = "";
                    passwordText2.text = "";
                    string dataposition = dc.Tables[0].Rows[i][2].ToString().Trim();
                    Gamedata.Instance.people = use;
                    Gamedata.Instance.location = dataposition;
                    b = false;
                    break;
                }
            }
        }
        if (b)
        {
            error.GetComponent<TMP_Text>().text = "用户名或密码错误";
            error.SetActive(true);
        }
        return !b;
    }
    //注册功能
    public bool register(string use, string password, string password2, string position) 
    {
        //调用查询函数
        string sql = "select* from UseTable";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);    
        bool b = true;
        //验证用户名是否为空
        if (use == "") 
        {
            b = false;
            error.GetComponent<TMP_Text>().text = "用户名不能为空";
            error.SetActive(true);
            return b;
        }
        //验证密码是否为空
        if (password == "")
        {
            b = false;
            error.GetComponent<TMP_Text>().text = "密码不能为空";
            error.SetActive(true);
            return b;
        }
        //验证用户名是否存在
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string datause = dc.Tables[0].Rows[i][0].ToString().Trim();
            if (datause.Equals(use))
            {
                b = false;
                error.GetComponent<TMP_Text>().text = "用户名已存在";
                error.SetActive(true);
                return b;
            }
        }
        //验证密码是否相同
        if (password != password2) 
        {
            b = false;
            error.GetComponent<TMP_Text>().text = "两次输入密码不一致";
            error.SetActive(true);
            return b;
        }
        if (b) {
            error.SetActive(true);
            error.GetComponent<TMP_Text>().color = new Color(25 / 255f, 25 / 255f, 112 / 255f, 255 / 255f);
            error.GetComponent<TMP_Text>().text = "正在登录，请稍候";
            userText.text = "";
            passwordText.text = "";
            passwordText2.text = "";
            Gamedata.Instance.people = use;
            Gamedata.Instance.location = position;
            //调用增加函数
            Debug.Log("注册成功");
            sql = "INSERT INTO UseTable VALUES ('" + use + "', " + password + ",'" + position + "')";
            sqlconnect.Instance.sqladd(sql);
        }
        return b;
    }
    public void exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
