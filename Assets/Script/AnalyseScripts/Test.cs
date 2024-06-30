using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform t;
    public GameObject o;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void test()
    {
        
    }
    /*
    public void XinChenDaiXie()
    {

        string mass = GameObject.Find("MainScene/XinChenDaiXie/IScrollViewContentent/Panel/Scroll View/Viewport/Content/GridLayout1/Column2/InputField").GetComponent<InputField>().text;
        string speed = GameObject.Find("MainScene/XinChenDaiXie/IScrollViewContentent/Panel/Scroll View/Viewport/Content/GridLayout1/Column2 (1)/InputField (1)").GetComponent<InputField>().text;
        string time = GameObject.Find("MainScene/XinChenDaiXie/IScrollViewContentent/Panel/Scroll View/Viewport/Content/GridLayout1/Column2/InputField2").GetComponent<InputField>().text;
        float slope = GameObject.Find("MainScene/XinChenDaiXie/IScrollViewContentent/Panel/Scroll View/Viewport/Content/GridLayout1/Column2 (1)/GridLayout1/Column1/Slider").GetComponent<Slider>().value;
        float e = (float)(4.184 * 0.01 * float.Parse(time) * (51 + 2.54 * float.Parse(mass) * float.Parse(speed) * float.Parse(speed) + 0.379 * float.Parse(mass) * float.Parse(speed) * Math.Tan(slope)));
        GameObject.Find("MainScene/XinChenDaiXie/IScrollViewContentent/Panel/Scroll View/Viewport/Content/GridLayout1/Column4").SetActive(true);
        GameObject.Find("MainScene/XinChenDaiXie/IScrollViewContentent/Panel/Scroll View/Viewport/Content/GridLayout1/Column4/Text").GetComponent<Text>().text = "能量消耗量  " + e.ToString("0.00") + " KJ";
    }

    public void BanYunShouLi()
    {

        string mass = GameObject.Find("MainScene/BanYunShouLi/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column2/InputField (TMP)").GetComponent<TMP_InputField>().text;
        string load = GameObject.Find("MainScene/BanYunShouLi/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column2/InputField (TMP) (1)").GetComponent<TMP_InputField>().text;
        int terrain = GameObject.Find("MainScene/BanYunShouLi/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column2/Dropdown").GetComponent<Dropdown>().value;
        float cm = 1;
        switch (terrain)
        {
            case 0: cm = 1; break;
            case 1: cm = 0.95f; break;
            case 2: cm = 0.9f; break;
        }
        float l = float.Parse(mass);
        float fm = float.Parse(load);
        float rwl = (float)(18.7 * (24.0f / 40.0f) * (0.82 + 4.35 / 90.0f) / fm * cm);
        float li = l / rwl;

    }

    public void XiaBeiBuShouLi()
    {
        float rx;
        float ry;
        float rz;
        int x;
        int y;
        int z;
        float[] data = new float[36];
        int[] results = new int[12];
        for (int i = 0; i < joints.Length; i++)
        {
            rx = GameObject.Find(joints[i]).GetComponent<Transform>().localEulerAngles.x;
            ry = GameObject.Find(joints[i]).GetComponent<Transform>().localEulerAngles.y;
            rz = GameObject.Find(joints[i]).GetComponent<Transform>().localEulerAngles.z;
            data[3 * i + 0] = rx;
            data[3 * i + 1] = ry;
            data[3 * i + 2] = rz;

        }
        data[3] = System.Math.Abs(data[3]);


        string mass = GameObject.Find("MainScene/XIaBeiBuShouLi/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column2/InputField").GetComponent<TMP_InputField>().text;
        string load = GameObject.Find("MainScene/XIaBeiBuShouLi/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column2/InputField (1)").GetComponent<TMP_InputField>().text;
        float mgbw = (float)(0.5 * float.Parse(mass) * 9.8);
        float mgload = (float)(float.Parse(load) * 9.8);
        float fm = (float)((System.Math.Sin(data[3]) * mgbw + 0.5 * mgload) / 6.5);
        float fc = System.Math.Abs((float)(fm + System.Math.Cos(data[3]) * (mgbw + mgload)));
        float fs = System.Math.Abs((float)(System.Math.Cos(data[3]) * (mgbw + mgload)));
    }

    public void RenWuFuHe()
    {
        string mass = GameObject.Find("MainScene/RenWuFuHe/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column2/InputField").GetComponent<InputField>().text;
        string load = GameObject.Find("MainScene/RenWuFuHe/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column2/InputField2").GetComponent<InputField>().text;
        int terrain = GameObject.Find("MainScene/RenWuFuHe/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column2/Dropdown").GetComponent<Dropdown>().value;
        string speed = GameObject.Find("MainScene/RenWuFuHe/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column2/InputField (1)").GetComponent<InputField>().text;
        float slope = GameObject.Find("MainScene/RenWuFuHe/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column2/GridLayout1/Column1/Slider").GetComponent<Slider>().value;
        float k = 1;
        switch (terrain)
        {
            case 0: k = 1; break;
            case 1: k = 1.1f; break;
            case 2: k = 1.5f; break;
            case 3: k = 1.8f; break;
            case 4: k = 2.1f; break;
            case 5: k = 2.3f; break;
        }
        float m1 = (float)(1.5 * float.Parse(mass) + 2 * (float.Parse(mass) + float.Parse(load)) * (float.Parse(load) / float.Parse(mass)) * (float.Parse(load) / float.Parse(mass)));
        float m2 = (float)(m1 + k * (float.Parse(mass) + float.Parse(load)) * (1.5 * float.Parse(speed) * float.Parse(speed) + 0.35 * float.Parse(speed) * slope));

        GameObject.Find("MainScene/RenWuFuHe/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column3").SetActive(true);
        GameObject.Find("MainScene/RenWuFuHe/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column4").SetActive(true);
        Image image1 = GameObject.Find("MainScene/RenWuFuHe/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column4/GridLayout1/Column1 (1)/Image").GetComponent<Image>();
        Text text1 = GameObject.Find("MainScene/RenWuFuHe/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column4/GridLayout1/Column1/Text2").GetComponent<Text>();
        if (m1 < 208) { image1.color = new UnityEngine.Color((0 / 255f), (255 / 255f), (1 / 255f), (255 / 255f)); text1.text = "轻"; }
        else
        {
            if (m1 <= 383) { image1.color = new UnityEngine.Color((207 / 255f), (255 / 255f), (0 / 255f), (255 / 255f)); text1.text = "中等"; }
            else
            {
                if (m1 <= 558) { image1.color = new UnityEngine.Color((255 / 255f), (202 / 255f), (0 / 255f), (255 / 255f)); text1.text = "重"; }
                else
                {
                    if (m1 <= 733) { image1.color = new UnityEngine.Color((255 / 255f), (36 / 255f), (0 / 255f), (255 / 255f)); text1.text = "很重"; }
                    else { image1.color = new UnityEngine.Color((255 / 255f), (0 / 255f), (216 / 255f), (255 / 255f)); text1.text = "极重"; }
                }
            }
        }
        GameObject.Find("MainScene/RenWuFuHe/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column4/Text").GetComponent<Text>().text = m1.ToString("0.00") + "w";

        Image image2 = GameObject.Find("MainScene/RenWuFuHe/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column4/GridLayout1 (1)/Column1 (1)/Image").GetComponent<Image>();
        Text text2 = GameObject.Find("MainScene/RenWuFuHe/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column4/GridLayout1 (1)/Column1/Text2").GetComponent<Text>();
        if (m2 < 208) { image2.color = new UnityEngine.Color((0 / 255f), (255 / 255f), (1 / 255f), (255 / 255f)); text2.text = "轻"; }
        else
        {
            if (m2 <= 383) { image2.color = new UnityEngine.Color((207 / 255f), (255 / 255f), (0 / 255f), (255 / 255f)); text2.text = "中等"; }
            else
            {
                if (m2 <= 558) { image2.color = new UnityEngine.Color((255 / 255f), (202 / 255f), (0 / 255f), (255 / 255f)); text2.text = "重"; }
                else
                {
                    if (m2 <= 733) { image2.color = new UnityEngine.Color((255 / 255f), (36 / 255f), (0 / 255f), (255 / 255f)); text2.text = "很重"; }
                    else { image2.color = new UnityEngine.Color((255 / 255f), (0 / 255f), (216 / 255f), (255 / 255f)); text2.text = "极重"; }
                }
            }
        }
        GameObject.Find("MainScene/RenWuFuHe/IScrollViewContentent/Scroll View/Viewport/Content/GridLayout1/Column4/Text (1)").GetComponent<Text>().text = m2.ToString("0.00") + "w";



    }


    */
}
