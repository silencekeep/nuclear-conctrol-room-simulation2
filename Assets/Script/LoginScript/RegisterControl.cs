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
    public TMP_InputField userText;//�û��������
    public TMP_InputField passwordText;//���������
    public TMP_InputField passwordText2;//���������2
    public TMP_Dropdown positionDropdown;//ְλ������
    public GameObject error;//������ʾ

    //�ж����ڵ�¼���滹��ע�����
    private int logres = 0;
    // Start is called before the first frame update
    void Start()
    {
        //�����б��ʼ��
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
    //��¼��ť
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
    //ע�ᰴť
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
    //ȷ�ϰ�ť
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



    //��¼����
    public bool login(string use, string password)
    {     
        //��ȡ���ݿ�����
        string sql = "select * from UseTable";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        //��֤�û���������
        bool b = true;
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string datause = dc.Tables[0].Rows[i][0].ToString().Trim();
            if (datause.Equals(use))
            {
                string datapassword = dc.Tables[0].Rows[i][1].ToString().Trim();
                Debug.Log("�û�����ȷ");
                Debug.Log(datapassword);
                Debug.Log(password);
                if (datapassword.Equals(password))
                {
                    Debug.Log("������ȷ");
                    error.SetActive(true);
                    error.GetComponent<TMP_Text>().color = new Color(25 / 255f, 25 / 255f, 112 / 255f, 255 / 255f);
                    error.GetComponent<TMP_Text>().text = "���ڵ�¼�����Ժ�";
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
            error.GetComponent<TMP_Text>().text = "�û������������";
            error.SetActive(true);
        }
        return !b;
    }
    //ע�Ṧ��
    public bool register(string use, string password, string password2, string position) 
    {
        //���ò�ѯ����
        string sql = "select* from UseTable";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);    
        bool b = true;
        //��֤�û����Ƿ�Ϊ��
        if (use == "") 
        {
            b = false;
            error.GetComponent<TMP_Text>().text = "�û�������Ϊ��";
            error.SetActive(true);
            return b;
        }
        //��֤�����Ƿ�Ϊ��
        if (password == "")
        {
            b = false;
            error.GetComponent<TMP_Text>().text = "���벻��Ϊ��";
            error.SetActive(true);
            return b;
        }
        //��֤�û����Ƿ����
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)
        {
            string datause = dc.Tables[0].Rows[i][0].ToString().Trim();
            if (datause.Equals(use))
            {
                b = false;
                error.GetComponent<TMP_Text>().text = "�û����Ѵ���";
                error.SetActive(true);
                return b;
            }
        }
        //��֤�����Ƿ���ͬ
        if (password != password2) 
        {
            b = false;
            error.GetComponent<TMP_Text>().text = "�����������벻һ��";
            error.SetActive(true);
            return b;
        }
        if (b) {
            error.SetActive(true);
            error.GetComponent<TMP_Text>().color = new Color(25 / 255f, 25 / 255f, 112 / 255f, 255 / 255f);
            error.GetComponent<TMP_Text>().text = "���ڵ�¼�����Ժ�";
            userText.text = "";
            passwordText.text = "";
            passwordText2.text = "";
            Gamedata.Instance.people = use;
            Gamedata.Instance.location = position;
            //�������Ӻ���
            Debug.Log("ע��ɹ�");
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
