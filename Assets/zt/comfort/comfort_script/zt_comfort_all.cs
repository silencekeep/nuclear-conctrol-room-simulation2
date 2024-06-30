using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_comfort_all : MonoBehaviour
{
    zt_comfort_click2 click7;
    zt_comfort_dropdown dropdown4;
    public float ForeArmangle_;
    public float ForeArmangle;
    public float Spineangle4;
    public float Spinecomfort_value4;
    public float Armangle1, Armcomfort_value1;
    zt_comfort_dropdown1 drop4;
    public GameObject txt25;
    zt_comfort_open1 open;
    void Start()
    {
        click7 = FindObjectOfType<zt_comfort_click2>();
        dropdown4 = FindObjectOfType<zt_comfort_dropdown>();
        drop4 = FindObjectOfType<zt_comfort_dropdown1>();
        open = FindObjectOfType<zt_comfort_open1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open.lister_comfort1 == 1)
        {
            click7.lister1 = 0;
        }
        if (click7.lister1 == 1)
        {
            //if (a1 == 0)
            //
            if (dropdown4.ValueChange == 1)
            {
                ForeArmangle_ = GameObject.Find(drop4.RightForeArm).GetComponent<Transform>().localEulerAngles.y - 360;
                //GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm").GetComponent<Transform>().localEulerAngles.y;

                ForeArmangle = -ForeArmangle_;
                if (ForeArmangle >= 360)
                {
                    ForeArmangle = 0;
                }
            }
            if (dropdown4.ValueChange == 0)
            {
                //angle_ = GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:RightShoulder/swat:RightArm/swat:RightForeArm").GetComponent<Transform>().localEulerAngles.y - 360;
                ForeArmangle_ = GameObject.Find(drop4.LeftForeArm).GetComponent<Transform>().localEulerAngles.y;
                ForeArmangle = ForeArmangle_;
                if (ForeArmangle_ <= -360)
                {
                    ForeArmangle = 0;
                }
            }


            if (dropdown4.ValueChange == 1)
            {
                Armangle1 = GameObject.Find(drop4.RightArm).GetComponent<Transform>().localEulerAngles.y - 360;
                Armcomfort_value1 = -Armangle1;
            }

            if (dropdown4.ValueChange == 0)
            {
                Armangle1 = GameObject.Find(drop4.LeftArm).GetComponent<Transform>().localEulerAngles.y;
                Armcomfort_value1 = Armangle1;
            }
            Spineangle4 = GameObject.Find(drop4.Spine).GetComponent<Transform>().localEulerAngles.x;
            Spinecomfort_value4 = Spineangle4;




            if (Spinecomfort_value4 >= -10 & Spinecomfort_value4 <= 20)
            {

                if (Armcomfort_value1 <= -60 & Armcomfort_value1 < 32)
                {
                    txt25.GetComponent<Text>().text = 3.ToString();
                    GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h9/TXT26").GetComponent<Image>().color = Color.yellow;
                }
                else if (Armcomfort_value1 <= 32 & Armcomfort_value1 < 101)
                {
                    txt25.GetComponent<Text>().text = 4.ToString();
                    GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h9/TXT26").GetComponent<Image>().color = Color.yellow;
                }
                else if (Armcomfort_value1 <= 101 & Armcomfort_value1 < 170)
                {
                    txt25.GetComponent<Text>().text = 2.ToString();
                    GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h9/TXT26").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
                }

            }


            if (Spinecomfort_value4 >= 20 & Spinecomfort_value4 <= 40)
            {
                if (ForeArmangle >= 0 & ForeArmangle < 70)
                {

                    txt25.GetComponent<Text>().text = 6.ToString();
                    GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h9/TXT26").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));


                }
                else if (ForeArmangle >= 70 & ForeArmangle < 140)
                {

                   txt25.GetComponent<Text>().text = 7.ToString();
                    GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h9/TXT26").GetComponent<Image>().color = Color.red;


                }
            }
        }


    }
}
