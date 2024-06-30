using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;
using System;

public class ScrollRectScripts : MonoBehaviour
{
    public Dropdown ActDropDown;
    public GameObject Content;
    public GameObject CombContent;
    public GameObject ActImage_Prefab;
    public Text ActText;
    public GameObject ActCombPanel;
    private int ActDropDownCount;
    private SqlConnection sqlCon;    
    private string sqlAddress = "server=127.0.0.1;database=ActToolsDB;uid=sa;pwd=123456";

    // Start is called before the first frame update
    void Start()
    {
        ActDropDownCount = ActDropDown.options.Count;
        UpdateContent();
        
    }

    public void UpdateContent()//更新动素图片显示列表
    {
        int count = Content.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(Content.transform.GetChild(i).gameObject);
            // table.transform.GetChild(i).gameObject.SetActive(false);
        }        
        for (int i = 0; i < ActDropDownCount; i++)//添加并修改预设的过程
        {
            //在Table下创建新的预设实例            
            GameObject row = Instantiate(ActImage_Prefab, Content.transform.position, Content.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(Content.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("ImagePref").GetComponent<Image>().sprite = Resources.Load("ActImg/"+ ActDropDown.options[i].text, typeof(Sprite)) as Sprite;// LoadAsset<Sprite>( ActDropDown.options[i].text); //Resources.Load<AnimationClip>("Animation/" + "idle"), "clip" + clipsIndex[1].ToString()
            row.transform.Find("ImagePrefText").GetComponent<Text>().text = ActDropDown.options[i].text;
            
        }
    }

    
    private void ReadActCombine(out int ActCombCount,out string[] ActCombName)
    {
        sqlCon = new SqlConnection(sqlAddress);
        sqlCon.Open();
        //int ActCombCount;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = sqlCon;
        SqlDataAdapter DC = new SqlDataAdapter("select distinct actionCombineName from actionCombine", sqlCon);
        System.Data.DataSet dc = new System.Data.DataSet();
        DC.Fill(dc);
        string[] Id_arr = new string[dc.Tables[0].Rows.Count];//定义存放数据库中一列的数组，dc.Tables[0].Rows.Count为一列数据的长度
        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的一列数据放进数组中
        {
            string Id = Convert.ToString(dc.Tables[0].Rows[i][0]);//double
            if (Id != null)
                Id_arr[i] = Id;
            //dc.Tables[0].Rows[0][0])表示取数据表中第一个数据
            //dc.Tables[0].Rows[i][0])表示取数据表中第一列所有数据
        }
        ActCombCount = dc.Tables[0].Rows.Count;
        ActCombName = Id_arr;
    }

    public void UpdateActcCombContent()//更新动素组合库图片显示列表
    {
        ActCombPanel.SetActive(true);
        int count = CombContent.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(CombContent.transform.GetChild(i).gameObject);          
        }
        ReadActCombine(out int ActCombCount, out string[] ActCombName);

        for (int i = 0; i < ActCombCount; i++)//添加并修改预设的过程
        {
            //在Table下创建新的预设实例            
            GameObject row = Instantiate(ActImage_Prefab, Content.transform.position, Content.transform.rotation) as GameObject;
            row.name = "ActCombName" + (i + 1);
            row.transform.SetParent(Content.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.Find("ImagePref").GetComponent<Image>().sprite = Resources.Load("ActImg/" + ActCombName[i], typeof(Sprite)) as Sprite;// LoadAsset<Sprite>( ActDropDown.options[i].text); //Resources.Load<AnimationClip>("Animation/" + "idle"), "clip" + clipsIndex[1].ToString()
            row.transform.Find("ImagePrefText").GetComponent<Text>().text = ActCombName[i];
        }
    }
    public void ActCombPanelClose()//动素组合库面板关闭
    {
        timielinedemoscript.CombBtnClick = false;
        ActCombPanel.SetActive(false);
    }
   
    // Update is called once per frame
    void Update()
    {
        ActDropDownCount = ActDropDown.options.Count;
        string CurText = ActItemprefScript.curtext;
        ActText.text =  CurText;
    }
}
