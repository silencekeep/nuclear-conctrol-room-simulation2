using NPOI.SS.Formula.Functions;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectRowControl : MonoBehaviour
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
        GameObject NumberText = GameObject.Find("2DUI/Canvas/ProjectManagementPanel/EditPanel/zujian/number/NumberText1");
        string number = this.transform.GetChild(0).GetComponent<TMP_Text>().text;
        Debug.Log(number);
        NumberText.GetComponent<TMP_Text>().text = number;
        GameObject querypanel = GameObject.Find("2DUI/Canvas/ProjectManagementPanel/QueryPanel");
        querypanel.SetActive(false);
        GameObject editpanel = GameObject.Find("2DUI/Canvas/ProjectManagementPanel/EditPanel");
        editpanel.SetActive(true);
        editpanel.GetComponent<EditPanel>().inition();
    }
    public void RowDeleteClick()//数据库删除功能
    {
        string Number = this.transform.GetChild(0).GetComponent<TMP_Text>().text;
        Destroy(this.transform.gameObject);
        string sql = "delete from ProjectData where Number = '" + Number + "'";
        sqlconnect.Instance.sqladd(sql);
    }
}
