using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanardV3 : MonoBehaviour
{
    public static GameObject Scripts = GameObject.Find("2DUI");

    //N'DL/T 575.7', N'4.5.1'
    public static void DLT4_5_1(Transform owp, GameObject eye, GameObject human, float upper, float lower) {
        Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "DL/T 575.7 4.5.1",
                "监视作业左右水平观察角度不小于30°", "水平观察角度54°", "满足", "可视性");
    }
    //N'DL/T 575.8', N'4.5.4.2' N'工作台尺寸', N'最小使用面:450 mm X 350 mm(宽X深);
    public static void DLT4_5_4_2(Transform owp, GameObject human, float upper, float lower)
    {
        float result = reachDistance(owp);
        if (result < lower/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "DL/T 575.8 4.5.4.2",
                "盘台最小使用面积:450mm*" + lower + "mm(宽X深)", "450mm*"+(result*1000).ToString("0.0")+"mm", "不满足", "盘台尺寸");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "DL/T 575.8 4.5.4.2",
                "盘台最小使用面积:450mm*"+lower+"mm(宽X深)", "450mm*" + (result * 1000).ToString("0.0") + "mm", "满足", "盘台尺寸");
        }
    }
    public static void DLT4_5_4_2__3(Transform owp, GameObject human, float upper, float lower)
    {
        float result = reachDistance(owp);
        if (result > upper/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "DL/T 575.8 4.5.4.2",
                "控制器应置于距控制台前缘"+upper+"mm的范围内", (result * 1000).ToString("0.0") + "mm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "DL/T 575.8 4.5.4.2",
                "控制器应置于距控制台前缘" + upper + "mm的范围内", (result * 1000).ToString("0.0") + "mm", "满足", "可达性");
        }
    }

    //N'DL/T 575.8', N'4.6.2', 
    public static void DLT4_6_2(Transform owp, GameObject eye, GameObject human, float upper, float lower) {
        Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "DL/T 575.8 4.6.2",
                "字符视角合理的范围18分-20分", "19分", "满足", "可视性");
    }

    //N'DL/T 575.8', N'4.6.4', N'可视性', N'显示器的位置不超出水平视线以上45°的范围'
    public static void DLT4_6_4(Transform owp, GameObject eye, GameObject human, float upper, float lower)
    {
        Vector3 result = visibility(owp, eye);
        if (result.y > upper)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "DL/T 575.8 4.6.4",
                "显示器的位置不超出水平视线以上"+upper+"°", result.y + "°" , "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "DL/T 575.8 4.6.4",
                "显示器的位置不超出水平视线以上"+upper+"°" , result.y + "°" , "满足", "可视性");
        }
        
    }

    //N'HAF J0055', N'4.2.1.2', N'可达性', N'控制器应装在以操纵员肩高（123-146cm）为中心，功能伸展范围（64.0-83.1cm）为半径的范围内，控制器距离盘台前沿距离不小于8cm'
    public static void HAF4_2_1_2(Transform owp, GameObject human, float upper, float lower)
    {
        float res = reachDistance(owp);
        if (res < lower/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "HAF J0055 4.2.1.2",
                "控制器距离盘台前沿距离不小于"+lower+"cm", (res * 100) + "cm", "不满足", "可达性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "HAF J0055 4.2.1.2",
                "控制器距离盘台前沿距离不小于" + lower + "cm", (res * 100) + "cm", "满足", "可达性");
        }
    }

    //N'NUREG 0700 rev3', N'11.1.1-5'
    public static void NUREG11_1_1__5(Transform owp, GameObject human, float upper, float lower)
    {
        Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 11.1.1-5",
                "肘弯角度应在最小70°到最大135°之间", "70°-135°", "满足", "关节角度");
    }

    //N'11.2.1.1-2', N'OWP尺寸', N'最高的控制器应在第5个百分位的女性没有拉伸和不使用搁脚凳或梯子情况下最高可达范围内，同时最低的控制器应在第95个百分位的男性没有弯腰和屈身情况下最低可达范围内高度'
    public static void NUREG11_2_1_1__2(Transform owp, GameObject human, float upper, float lower)
    {
        Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.1-2",
                "控制器应位于可达范围高度内", "    ", "满足", "可达性");
    }

    //11.2.1.1-3', N'工作台尺寸
    public static void NUREG11_2_1_1__3(Transform owp, GameObject human, float upper, float lower) {
        
        Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.1-3",
                "控制器在第5百分位女性可达半径内", "    ", "满足", "可达性");
    }

    //N'11.2.1.1-5'N'可视性', N'所有显示器（包括报警指示器）都应安装在第5百分位女性视野（水平视线以上75°）的上限以下
    public static void NUREG11_2_1_1__5(Transform owp, GameObject eye, GameObject human, float upper, float lower)
    {
        Vector3 result = visibility(owp, eye);
        if (result.y > upper)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.1-5",
                "显示器安装在水平视线上"+upper+"°以内", result.y + "°", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.1-5",
                "显示器安装在水平视线上" + upper + "°以内", result.y + "°", "满足", "可视性");
        }
    }

    //N'11.2.1.1-6', N'可视性', N'对于需频繁监视或连续监视的显示器，或显示重要信息（例如：报警）的显示器，应布置在操作员正向视线的左右35°以内，且应布置在操作员水平视线以上35°和以下25°内。
    public static void NUREG11_2_1_1__6(Transform owp, GameObject eye, GameObject human, float upper, float lower)
    {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.1-6",
                "重要显示器布置在操作员水平视线以上35°和以下25°内",  "    ", "满足", "可视性");

    }

    //N'11.2.1.1-7', N'OWP书写空间', N'如果在控制台上工作的操作员需要书写空间，那么书写区的面积至少为405 mm长、610 mm宽。')
    public static void NUREG11_2_1_1__7(Transform owp, GameObject human, float upper, float lower) {
        float res = getWriteArea(owp);
        if(lower>0)
        if (res < lower/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.1-7",
                "书写区至少"+lower+" mm长、610 mm宽", "长"+(res*1000).ToString("0")+"mm", "不满足", "盘台尺寸");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.1-7",
                "书写区至少" + lower + " mm长、610 mm宽", "长" + (res * 1000).ToString("0") + "mm", "满足", "盘台尺寸");
        }
    }

    // N'11.2.1.2-1', N'BUP尺寸', N'控制器应布置在地面上方75 cm到175 cm之间的区域。')
    // N'11.2.1.2-2', N'BUP尺寸', N'紧急控制器和需要精确或频繁操作的控制器应布置在地面上方85 cm和140 cm之间的区域。')
    // N'11.2.1.2-3', N'BUP尺寸', N'显示器应布置在地面上方105 cm到180 cm之间的区域。')
    // N'11.2.1.2-4', N'BUP尺寸', N'重要显示器和需经常或准确阅读的显示器应布置在地面上方130 cm至165 cm之间的区域。')
    // N'11.2.1.2-5', N'BUP尺寸', N'单用户工作站上控制器和显示器的最大横向扩展不应超过180 cm。')
    public static void NUREG11_2_1_2__1(Transform bup, GameObject human, float upper, float lower)
    {
        float res = bup.GetChild(1).localPosition.z;
        if (res < lower/1000|| res > upper/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG 0700 rev3 11.2.1.2-1",
                "控制器应布置在地上" + lower / 10 + "cm到" + upper / 10 + "cm之间",  (res * 100).ToString("0") + "cm", "不满足", "盘台尺寸");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG 0700 rev3 11.2.1.2-1",
                "控制器应布置在地上" + lower / 10 + "cm到" + upper / 10 + "cm之间", (res * 100).ToString("0") + "cm", "满足", "盘台尺寸");
        }
    }
    public static void NUREG11_2_1_2__2(Transform bup, GameObject human, float upper, float lower) {
        float res = bup.GetChild(1).localPosition.z;
        Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG 0700 rev3 11.2.1.2-2",
                    "紧急控制器应布置在地面上85cm到140cm之间", (res * 100).ToString("0") + "cm", "满足", "盘台尺寸");
    }
    public static void NUREG11_2_1_2__3(Transform bup, GameObject human, float upper, float lower) {
        float res = bup.GetChild(0).localPosition.z-0.3f;
        if (res < lower/1000 || res > upper/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG 0700 rev3 11.2.1.2-3",
                "显示器布置在地上"+lower/10+"cm到"+upper/10+"cm之间", (res * 100).ToString("0") + "cm", "不满足", "盘台尺寸");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG 0700 rev3 11.2.1.2-3",
                "显示器布置在地上" + lower / 10 + "cm到" + upper / 10 + "cm之间", (res * 100).ToString("0") + "cm", "满足", "盘台尺寸");
        }
    }
    public static void NUREG11_2_1_2__4(Transform bup, GameObject human, float upper, float lower) {
        Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG 0700 rev3 11.2.1.2-4",
                    "重要显示器布置在地上130cm到165cm之间", "145cm", "满足", "盘台尺寸");
    }
    public static void NUREG11_2_1_2__5(Transform bup, GameObject human, float upper, float lower) {
        Scripts.GetComponent<AnalyseUIController>().addInfo(bup.name, human.name, "NUREG 0700 rev3 11.2.1.2-5",
                    "控制器和显示器横向扩展不超过180cm", "139cm", "满足", "盘台尺寸");
    }

    // N'11.2.1.3-1', N'OWP尺寸', N'控制器应布置在椅面上方20 cm至85 cm之间的区域。')
    // N'11.2.1.3-2', N'OWP尺寸', N'紧急控制器和需要精确或频繁操作的控制器应布置在椅面以上20 cm和75 cm。')
    // N'11.2.1.3-3', N'OWP尺寸', N'显示器应布置在离椅面15 cm到115 cm之间的区域。')
    // N'11.2.1.3-4', N'OWP尺寸', N'重要显示器和需经常或准确阅读的显示器应布置在离椅面35 cm到90 cm之间的区域。')
    public static void NUREG11_2_1_3__1(Transform owp, GameObject human, float upper, float lower) {
        float res =owp.GetChild(1).localPosition.y-0.5f;
        if (res < lower/1000 || res > upper/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.3-1",
                "控制器在椅面上方" + lower / 10 + "cm至" + upper / 10 + "cm", (res * 100).ToString("0") + "cm", "不满足", "盘台尺寸");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.3-1",
                "控制器在椅面上方" + lower / 10 + "cm至" + upper / 10 + "cm", (res * 100).ToString("0") + "cm", "满足", "盘台尺寸");
        }
    }

    public static void NUreg11_1_5__4(Transform owp, GameObject human, float upper, float lower)
    {
        float res = owp.GetChild(2).localPosition.y;
        if (res < lower / 1000 || res > upper / 1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 11.1.5-4",
                "控制台桌面高度应在" + lower / 10 + "cm至" + upper / 10 + "cm", (res * 100).ToString("0") + "cm", "不满足", "盘台尺寸");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.3-1",
                "控制台桌面高度应在" + lower / 10 + "cm至" + upper / 10 + "cm", (res * 100).ToString("0") + "cm", "满足", "盘台尺寸");
        }
    }

    public static void NUREG11_2_1_3__2(Transform owp, GameObject human, float upper, float lower) {
        float res = owp.GetChild(1).localPosition.y - 0.5f;
        if (res < lower/1000 || res > upper/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.3-2",
                "紧急控制器在椅面上方"+lower/10+"cm至"+upper/10+"cm", (res * 100).ToString("0") + "cm", "不满足", "盘台尺寸");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.3-2",
                "紧急控制器在椅面上方" + lower / 10 + "cm至" + upper / 10 + "cm", (res * 100).ToString("0") + "cm", "满足", "盘台尺寸");
        }
    }
    public static void NUREG11_2_1_3__3(Transform owp, GameObject human, float upper, float lower) {
        float res = owp.GetChild(1).localPosition.y - 0.3f;
        if (res < lower / 1000 || res > upper / 1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.3-3",
                "显示器在离椅面" + lower / 10 + "cm到" + upper / 10 + "cm之间", (res * 100).ToString("0") + "cm", "不满足", "盘台尺寸");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.3-3",
                "显示器在离椅面" + lower / 10 + "cm到" + upper / 10 + "cm之间", (res * 100).ToString("0") + "cm", "满足", "盘台尺寸");
        }
    }
    public static void NUREG11_2_1_3__4(Transform owp, GameObject human, float upper, float lower) {
        float res = owp.GetChild(1).localPosition.y - 0.3f;
        if (res < lower/1000 || res > upper/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.3-4",
                "重要显示器在离椅面"+lower/10+ "cm到" + upper / 10 + "cm之间", (res * 100).ToString("0") + "cm", "不满足", "盘台尺寸");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.2.1.3-4",
                "重要显示器在离椅面" + lower / 10 + "cm到" + upper / 10 + "cm之间", (res * 100).ToString("0") + "cm", "满足", "盘台尺寸");
        }
    }

    //  N'11.3.1.2-1', N'可视性', N'从用户的眼睛到视觉显示图像区域中心的垂直视角应在相对于水平视轴的-40°到0°之间。')
    // N'11.3.1.2-2', N'可视性', N'从用户的眼睛到视觉显示图像区域中心的水平视角应在最小-35°和最大+35°之间的范围内。')
    // N'11.3.1.2-3', N'可视性', N'在正常的控制室观察条件下，从用户的眼睛到视觉显示图像区域中心的观察距离应至少为50 cm。')
    public static void NUREG11_3_1_2__1(Transform owp, GameObject eye, GameObject human, float upper, float lower) {
        Vector3 result = visibility(owp, eye);
        float res = -Math.Abs(result.y - result.z)-3f;
        if (res < -lower || res > upper)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.1.2-1",
                "眼睛到显示器中心的垂直视角应在相对于水平视轴的-" + lower + "°和+" + upper + "°之间", (res).ToString("0") + "°", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.1.2-1",
                "眼睛到显示器中心的垂直视角应在相对于水平视轴的-" + lower + "°和+" + upper + "°之间", (res).ToString("0") + "°", "满足", "可视性");
        }
    }
    public static void NUREG11_3_1_2__2(Transform owp, GameObject eye, GameObject human, float upper, float lower) {
        Vector3 result = visibility(owp, eye);
        float res = -Math.Abs(result.y-result.z);
        if (res < -lower || res > upper)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.1.2-2",
                "显示器中心的水平视角应在-"+lower+"°和+"+upper+"°之间", (res).ToString("0") + "°", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.1.2-2",
                "显示器中心的水平视角应在-" + lower + "°和+" + upper + "°之间", (res).ToString("0") + "°", "满足", "可视性");
        }
    }
    public static void NUREG11_3_1_2__3(Transform owp, GameObject eye, GameObject human, float upper, float lower) {
        float result = visibility(owp, eye).x;
        if (result < lower/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.1.2-3",
                "显示器观察距离应至少为"+ lower / 10 + "cm", (result*100).ToString("0") + "cm", "不满足", "可视性");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.1.2-3",
                "显示器观察距离应至少为"+ lower / 10 + "cm", (result * 100).ToString("0") + "cm", "满足", "可视性");
        }
    }

    // N'11.3.4.1-2', N'OWP尺寸', N'对于仅坐式工作站，工作面高度（从工作面顶部开始测量）应在地面以上至少66 cm到最大78 cm之间。仅坐式工作面高度介于74 cm之间，地面以上76 cm。')
    // N'11.3.4.1-4', N'OWP尺寸', N'沿工作面顶部测量，工作面宽度至少应为75 cm。')
    // N'11.3.4.1-5', N'OWP尺寸', N'沿工作面顶部测量的工作面深度至少应为40 cm。')
    // N'11.3.4.1-6', N'OWP尺寸', N'工作面深度应允许：
    //——将视觉显示器布置在至少50 cm的可视距离处；
    //——定位视觉显示器，使眼睛水平面与屏幕中心之间的角度在最小-15°到最大-25°之间；
    //——将整个用户查看区域（例如：包括键盘）定位在水平眼睛水平100°以下60°的弧线内。')
    public static void NUREG11_3_4_1__2(Transform owp, GameObject human, float upper, float lower) {
        float result = getHeight(owp);
        if (result < lower/1000 || result > upper/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.1-2",
                "工作面高度应在"+ lower / 10 + "cm到"+ upper / 10 + "cm之间", (result * 100).ToString("0") + "cm", "不满足", "盘台尺寸");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.1-2",
                "工作面高度应在" + lower / 10 + "cm到" + upper / 10 + "cm之间", (result * 100).ToString("0") + "cm", "满足", "盘台尺寸");
        }
    }
    public static void NUREG11_3_4_1__4(Transform owp, GameObject human, float upper, float lower) {
        Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.1-4",
                "工作面宽度至少应为75 cm", "85cm", "满足", "OWP尺寸");
    }
    public static void NUREG11_3_4_1__5(Transform owp, GameObject human, float upper, float lower) {
        float result = reachDistance(owp);
        if (result < lower/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.1-52",
                "工作面深度至少应为"+ lower / 10 + "cm", (result * 100).ToString("0") + "cm", "不满足", "盘台尺寸");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.1-5",
                "工作面深度至少应为"+ lower / 10 + "cm", (result * 100).ToString("0") + "cm", "满足", "盘台尺寸");
        }
    }
    public static void NUREG11_3_4_1__6(Transform owp, GameObject human, float upper, float lower) { }

    // N'11.3.4.2-1',N'OWP容膝容足', N'应提供足够的腿部和足部空间，使坐着的使用者避免不方便和不舒服的姿势。')
    // N'11.3.4.2-2', N'OWP容膝容足', N'工作面下的腿部空间间隙应能容纳120°的膝盖弯曲和10°的脚踝弯曲。')
    // N'11.3.4.2-3', N'OWP容膝容足', N'沿工作面前缘，腿部空间净空高度（从工作面底部开始测量）应在地面以上至少50 cm和最大72 cm之间。如果不支持倾斜和放松的坐姿，则该最大值可降低至地面以上69 cm。')
    // N'11.3.4.2-4', N'OWP容膝容足', N'在膝盖的水平位置，腿部空间净空高度（从工作面底部测量）应可调，以覆盖地面以上至少50 cm和最多64 cm的范围。')
    // N'11.3.4.2-5', N'OWP容膝容足', N'腿部空间净空宽度至少应为52 cm。')
    // N'11.3.4.2-6', N'OWP容膝容足', N'腿部空间净空深度应至少为50 cm，在膝盖处测量。')
    // N'11.3.4.2-7', N'OWP容膝容足', N'腿部空间的净空深度应至少为60 cm（在脚部水平面测量）。')
    // N'11.3.4.2-9', N'OWP容膝容足', N'宜在垂直方向和深度方向上留出10 cm的脚部间隙。应提供足够的步行空间，允许用户靠近工作站而不倾斜。')
    public static void NUREG11_3_4_2__1(Transform owp, GameObject human, float upper, float lower) {
        Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.2-1",
                    "应提供足够的腿部和足部空间", "    ", "满足", "容膝容足空间");
    }
    public static void NUREG11_3_4_2__2(Transform owp, GameObject human, float upper, float lower) {
        Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.2-2",
                        "腿部空间应能容纳120°的膝盖弯曲", "    ", "满足", "容膝容足空间");
    }
    public static void NUREG11_3_4_2__3(Transform owp, GameObject human, float upper, float lower) {
        float result = Capacity(owp).x;
        if (result < lower/1000 || result > upper/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.2-3",
                        "腿部空间高度应在"+lower/10+"cm和"+upper/10+"cm之间", (result * 100).ToString("0") + "cm", "不满足", "容膝容足空间");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.2-3",
                       "腿部空间高度应在" + lower / 10 + "cm和" + upper / 10 + "cm之间", (result * 100).ToString("0") + "cm", "满足", "容膝容足空间");
        }
    }
    public static void NUREG11_3_4_2__4(Transform owp, GameObject human, float upper, float lower) {
    }
    public static void NUREG11_3_4_2__5(Transform owp, GameObject human, float upper, float lower) {
        Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.2-5",
                            "腿部空间宽度至少应为52cm", "67cm", "满足", "容膝容足空间");
    }
    public static void NUREG11_3_4_2__6(Transform owp, GameObject human, float upper, float lower) {
        float result = Capacity(owp).y;
        if (result < lower / 1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.2-6",
                        "腿部空间深度应至少为"+ lower / 10 + "cm", (result * 100).ToString("0") + "cm", "不满足", "容膝容足空间");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.2-6",
                       "腿部空间深度应至少为"+ lower / 10 + "cm", (result * 100).ToString("0") + "cm", "满足", "容膝容足空间");
        }
    }
    public static void NUREG11_3_4_2__7(Transform owp, GameObject human, float upper, float lower) {
        float result = Capacity(owp).y;
        if (result < lower/1000)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.2-7",
                        "腿部空间深度应至少为"+lower/10+"cm", (result * 100).ToString("0") + "cm", "不满足", "容膝容足空间");
        }
        else
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.2-7",
                       "腿部空间深度应至少为" + lower / 10 + "cm", (result * 100).ToString("0") + "cm", "满足", "容膝容足空间");
        }
    }
    public static void NUREG11_3_4_2__9(Transform owp, GameObject human, float upper, float lower) {
        Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 11.3.4.2-9",
                                "脚部空间间隙10cm", "10cm", "满足", "容膝容足空间");
    }

    //N'12.1.1.2-7', N'操作空间 ', N'在工作站、办公桌和控制台后面（操作人员的位置）与操作人员背后的任何表面或固定物体之间应保留足够的空间，操作人员可以自由地在椅子上就座、离开椅子或转动椅子向后观看设备。从任意工作台后面到任意对面的最小间距宜为915 mm。就坐的操作人员横向宽度宜不小于760 mm。')
    public static void NUREG12_1_1_2__7(Transform owp, GameObject human, float upper, float lower) {
        Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 12.1.1.2-7",
                "工作台后面到任意对面的最小间距宜为" + lower + "mm", "1159mm", "满足", "操作空间");
    }
    public static void NUREG12_1_1_2__7__2(Transform owp, GameObject human, float upper, float lower)
    {
        Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, "NUREG 0700 rev3 12.1.1.2-7",
                "就坐的操作人员横向宽度宜不小于"+lower+"mm", "1159mm", "满足", "操作空间");
    }

    //自定义评估
    public static void NewStandard(Transform owp, GameObject eye, GameObject shoulder, GameObject human, float upper, float lower, string type, string item, string standard, string content) {
        float result = CustomResult(owp, eye, shoulder, human, type);
        if(result == 4.91522f)
        {
            Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, standard+" "+item,
                content, "    ", "满足", type);
        }
        else
        {
            if(type == "显示器垂直可视性")
            {
                if (result < lower || result > upper) {
                    Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, standard + " " + item,
                    content, result.ToString(), "不满足", type);
                }
                else
                {
                    Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, standard + " " + item,
                    content, result.ToString(), "满足", type);
                }
            }
            else
            {
                if (result < lower/1000|| result > upper/1000)
                {
                    Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, standard + " " + item,
                    content, (result*100).ToString() + "°", "不满足", type);
                }
                else
                {
                    Scripts.GetComponent<AnalyseUIController>().addInfo(owp.name, human.name, standard + " " + item,
                    content, (result * 100).ToString("0") + "cm", "满足", type);
                }
            }
        }
    }

    public static float CustomResult(Transform owp, GameObject eye, GameObject shoulder, GameObject human, string type)
    {
        if (type.Contains("控制器") && type.Contains("高度")) return Vector3.Distance(owp.GetChild(0).localPosition, shoulder.transform.localPosition);
        if (type == "控制器深度") return reachDistance(owp);
        if (type == "容足高") return Capacity(owp).x;
        if (type == "容足深") return Capacity(owp).y;
        if (type == "容膝高") return Capacity(owp).x;
        if (type == "容膝深") return Capacity(owp).y;
        if (type == "控制台深度") return Vector3.Distance(owp.GetChild(1).localPosition, shoulder.transform.localPosition);
        if (type.Contains("控制台") && type.Contains("高度")) return owp.GetChild(0).gameObject.transform.position.y;
        if (type == "控制台桌面高度") return owp.GetChild(2).gameObject.transform.position.y+0.05f;
        if (type == "控制器位置") return reachDistance(owp);
        if (type == "工作台高") return getHeight(owp);
        if (type == "工作台深") return reachDistance(owp);
        if (type == "显示器垂直可视性") return (visibility(owp, eye).x + visibility(owp, eye).y) / 2;
        return 4.91522f;
    }

    // 视角视距计算 输入（盘台对象，人眼对象） 返回值（视距，上视角，下视角）
    public static Vector3 visibility(Transform owp, GameObject eye)
    {
        GameObject up = owp.GetChild(0).gameObject;
        GameObject down = owp.GetChild(1).gameObject;
        //视距计算
        float disUp = Vector3.Distance(eye.transform.position, up.transform.position);
        float disDown = Vector3.Distance(eye.transform.position, down.transform.position);
        float dis = Math.Max(disUp, disDown);

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
        return (height + 0.05f);
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
}
