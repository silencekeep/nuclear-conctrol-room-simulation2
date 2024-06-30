using CNCC.Models;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeDa : MonoBehaviour
{
    public Camera Camera;
    public InputField humanName;
    public InputField targetName;
    public Text distanceLeft;
    public Text distanceRight;
    private bool human;
    private bool target;
    private bool flag;
    private GameObject myHuman;
    private GameObject myTarget;
    private GameObject lefthand;
    private GameObject righthand;
    private GameObject sphereLeft;
    private GameObject sphereRight;
    private bool showFlag;
    public Text res;

    private GameObject leftshoulder;
    private GameObject rightshoulder;
    private GameObject eye;
    public bool refreshFlag = true;

    public Dropdown dd;
    private int choose;
    public GameObject caibiaoPanel;
    public GameObject table;
    private bool diy;
    private string sex;

    // Start is called before the first frame update
    void Start()
    {
        human = false;
        target = false;
        showFlag = false;
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
                    if (myHuman.transform.GetChild(0).transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManLeftShoulder/LowManLeftArm/LowManLeftForeArm/LowManLeftHand"))
                    {
                        eye = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManNeck/LowManHead/LowManHead_end").gameObject;
                        lefthand = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManLeftShoulder/LowManLeftArm/LowManLeftForeArm/LowManLeftHand").gameObject;
                        righthand = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManRightShoulder/LowManRightArm/LowManRightForeArm/LowManRightHand").gameObject;
                        leftshoulder = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManLeftShoulder").gameObject;
                        rightshoulder = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManRightShoulder").gameObject;
                        sphereLeft = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManLeftShoulder/LowManLeftArm/left/Sphere").gameObject;
                        sphereRight = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManRightShoulder/LowManRightArm/right/Sphere").gameObject;
                    }
                    else
                    {
                        eye = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M").gameObject;
                        lefthand = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Scapula_L/Shoulder_L/Elbow_L/Wrist_L/Cup_L").gameObject;
                        righthand = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Scapula_R1/Shoulder_R/Elbow_R/Wrist_R/Cup_R").gameObject;
                        leftshoulder = myHuman.transform.Find("Root_M/Spine1_M/Chest_M").gameObject;
                        rightshoulder = myHuman.transform.Find("Root_M/Spine1_M/Chest_M").gameObject;
                        sphereLeft = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Scapula_L/Shoulder_L/left/Sphere").gameObject;
                        sphereRight = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Scapula_R1/Shoulder_R/right/Sphere").gameObject;
                    }
                    */
                    eye = myHuman.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).gameObject;
                    lefthand = myHuman.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftHand).gameObject;
                    //righthand = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Scapula_R1/Shoulder_R/Elbow_R/Wrist_R/Cup_R").gameObject;
                    righthand = myHuman.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand).gameObject;
                    leftshoulder = myHuman.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftUpperArm).gameObject;
                    rightshoulder = myHuman.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightUpperArm).gameObject;
                    sphereLeft = myHuman.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftUpperArm).Find("left/Sphere").gameObject;
                    //sphereRight = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Scapula_R1/Shoulder_R/right/Sphere").gameObject;//
                    sphereRight = myHuman.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightUpperArm).Find("right/Sphere").gameObject;
                    refreshFlag = true;
                    sex = hit.transform.GetComponent<Model>().gender;
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
    public void transition()
    {
        if (!showFlag)
        {
            if ("男".Equals(sex))
            {
                sphereLeft.transform.localScale = new Vector3(100f, 100f, 100f);
                sphereRight.transform.localScale = new Vector3(100f, 100f, 100f);
                showFlag = true;
            }
            else
            {
                sphereLeft.transform.localScale = new Vector3(70f, 70f, 70f);
                sphereRight.transform.localScale = new Vector3(70f, 70f, 70f);
                showFlag = true;
            }
        }
        else
        {
            sphereLeft.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            sphereRight.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            showFlag = false;

        }
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
        flag = true;
        if (humanName.text != "" && targetName.text != "" && flag)
        {
            if (diy)
            {
                custom();
                //diy = false;
            }
            else
            {

                {
                    if (myTarget.GetComponent<Measurements>())
                    {
                        myTarget.GetComponent<Measurements>().setDistanceObject(lefthand);
                        Vector2 disLeft = myTarget.GetComponent<Measurements>().getDistance();
                        distanceLeft.text = (disLeft.y - 0.6).ToString("0.00") + "米";

                        Bounds bounds = myTarget.GetComponent<Collider>().bounds;
                        Vector3 point1 = bounds.ClosestPoint(righthand.transform.position);
                        Vector3 point2 = righthand.transform.position;
                        distanceRight.text = (Vector3.Distance(point1, point2) - 0.6).ToString("0.00") + "米";
                        if (disLeft.y > 0.45 && Vector3.Distance(point1, point2) > 0.45)
                        {
                            res.text = "否";
                        }
                        else
                        {
                            res.text = "是";
                        }
                    }
                    else
                    {
                        myTarget.AddComponent<Measurements>();
                        myTarget.GetComponent<Measurements>().setDistanceObject(lefthand);
                        Vector2 disLeft = myTarget.GetComponent<Measurements>().getDistance();
                        distanceLeft.text = Mathf.Abs((float)(disLeft.y - 0.6)).ToString("0.00") + "米";

                        Bounds bounds = myTarget.GetComponent<Collider>().bounds;
                        Vector3 point1 = bounds.ClosestPoint(righthand.transform.position);
                        Vector3 point2 = righthand.transform.position;
                        distanceRight.text = Mathf.Abs((float)(Vector3.Distance(point1, point2) - 0.6)).ToString("0.00") + "米";
                        if (disLeft.y > 0.45 && Vector3.Distance(point1, point2) > 0.45)
                        {
                            res.text = "否";
                        }
                        else
                        {
                            res.text = "是";
                        }
                    }
                    GameObject Scripts = GameObject.Find("2DUI");
                    Scripts.GetComponent<AnalyseUIController>().refresh();
                    Scripts.GetComponent<Frame>().frame(myTarget, myHuman, eye, dd.captionText.text);

                    if (choose == 0)
                    {
                        CapacityAnalyse.BUPCapacity(myTarget.transform, myHuman);
                        ReachableAnalyse.BUPReachable(myTarget.transform, leftshoulder, rightshoulder, myHuman);

                    }
                    if (choose == 1)
                    {
                        CapacityAnalyse.OWPCapacity(myTarget.transform, myHuman);
                        ReachableAnalyse.OWPReachable(myTarget.transform, myHuman);
                        ReachableAnalyse.OWPAnalyse(myTarget.transform, myHuman);
                    }
                    if (choose == 2)
                    {
                        CapacityAnalyse.ECPCapacity(myTarget.transform, myHuman);
                        ReachableAnalyse.ECPReachable(myTarget.transform, myHuman);
                    }
                    if (choose == 3)
                    {
                        CapacityAnalyse.ECPCapacity(myTarget.transform, myHuman);
                        ReachableAnalyse.ECPReachable(myTarget.transform, myHuman);
                    }
                }
            }
        }
    }

    public void stop()
    {
        human = false;
        target = false;
        flag = true;
        showFlag = false;
        if (sphereLeft)
        {
            sphereLeft.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            sphereRight.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        }
        
        flag = false;
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
        print("?");
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
