using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kedaxingfenxi : MonoBehaviour
{
    public GameObject caozuodian,thispop,hand1;
    public Dropdown dp;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (thispop.activeInHierarchy)
        {
            text.text = "距离差：" + KEDA(hand1.transform.position , caozuodian.transform.position);
        }
    }

    public string KEDA(Vector3 P1, Vector3 P2)
    {
        float dis;
        dis = Vector3.Distance(P1,P2);
        return dis.ToString();
        //return "∞";
    }

}
