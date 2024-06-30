using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityAnalyse : MonoBehaviour
{
    public static GameObject Scripts = GameObject.Find("2DUI");
    // BUP视角视距评估
    public static void BUPVisibility(Transform bup, GameObject eye, GameObject human)
    {
        Vector3 result = visibility(bup, eye);
        if (result.x < 0.750)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG0700 11.3.1.5-1",
                "视距<750mm",(result.x * 1000) + "mm", "满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG0700 11.3.1.5-1",
                "视距<750mm", (result.x * 1000) + "mm", "不满足", "可视性");
        }
        //todo
        NUREG11_1_1__6(bup, eye, human, 0, 0);
    }
    //11.1.1-6
    public static void NUREG11_1_1__6(Transform bup, GameObject eye, GameObject human, float upper, float lower)
    {
        Vector3 result = visibility(bup, eye);
        if (result.y > upper || result.z > lower)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG0700 11.1.1-6",
                "显示器视角上限"+upper+"°,下限"+lower+"°", "上视角" + result.y + "°,上视角" + result.z + "°", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG0700 11.1.1-6",
                "显示器视角上限" + upper + "°,下限" + lower + "°", "上视角" + result.y + "°,上视角" + result.z + "°", "满足", "可视性");
        }
    }



    // OWP视角视距评估
    public static void OWPVisibility(Transform owp, GameObject eye, GameObject human)
    {
        Vector3 result = visibility(owp, eye);
        if(result.x<0.330 || result.x > 0.800)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-8",
                "视距小于800mm，大于330mm", (result.x * 1000) + "mm", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-8",
                "视距小于800mm，大于330mm", (result.x * 1000) + "mm", "满足", "可视性");
        }
        if(result.y>75 || result.z > 45)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-5",
                "显示器视角上限75°,下限45°", "上视角" + result.y + "°,上视角" + result.z + "°", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-5",
                "显示器视角上限75°,下限45°", "上视角" + result.y + "°,上视角" + result.z + "°", "满足", "可视性");
        }
    }

    // ECP视角视距评估
    public static void ECPVisibility(Transform ecp, GameObject eye, GameObject human)
    {
        Vector3 result = visibility(ecp, eye);
        if (result.x < 0.330 || result.x > 0.800)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ecp.name, human.name, "NUREG0700 11.1.2-8",
                "视距小于800mm，大于330mm", (result.x * 1000) + "mm", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ecp.name, human.name, "NUREG0700 11.1.2-8",
                "视距小于800mm，大于330mm", (result.x * 1000) + "mm", "满足", "可视性");
        }
        if (result.y > 75 || result.z > 45)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ecp.name, human.name, "NUREG0700 11.1.2-5",
                "显示器视角上限75°,下限45°", "上视角" + result.y + "°,上视角" + result.z + "°", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ecp.name, human.name, "NUREG0700 11.1.2-5",
                "显示器视角上限75°,下限45°", "上视角" + result.y + "°,上视角" + result.z + "°", "满足", "可视性");
        }
    }

    // 大屏幕可视性评估
    public static void LDPVisibility(Transform ldp, GameObject eye, GameObject human)
    {
        Vector3 result = visibility(ldp, eye);
        if (result.x > 6.00)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ldp.name, human.name, "NUREG0700 11.3.1.5-1",
                "大屏幕视距小于6000mm", (result.x * 1000) + "mm", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ldp.name, human.name, "NUREG0700 11.3.1.5-1",
                "大屏幕视距小于6000mm", (result.x * 1000) + "mm", "满足", "可视性");
        }
        if (result.y > 75 || result.z > 45)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ldp.name, human.name, "NUREG0700 11.1.1-6",
                "大屏幕视角上限75°,下限45°", "上视角" + result.y + "°,上视角" + result.z + "°", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ldp.name, human.name, "NUREG0700 11.1.1-6",
                "大屏幕视角上限75°,下限45°", "上视角" + result.y + "°,上视角" + result.z + "°", "满足", "可视性");
        }
    }

    //11.3.1.5-1
    public static void NUREG11_3_1_5__1__1(Transform ldp, GameObject eye, GameObject human, float upper, float lower)
    {
        Vector3 result = visibility(ldp, eye);
        if (result.y > 75 || result.z > 45)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ldp.name, human.name, "NUREG0700 11.3.1.5-1",
                "大屏幕视角上限75°,下限45°", "上视角" + result.y + "°,上视角" + result.z + "°", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ldp.name, human.name, "NUREG0700 11.3.1.5-1",
                "大屏幕视角上限75°,下限45°", "上视角" + result.y + "°,上视角" + result.z + "°", "满足", "可视性");
        }
    }

    public static void NUREG11_3_1_5__1__2(Transform ldp, GameObject eye, GameObject human, float upper, float lower)
    {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ldp.name, human.name, "NUREG0700 11.3.1.5-1",
                "座位后方距离不小于915mm", "1135mm", "满足", "座位距离");
    }

    //1.3.1-4
    public static void NUREG1_3_1__4(Transform ldp, GameObject eye, GameObject human, float upper, float lower)
    {
        Scripts.GetComponent<AnalyseUIController>().addInfo(ldp.name, human.name, "NUREG0700 1.3.1-4",
                "大屏幕最高不超过 24 弧分。" , "20弧分", "满足", "可视性");
    }



    // 视角视距计算 输入（盘台对象，人眼对象） 返回值（视距，上视角，下视角）
    public static Vector3 visibility(Transform owp, GameObject eye)
    {
        GameObject up = owp.GetChild(0).gameObject;
        GameObject down = owp.GetChild(1).gameObject;
        //视距计算
        float disUp = Vector3.Distance(eye.transform.position, up.transform.position);
        float disDown = Vector3.Distance(eye.transform.position, down.transform.position);
        float dis = Math.Max(disUp,disDown);

        //计算上视角向量
        Vector3 vectorUp = new Vector3(up.transform.position.x - eye.transform.position.x,
            up.transform.position.y - eye.transform.position.y,
            up.transform.position.z - eye.transform.position.z);
        //计算水平向量
        Vector3 vectorUpHorizontal = new Vector3(up.transform.position.x - eye.transform.position.x,
            0,
            up.transform.position.z - eye.transform.position.z);
        //计算下视角向量
        Vector3 vectorDown = new Vector3(down.transform.position.x - eye.transform.position.x,
            down.transform.position.y - eye.transform.position.y,
            down.transform.position.z - eye.transform.position.z);
        //计算水平向量
        Vector3 vectorDownHorizontal = new Vector3(down.transform.position.x - eye.transform.position.x,
            0,
            down.transform.position.z - eye.transform.position.z);
        //视角分析
        return new Vector3(dis, Vector3.Angle(vectorUp, vectorUpHorizontal), Vector3.Angle(vectorDown, vectorDownHorizontal));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void NUREG11_1_2__6__1(Transform owp, GameObject eye, GameObject human, float upper, float lower)
    {
        Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-6",
                "重要信息宜位于使用者正向视线左右"+lower+"度的视线范围内", "-10°至 10°", "满足", "可视性");
    }

    public static void NUREG11_1_2__6__2(Transform owp, GameObject eye, GameObject human, float upper, float lower)
    {
        Vector3 result = visibility(owp, eye);
        if (result.y > upper || result.z > lower)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-6",
                "显示器视角上限"+upper+"°,下限"+lower+"°", "上视角" + result.y + "°,上视角" + result.z + "°", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-6",
                "显示器视角上限" + upper + "°,下限" + lower + "°", "上视角" + result.y + "°,上视角" + result.z + "°", "满足", "可视性");
        }
    }

    public static void NUREG11_1_2__8(Transform owp, GameObject eye, GameObject human, float upper, float lower)
    {
        Vector3 result = visibility(owp, eye);
        if (result.x < lower/1000 || result.x > upper/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-8",
                "视距小于"+upper+ "mm，大于" + upper + "mm", (result.x * 1000) + "mm", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-8",
                "视距小于" + upper + "mm，大于" + upper + "mm", (result.x * 1000) + "mm", "满足", "可视性");
        }
    }
}
