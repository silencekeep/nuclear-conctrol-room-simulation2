using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UseControl : MonoBehaviour
{
    public GameObject table;//表格的父节点
    public TMP_InputField nameinput;
    public GameObject error;//表格的父节点
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void inition()
    {
        error.SetActive(false);
        string sql = "select * from UseTable";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        for (int i = 0; i < table.transform.childCount; i++)
            Destroy(table.transform.GetChild(i).gameObject);
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)//添加并修改预设的过程，将创建10行
        {
            //在Table下创建新的预设实例
            GameObject row = Instantiate(Resources.Load("PreFabs/UseRow"), table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("Name").GetComponent<TMP_Text>().text = Convert.ToString(dc.Tables[0].Rows[i][0]);
            row.transform.Find("Password").GetComponent<TMP_InputField>().text = Convert.ToString(dc.Tables[0].Rows[i][1]);
            TMP_Dropdown positionDrop = row.transform.Find("Dropdown").GetComponent<TMP_Dropdown>();
            positionDrop.ClearOptions();
            sql = "select * from roles";
            System.Data.DataSet dcRole = sqlconnect.Instance.sqlquery(sql);
            int k = 0;
            for (int j = 0; j < dcRole.Tables[0].Rows.Count; j++)
            {
                positionDrop.options.Add(new TMP_Dropdown.OptionData());
                positionDrop.options[j].text = Convert.ToString(dcRole.Tables[0].Rows[j][0]);
                if (positionDrop.options[j].text.Equals(Convert.ToString(dc.Tables[0].Rows[i][2])))
                    k = j;
            }
            positionDrop.value = k;
        }
    }
    public void addButton()
    {
        error.SetActive(false);
        string sql = "select * from UseTable where [use] = '" + nameinput.text +"'";
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery(sql);
        if (nameinput.text.Length == 0)
        {
            error.GetComponent<TMP_Text>().text = "请输入用戶名";
            error.SetActive(true);
        }
        else if (dc.Tables[0].Rows.Count > 0){
            error.GetComponent<TMP_Text>().text = "用户名不可重复";
            error.SetActive(true);
        }
        else
        {
            //创建空行
            GameObject row = Instantiate(Resources.Load("PreFabs/UseRow"), table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + table.transform.childCount;
            row.transform.SetParent(table.transform);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;
            //设置用户名
            row.transform.Find("Name").GetComponent<TMP_Text>().text = nameinput.text;
            //设置下拉框
            TMP_Dropdown positionDrop = row.transform.Find("Dropdown").GetComponent<TMP_Dropdown>();
            sql = "select * from roles";
            System.Data.DataSet dcRole = sqlconnect.Instance.sqlquery(sql);
            for (int j = 0; j < dcRole.Tables[0].Rows.Count; j++)
            {
                positionDrop.options.Add(new TMP_Dropdown.OptionData());
                positionDrop.options[j].text = Convert.ToString(dcRole.Tables[0].Rows[j][0]);
            }

        }

    }

}
