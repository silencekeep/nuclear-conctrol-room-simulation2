using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using CNCC.Panels;

public class StationChange : MonoBehaviour
{
    private bool target;
    private GameObject myTarget;
    public InputField targetName;
    public GameObject Station, RelationPanel, AlertPanel, AlertPanel1, AlertPanel2, BUPAlertPanel2;
    public Slider lengthSlider, widthSlider, heightSlider;
    public Text LengthNowtext, WidthNowtext, HeightNowtext, wordhightext;
    double lengthFloat, widthFloat,heightFloat;
    public string stretch_Sitting, kneeHeight_Sitting,shoulder_Standing;
    public Dropdown Dropdown;
    public static bool QType1DropDownValueChanged;
    //private SqlConnection sqlCon;
    public static int querysign;
    //private string sqlAddress = "server=127.0.0.1;database=NuclearControlRoomItemDB;uid=sa;pwd=123456";

    void Start()
    {
        QType1DropDownValueChanged = false;
    }

    void Update()
    {
        if (target && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetName.text = hit.transform.name;
                myTarget = hit.transform.gameObject;
                if (myTarget.GetComponent<OWP_two>())
                {
                    GameObject.Find("Canvas/RelationPanel/StationChange/HeightMinText").GetComponent<Text>().text = "60";
                    GameObject.Find("Canvas/RelationPanel/StationChange/HeightMaxText").GetComponent<Text>().text = "90";
                    GameObject.Find("Canvas/RelationPanel/StationChange/HeightText").GetComponent<Text>().text = "桌面高度";
                }
                else if (myTarget.GetComponent<BUP>())
                {
                    GameObject.Find("Canvas/RelationPanel/StationChange/HeightMinText").GetComponent<Text>().text = "180";
                    GameObject.Find("Canvas/RelationPanel/StationChange/HeightMaxText").GetComponent<Text>().text = "220";
                    GameObject.Find("Canvas/RelationPanel/StationChange/HeightText").GetComponent<Text>().text = "控制台高度";
                }
            }
            target = false;

        }
    }

    public void targetChoose()
    {
        target = true;
    }

    public void Change() {
        if (myTarget.GetComponent<OWP_two>())
        {
            Vector3 vectorPart1 = new Vector3(lengthSlider.value, heightSlider.value, 1);
            Vector3 vectorPart2 = new Vector3(lengthSlider.value, 1, widthSlider.value);
            myTarget.GetComponent<OWP_two>().SetPart1Scale(vectorPart1);
            myTarget.GetComponent<OWP_two>().SetPart2Scale(vectorPart2);
        }
        else if(myTarget.GetComponent<BUP>())
        {
            Vector3 vector = new Vector3(lengthSlider.value, heightSlider.value, widthSlider.value);
            myTarget.transform.localScale = vector;
        }
       
        //myTarget.transform.localScale = new Vector3(lengthSlider.value, heightSlider.value, widthSlider.value);
    }

    public void SliderChange()
    {
        if (myTarget.GetComponent<OWP_two>())
        {
            //宽
            lengthFloat = 135 * lengthSlider.value + 10;
            LengthNowtext.text = lengthFloat.ToString("0.0");

            //深
            widthFloat = 60 * widthSlider.value;
            WidthNowtext.text = widthFloat.ToString("0.0");
            //字高
            double wordHigh = 0.32 + (widthFloat - 40) / 40 * 0.16;
            wordhightext.text = wordHigh.ToString("0.00");

            //高
            heightFloat = 150 * heightSlider.value - 75;
            HeightNowtext.text = heightFloat.ToString("0.0");
        }
        else if (myTarget.GetComponent<BUP>())
        {
            //宽
            lengthFloat = 135 * lengthSlider.value + 10;
            LengthNowtext.text = lengthFloat.ToString("0.0");

            //深
            widthFloat = 48 * widthSlider.value + 8;
            WidthNowtext.text = widthFloat.ToString("0.0");
            GameObject.Find("Canvas/RelationPanel/StationChange/WidthMaxText").GetComponent<Text>().text = "72.0";

            double wordHigh = 0.32 + (widthFloat - 40) / 40 * 0.16;
            wordhightext.text = wordHigh.ToString("0.00");

            //高
            heightFloat = 200 * heightSlider.value;
            HeightNowtext.text = heightFloat.ToString("0.0");
            
        }

    }

    public void Alert() 
    {

        double length = 2 * Convert.ToDouble(stretch_Sitting) + (double)22.3;
        if (myTarget.GetComponent<Panel>())
        {
            if (lengthFloat > length)
            {
                AlertPanel.SetActive(true);
            }
            else
            {
                AlertPanel.SetActive(false);
            }
        }
    }

    public void Alert1()
    {

       
        double width = Convert.ToDouble(stretch_Sitting) - (double)12.7;

        if (myTarget.GetComponent<Panel>())
        {
            if (widthFloat > width)
            {
                AlertPanel1.SetActive(true);
            }
            else
            {
                AlertPanel1.SetActive(false);
            }
        }


    }
    public void Alert2()//增加BUP判断，可达性
    {
        double height = Convert.ToDouble(kneeHeight_Sitting) + (double)15.8;
        double number = (widthFloat + 12.7) * (widthFloat + 12.7) + (heightFloat - Convert.ToDouble(shoulder_Standing))* (heightFloat - Convert.ToDouble(shoulder_Standing));
        double distance = Math.Sqrt(number);
        if (myTarget.GetComponent<OWP_two>())
        {
            if (heightFloat < height)
            {
                AlertPanel2.SetActive(true);
            }
            else
            {
                AlertPanel2.SetActive(false);
            }
        }
        else
        {
            if (Convert.ToDouble(stretch_Sitting) < distance)
            {
                BUPAlertPanel2.SetActive(true);
            }
            else
            {
                BUPAlertPanel2.SetActive(false);
            }
        }
    }

    public void Close_Button() {
        RelationPanel.SetActive(false);
    }
    public void AlertYes_Button()
    {
        AlertPanel.SetActive(false);
        AlertPanel1.SetActive(false);
        AlertPanel2.SetActive(false);
        BUPAlertPanel2.SetActive(false);
    }


    public void Conditionalquery()
    {
        //sqlCon = new SqlConnection(sqlAddress);
        //sqlCon.Open();
        SqlCommand cmd = new SqlCommand();
        //cmd.Connection = sqlCon;
        string Qtype1 = Dropdown.captionText.text;
        SqlDataAdapter DC;
        //if (QTypeDropdown.captionText.text != "不限")
        //{
        //    DC = new SqlDataAdapter("select * from actions where ActType='" + Qtype + "'", sqlCon);            
        //}
        //else
        //{
        //    DC = new SqlDataAdapter("select * from actions where ActGenType='" + Qtype1 + "'", sqlCon);
        //}

        //DC = new SqlDataAdapter("select * from zuozi where percentage='" + Qtype1 + "'", sqlCon);
        System.Data.DataSet dc = sqlconnect.Instance.sqlquery("select * from zuozi where percentage='" + Qtype1 + "'");
        System.Data.DataSet dc1 = sqlconnect.Instance.sqlquery("select * from zhanzi where percentage='" + Qtype1 + "'");
        //System.Data.DataSet dc = new System.Data.DataSet();
        //DC.Fill(dc);

        //stretch_Sitting = Convert.ToString(dc.Tables[0].Rows[0][4]);
        //kneeHeight_Sitting = Convert.ToString(dc.Tables[0].Rows[0][7]);
        string[] shoulder_arr = new string[dc.Tables[0].Rows.Count];
        string[] stretch_arr = new string[dc.Tables[0].Rows.Count];
        string[] kneeHeight_arr = new string[dc.Tables[0].Rows.Count];

        for (int i = 0; i < dc.Tables[0].Rows.Count; i++)   //将数据表中的数据放进数组中
        {
            string stretch = Convert.ToString(dc.Tables[0].Rows[i][4]);
            if (stretch != null)
                stretch_arr[i] = stretch;
            stretch_Sitting = stretch_arr[i];

            string kneeHeight = Convert.ToString(dc.Tables[0].Rows[i][7]);
            if (kneeHeight != null)
                kneeHeight_arr[i] = kneeHeight;
            kneeHeight_Sitting = kneeHeight_arr[i];

            //sqlCon.Close();
        }

        //获取站姿肩高
        for (int i = 0; i < dc1.Tables[0].Rows.Count; i++)   //将数据表中的数据放进数组中
        {
            string shoulder = Convert.ToString(dc1.Tables[0].Rows[i][2]);
            if (shoulder != null)
                shoulder_arr[i] = shoulder;
            shoulder_Standing = shoulder_arr[i];

            //sqlCon.Close();
        }


    }
    }




