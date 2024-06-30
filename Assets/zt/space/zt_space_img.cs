using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_space_img : MonoBehaviour
{
    zt_space_right1 space_right1;
    
    zt_space_left1 space_left1;
   
    zt_space_click space_click;


    zt_space_2right1 space_2right1;
    
    zt_space_2left1 space_2left1;
    

    zt_space_3right1 space_3right1;
    
    zt_space_3left1 space_3left1;


    zt_space_right2 space_right2; zt_space_left2 space_left2;
    zt_space_2right2 space_2right2; zt_space_2left2 space_2left2;
    zt_space_3right2 space_3right2; zt_space_3left2 space_3left2;

    public string l1, r1;
    zt_space_dropdown dropdown1;
    zt_space_open1 open;
    void Start()
    {
        space_right1 = FindObjectOfType<zt_space_right1>();
        space_right2 = FindObjectOfType<zt_space_right2>();
        space_left1 = FindObjectOfType<zt_space_left1>();
        space_left2 = FindObjectOfType<zt_space_left2>();

        space_2right1 = FindObjectOfType<zt_space_2right1>();
        space_2right2 = FindObjectOfType<zt_space_2right2>();
        space_2left1 = FindObjectOfType<zt_space_2left1>();
        space_2left2 = FindObjectOfType<zt_space_2left2>();


        space_3right1 = FindObjectOfType<zt_space_3right1>();
        space_3right2 = FindObjectOfType<zt_space_3right2>();
        space_3left1 = FindObjectOfType<zt_space_3left1>();
        space_3left2 = FindObjectOfType<zt_space_3left2>();

        space_click = FindObjectOfType<zt_space_click>();
        dropdown1 = FindObjectOfType<zt_space_dropdown>();
        open = FindObjectOfType<zt_space_open1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open.lister_space1 == 1)
        {
            space_click.lister = 0;
        }
        if (space_click.lister == 1)
        {
            
            if (dropdown1.a == 2)
            {

                if (space_left2.left2 == 0)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h2/img1").GetComponent<Image>().color = Color.green;
                    l1 = "合适";
                }
                if (space_left2.left2 == 1 && space_left1.left1 == 0)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h2/img1").GetComponent<Image>().color = Color.yellow;
                    l1 = "中等";
                }
                if (space_left1.left1 == 1)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h2/img1").GetComponent<Image>().color = Color.red;
                    l1 = "不足";
                }




                if (space_right2.right2 == 0)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h3/img2").GetComponent<Image>().color = Color.green;
                    r1 = "合适";
                }
                if (space_right2.right2 == 1 && space_right1.right1 == 0)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h3/img2").GetComponent<Image>().color = Color.yellow;
                    r1 = "中等";
                }
                if (space_right1.right1 == 1)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h3/img2").GetComponent<Image>().color = Color.red;
                    r1 = "不足";
                }
            }

            if (dropdown1.a == 3)
            {
                if (space_2left2.left2 == 0)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h2/img1").GetComponent<Image>().color = Color.green;
                    l1 = "合适";
                }
                if (space_2left2.left2 == 1 && space_left1.left1 == 0)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h2/img1").GetComponent<Image>().color = Color.yellow;
                    l1 = "中等";
                }
                if (space_2left1.left1 == 1)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h2/img1").GetComponent<Image>().color = Color.red;
                    l1 = "不足";
                }



                if (space_2right2.right2 == 0)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h3/img2").GetComponent<Image>().color = Color.green;
                    r1 = "合适";
                }
                if (space_2right2.right2 == 1 && space_right1.right1 == 0)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h3/img2").GetComponent<Image>().color = Color.yellow;
                    r1 = "中等";
                }
                if (space_2right1.right1 == 1)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h3/img2").GetComponent<Image>().color = Color.red;
                    r1 = "不足";
                }


            }

            if (dropdown1.a == 4)
            {

                if (space_3left2.left2 == 0)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h2/img1").GetComponent<Image>().color = Color.green;
                    l1 = "合适";
                }
                if (space_3left2.left2 == 1 && space_left1.left1 == 0)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h2/img1").GetComponent<Image>().color = Color.yellow;
                    l1 = "中等";
                }
                if (space_3left1.left1 == 1)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h2/img1").GetComponent<Image>().color = Color.red;
                    l1 = "不足";
                }



                if (space_3right2.right2 == 0)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h3/img2").GetComponent<Image>().color = Color.green;
                    r1 = "合适";
                }
                if (space_3right2.right2 == 1 && space_right1.right1 == 0)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h3/img2").GetComponent<Image>().color = Color.yellow;
                    r1 = "中等";
                }
                if (space_3right1.right1 == 1)
                {
                    GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h3/img2").GetComponent<Image>().color = Color.red;
                    r1 = "不足";
                }

                
            }

        }
    }
}
