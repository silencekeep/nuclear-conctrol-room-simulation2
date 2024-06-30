using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_comfort6 : MonoBehaviour
{
    zt_comfort_click2 click6;
    public float angle5;
    public float comfort_value5;
    public int a3 = 0;
    zt_comfort_dropdown dropdown3;
    zt_comfort_dropdown1 drop3;
    zt_comfort_open1 open;

    void Start()
    {
        click6= FindObjectOfType<zt_comfort_click2>();
        dropdown3 = FindObjectOfType<zt_comfort_dropdown>();
        drop3 = FindObjectOfType<zt_comfort_dropdown1>();
        open = FindObjectOfType<zt_comfort_open1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open.lister_comfort1 == 1)
        {
            click6.lister1 = 0;
        }
        if (click6.lister1 == 1)
        {
            if(dropdown3.ValueChange==1)
            {
                angle5 = GameObject.Find(drop3.RightHand).GetComponent<Transform>().localEulerAngles.z-360;
                comfort_value5 = -angle5;
                if(comfort_value5>=350|| comfort_value5 <=-350)
                {
                    comfort_value5 = 0;
                }
            }
            if (dropdown3.ValueChange == 0)
            {
                angle5 = GameObject.Find(drop3.LeftHand).GetComponent<Transform>().localEulerAngles.z - 360;
                comfort_value5 = angle5;
                if (comfort_value5 >= 360 || comfort_value5 <= -360)
                {
                    comfort_value5 = 0;
                }
            }


                if (comfort_value5 >= -70 & comfort_value5 < -55)
            {
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT22").GetComponent<Text>().text = 2.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT23").GetComponent<Image>().color = Color.blue;
            }
            if (comfort_value5 >= -55 & comfort_value5 < -40)
            {
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT22").GetComponent<Text>().text = 2.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT23").GetComponent<Image>().color = Color.blue;
            }
            if (comfort_value5 >= -40 & comfort_value5 < -25)
            {
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT22").GetComponent<Text>().text = 2.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT23").GetComponent<Image>().color = Color.blue;
            }
            if (comfort_value5 >= -25 & comfort_value5 < -10)
            {
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT22").GetComponent<Text>().text = 2.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT23").GetComponent<Image>().color = Color.blue;
            }
            if (comfort_value5 >= -10 & comfort_value5 <= 5)
            {
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT22").GetComponent<Text>().text = 1.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT23").GetComponent<Image>().color = Color.green;
            }
            if (comfort_value5 >= 5 & comfort_value5 <= 20)
            {
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT22").GetComponent<Text>().text = 1.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT23").GetComponent<Image>().color = Color.green;
            }
            if (comfort_value5 >= 20 & comfort_value5 <= 35)
            {
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT22").GetComponent<Text>().text = 1.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT23").GetComponent<Image>().color = Color.green;
            }
            if (comfort_value5 >= 35 & comfort_value5 <= 50)
            {
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT22").GetComponent<Text>().text = 2.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT23").GetComponent<Image>().color = Color.blue;
            }
            if (comfort_value5 >= 50 & comfort_value5 <= 65)
            {
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT22").GetComponent<Text>().text = 2.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT23").GetComponent<Image>().color = Color.blue;
            }
            if (comfort_value5 >= 65 & comfort_value5 <= 80)
            {
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT22").GetComponent<Text>().text = 2.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT23").GetComponent<Image>().color = Color.blue;
            }
            //else
            //{
            //angle3 = GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm").GetComponent<Transform>().localEulerAngles.y;
            // comfort_value3 = 0.0001f * angle3 * angle3 - 0.0218f * angle3 + 0.9651f;
            // }
        }
    }
}
