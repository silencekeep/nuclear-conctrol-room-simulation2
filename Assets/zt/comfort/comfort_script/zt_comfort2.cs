using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_comfort2 : MonoBehaviour
{
    zt_comfort_click2 click4;
    public float angle1;
    public float comfort_value1;
    public int a1 = 0;
    zt_comfort_dropdown dropdown2;
    zt_comfort_dropdown1 drop2;
    public GameObject txt12;
    zt_comfort_open1 open;

    void Start()
    {
        click4 = FindObjectOfType<zt_comfort_click2>();
        dropdown2 = FindObjectOfType<zt_comfort_dropdown>();
        drop2 = FindObjectOfType<zt_comfort_dropdown1>();
        open = FindObjectOfType<zt_comfort_open1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open.lister_comfort1 == 1)
        {
            click4.lister1 = 0;
        }

        if (click4.lister1 == 1)
        {
            
            if (dropdown2.ValueChange==1)
            {
                angle1 = GameObject.Find(drop2.RightArm).GetComponent<Transform>().localEulerAngles.y - 360;
                comfort_value1 = -angle1;
            }
                
             if(dropdown2.ValueChange==0)
            {
                angle1 = GameObject.Find(drop2.LeftArm).GetComponent<Transform>().localEulerAngles.y;
                comfort_value1 = angle1;
            }

            if (comfort_value1 >= -60 & comfort_value1 < -37)
            {
                txt12.GetComponent<Text>().text = 2.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h5/TXT14").GetComponent<Image>().color = Color.blue;
            }
            if (comfort_value1 >= -37 & comfort_value1 < -14)
            {
                txt12.GetComponent<Text>().text = 2.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h5/TXT14").GetComponent<Image>().color = Color.blue;
            }
            if (comfort_value1 >= -14 & comfort_value1 < 9)
            {
                txt12.GetComponent<Text>().text = 1.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h5/TXT14").GetComponent<Image>().color = Color.green;
            }
            if (comfort_value1 >= 9 & comfort_value1 < 32)
            {
                txt12.GetComponent<Text>().text = 2.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h5/TXT14").GetComponent<Image>().color = Color.blue;
            }
            if (comfort_value1 >= 32 & comfort_value1 <= 55)
            {
                txt12.GetComponent<Text>().text = 3.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h5/TXT14").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value1 >= 55 & comfort_value1 <= 78)
            {
                txt12.GetComponent<Text>().text = 3.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h5/TXT14").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value1 >= 78 & comfort_value1 <= 101)
            {
                txt12.GetComponent<Text>().text = 3.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h5/TXT14").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value1 >= 101 & comfort_value1 <= 124)
            {
                txt12.GetComponent<Text>().text = 4.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h5/TXT14").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            if (comfort_value1 >= 124 & comfort_value1 <= 147)
            {
                txt12.GetComponent<Text>().text = 4.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h5/TXT14").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            if (comfort_value1 >= 147 & comfort_value1 <= 170)
            {
                txt12.GetComponent<Text>().text = 4.ToString();
                GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h5/TXT14").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            //else
            //{
            //GameObject.Find("TXT7").GetComponent<Text>().text = 1.ToString();
            //GameObject.Find("TXT8").GetComponent<Image>().color = Color.red;
            //}
        }
    }
}
