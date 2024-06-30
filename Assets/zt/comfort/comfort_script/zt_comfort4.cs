using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class zt_comfort4 : MonoBehaviour
{
    zt_comfort_click2 click4;
    public float angle3;
    public float comfort_value3;
    public int a3 = 0;

    void Start()
    {
        click4 = FindObjectOfType<zt_comfort_click2>();
    }

    // Update is called once per frame
    void Update()
    {

        if (click4.lister1 == 1)
        {
            //if (a1 == 0)
            //{
            angle3 = GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm").GetComponent<Transform>().localEulerAngles.y;
           
            //a1++;
            //}

            if (comfort_value3 >= 0 & comfort_value3 < 14)
            {
                GameObject.Find("TXT11").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT13").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value3 >= 14 & comfort_value3 < 28)
            {
                GameObject.Find("TXT11").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT13").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value3 >= 28 & comfort_value3 < 42)
            {
                GameObject.Find("TXT11").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT13").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value3 >= 42& comfort_value3 <56)
            {
                GameObject.Find("TXT11").GetComponent<Text>().text = 4.ToString();
                GameObject.Find("TXT13").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            if (comfort_value3 >= 56& comfort_value3 <=70)
            {
                GameObject.Find("TXT11").GetComponent<Text>().text = 4.ToString();
                GameObject.Find("TXT13").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            if (comfort_value3 >= 40 & comfort_value3 <= 84)
            {
                GameObject.Find("TXT11").GetComponent<Text>().text = 5.ToString();
                GameObject.Find("TXT13").GetComponent<Image>().color = Color.red;
            }
            if (comfort_value3 >= 84 & comfort_value3 <= 98)
            {
                GameObject.Find("TXT11").GetComponent<Text>().text = 5.ToString();
                GameObject.Find("TXT13").GetComponent<Image>().color = Color.red;
            }
            if (comfort_value3 >= 98 & comfort_value3 <= 112)
            {
                GameObject.Find("TXT11").GetComponent<Text>().text = 5.ToString();
                GameObject.Find("TXT13").GetComponent<Image>().color = Color.red;
            }
            if (comfort_value3 >= 112 & comfort_value3 <= 126)
            {
                GameObject.Find("TXT11").GetComponent<Text>().text = 5.ToString();
                GameObject.Find("TXT13").GetComponent<Image>().color = Color.red;
            }
            if (comfort_value3 >=126& comfort_value3 <= 140)
            {
                GameObject.Find("TXT11").GetComponent<Text>().text = 5.ToString();
                GameObject.Find("TXT13").GetComponent<Image>().color = Color.red;
            }
            //else
            //{
            //angle3 = GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm").GetComponent<Transform>().localEulerAngles.y;
            // comfort_value3 = 0.0001f * angle3 * angle3 - 0.0218f * angle3 + 0.9651f;
            // }
        }
    }
}
