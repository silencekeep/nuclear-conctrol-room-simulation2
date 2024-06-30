using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UseRowControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RowEditClick()
    {
        GameObject error = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/useControl/backgroundLeft/backgroundLeft-2/error");
        error.SetActive(false);
        string use = this.transform.GetChild(0).GetComponent<TMP_Text>().text;
        string password = this.transform.GetChild(1).GetComponent<TMP_InputField>().text;
        TMP_Dropdown positionDrop = this.transform.GetChild(2).GetComponent<TMP_Dropdown>();
        string position = positionDrop.options[positionDrop.value].text;
        if (password.Length == 0)
        {
            error.GetComponent<TMP_Text>().text = "请输入密码";
            error.SetActive(true);
        }
        else
        {
            //判断是更新还是新增
            string sql = "select * from UseTable where [use] = '" + use + "'";
            System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
            if (dc.Tables[0].Rows.Count > 0)
            {
                sql = "update UseTable set [use] = '" + use + "' , password = '" + password + "' , position = '" + position + "' where [use] = '" + use + "'";
            }
            else
            {
                sql = "insert into UseTable values ('" + use + "', '" + password + "', '" + position + "')";

            }
            Debug.Log(sql);
            sqlconnect.Instance.sqladd(sql);

        }

    }
    public void RowDeleteClick()//数据库删除功能
    {
        GameObject error = GameObject.Find("2DUI/Canvas/JurisdictionManagementPanel/useControl/backgroundLeft/backgroundLeft-2/error");
        error.SetActive(false);
        string use = this.transform.GetChild(0).GetComponent<TMP_Text>().text;
        Destroy(this.transform.gameObject);
        string sql = "delete from UseTable where [use] = '" + use + "'";
        Debug.Log(sql);
        sqlconnect.Instance.sqladd(sql);
    }
}
