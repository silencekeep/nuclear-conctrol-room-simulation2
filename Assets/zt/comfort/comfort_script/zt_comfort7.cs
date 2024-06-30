using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_comfort7 : MonoBehaviour
{
    zt_comfort_click2 click7;
    public float angle6;
    public float comfort_value6;
    public int a3 = 0;

    void Start()
    {
        click7 = FindObjectOfType<zt_comfort_click2>();
    }

    // Update is called once per frame
    void Update()
    {

        if (click7.lister1 == 1)
        {
            //if (a1 == 0)
            //{
            angle6 = GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm/swat:LeftHand").GetComponent<Transform>().localEulerAngles.z-360;
            comfort_value6 = angle6;
            //a1++;
            //}

            if (comfort_value6 >= -70 & comfort_value6 < -55)
            {
                GameObject.Find("TXT22").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT23").GetComponent<Image>().color = Color.blue;
            }
            if (comfort_value6 >= -55 & comfort_value6 < -40)
            {
                GameObject.Find("TXT22").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT23").GetComponent<Image>().color = Color.blue;
            }
            if (comfort_value6 >= -40 & comfort_value6 < -25)
            {
                GameObject.Find("TXT22").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT23").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value6 >= -25 & comfort_value6 < -10)
            {
                GameObject.Find("TXT22").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT23").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value6 >= -10 & comfort_value6 <= 5)
            {
                GameObject.Find("TXT22").GetComponent<Text>().text = 2.ToString();
                GameObject.Find("TXT23").GetComponent<Image>().color = Color.blue;
            }
            if (comfort_value6 >= 5 & comfort_value6 <= 20)
            {
                GameObject.Find("TXT22").GetComponent<Text>().text = 2.ToString();
                GameObject.Find("TXT23").GetComponent<Image>().color = Color.blue;
            }
            if (comfort_value6 >= 20 & comfort_value6 <= 35)
            {
                GameObject.Find("TXT22").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT23").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value6 >= 35 & comfort_value6 <= 50)
            {
                GameObject.Find("TXT22").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT23").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value6 >= 50 & comfort_value6 <= 65)
            {
                GameObject.Find("TXT22").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT23").GetComponent<Image>().color = Color.yellow;
            }
            if (comfort_value6 >= 65 & comfort_value6 <= 80)
            {
                GameObject.Find("TXT22").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT23").GetComponent<Image>().color = Color.yellow;
            }
            //else
            //{
            //angle3 = GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm/swat:LeftForeArm").GetComponent<Transform>().localEulerAngles.y;
            // comfort_value3 = 0.0001f * angle3 * angle3 - 0.0218f * angle3 + 0.9651f;
            // }
        }
    }
}
