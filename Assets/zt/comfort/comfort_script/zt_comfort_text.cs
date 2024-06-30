using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_comfort_text : MonoBehaviour
{
    public string forearm;
    public string upperarm;
    public string spine;
    public string wrist;
    public string all;
    public string jieguo_text;
    public GameObject chenggong;
    public string otherjoint;
    zt_comfort_click2 click;
    public List<string> joint = new List<string>();
    public int a = 0;
    zt_comfort_open1 open;


    void Start()
    {
        click = FindObjectOfType<zt_comfort_click2>();
        open = FindObjectOfType<zt_comfort_open1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open.lister_comfort1 == 1)
        {
            click.lister1 = 0;
            a = 0;
            joint.Clear();
            forearm = " "; upperarm = " "; spine = " "; wrist = " "; all = " ";
        }
        if (click.lister1 == 1)
        {
            a++;
            if (a == 10)
            {
                forearm = GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h4/TXT11").GetComponent<Text>().text;
                upperarm = GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h5/TXT12").GetComponent<Text>().text;
                spine = GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h6/TXT16").GetComponent<Text>().text;
                wrist = GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT22").GetComponent<Text>().text;
                all = GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h9/TXT25").GetComponent<Text>().text;



                if (forearm == "3" || forearm == "4" || forearm == "5")
                {
                    joint.Add("前臂");
                }

                if (upperarm == "3" || upperarm == "4" || upperarm == "5")
                {
                    joint.Add("上臂");
                }
                if (spine == "3" || spine == "4" || spine == "5")
                {
                    joint.Add("躯干");
                }
                if (wrist == "3" || wrist == "4" || wrist == "5")
                {
                    joint.Add("腕部");
                }
                 string otherjoint = string.Join("、", joint.ToArray());
                GameObject.Find("report").GetComponent<Text>().text = "上肢整体舒适度评分为:" + all + "，建议调节的肢体为：" + otherjoint;
                
            }
            

        }
        if (click.lister2 == 1)
        {
            jieguo_text = all + otherjoint;
            chenggong.GetComponent<Text>().text = "结果导出成功";
            click.lister2 = 0;
        }

    }
}
