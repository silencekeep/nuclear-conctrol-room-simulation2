using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keshixingfenxi : MonoBehaviour
{
    public Text hitlist1, hitlist2;
    public Button LB,RB;
    public LayerMask LM;

    public GameObject zuoshou1;
    public GameObject youshou1;
    public GameObject zuoshou2;
    public GameObject youshou2;
    public GameObject zuoshou3;
    public GameObject youshou3;
    public Dropdown xuanren;
    public GameObject thisPop;
    public GameObject eye1;
    public GameObject eye2;
    public GameObject eye3;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (thisPop.activeInHierarchy)
        {
            YunXing();
        }
    }

    public string KESHI(GameObject shou, GameObject eye)
    {
        RaycastHit hit;
        /*LM = LayerMask.GetMask("EyeBlock")*/;
        //if (Physics.Linecast(eye.transform.position, shou.transform.position, out hit, LM))
        //{
        //    Debug.Log("blocked:" + hit.transform.name);
        //    return hit.transform.name;
        //}
        //else { return "0"; }

        Ray ray = new Ray(eye.transform.position, shou.transform.position - eye.transform.position);

        Debug.DrawLine(eye.transform.position, shou.transform.position - eye.transform.position,Color.red);

        Physics.Raycast(ray, out hit, LM);

        if (hit.transform != null)
        {
            Debug.Log("blocked:" + hit.transform.name);
            return hit.transform.name;
        }
        else { return "0"; }
    }

    void YunXing()
    {
        switch (xuanren.value)
        {
            case 0:
                if (KESHI(zuoshou1,eye1) == "0")
                {
                    hitlist1.text = "左手可视\n";
                    LB.GetComponent<Image>().color = Color.green;
                }
                if (KESHI(zuoshou1, eye1) != "0")
                {
                    hitlist1.text = "遮挡:" + KESHI(zuoshou1, eye1);
                    //LB.GetComponent<Image>().color = Color.green;
                }
                if (KESHI(youshou1, eye1) == "0")
                {
                    hitlist2.text = "右手可视\n";
                    RB.GetComponent<Image>().color = Color.green;
                }
                if (KESHI(youshou1, eye1) != "0")
                {
                    hitlist2.text = "遮挡:" + KESHI(youshou1, eye1);
                    //LB.GetComponent<Image>().color = Color.green;
                }
                break;
            case 1:
                if (KESHI(zuoshou2, eye2) == "0")
                {
                    hitlist1.text = "左手可视\n";
                    LB.GetComponent<Image>().color = Color.green;
                }
                if (KESHI(zuoshou2, eye2) != "0")
                {
                    hitlist1.text = "遮挡:" + KESHI(zuoshou2, eye2);
                    //LB.GetComponent<Image>().color = Color.green;
                }
                if (KESHI(youshou2, eye2) == "0")
                {
                    hitlist2.text = "右手可视\n";
                    RB.GetComponent<Image>().color = Color.green;
                }
                if (KESHI(youshou2, eye2) != "0")
                {
                    hitlist2.text = "遮挡:" + KESHI(youshou2, eye2);
                    //LB.GetComponent<Image>().color = Color.green;
                }
                break;
            case 2:
                if (KESHI(zuoshou3, eye3) == "0")
                {
                    hitlist1.text = "左手可视\n";
                    LB.GetComponent<Image>().color = Color.green;
                }
                if (KESHI(zuoshou3, eye3) != "0")
                {
                    hitlist1.text = "遮挡:" + KESHI(zuoshou1, eye3);
                    //LB.GetComponent<Image>().color = Color.green;
                }
                if (KESHI(youshou3, eye3) == "0")
                {
                    hitlist2.text = "右手可视\n";
                    RB.GetComponent<Image>().color = Color.green;
                }
                if (KESHI(youshou3, eye3) != "0")
                {
                    hitlist2.text = "遮挡:" + KESHI(youshou3, eye3);
                    //LB.GetComponent<Image>().color = Color.green;
                }
                break;
        }
    }
}
