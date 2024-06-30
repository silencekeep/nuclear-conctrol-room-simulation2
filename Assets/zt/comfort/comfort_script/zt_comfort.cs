using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_comfort : MonoBehaviour
{
    zt_comfort_click2 click4;
     public float angle_;
    public float angle;
    public float comfort_value;
    public int a = 0;
    zt_comfort_dropdown dropdown1;
    zt_comfort_dropdown1 drop1;
    public GameObject txt11;
    zt_comfort_open1 open;

    void Start()
    {
        click4 = FindObjectOfType<zt_comfort_click2>();
        dropdown1 = FindObjectOfType<zt_comfort_dropdown>();
        drop1 = FindObjectOfType<zt_comfort_dropdown1>();
        open = FindObjectOfType<zt_comfort_open1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open.lister_comfort1 == 1)
        {
            click4.lister1 = 0;
        }

        if (click4.lister1==1)
        {
            //if (a1 == 0)
            //
            if(dropdown1.ValueChange==1)
            {
                angle_ = GameObject.Find(drop1.RightForeArm).GetComponent<Transform>().localEulerAngles.y - 360;
                //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm").GetComponent<Transform>().localEulerAngles.y;
                
                angle = -angle_;
                if (angle_ <= -360)
                {
                    angle = 0;
                }

            }
            if(dropdown1.ValueChange == 0)
            {
                //angle_ = GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm").GetComponent<Transform>().localEulerAngles.y - 360;
                angle_=GameObject.Find(drop1.LeftForeArm).GetComponent<Transform>().localEulerAngles.y;
                angle = angle_;
                if (angle <= -360)
                {
                    angle = 0;
                }
            }
            
           

            if (angle >= 0 & angle < 14)
            {
                txt11.GetComponent<Text>().text = 3.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h4/TXT13").GetComponent<Image>().color = Color.yellow;
            }
            if (angle >= 14 & angle < 28)
            {
                txt11.GetComponent<Text>().text = 3.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h4/TXT13").GetComponent<Image>().color = Color.yellow;
            }
            if (angle >= 28 & angle < 42)
            {
                txt11.GetComponent<Text>().text = 3.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h4/TXT13").GetComponent<Image>().color = Color.yellow;
            }
            if (angle >= 42 & angle < 56)
            {
                txt11.GetComponent<Text>().text = 4.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h4/TXT13").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            if (angle >= 56 & angle <= 70)
            {
                txt11.GetComponent<Text>().text = 4.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h4/TXT13").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            if (angle >= 70 & angle <= 84)
            {
                txt11.GetComponent<Text>().text = 5.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h4/TXT13").GetComponent<Image>().color = Color.red;
            }
            if (angle >= 84 & angle <= 98)
            {
                txt11.GetComponent<Text>().text = 5.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h4/TXT13").GetComponent<Image>().color = Color.red;
            }
            if (angle >= 98 & angle <= 112)
            {
                txt11.GetComponent<Text>().text = 5.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h4/TXT13").GetComponent<Image>().color = Color.red;
            }
            if (angle >= 112 & angle <= 126)
            {
                txt11.GetComponent<Text>().text = 5.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h4/TXT13").GetComponent<Image>().color = Color.red;
            }
            if (angle >= 126 & angle <= 140)
            {
                txt11.GetComponent<Text>().text = 5.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h4/TXT13").GetComponent<Image>().color = Color.red;
            }
            //else
            //{
            //angle3 = GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm").GetComponent<Transform>().localEulerAngles.y;
            // comfort_value3 = 0.0001f * angle3 * angle3 - 0.0218f * angle3 + 0.9651f;
            // }
        }
    }
}
