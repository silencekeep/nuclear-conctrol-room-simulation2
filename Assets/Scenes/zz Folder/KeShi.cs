using CNCC.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeShi : MonoBehaviour
{
    public Camera Camera;
    private GameObject coneLeft;
    private GameObject coneRight;
    private GameObject eye;
    private GameObject eyeL;
    private GameObject eyeR;
    private GameObject leftshoulder;
    private bool human;
    private bool target;
    private bool flag;
    public bool refreshFlag = false;
    private GameObject myHuman;
    private GameObject myTarget;
    public InputField humanName;
    public InputField targetName;
    public Dropdown dd;
    public Text distance;
    private int choose;
    public Text res;
    private Camera CameraLeft;
    private Camera CameraRight;
    public RawImage leftImage;
    public RawImage rightImage;
    // Start is called before the first frame update

    private float d = 10;
    private float a = 180;

    public Material red;
    public Material green;
    public Material yellow;

    private GameObject coneLeftMin;
    private GameObject coneRightMin;
    private GameObject coneLeftMid;
    private GameObject coneRightMid;
    private GameObject coneLeftMax;
    private GameObject coneRightMax;
    private LineRenderer line;

    public GameObject caibiaoPanel;
    public GameObject table;
    private bool diy;
    public RenderTexture leftR;
    public RenderTexture rightR;
    void Start()
    {
        human = false;
        target = false;
        diy = false;
        flag = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (human && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag.Equals("human"))
                {
                    myHuman = hit.transform.GetChild(0).gameObject;
                    humanName.text = hit.transform.GetComponent<Model>().Name;
                    /*
                    if (myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManNeck/LowManHead/LowManHead_end/left/Cone"))
                    {
                        coneLeft = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManNeck/LowManHead/LowManHead_end/left/Cone").gameObject;
                        coneRight = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManNeck/LowManHead/LowManHead_end/right/Cone").gameObject;
                        eye = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManNeck/LowManHead/LowManHead_end").gameObject;
                        eyeL = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManNeck/LowManHead/LowManHead_end/left").gameObject;
                        eyeR = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManNeck/LowManHead/LowManHead_end/right").gameObject;
                        leftshoulder = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManLeftShoulder").gameObject;
                        GameObject CameraL = GameObject.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManNeck/LowManHead/LowManHead_end/camera/left");
                        CameraLeft = CameraL.GetComponent<Camera>();
                        GameObject CameraR = GameObject.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManNeck/LowManHead/LowManHead_end/camera/right");
                        CameraRight = CameraR.GetComponent<Camera>();
                    }
                    else
                    {
                        leftshoulder = myHuman.transform.Find("Root_M/Spine1_M/Chest_M").gameObject;
                        coneLeft = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/left/Cone").gameObject;
                        coneRight = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/right/Cone").gameObject;
                        eye = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M").gameObject;
                        eyeL = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/left").gameObject;
                        eyeR = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/right").gameObject;
                        GameObject CameraL = GameObject.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/camera/left");
                        CameraLeft = CameraL.GetComponent<Camera>();
                        GameObject CameraR = GameObject.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/camera/right");
                        CameraRight = CameraR.GetComponent<Camera>();
                        GameObject cc = GameObject.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/camera");
                        cc.transform.localPosition = new Vector3(0, 0, 0);
                    }
                    */
                    if (CameraLeft != null)
                    {
                        CameraLeft.targetTexture = null;
                    }
                    if (CameraRight != null)
                    {
                        CameraRight.targetTexture = null;
                    }
                    leftshoulder = myHuman.transform.Find("Root_M/Spine1_M/Chest_M").gameObject;
                    coneLeft = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/left/Cone").gameObject;
                    coneRight = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/right/Cone").gameObject;
                    eye = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M").gameObject;
                    eyeL = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/left").gameObject;
                    eyeR = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/right").gameObject;
                    GameObject CameraL = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/camera/left").gameObject;
                    CameraLeft = CameraL.GetComponent<Camera>();
                    GameObject CameraR = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/camera/right").gameObject;
                    CameraRight = CameraR.GetComponent<Camera>();
                    GameObject cc = GameObject.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/camera");
                    cc.transform.localPosition = new Vector3(0, 0, 0);
                    refreshFlag = true;
                    CameraL.transform.localPosition = new Vector3(-0.05f, 0, 0);
                    CameraR.transform.localPosition = new Vector3(0.05f, 0, 0);
                    CameraLeft.targetTexture = leftR;
                    CameraRight.targetTexture = rightR;
                }
            }
            human = false;

        }

        if (target && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetName.text = hit.transform.name;
                myTarget = hit.transform.gameObject;
            }
            target = false;
            if (hit.transform.name.Contains("BUP")) choose = 0;
            if (hit.transform.name.Contains("OWP")) choose = 1;
            if (hit.transform.name.Contains("ECP")) choose = 2;
            if (hit.transform.name.Contains("VDU")) choose = 3;
        }

        
        

    }
    private Texture2D GetTextureByString(string textureStr)
    {
        Texture2D tex = new Texture2D(1, 1);
        byte[] arr = Convert.FromBase64String(textureStr);
        tex.LoadImage(arr);
        tex.Apply();
        return tex;
    }
    private string SetImageToString(string imgPath)
    {
        FileStream fs = new FileStream(imgPath, FileMode.Open);
        byte[] imgByte = new byte[fs.Length];
        fs.Read(imgByte, 0, imgByte.Length);
        fs.Close();
        return Convert.ToBase64String(imgByte);
    }
    public void apply()
    {
        if (coneLeft && coneRight)
        {
            //change(0.01f, 0.01f);
            UnityEngine.Object.Destroy(coneLeftMin);
            UnityEngine.Object.Destroy(coneRightMin);
            UnityEngine.Object.Destroy(coneLeftMid);
            UnityEngine.Object.Destroy(coneRightMid);
            UnityEngine.Object.Destroy(coneLeftMax);
            UnityEngine.Object.Destroy(coneRightMax);
        }
        change(0.01f, 0.01f);
        refreshFlag = true;
        /***
        if (dp.GetComponent<Dropdown>().value == 3)
        {
            float length = float.Parse(legthT.text);
            float angle = float.Parse(angleT.text);
            change(length, angle);
            a = angle+10;
            d = length;
        }
        else {
            if (dp.GetComponent<Dropdown>().value == 2)
            {
                change(5.0f, 60f);
                a = 70f;
                d = 5.0f;
                legthT.text = "5";
                angleT.text = "60";
            }
            else
            {
                if (dp.GetComponent<Dropdown>().value == 1)
                {
                    change(5.0f, 30f);
                    a = 40f;
                    d = 5.0f;
                    legthT.text = "5";
                    angleT.text = "30";
                }
                else
                {
                    change(5.0f, 15f);
                    a = 30f;
                    d = 5.0f;
                    legthT.text = "5";
                    angleT.text = "15";
                }
            }
        }
        
        ***/
    }

    public void cancel()
    {
        if (coneLeft && coneRight)
        {
            //change(0.01f, 0.01f);
            UnityEngine.Object.Destroy(coneLeftMin);
            UnityEngine.Object.Destroy(coneRightMin);
            UnityEngine.Object.Destroy(coneLeftMid);
            UnityEngine.Object.Destroy(coneRightMid);
            UnityEngine.Object.Destroy(coneLeftMax);
            UnityEngine.Object.Destroy(coneRightMax);
        }
    }

    public void change(float length, float angle)
    {
        //coneLeft.transform.localScale = new Vector3((float)(Math.Round(2 * length * 50 * Math.Tan(angle*1.00/180*Math.PI),5)), (float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(length * 50));
        //coneRight.transform.localScale = new Vector3((float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(length * 50));
        //coneLeft.transform.localPosition = new Vector3(0,0, (float)(-0.01* length * 50));
        //coneRight.transform.localPosition = new Vector3(0, 0, (float)(-0.01 * length * 50));

        coneLeftMin = Instantiate(coneLeft);
        coneRightMin = Instantiate(coneLeft);
        coneLeftMin.transform.parent = eyeL.transform;
        coneRightMin.transform.parent = eyeR.transform;
        length = 5.0f; angle = 15.0f;
        coneLeftMin.transform.localScale = new Vector3((float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(length * 50));
        coneRightMin.transform.localScale = new Vector3((float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(length * 50));
        coneLeftMin.transform.localPosition = new Vector3(0, 0, (float)(-0.01 * length * 50));
        coneRightMin.transform.localPosition = new Vector3(0, 0, (float)(-0.01 * length * 50));
        coneLeftMin.transform.localEulerAngles = new Vector3(0, 0, 0);
        coneRightMin.transform.localEulerAngles = new Vector3(0, 0, 0);
        coneLeftMin.GetComponent<MeshRenderer>().sharedMaterial = red;
        coneRightMin.GetComponent<MeshRenderer>().sharedMaterial = red;

        coneLeftMid = Instantiate(coneLeft);
        coneRightMid = Instantiate(coneLeft);
        coneLeftMid.transform.parent = eyeL.transform;
        coneRightMid.transform.parent = eyeR.transform;
        length = 5.0f; angle = 30.0f;
        coneLeftMid.transform.localScale = new Vector3((float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(length * 50));
        coneRightMid.transform.localScale = new Vector3((float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(length * 50));
        coneLeftMid.transform.localPosition = new Vector3(0, 0, (float)(-0.01 * length * 50));
        coneRightMid.transform.localPosition = new Vector3(0, 0, (float)(-0.01 * length * 50));
        coneLeftMid.transform.localEulerAngles = new Vector3(0, 0, 0);
        coneRightMid.transform.localEulerAngles = new Vector3(0, 0, 0);
        coneLeftMid.GetComponent<MeshRenderer>().sharedMaterial = yellow;
        coneRightMid.GetComponent<MeshRenderer>().sharedMaterial = yellow;

        coneLeftMax = Instantiate(coneLeft);
        coneRightMax = Instantiate(coneLeft);
        coneLeftMax.transform.parent = eyeL.transform;
        coneRightMax.transform.parent = eyeR.transform;
        length = 5.0f; angle = 50.0f;
        coneLeftMax.transform.localScale = new Vector3((float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(length * 50));
        coneRightMax.transform.localScale = new Vector3((float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(Math.Round(2 * length * 50 * Math.Tan(angle * 1.00 / 180 * Math.PI), 5)), (float)(length * 50));
        coneLeftMax.transform.localPosition = new Vector3(0, 0, (float)(-0.01 * length * 50));
        coneRightMax.transform.localPosition = new Vector3(0, 0, (float)(-0.01 * length * 50));
        coneLeftMax.transform.localEulerAngles = new Vector3(0, 0, 0);
        coneRightMax.transform.localEulerAngles = new Vector3(0, 0, 0);
        coneLeftMax.GetComponent<MeshRenderer>().sharedMaterial = green;
        coneRightMax.GetComponent<MeshRenderer>().sharedMaterial = green;
    }

    public void humanChoose()
    {
        human = true;
    }

    public void targetChoose()
    {
        target = true;
    }

    public void start()
    {
        if (humanName.text != "" && targetName.text != "")
        {
            if (diy)
            {
                custom();
                //diy = false;
            }
            else
            {
                if (myTarget.GetComponent<Measurements>())
                {
                    myTarget.GetComponent<Measurements>().setDistanceObject(eye);
                    Vector2 disLeft = myTarget.GetComponent<Measurements>().getDistance();
                    distance.text = disLeft.y.ToString("0.00") + "米";

                    if (coneLeft.GetComponent<Measurements>())
                    {
                        coneLeft.GetComponent<Measurements>().setDistanceObject(myTarget);
                        Vector4 ans = coneLeft.GetComponent<Measurements>().getAngles();
                        print(ans);
                        if (disLeft.y <= d && ans.x <= a && ans.y <= a && ans.z <= a && ans.w <= a)
                        {
                            res.text = "是";
                        }
                        else
                        {
                            res.text = "否";
                        }

                    }
                    else
                    {
                        coneLeft.AddComponent<Measurements>();
                    }

                }
                else
                {
                    myTarget.AddComponent<Measurements>();
                    myTarget.GetComponent<Measurements>().setDistanceObject(eye);
                    Vector2 disLeft = myTarget.GetComponent<Measurements>().getDistance();
                    distance.text = disLeft.y.ToString("0.00") + "米";

                    if (coneLeft.GetComponent<Measurements>())
                    {
                        coneLeft.GetComponent<Measurements>().setDistanceObject(myTarget);
                        Vector4 ans = coneLeft.GetComponent<Measurements>().getAngles();
                        print(ans);
                        if (disLeft.y <= d && ans.x <= a && ans.y <= a && ans.z <= a && ans.w <= a)
                        {
                            res.text = "是";
                        }
                        else
                        {
                            res.text = "否";
                        }

                    }
                    else
                    {
                        coneLeft.AddComponent<Measurements>();
                    }
                }

                GameObject Scripts = GameObject.Find("2DUI");
                Scripts.GetComponent<AnalyseUIController>().refresh();
                Scripts.GetComponent<Frame>().frame(myTarget, myHuman, eye, dd.captionText.text);

                if (choose == 0)
                {
                    VisibilityAnalyse.BUPVisibility(myTarget.transform, eye, myHuman);

                }
                if (choose == 1)
                {
                    VisibilityAnalyse.OWPVisibility(myTarget.transform, eye, myHuman);
                }
                if (choose == 2)
                {
                    VisibilityAnalyse.ECPVisibility(myTarget.transform, eye, myHuman);
                }
                if (choose == 3)
                {
                    VisibilityAnalyse.ECPVisibility(myTarget.transform, eye, myHuman);
                }
                refreshFlag = false;
                /*
                line = eye.AddComponent<LineRenderer>();
                line.positionCount = 3;
                line.startColor = Color.blue;
                line.endColor = Color.red;
                // 设置线段起点宽度和终点宽度
                line.startWidth = 0.02f;
                line.endWidth = 0.02f;
                try
                {
                    GameObject up = myTarget.transform.GetChild(0).gameObject;
                    GameObject down = myTarget.transform.GetChild(1).gameObject;

                    line.SetPosition(0, up.transform.position);
                    line.SetPosition(1, eye.transform.position);
                    line.SetPosition(2, down.transform.position);
                }
                catch
                {
                    Debug.Log("");
                }
                */
            }
        }
    }

    public void stop()
    {
        
        human = false;
        target = false;
        flag = false;
        if(coneLeft && coneRight) {
            //change(0.01f, 0.01f);
            UnityEngine.Object.Destroy(coneLeftMin);
            UnityEngine.Object.Destroy(coneRightMin);
            UnityEngine.Object.Destroy(coneLeftMid);
            UnityEngine.Object.Destroy(coneRightMid);
            UnityEngine.Object.Destroy(coneLeftMax);
            UnityEngine.Object.Destroy(coneRightMax);
        }      
        humanName.text = "";
        targetName.text = "";
        diy = false;
    }

    public void customAnalyse()
    {
        caibiaoPanel.SetActive(true);
        //caibiaoPanel.transform.Find("MainPanel").GetComponent<TableCreate2>().SelectByType(choose);
        diy = true;
    }

    public void custom()
    {
        //获取行内容
        int j = table.transform.childCount;
        for (int i = 0; i < j; i++)
        {
            bool isSelected = table.transform.GetChild(i).transform.Find("Toggle").GetComponent<Toggle>().isOn;
            if (isSelected)
            {
                bool flag = true;
                string item = table.transform.GetChild(i).transform.Find("item").GetComponent<Text>().text.Replace(" ", "");
                string standard = table.transform.GetChild(i).transform.Find("standard").GetComponent<Text>().text.Replace(" ", "");
                string upper0 = table.transform.GetChild(i).transform.Find("upper").GetComponent<TMP_InputField>().text;
                string lower0 = table.transform.GetChild(i).transform.Find("lower").GetComponent<TMP_InputField>().text;
                string type = table.transform.GetChild(i).transform.Find("type1").GetComponent<Text>().text;
                float upper = float.MaxValue;
                float lower = float.MinValue;
                string content = table.transform.GetChild(i).transform.Find("content").GetComponent<Text>().text;
                if (upper0 != null && upper0 != "")
                {
                    upper = float.Parse(upper0);
                }
                if (lower0 != null && lower0 != "")
                {
                    lower = float.Parse(lower0);
                }
                if (item == "11.1.1-2")
                {
                    ReachableAnalyse.NUREG11_1_1__2(myTarget.transform, leftshoulder, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.1-3")
                {
                    ReachableAnalyse.NUREG11_1_1__3(myTarget.transform, leftshoulder, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.1-4")
                {
                    ReachableAnalyse.NUREG11_1_1__4(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.1-5")
                {
                    ReachableAnalyse.NUREG11_1_1__5(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.1-6")
                {
                    VisibilityAnalyse.NUREG11_1_1__6(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.1.5-1(1)")
                {
                    VisibilityAnalyse.NUREG11_3_1_5__1__1(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.1.5-1(2)")
                {
                    VisibilityAnalyse.NUREG11_3_1_5__1__2(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "1.3.1-4")
                {
                    VisibilityAnalyse.NUREG1_3_1__4(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "12.1.1.3-5")
                {
                    CapacityAnalyse.NUREG12_1_1_3__5(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.1-10(1)")
                {
                    CapacityAnalyse.BUPCapacity(myTarget.transform, myHuman);
                    continue;
                }
                if (item == "11.1.1-10(2)")
                {
                    CapacityAnalyse.BUPCapacity(myTarget.transform, myHuman);
                    continue;
                }
                if (item == "11.1.2-1")
                {
                    ReachableAnalyse.NUREG11_1_2__1(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-2")
                {
                    ReachableAnalyse.OWPReachable(myTarget.transform, myHuman);
                    continue;
                }
                if (item == "11.1.2-3")
                {
                    ReachableAnalyse.NUREG11_1_2__3(myTarget.transform, leftshoulder, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-6(1)")
                {
                    VisibilityAnalyse.NUREG11_1_2__6__1(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-6(2)")
                {
                    VisibilityAnalyse.NUREG11_1_2__6__2(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-8")
                {
                    VisibilityAnalyse.NUREG11_1_2__8(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.5-4")
                {
                    StanardV3.NUreg11_1_5__4(myTarget.transform, myHuman, upper, lower);
                }
                if (item == "11.1.2-10(1)")
                {
                    CapacityAnalyse.NUREG11_1_2__10__1(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-10(2)")
                {
                    CapacityAnalyse.NUREG11_1_2__10__2(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-10(3)")
                {
                    CapacityAnalyse.NUREG11_1_2__10__3(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-10(4)")
                {
                    CapacityAnalyse.NUREG11_1_2__10__4(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.5.1(1)")
                {
                    StanardV3.DLT4_5_1(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.5.1(2)")
                {
                    StanardV3.DLT4_5_1(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.5.4.2(1)")
                {
                    StanardV3.DLT4_5_4_2(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.5.4.2(2)")
                {
                    StanardV3.DLT4_5_4_2(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.5.4.2(3)")
                {
                    StanardV3.DLT4_5_4_2__3(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.6.2")
                {
                    StanardV3.DLT4_6_2(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.6.4")
                {
                    StanardV3.DLT4_6_4(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.2.1.2")
                {
                    StanardV3.HAF4_2_1_2(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (standard == "NUREG0700rev3" && item == "11.1.1-5")
                {
                    StanardV3.NUREG11_1_1__5(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-2")
                {
                    StanardV3.NUREG11_2_1_1__2(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-3")
                {
                    StanardV3.NUREG11_2_1_1__3(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-5")
                {
                    StanardV3.NUREG11_2_1_1__5(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-6(1)")
                {
                    StanardV3.NUREG11_2_1_1__6(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-6(2)")
                {
                    StanardV3.NUREG11_2_1_1__6(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-7(1)")
                {
                    StanardV3.NUREG11_2_1_1__7(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-7(2)")
                {
                    StanardV3.NUREG11_2_1_1__7(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.2-1")
                {
                    StanardV3.NUREG11_2_1_2__1(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.2-2")
                {
                    StanardV3.NUREG11_2_1_2__2(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.2-3")
                {
                    StanardV3.NUREG11_2_1_2__3(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.2-4")
                {
                    StanardV3.NUREG11_2_1_2__4(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.2-5")
                {
                    StanardV3.NUREG11_2_1_2__5(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.3-1")
                {
                    StanardV3.NUREG11_2_1_3__1(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.3-2")
                {
                    StanardV3.NUREG11_2_1_3__2(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.3-3")
                {
                    StanardV3.NUREG11_2_1_3__3(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.3-4")
                {
                    StanardV3.NUREG11_2_1_3__4(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.1.2-1")
                {
                    StanardV3.NUREG11_3_1_2__1(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.1.2-2")
                {
                    StanardV3.NUREG11_3_1_2__2(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.1.2-3")
                {
                    StanardV3.NUREG11_3_1_2__3(myTarget.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.1-2")
                {
                    StanardV3.NUREG11_3_4_1__2(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.1-4")
                {
                    StanardV3.NUREG11_3_4_1__4(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.1-5")
                {
                    StanardV3.NUREG11_3_4_1__5(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.1-6")
                {
                    StanardV3.NUREG11_3_4_1__6(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-1")
                {
                    StanardV3.NUREG11_3_4_2__1(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-2")
                {
                    StanardV3.NUREG11_3_4_2__2(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-3")
                {
                    StanardV3.NUREG11_3_4_2__3(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-4")
                {
                    StanardV3.NUREG11_3_4_2__4(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-5")
                {
                    StanardV3.NUREG11_3_4_2__5(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-6")
                {
                    StanardV3.NUREG11_3_4_2__6(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-7")
                {
                    StanardV3.NUREG11_3_4_2__7(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-9")
                {
                    StanardV3.NUREG11_3_4_2__9(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "12.1.1.2-7(1)")
                {
                    StanardV3.NUREG12_1_1_2__7(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "12.1.1.2-7(2)")
                {
                    StanardV3.NUREG12_1_1_2__7__2(myTarget.transform, myHuman, upper, lower);
                    continue;
                }
                StanardV3.NewStandard(myTarget.transform, eye, leftshoulder, myHuman, upper, lower, type, item, standard, content);
            }
        }
    }
}
