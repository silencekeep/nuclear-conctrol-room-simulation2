using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class post_text : MonoBehaviour
{
    private GameObject min_tex1, min_tex2, min_tex3, min_tex4, min_tex5, min_tex6, min_tex7, min_tex8, min_tex9, min_tex10;
    private GameObject max_text1, max_text2, max_text3, max_text4, max_text5, max_text6, max_text7, max_text8, max_text9, max_text10;
    post_dropdown dropdown;
    void Start()
    {
        dropdown = FindObjectOfType<post_dropdown>();
        min_tex1 = GameObject.Find("min/Text1");
        min_tex2= GameObject.Find("min/Text2");
        min_tex3 = GameObject.Find("min/Text3");
        min_tex4 = GameObject.Find("min/Text4");
        min_tex5 = GameObject.Find("min/Text5");
        min_tex6 = GameObject.Find("min/Text6");
        min_tex7 = GameObject.Find("min/Text7");
        min_tex8 = GameObject.Find("min/Text8");
        min_tex9 = GameObject.Find("min/Text9");
        min_tex10 = GameObject.Find("min/Text10");

        max_text1 = GameObject.Find("max/Text1");
        max_text2 = GameObject.Find("max/Text2");
        max_text3 = GameObject.Find("max/Text3");
        max_text4 = GameObject.Find("max/Text4");
        max_text5 = GameObject.Find("max/Text5");
        max_text6 = GameObject.Find("max/Text6");
        max_text7 = GameObject.Find("max/Text7");
        max_text8 = GameObject.Find("max/Text8");
        max_text9 = GameObject.Find("max/Text9");
        max_text10 = GameObject.Find("max/Text10");


    }

    // Update is called once per frame
    void Update()
    {
        if(dropdown.a==1)
        {
            min_tex1.GetComponent<Text>().text = (-61).ToString();
            min_tex2.GetComponent<Text>().text = (-34).ToString();
            min_tex3.GetComponent<Text>().text = (-48).ToString();
            min_tex4.GetComponent<Text>().text = (-199).ToString();
            min_tex5.GetComponent<Text>().text = 0.ToString();
            min_tex6.GetComponent<Text>().text = 0.ToString();
            min_tex7.GetComponent<Text>().text = 0.ToString();
            min_tex8.GetComponent<Text>().text = 0.ToString();
            min_tex9.GetComponent<Text>().text = (-87).ToString();
            min_tex10.GetComponent<Text>().text = (-30).ToString();

            max_text1.GetComponent<Text>().text = 180.ToString();
            max_text2.GetComponent<Text>().text = 140.ToString();
            max_text3.GetComponent<Text>().text = 134.ToString();
            max_text4.GetComponent<Text>().text = 78.ToString();
            max_text5.GetComponent<Text>().text = 144.ToString();
            max_text6.GetComponent<Text>().text = 90.ToString();
            max_text7.GetComponent<Text>().text = 90.ToString();
            max_text8.GetComponent<Text>().text = 90.ToString();
            max_text9.GetComponent<Text>().text = 104.ToString();
            max_text10.GetComponent<Text>().text = 44.ToString();


        }
        if(dropdown.a==2)
        {
            min_tex1.GetComponent<Text>().text = (-180).ToString();
            min_tex2.GetComponent<Text>().text = (-140).ToString();
            min_tex3.GetComponent<Text>().text = (-134).ToString();
            min_tex4.GetComponent<Text>().text = (-78).ToString();
            min_tex5.GetComponent<Text>().text = (-144).ToString();
            min_tex6.GetComponent<Text>().text = (-90).ToString();
            min_tex7.GetComponent<Text>().text = (-90).ToString();
            min_tex8.GetComponent<Text>().text = (-90).ToString();
            min_tex9.GetComponent<Text>().text = (-104).ToString();
            min_tex10.GetComponent<Text>().text = (-44).ToString();

            max_text1.GetComponent<Text>().text = 61.ToString();
            max_text2.GetComponent<Text>().text = 34.ToString();
            max_text3.GetComponent<Text>().text = 48.ToString();
            max_text4.GetComponent<Text>().text = 199.ToString();
            max_text5.GetComponent<Text>().text = 0.ToString();
            max_text6.GetComponent<Text>().text = 0.ToString();
            max_text7.GetComponent<Text>().text = 0.ToString();
            max_text8.GetComponent<Text>().text = 0.ToString();
            max_text9.GetComponent<Text>().text = 87.ToString();
            max_text10.GetComponent<Text>().text = 30.ToString();
        }
    }
}
