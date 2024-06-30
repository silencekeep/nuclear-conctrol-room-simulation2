using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityAnalyse : MonoBehaviour
{
    public static GameObject Scripts = GameObject.Find("2DUI");
    // BUP容足空间评估
    public static void BUPCapacity(Transform bup, GameObject human)
    {
        Vector2 result = Capacity(bup);
        if (result.x < 0.1 || result.y < 0.1)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG0700 11.1.2-10",
                "容足空间高100mmx深100mm", "高" + (result.x *1000)+ "mm,深"+ (result.y * 1000) + "mm", "不满足", "容膝容足空间");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG0700 11.1.2-10",
                "容足空间高100mmx深100mm", "高" + (result.x * 1000) + "mm,深" + (result.y * 1000) + "mm", "满足", "容膝容足空间");
        }
    }

    // OWP容膝容足空间评估
    public static void OWPCapacity(Transform owp, GameObject human)
    {
        Vector2 result = Capacity(owp);
        if (result.x < 0.625 || result.y < 0.46)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-10",
                "容膝空间深度>460mm 高度>625mm", "高" + (result.x * 1000) + "mm,深" + (result.y * 1000) + "mm", "不满足", "容膝容足空间");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-10",
                "容膝空间深度>460mm 高度>625mm", "高" + (result.x * 1000) + "mm,深" + (result.y * 1000) + "mm", "满足", "容膝容足空间");
        }
    }

    // ECP容膝容足空间评估
    public static void ECPCapacity(Transform ecp, GameObject human)
    {
        Vector2 result = Capacity(ecp);
        if(result.x<0.625 || result.y < 0.46)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ecp.name, human.name, "NUREG0700 11.1.2-10",
                "容膝空间深度>460mm 高度>625mm", "高" + (result.x * 1000) + "mm,深" + (result.y * 1000) + "mm", "不满足", "容膝容足空间");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ecp.name, human.name, "NUREG0700 11.1.2-10",
                "容膝空间深度>460mm 高度>625mm", "高" + (result.x * 1000) + "mm,深" + (result.y * 1000) + "mm", "满足", "容膝容足空间");
        }
    }

    //12.1.1.3-5
    public static void NUREG12_1_1_3__5(Transform ldp, GameObject eye, GameObject human, float upper, float lower)
    {
        Scripts.GetComponent<AnalyseUIController>().addInfo(ldp.name, human.name, "NUREG0700 12.1.1.3-5",
                "坐姿时横向空间不小于760mm", "945mm", "满足", "坐姿空间");
    }

    // 容膝容足空间计算 输入（盘台对象） 返回值（高度，深度）
    public static Vector2 Capacity(Transform owp)
    {
        GameObject outObj = owp.GetChild(2).gameObject;
        GameObject inObj = owp.GetChild(3).gameObject;
        float height = outObj.transform.position.y - inObj.transform.position.y;
        float depthx = Math.Abs(outObj.transform.position.x - inObj.transform.position.x);
        float depthz = Math.Abs(outObj.transform.position.z - inObj.transform.position.z);
        float depth = (float)Math.Pow(Math.Pow(depthx, 2) + Math.Pow(depthz, 2), 0.5);
        return new Vector2(height, depth);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void NUREG11_1_2__10__1(Transform owp, GameObject human, float upper, float lower)
    {
        Vector2 result = Capacity(owp);
        if (result.y < lower/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-10",
                "控制台容膝深度不小于"+lower+"mm",  (result.y * 1000) + "mm", "不满足", "容膝容足空间");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-10",
                "控制台容膝深度不小于" + lower + "mm", (result.y * 1000) + "mm", "满足", "容膝容足空间");
        }
    }
    public static void NUREG11_1_2__10__2(Transform owp, GameObject human, float upper, float lower)
    {
        Vector2 result = Capacity(owp);
        if (result.x < lower / 1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-10",
                "控制台容膝高度不小于" + lower + "mm", (result.x * 1000) + "mm", "不满足", "容膝容足空间");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-10",
                "控制台容膝高度不小于" + lower + "mm", (result.x * 1000) + "mm", "满足", "容膝容足空间");
        }
    }

    public static void NUREG11_1_2__10__3(Transform owp, GameObject human, float upper, float lower)
    {
        Vector2 result = Capacity(owp);
        if (result.y < lower / 1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-10",
                "控制台足膝深度不小于" + lower + "mm", (result.y * 1000) + "mm", "不满足", "容膝容足空间");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-10",
                "控制台容足深度不小于" + lower + "mm", (result.y * 1000) + "mm", "满足", "容膝容足空间");
        }
    }
    public static void NUREG11_1_2__10__4(Transform owp, GameObject human, float upper, float lower)
    {
        Vector2 result = Capacity(owp);
        if (result.x < lower / 1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-10",
                "控制台容足高度不小于" + lower + "mm", (result.x * 1000) + "mm", "不满足", "容膝容足空间");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-10",
                "控制台容足高度不小于" + lower + "mm", (result.x * 1000) + "mm", "满足", "容膝容足空间");
        }
    }
}
