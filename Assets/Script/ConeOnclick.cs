using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ConeOnclick : MonoBehaviour
{
    public GameObject left1;
    public GameObject right1;
    public GameObject left2;
    public GameObject right2;
    public GameObject left3;
    public GameObject right3;
    public Toggle LeftTo;
    public Toggle RightTo;
    public Dropdown whichperson;
    public InputField InLenth;
    public InputField InAngle;
    private float length, openAngle;
    public Text HitList;

    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnClick()
    {
        switch (whichperson.value)
        {
            case (0):
                if (LeftTo.isOn)
                {
                    //length = Convert.ToSingle(InLenth.text);
                    //openAngle = Convert.ToSingle(InAngle.text);
                    //left1.transform.localScale = new Vector3(length / 100, length / 100, length / 100);
                    ////锚点位置要改
                    //left1.transform.localScale = new Vector3(openAngle / 40, openAngle / 40, 1);
                    left1.AddComponent<ConeDetect>();
                    left1.GetComponent<ConeDetect>().text = HitList;
                    left1.SetActive(true);
                }
                if (RightTo.isOn)
                {
                    right1.SetActive(true);
                }
                break;
            case (1):
                if (LeftTo.isOn)
                {
                    left2.SetActive(true);
                }
                if (RightTo.isOn)
                {
                    right2.SetActive(true);
                }
                break;
            case (2):
                if (LeftTo.isOn)
                {
                    left3.SetActive(true);
                }
                if (RightTo.isOn)
                {
                    right3.SetActive(true);
                }
                break;
        }

        //if (LeftTo.isOn)
        //{
        //    left.SetActive(true);
        //}
        //if (RightTo.isOn)
        //{
        //    right.SetActive(true);
        //}
    }
}
