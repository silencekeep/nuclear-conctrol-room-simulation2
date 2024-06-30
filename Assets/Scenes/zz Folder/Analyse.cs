using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CNCC.Models;

public class Analyse : MonoBehaviour
{
    public Camera Camera;
    public InputField humanName;
    private GameObject myTarget;
    public InputField targetName;
    private bool target;


    public Toggle keshiToggle;
    public Toggle kedaToggle;
    public Toggle ronfzuToggle;
    public Toggle pingmuToggle;
    public Dropdown dd;
    private int choose;
    private bool human;
    private bool flag;
    public bool refreshFlag = true;
    private GameObject myHuman;
    private GameObject eye;
    private GameObject leftshoulder;
    private GameObject rightshoulder;
    private GameObject pantai;

    public GameObject caibiaoPanel;
    public GameObject table;
    private bool diy;

    // Start is called before the first frame update
    void Start()
    {
        human = false;
        flag = true;
        diy = false;
    }

    // Update is called once per frame
    void Update()
    {
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
                    if (myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManLeftShoulder/LowManLeftArm/LowManLeftForeArm/LowManLeftHand"))
                    {
                        eye = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManNeck/LowManHead/LowManHead_end").gameObject;
                        leftshoulder = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManLeftShoulder").gameObject;
                        rightshoulder = myHuman.transform.Find("Male Bones/LowManHips/LowManSpine/LowManSpine1/LowManSpine2/LowManRightShoulder").gameObject;
                    }
                    else
                    {
                        eye = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M").gameObject;
                        leftshoulder = myHuman.transform.Find("Root_M/Spine1_M/Chest_M").gameObject;
                        rightshoulder = myHuman.transform.Find("Root_M/Spine1_M/Chest_M").gameObject;
                    }
                    */
                    eye = myHuman.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).gameObject;
                    //leftshoulder = myHuman.transform.Find("Root_M/Spine1_M/Chest_M").gameObject;
                    leftshoulder = myHuman.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftUpperArm).gameObject;
                    //rightshoulder = myHuman.transform.Find("Root_M/Spine1_M/Chest_M").gameObject;
                    rightshoulder = myHuman.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightUpperArm).gameObject;
                    refreshFlag = true;
                }
            }
            human = false;

        }
        
    }

    public void humanChoose()
    {
        human = true;
    }


    public void start()
    {
        flag = true;
    }

    public void stop()
    {
        human = false;
        flag = false;
        humanName.text = "";
        diy = false;
    }

    public void targetChoose()
    {
        target = true;
    }

    public void analyse()
    {
        if (humanName.text != "" && targetName.text != "" && flag)

        {
            pantai = myTarget;
            GameObject Scripts = GameObject.Find("2DUI");
            Scripts.GetComponent<AnalyseUIController>().refresh();
            Scripts.GetComponent<Frame>().frame(pantai, myHuman, eye, dd.captionText.text);

            if (diy)
            {
                custom();
                //diy = false;
            }
            else
            {
                if (choose == 0)
                {
                    if (keshiToggle.isOn) VisibilityAnalyse.BUPVisibility(pantai.transform, eye, myHuman);
                    if (ronfzuToggle.isOn) CapacityAnalyse.BUPCapacity(pantai.transform, myHuman);
                    if (kedaToggle.isOn) ReachableAnalyse.BUPReachable(pantai.transform, leftshoulder, rightshoulder, myHuman);

                }
                if (choose == 1)
                {
                    if (ronfzuToggle.isOn) CapacityAnalyse.OWPCapacity(pantai.transform, myHuman);
                    if (keshiToggle.isOn) VisibilityAnalyse.OWPVisibility(pantai.transform, eye, myHuman);
                    if (kedaToggle.isOn) ReachableAnalyse.OWPReachable(pantai.transform, myHuman);
                    if (kedaToggle.isOn) ReachableAnalyse.OWPAnalyse(pantai.transform, myHuman);
                }
                if (choose == 2)
                {
                    if (ronfzuToggle.isOn) CapacityAnalyse.ECPCapacity(pantai.transform, myHuman);
                    if (keshiToggle.isOn) VisibilityAnalyse.ECPVisibility(pantai.transform, eye, myHuman);
                    if (kedaToggle.isOn) ReachableAnalyse.ECPReachable(pantai.transform, myHuman);
                }
                if (choose == 3)
                {
                    if (ronfzuToggle.isOn) CapacityAnalyse.ECPCapacity(pantai.transform, myHuman);
                    if (keshiToggle.isOn) VisibilityAnalyse.ECPVisibility(pantai.transform, eye, myHuman);
                    if (kedaToggle.isOn) ReachableAnalyse.ECPReachable(pantai.transform, myHuman);
                }
                if (choose == 4)
                {
                    VisibilityAnalyse.LDPVisibility(pantai.transform, eye, myHuman);
                }
            }
        }
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
                    ReachableAnalyse.NUREG11_1_1__2(pantai.transform, leftshoulder, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.1-3")
                {
                    ReachableAnalyse.NUREG11_1_1__3(pantai.transform, leftshoulder, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.1-4")
                {
                    ReachableAnalyse.NUREG11_1_1__4(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.1-5")
                {
                    ReachableAnalyse.NUREG11_1_1__5(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.1-6") {
                    VisibilityAnalyse.NUREG11_1_1__6(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.1.5-1(1)") {
                    VisibilityAnalyse.NUREG11_3_1_5__1__1(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.1.5-1(2)")
                {
                    VisibilityAnalyse.NUREG11_3_1_5__1__2(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "1.3.1-4") {
                    VisibilityAnalyse.NUREG1_3_1__4(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "12.1.1.3-5") {
                    CapacityAnalyse.NUREG12_1_1_3__5(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.1-10(1)")
                {
                    CapacityAnalyse.BUPCapacity(pantai.transform, myHuman);
                    continue;
                }
                if (item == "11.1.1-10(2)") {
                    CapacityAnalyse.BUPCapacity(pantai.transform, myHuman);
                    continue;
                }
                if (item == "11.1.2-1") {
                    ReachableAnalyse.NUREG11_1_2__1(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-2") {
                    ReachableAnalyse.OWPReachable(pantai.transform, myHuman);
                    continue;
                }
                if (item == "11.1.2-3")
                {
                    ReachableAnalyse.NUREG11_1_2__3(pantai.transform, leftshoulder, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-6(1)") {
                    VisibilityAnalyse.NUREG11_1_2__6__1(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-6(2)")
                {
                    VisibilityAnalyse.NUREG11_1_2__6__2(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-8")
                {
                    VisibilityAnalyse.NUREG11_1_2__8(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.5-4") {
                    StanardV3.NUreg11_1_5__4(pantai.transform, myHuman, upper, lower);
                }
                if (item == "11.1.2-10(1)") {
                    CapacityAnalyse.NUREG11_1_2__10__1(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-10(2)")
                {
                    CapacityAnalyse.NUREG11_1_2__10__2(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-10(3)")
                {
                    CapacityAnalyse.NUREG11_1_2__10__3(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.1.2-10(4)")
                {
                    CapacityAnalyse.NUREG11_1_2__10__4(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.5.1(1)") {
                    StanardV3.DLT4_5_1(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.5.1(2)")
                {
                    StanardV3.DLT4_5_1(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.5.4.2(1)") {
                    StanardV3.DLT4_5_4_2(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.5.4.2(2)")
                {
                    StanardV3.DLT4_5_4_2(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.5.4.2(3)")
                {
                    StanardV3.DLT4_5_4_2__3(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.6.2") {
                    StanardV3.DLT4_6_2(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.6.4") {
                    StanardV3.DLT4_6_4(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "4.2.1.2") {
                    StanardV3.HAF4_2_1_2(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (standard == "NUREG0700rev3" && item == "11.1.1-5") {
                    StanardV3.NUREG11_1_1__5(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-2") {
                    StanardV3.NUREG11_2_1_1__2(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-3") {
                    StanardV3.NUREG11_2_1_1__3(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-5") {
                    StanardV3.NUREG11_2_1_1__5(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-6(1)") {
                    StanardV3.NUREG11_2_1_1__6(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-6(2)")
                {
                    StanardV3.NUREG11_2_1_1__6(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-7(1)") {
                    StanardV3.NUREG11_2_1_1__7(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.1-7(2)")
                {
                    StanardV3.NUREG11_2_1_1__7(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.2-1")
                {
                    StanardV3.NUREG11_2_1_2__1(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.2-2")
                {
                    StanardV3.NUREG11_2_1_2__2(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.2-3")
                {
                    StanardV3.NUREG11_2_1_2__3(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.2-4")
                {
                    StanardV3.NUREG11_2_1_2__4(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.2-5")
                {
                    StanardV3.NUREG11_2_1_2__5(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.3-1")
                {
                    StanardV3.NUREG11_2_1_3__1(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.3-2")
                {
                    StanardV3.NUREG11_2_1_3__2(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.3-3")
                {
                    StanardV3.NUREG11_2_1_3__3(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.2.1.3-4")
                {
                    StanardV3.NUREG11_2_1_3__4(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.1.2-1")
                {
                    StanardV3.NUREG11_3_1_2__1(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.1.2-2")
                {
                    StanardV3.NUREG11_3_1_2__2(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.1.2-3")
                {
                    StanardV3.NUREG11_3_1_2__3(pantai.transform, eye, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.1-2")
                {
                    StanardV3.NUREG11_3_4_1__2(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.1-4")
                {
                    StanardV3.NUREG11_3_4_1__4(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.1-5")
                {
                    StanardV3.NUREG11_3_4_1__5(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.1-6")
                {
                    StanardV3.NUREG11_3_4_1__6(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-1")
                {
                    StanardV3.NUREG11_3_4_2__1(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-2")
                {
                    StanardV3.NUREG11_3_4_2__2(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-3")
                {
                    StanardV3.NUREG11_3_4_2__3(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-4")
                {
                    StanardV3.NUREG11_3_4_2__4(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-5")
                {
                    StanardV3.NUREG11_3_4_2__5(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-6")
                {
                    StanardV3.NUREG11_3_4_2__6(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-7")
                {
                    StanardV3.NUREG11_3_4_2__7(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "11.3.4.2-9")
                {
                    StanardV3.NUREG11_3_4_2__9(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "12.1.1.2-7(1)")
                {
                    StanardV3.NUREG12_1_1_2__7(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                if (item == "12.1.1.2-7(2)")
                {
                    StanardV3.NUREG12_1_1_2__7__2(pantai.transform, myHuman, upper, lower);
                    continue;
                }
                StanardV3.NewStandard(pantai.transform, eye, leftshoulder, myHuman, upper, lower, type, item, standard, content);
            }
        }
    }

    public void customAnalyse()
    {

        caibiaoPanel.SetActive(true);
        //caibiaoPanel.transform.Find("MainPanel").GetComponent<TableCreate2>().SelectByType(choose);
        diy = true;
    }
}
