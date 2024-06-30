using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_comfort3 : MonoBehaviour
{
    zt_comfort_click2 click4;
    public float angle2;
    public float comfort_value2;
    public int a2 = 0;

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
            angle2 = GameObject.Find("swat/swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:LeftShoulder/swat:LeftArm").GetComponent<Transform>().localEulerAngles.y;

            //a1++;
            //}

            if (angle2 >= -60 & angle2 < -37)
            {
                GameObject.Find("TXT12").GetComponent<Text>().text = 2.ToString();
                GameObject.Find("TXT14").GetComponent<Image>().color = Color.blue;
            }
            if (angle2 >= -37 & angle2 < -14)
            {
                GameObject.Find("TXT12").GetComponent<Text>().text = 2.ToString();
                GameObject.Find("TXT14").GetComponent<Image>().color = Color.blue;
            }
            if (angle2 >= -14 & angle2 < 9)
            {
                GameObject.Find("TXT12").GetComponent<Text>().text = 1.ToString();
                GameObject.Find("TXT14").GetComponent<Image>().color = Color.green;
            }
            if (angle2 >= 9 & angle2 < 32)
            {
                GameObject.Find("TXT12").GetComponent<Text>().text = 2.ToString();
                GameObject.Find("TXT14").GetComponent<Image>().color = Color.blue;
            }
            if (angle2 >= 32 & angle2 <= 55)
            {
                GameObject.Find("TXT12").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT14").GetComponent<Image>().color = Color.yellow;
            }
            if (angle2 >= 55 & angle2 <= 78)
            {
                GameObject.Find("TXT12").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT14").GetComponent<Image>().color = Color.yellow;
            }
            if (angle2 >= 78 & angle2 <= 101)
            {
                GameObject.Find("TXT12").GetComponent<Text>().text = 3.ToString();
                GameObject.Find("TXT14").GetComponent<Image>().color = Color.yellow;
            }
            if (angle2 >= 101 & angle2 <= 124)
            {
                GameObject.Find("TXT12").GetComponent<Text>().text = 4.ToString();
                GameObject.Find("TXT14").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            if (angle2 >= 124 & angle2 <= 147)
            {
                GameObject.Find("TXT12").GetComponent<Text>().text = 4.ToString();
                GameObject.Find("TXT14").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            if (angle2 >= 147 & angle2 <= 170)
            {
                GameObject.Find("TXT12").GetComponent<Text>().text = 4.ToString();
                GameObject.Find("TXT14").GetComponent<Image>().color = new Color((176 / 255f), (144 / 255f), (46 / 255f), (255 / 255f));
            }
            //else
            //{
            //GameObject.Find("TXT7").GetComponent<Text>().text = 1.ToString();
            //GameObject.Find("TXT8").GetComponent<Image>().color = Color.red;
            //}
        }
    }
}
