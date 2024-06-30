using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class contsize : MonoBehaviour
{
    public GameObject DZTimelinePanel;
    public GameObject TimeAxis;
    public GameObject contentUI;
   // public GameObject Track1;
    float Track1Length;
    float Track2Length;
    float Track3Length;
    GameObject Track1;
    GameObject Track2;
    GameObject Track3;

   // private GameObject imagecontent;
    public Text TimeTextPrefab;
    //public GameObject contentui;
    // Start is called before the first frame update
    void Start()
    {
        if (DZTimelinePanel.activeInHierarchy == true)
        {
            for (int i = 0; i * 55f < 100000 / 1.0f; i++)//设置时间轴刻度
            {
                Text TimeText = Instantiate(TimeTextPrefab, transform.position, Quaternion.identity) as Text;
                TimeText.transform.SetParent(TimeAxis.transform);
                TimeText.text = (i * 60).ToString();
                TimeText.transform.Translate(new Vector3(TimeAxis.transform.localPosition.x + (60f * i), TimeText.transform.localPosition.y - 60, TimeText.transform.localPosition.z));

                //TimeLabel.transform.Translate((local2Screen(realPosition, _yPoint) - new Vector2(-40f * i, 20), new Vector2(0, 0)), (i * 10).ToString());
                //int timenumber = 0;
                //GUI.Label(new Rect(local2Screen(realPosition, _yPoint) - new Vector2(-40f * i, 20), new Vector2(0, 0)), ( i * 10).ToString(), guiStyleY);
            }
        }
        else
        {
            for (int i = 0; i * 55f < 100000 / 1.0f; i++)//设置时间轴刻度
            {
                Text TimeText = Instantiate(TimeTextPrefab, transform.position, Quaternion.identity) as Text;
                TimeText.transform.SetParent(TimeAxis.transform);
                TimeText.text = (i * 6).ToString();
                TimeText.transform.Translate(new Vector3(TimeAxis.transform.localPosition.x + (60f * i), TimeText.transform.localPosition.y - 60, TimeText.transform.localPosition.z));

                //TimeLabel.transform.Translate((local2Screen(realPosition, _yPoint) - new Vector2(-40f * i, 20), new Vector2(0, 0)), (i * 10).ToString());
                //int timenumber = 0;
                //GUI.Label(new Rect(local2Screen(realPosition, _yPoint) - new Vector2(-40f * i, 20), new Vector2(0, 0)), ( i * 10).ToString(), guiStyleY);
            }
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (DZTimelinePanel.activeInHierarchy == true)
        {
            GameObject TimeScrollView = GameObject.Find("TimeScrollView");

            if (HumanNumber.humanNum == 1)
            {
                Track1 = GameObject.Find("DZTrack1");
                Track1Length = Track1.GetComponent<RectTransform>().sizeDelta.x;
                Track2Length = 0;
                Track3Length = 0;
            }
            else if (HumanNumber.humanNum == 2)
            {
                Track1 = GameObject.Find("DZTrack1");
                Track2 = GameObject.Find("DZTrack2");
                Track1Length = Track1.GetComponent<RectTransform>().sizeDelta.x;
                Track2Length = Track2.GetComponent<RectTransform>().sizeDelta.x;
                Track3Length = 0;
            }
            else if (HumanNumber.humanNum == 3)
            {
                Track1 = GameObject.Find("DZTrack1");
                Track2 = GameObject.Find("DZTrack2");
                Track3 = GameObject.Find("DZTrack3");
                Track1Length = Track1.GetComponent<RectTransform>().sizeDelta.x;
                Track2Length = Track2.GetComponent<RectTransform>().sizeDelta.x;
                Track3Length = Track3.GetComponent<RectTransform>().sizeDelta.x;                
            }
           
            //float Track1Length = Track1.GetComponent<RectTransform>().sizeDelta.x;
            //float Track2Length = Track2.GetComponent<RectTransform>().sizeDelta.x;
            //float Track3Length = Track3.GetComponent<RectTransform>().sizeDelta.x;
            float maxlength = Track1Length >= Track2Length ? Track1Length : Track2Length;
            maxlength = maxlength >= Track3Length ? maxlength : Track3Length;
            contentUI.GetComponent<RectTransform>().sizeDelta = new Vector2(maxlength + 100, contentUI.GetComponent<RectTransform>().sizeDelta.y);
            TimeAxis.GetComponent<RectTransform>().sizeDelta = new Vector2(maxlength + 100, TimeAxis.GetComponent<RectTransform>().sizeDelta.y);
        }
        else
        {
            GameObject DZTimeScrollView = GameObject.Find("TimeScrollView");
            if (HumanNumber.humanNum == 1)
            {
                Track1 = GameObject.Find("Track1");
                Track1Length = Track1.GetComponent<RectTransform>().sizeDelta.x;
                Track2Length = 0;
                Track3Length = 0;
            }
            else if (HumanNumber.humanNum == 2)
            {
                Track1 = GameObject.Find("Track1");
                Track2 = GameObject.Find("Track2");
                Track1Length = Track1.GetComponent<RectTransform>().sizeDelta.x;
                Track2Length = Track2.GetComponent<RectTransform>().sizeDelta.x;
                Track3Length = 0;
            }
            else if (HumanNumber.humanNum == 3)
            {
                Track1 = GameObject.Find("Track1");
                Track2 = GameObject.Find("Track2");
                Track3 = GameObject.Find("Track3");
                Track1Length = Track1.GetComponent<RectTransform>().sizeDelta.x;
                Track2Length = Track2.GetComponent<RectTransform>().sizeDelta.x;
                Track3Length = Track3.GetComponent<RectTransform>().sizeDelta.x;
            }
            //GameObject Track1 = GameObject.Find("Track1");
            //GameObject Track2 = GameObject.Find("Track2");
            //GameObject Track3 = GameObject.Find("Track3");
            //float Track1Length = Track1.GetComponent<RectTransform>().sizeDelta.x;
            //float Track2Length = Track2.GetComponent<RectTransform>().sizeDelta.x;
            //float Track3Length = Track3.GetComponent<RectTransform>().sizeDelta.x;
            float maxlength = Track1Length >= Track2Length ? Track1Length : Track2Length;
            maxlength = maxlength >= Track3Length ? maxlength : Track3Length;
            contentUI.GetComponent<RectTransform>().sizeDelta = new Vector2(maxlength + 100, contentUI.GetComponent<RectTransform>().sizeDelta.y);
            TimeAxis.GetComponent<RectTransform>().sizeDelta = new Vector2(maxlength + 100, TimeAxis.GetComponent<RectTransform>().sizeDelta.y);
        }
        //contentUI.GetComponent<RectTransform>().sizeDelta = new Vector2(10000, contentUI.GetComponent<RectTransform>().sizeDelta.y);
        //if(contentUI.GetComponent<RectTransform>().sizeDelta.x >= TimeScrollView.GetComponent<RectTransform>().sizeDelta.x)
        // {
        //     GraphAxis.length = 2*TimeScrollView.GetComponent<RectTransform>().sizeDelta.x + 300;
        // }

    }
}
