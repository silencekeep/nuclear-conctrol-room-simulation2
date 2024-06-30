using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class post_text2 : MonoBehaviour
{
    post_slider slider1;
    post_slider2 slider2;
    post_dropdown dropdown1;
    private GameObject text1, text2, text3, text4, text5, text6, text7, text8, text9, text10;
    void Start()
    {
        slider1 = FindObjectOfType<post_slider>();
        slider2 = FindObjectOfType<post_slider2>();
        dropdown1 = FindObjectOfType<post_dropdown>();
        text1 = GameObject.Find("post_text2/Text1");
        text2 = GameObject.Find("post_text2/Text2");
        text3 = GameObject.Find("post_text2/Text3");
        text4 = GameObject.Find("post_text2/Text4");
        text5 = GameObject.Find("post_text2/Text5");
        text6 = GameObject.Find("post_text2/Text6");
        text7 = GameObject.Find("post_text2/Text7");
        text8 = GameObject.Find("post_text2/Text8");
        text9 = GameObject.Find("post_text2/Text9");
        text10 = GameObject.Find("post_text2/Text10");


    }

    // Update is called once per frame
    void Update()
    {
        if(dropdown1.a==1)
        {
            text1.GetComponent<Text>().text = slider1.shoulder_x_value.ToString();
            text2.GetComponent<Text>().text = slider1.shoulder_y_value.ToString();
            text3.GetComponent<Text>().text = slider1.shoulder_z_value.ToString();
            text4.GetComponent<Text>().text = slider1.elbow_x_value.ToString();
            text5.GetComponent<Text>().text = slider1.elbow_y_value.ToString();
            text6.GetComponent<Text>().text = slider1.figure1_value.ToString();
            text7.GetComponent<Text>().text = slider1.figure2_value.ToString();
            text8.GetComponent<Text>().text = slider1.figure3_value.ToString();
            text9.GetComponent<Text>().text = slider1.wrist_z_value.ToString();
            text10.GetComponent<Text>().text = slider1.wrist_y_value.ToString();

        }
        if(dropdown1.a==2)
        {
            text1.GetComponent<Text>().text = slider2.shoulder_x_value.ToString();
            text2.GetComponent<Text>().text = slider2.shoulder_y_value.ToString();
            text3.GetComponent<Text>().text = slider2.shoulder_z_value.ToString();
            text4.GetComponent<Text>().text = slider2.elbow_x_value.ToString();
            text5.GetComponent<Text>().text = slider2.elbow_y_value.ToString();
            text6.GetComponent<Text>().text = slider2.figure1_value.ToString();
            text7.GetComponent<Text>().text = slider2.figure2_value.ToString();
            text8.GetComponent<Text>().text = slider2.figure3_value.ToString();
            text9.GetComponent<Text>().text = slider2.wrist_z_value.ToString();
            text10.GetComponent<Text>().text = slider2.wrist_y_value.ToString();
        }
    }
}
