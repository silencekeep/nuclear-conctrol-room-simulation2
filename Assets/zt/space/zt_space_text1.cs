using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_space_text1 : MonoBehaviour
{
    zt_space_text text1;
    zt_space_click click2;
    zt_space_img img1;


    public string left_upperarm4;
    public string right_upperarm4;
    public string right_Forearm4;
    public string left_Forearm4;
    public string jieguo ;
    zt_space_open1 open;
    void Start()
    {
        text1 = FindObjectOfType<zt_space_text>();
        click2 = FindObjectOfType<zt_space_click>();
        img1 = FindObjectOfType<zt_space_img>();
        open= FindObjectOfType<zt_space_open1>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (click2.lister == 1)
        {
            
            if(text1.left_Forearm3.Length==0)
            {
                left_Forearm4 = "无";
            }
            else
            {
                left_Forearm4 = text1.left_Forearm3;
            }


            if (text1.left_upperarm3.Length == 0)
            {
                left_upperarm4 = "无";
            }
            else
            {
                left_upperarm4 = text1.left_upperarm3;
            }



            if (text1.right_Forearm3.Length==0)
            {
                right_Forearm4 = "无";
            }
            else
            {
                right_Forearm4 = text1.right_Forearm3;
            }

            if (text1.right_upperarm3.Length==0 )
            {
                right_upperarm4= "无";
            }
            else
            {
                right_upperarm4 = text1.right_upperarm3;
            }

            GameObject.Find("Text2").GetComponent<Text>().text = "左手操作空间等级为：" +img1.l1+"\n"+ "右手操作空间等级为：" +img1.r1 + "\n" + "左上臂碰撞物：" + left_upperarm4 + "\n" + "左前臂碰撞物："  + left_Forearm4 + "\n" + "右上臂碰撞物：" + right_upperarm4 + "\n" + "右前臂碰撞物：" + right_Forearm4;
            if(click2.lister1==1)
            {
                jieguo = img1.l1;
                GameObject.Find("zt_space/Canvas/Panel/Canvas1/chenggong").GetComponent<Text>().text = "报告导出成功！";
                click2.lister1 = 0;
            }
            




        }
    }
}
