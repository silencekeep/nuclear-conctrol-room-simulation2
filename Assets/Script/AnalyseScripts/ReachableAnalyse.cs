using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachableAnalyse : MonoBehaviour
{
    public static GameObject Scripts = GameObject.Find("2DUI");
    // BUP可触及范围评估
    public static void BUPReachable(Transform bup, GameObject leftShoulder, GameObject rightShoulder, GameObject human)
    {
        float res = reachDistance(bup);
        if (res < 0.076)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG0700 11.1.2-4",
                "控制器至桌面边缘距离>76mm", (res * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG0700 11.1.2-4",
                "控制器至桌面边缘距离>76mm", (res * 1000) + "mm", "满足", "可达性");
        }
        if (res >= 0.635)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG0700 11.1.2-5",
                "控制器至桌面边缘距离<635mm", (res * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG0700 11.1.2-5",
                "控制器至桌面边缘距离<635mm", (res * 1000) + "mm", "满足", "可达性");
        }
        float ret = shoulderDistance(bup, leftShoulder, rightShoulder);
        if (ret >= 0.792)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG0700 11.1.2-2、3",
                "肩部至操作台<792mm", (ret * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG0700 11.1.2-2、3",
                "肩部至操作台<792mm", (ret * 1000) + "mm", "满足", "可达性");
        }
    }

    // OWP可触及范围评估
    public static void OWPReachable(Transform owp, GameObject human)
    {
        float res = reachDistance(owp);
        if (res < 0.08)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-2",
                "控制器和键盘离桌面前沿至少80mm", (res * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-2",
                "控制器和键盘离桌面前沿至少80mm", (res * 1000) + "mm", "满足", "可达性");
        }
        if (res >= 0.614)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-3",
                "控制器至桌面边缘<614mm", (res * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-3",
                "控制器至桌面边缘<614mm", (res * 1000) + "mm", "满足", "可达性");
        }
    }
    // OWP高度、书写空间评估
    public static void OWPAnalyse(Transform owp, GameObject human)
    {
        float height = getHeight(owp);
        if (height < 0.66 || height > 0.79)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-1",
                "控制台桌面高度660-790mm", (height * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-1",
                "控制台桌面高度660-790mm", (height * 1000) + "mm", "满足", "可达性");
        }
        float area = getWriteArea(owp);
        if (area < 0.4)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-5",
                "预留600mm x 400mm的书写空间", "600mm x "+(area * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-5",
                "预留600mm x 400mm的书写空间", "600mm x " + (area * 1000) + "mm", "满足", "可达性");
        }
    }

    // 11.1.2-1
    public static void NUREG11_1_2__1(Transform owp, GameObject human, float upper, float lower) {
        float height = owp.GetChild(0).gameObject.transform.position.y;
        if (height < lower/1000 || height > upper/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-1",
                "控制台桌面高度"+lower+"-"+upper+"mm", (height * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-1",
                "控制台桌面高度" + lower + "-" + upper + "mm", (height * 1000) + "mm", "满足", "可达性");
        }
    }

    // ECP可触及范围评估
    public static void ECPReachable(Transform ecp, GameObject human)
    {
        float res = reachDistance(ecp);
        if (res < 0.08)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ecp.name, human.name, "NUREG0700 11.1.2-2",
                "控制器和键盘离桌面前沿至少80mm", (res * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ecp.name, human.name, "NUREG0700 11.1.2-2",
                "控制器和键盘离桌面前沿至少80mm", (res * 1000) + "mm", "满足", "可达性");
        }
        if (res >= 0.614)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ecp.name, human.name, "NUREG0700 11.1.2-3",
                "控制器至桌面边缘<614mm", (res * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(ecp.name, human.name, "NUREG0700 11.1.2-3",
                "控制器至桌面边缘<614mm", (res * 1000) + "mm", "满足", "可达性");
        }
    }

    //控制器桌面距离计算
    public static float reachDistance(Transform ecp)
    {
        GameObject down = ecp.GetChild(1).gameObject;
        GameObject outObj = ecp.GetChild(2).gameObject;
        float depthx = Math.Abs(outObj.transform.position.x - down.transform.position.x);
        float depthz = Math.Abs(outObj.transform.position.z - down.transform.position.z);
        float depth = (float)Math.Pow(Math.Pow(depthx, 2) + Math.Pow(depthz, 2), 0.5);
        return depth;
    }
    //肩部操作距离计算
    public static float shoulderDistance(Transform bup, GameObject leftShoulder, GameObject rightShoulder)
    {
        GameObject down = bup.GetChild(1).gameObject;
        float disLeft = Vector3.Distance(leftShoulder.transform.position, down.transform.position);
        float disRight = Vector3.Distance(rightShoulder.transform.position, down.transform.position);
        float dis = Math.Max(disLeft, disRight);
        return dis;
    }
    //操作台高度计算
    public static float getHeight(Transform owp)
    {
        GameObject outObj = owp.GetChild(2).gameObject;
        float height = outObj.transform.position.y;
        return (height+0.05f);
    }
    //书写空间计算
    public static float getWriteArea(Transform owp)
    {
        GameObject down = owp.GetChild(1).gameObject;
        GameObject outObj = owp.GetChild(2).gameObject;
        float depthx = Math.Abs(outObj.transform.position.x - down.transform.position.x);
        float depthz = Math.Abs(outObj.transform.position.z - down.transform.position.z);
        float depth = (float)Math.Pow(Math.Pow(depthx, 2) + Math.Pow(depthz, 2), 0.5);
        return (depth - 0.1f);
    }

    //N'11.1.1-2', N'BUP', N'控制器高度', N'对于站姿控制台，控制器最高宜位于 5%成年女性无需费力即可触及的位置；最低宜位于 95%成年男性无需费力即可触及的位置。', N'792', N'614')
    public static void NUREG11_1_1__2(Transform owp, GameObject shoulder,GameObject human, float upper, float lower)
    {
        float res = Vector3.Distance(owp.GetChild(0).localPosition, shoulder.transform.localPosition);
        if (res > upper/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.1-2",
                "控制器最高宜位于5%成年女性无需费力即可触及的位置", (res * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-2",
                "控制器最高宜位于 5%成年女性无需费力即可触及的位置", (res * 1000) + "mm", "满足", "可达性");
        }
    }

    //N'11.1.1-3' 控制台深度', N'控制台及其深度宜保证所有的控制器位于 5%成年女性无需费力即可触及的位置。', N'792', N'614')
    public static void NUREG11_1_1__3(Transform owp, GameObject shoulder, GameObject human, float upper, float lower)
    {
        float res = Vector3.Distance(owp.GetChild(1).localPosition, shoulder.transform.localPosition);
        if (res > upper / 1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.1-3",
                "控制台及其深度宜保证所有的控制器位于5%成年女性无需费力即可触及的位置", (res * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.1-3",
                "控制台及其深度宜保证所有的控制器位于5%成年女性无需费力即可触及的位置", (res * 1000) + "mm", "满足", "可达性");
        }
    }

    //N'控制器深度', N'控制器应至少距离控制台边缘约 3 英寸（76mm）以防止误触发。', N'', N'76')
    public static void NUREG11_1_1__4(Transform owp,  GameObject human, float upper, float lower)
    {
        float res = reachDistance(owp);
        if (res < lower / 1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.1-3",
                "控制器应至少距离控制台边缘约 3 英寸（76mm）", (res * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.1-3",
                "控制器应至少距离控制台边缘约 3 英寸（76mm）", (res * 1000) + "mm", "满足", "可达性");
        }
    }

    internal static void NUREG11_1_2__3(Transform owp, GameObject shoulder, GameObject human, float upper, float lower)
    {
        float res = Vector3.Distance(shoulder.transform.position, owp.position);
        if (res < lower / 1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-3",
                "控制器和键盘的位置应位于 5%的成年女性坐姿状态下功能伸展范围内", (res * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.2-3",
                "控制器和键盘的位置应位于 5%的成年女性坐姿状态下功能伸展范围内。", (res * 1000) + "mm", "满足", "可达性");
        }
    }

    //N'控制器深度', N'控制器距离控制台边缘不宜超过 25 英寸（635mm）', N'635', N'')
    public static void NUREG11_1_1__5(Transform owp, GameObject human, float upper, float lower)
    {
        float res = reachDistance(owp);
        if (res > upper / 1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.1-3",
                "控制器应至少距离控制台边缘约 3 英寸（76mm）", (res * 1000) + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG0700 11.1.1-3",
                "控制器应至少距离控制台边缘约 3 英寸（76mm）", (res * 1000) + "mm", "满足", "可达性");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
