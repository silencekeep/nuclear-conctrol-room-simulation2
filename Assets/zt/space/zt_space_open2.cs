using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class zt_space_open2 : MonoBehaviour
{
    zt_space_open1 open1;
    public  GameObject space;
    zt_space_click click;
    zt_space_text1 text1;
    zt_space_text text;
    zt_space_img img;
    void Start()
    {
        open1 = FindObjectOfType<zt_space_open1>();
        
        space.SetActive(false);
        click = FindObjectOfType<zt_space_click>();
        text1 = FindObjectOfType<zt_space_text1>();
        text = FindObjectOfType<zt_space_text>();

    }

    // Update is called once per frame
    void Update()
    {
        if(open1.lister_space==1)
        {
               
                space.SetActive(true);
            open1.lister_space = 0;
            GameObject.Find("Text2").GetComponent<Text>().text = null;
            GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h2/space10").GetComponent<Text>().text = null;
            GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h4/space12").GetComponent<Text>().text = null;
            GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h5/space13").GetComponent<Text>().text = null;
            GameObject.Find("zt_space/Canvas/Panel/Canvas1/v1/h3/space11").GetComponent<Text>().text = null;


        }
        if (open1.lister_space1 == 1)
        {
            text1.left_Forearm4=" ";
            text1.left_upperarm4 = " ";
            text1.right_Forearm4 = " ";
            text1.right_upperarm4 = " ";
            text.left_Forearm2 .Clear();
            text.left_Forearm3 = " ";
            text.left_upperarm2.Clear();
            text.left_upperarm3 = " ";
            text.right_Forearm2 .Clear();
            text.right_Forearm3 = " ";
            text.right_upperarm2 .Clear();
            text.right_upperarm3 = " ";
            GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h2/img1").GetComponent<Image>().color = new Color(204/255f, 204/255f, 204/255f, 255/255f);
            GameObject.Find("zt_space/Canvas/Panel/Canvas1/v2/h3/img2").GetComponent<Image>().color = new Color(204 / 255f, 204 / 255f, 204 / 255f, 255 / 255f);


            click.lister = 0;
            space.SetActive(false);
            open1.lister_space1 = 0;
        }

    }
}
