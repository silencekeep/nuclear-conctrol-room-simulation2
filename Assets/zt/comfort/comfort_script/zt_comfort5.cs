using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_comfort5 : MonoBehaviour
{
    zt_comfort_click2 click5;
    public float angle4;
    public float comfort_value4;
    public int a3 = 0;
    zt_comfort_dropdown1 drop5;
    public GameObject txt16;
    zt_comfort_open1 open;
    zt_comfort_open2 open1;

    void Start()
    {
        click5 = FindObjectOfType<zt_comfort_click2>();
        drop5 = FindObjectOfType<zt_comfort_dropdown1>();
        open = FindObjectOfType<zt_comfort_open1>();
        open1= FindObjectOfType<zt_comfort_open2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open.lister_comfort1 == 1)
        {
            click5.lister1 = 0;
        }

        if (click5.lister1 == 1)
        {


            open.lister_comfort1 = 0;

            angle4 = GameObject.Find(drop5.Spine).GetComponent<Transform>().localEulerAngles.x;
            comfort_value4 = angle4;


            if (comfort_value4 >= -10 & comfort_value4 < -5)
            {
                txt16.GetComponent<Text>().text = 3.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h6/TXT17").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value4 >= -5 & comfort_value4 < 0)
            {
                txt16.GetComponent<Text>().text = 3.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h6/TXT17").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value4 >= 0 & comfort_value4 < 5)
            {
                txt16.GetComponent<Text>().text = 3.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h6/TXT17").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value4 >= 5 & comfort_value4 < 10)
            {
                txt16.GetComponent<Text>().text = 3.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h6/TXT17").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value4 >= 10 & comfort_value4 <= 15)
            {
                txt16.GetComponent<Text>().text = 4.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h6/TXT17").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            if (comfort_value4 >= 15 & comfort_value4 <= 20)
            {
                txt16.GetComponent<Text>().text = 4.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h6/TXT17").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            if (comfort_value4 >= 20 & comfort_value4 <= 25)
            {
                txt16.GetComponent<Text>().text = 4.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h6/TXT17").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            if (comfort_value4 >= 25 & comfort_value4 <= 30)
            {
                txt16.GetComponent<Text>().text = 4.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h6/TXT17").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            if (comfort_value4 >= 30 & comfort_value4 <= 35)
            {
                txt16.GetComponent<Text>().text = 5.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h6/TXT17").GetComponent<Image>().color = Color.red;
            }
            if (comfort_value4 >= 35 & comfort_value4 <= 40)
            {
                txt16.GetComponent<Text>().text = 5.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h6/TXT17").GetComponent<Image>().color = Color.red;
            }

            


        }
       
    }
}
