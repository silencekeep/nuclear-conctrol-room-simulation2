using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using System.IO;

public class timielinedemoscript : MonoBehaviour
{
    /// <summary>
    /// 设置Timeline的右键菜单事件以及其他事件代码
    /// </summary>
    //public GameObject TimelinePanel;
    //public GameObject DataBasePanel;
    public GameObject TimelinePanel;
    public GameObject DZTimelinePanel;
    //public GameObject HumanText;
    public GameObject TimeLineEditorMenu;
    public GameObject DurSetPanel;
    public GameObject ForceSetPanel;
    public GameObject Pname;
    public GameObject Ptime;
    public GameObject Pposition;
    public GameObject Ptools;
    public GameObject Pforce;
    public Text CurActText;

    public Button ForceBtn;
    public Button AddAfter;
    public Button Durition;
    public Button Property;
    public Button Delete;
    public Dropdown ActDropdown;
    public Dropdown ActTypeDropdown;
    private float TimeWidth;
    // public static bool TimeWidthFlag;使用DuritionScript脚本时用到的变量
    //public bool IsDeleted;   
    public Image ImagePrefab;
    private Transform track;
    private int i;
    public Dropdown HumanDropdown;
    public InputField WaitTimeinputField;
    public GameObject WaitTimeSetPanel;
    public GameObject ActPropertySetPanel;
    public Image WaitActImagePrefab;
    private float WaitTimeWidth;
    public GameObject ActNameUI;
    private float Force;
    public static bool CombBtnClick;

    private void Start()
    {
        CombBtnClick = false;
        //ImagePrefab.AddListener(EventTriggerType.PointerClick, Clicked);
        ForceBtn.GetComponent<Button>().onClick.AddListener(Flag);
        AddAfter.GetComponent<Button>().onClick.AddListener(Flag);
        Durition.GetComponent<Button>().onClick.AddListener(Flag);
        Delete.GetComponent<Button>().onClick.AddListener(Flag);
        Property.GetComponent<Button>().onClick.AddListener(Flag);
        Delete.GetComponent<Button>().onClick.AddListener(DeleteClicked);
        ActPropertySetPanel.SetActive(false);

    }
    private void Awake()
    {
        i = 0;
        //TimeWidthFlag = false;
    }
    void Update()
    {


    }


    public void TimeLineEditor()//右键点击事件：在鼠标位置出现右键菜单
    {
        GameObject canvas = GameObject.Find("Canvas");
        Vector2 localPoint = Input.mousePosition - new Vector3(Screen.width * 0.5f - 50.0f, Screen.height * 0.5f - 50.0f);
        TimeLineEditorMenu.transform.localPosition = localPoint / canvas.transform.localScale.x;
        TimeLineEditorMenu.SetActive(true);
    }


    public void Flag()//控制右键菜单消失
    {
        TimeLineEditorMenu.SetActive(false);
    }
    private void CreateNewActBotton() //在数字人时间轴Track上动态创建动素方块
    {
        //if (HumanDropdown.captionText.text == "数字人1")
        //{
        //    track = GameObject.Find("Track1").transform;

        //}
        //else if(HumanDropdown.captionText.text == "数字人2")
        //{
        //    track = GameObject.Find("Track2").transform;
        //}
        //else if (HumanDropdown.captionText.text == "数字人3")
        //{
        //    track = GameObject.Find("Track3").transform; 
        //}

        if (DZTimelineEditScript.CurTrack.name == "DZTrack1")
        {
            track = GameObject.Find("Track1").transform;

        }
        else if (DZTimelineEditScript.CurTrack.name == "DZTrack2")
        {
            track = GameObject.Find("Track2").transform;
        }
        else if (DZTimelineEditScript.CurTrack.name == "DZTrack3")
        {
            track = GameObject.Find("Track3").transform;
        }
        Image obj = Instantiate(ImagePrefab, transform.position, Quaternion.identity) as Image;
        //print(ImagePrefab.name);
        obj.transform.SetParent(track);
        Text text = obj.GetComponentInChildren<Text>();
        text.text = CurActText.text;

        //按钮显示的位置                                                                                                                                                            
        RectTransform rtr = obj.GetComponent<RectTransform>();
        ////设置父级基准位置                                                                                                                                                            
        //rtr.anchorMin = new Vector2(0.5f, 0.5f);                                                                                                                                                            
        //rtr.anchorMax = new Vector2(0.5f, 0.5f);                                                                                                                                                            
        ////定义控件自身定位点位置                                                                                                                                                            
        //rtr.pivot = new Vector2(0.5f+i*50, 0.5f);                                                                                                                                                            
        ////定义控件定位点相对基准位置的偏移                                                                                                                                                            
        //rtr.anchoredPosition = new Vector2(0, 0);                                                                                                                                                             
        //定义控件大小   
        ActTagScript ActTag = obj.GetComponent<ActTagScript>();
        ActTag.ActTag = DZTimelineEditScript.CurDZTag;
        ActTag.ActType = ActTypeDropdown.captionText.text;
        //设置右键菜单
        rightclick RgtClc = obj.GetComponent<rightclick>();
        RgtClc.rightClick.AddListener(TimeLineEditor);
        //12.28修改
        if (CombBtnClick == false)
        {
            float length = Resources.Load<AnimationClip>("Animation/" + ActTag.ActType.Trim() + "/" + text.text.Trim()).length * 10;
            rtr.sizeDelta = new Vector2(length, 20);
        }
        else if (CombBtnClick == true)
        {            
            float length = Resources.Load<AnimationClip>("Animation/动素组合/"  + text.text.Trim()).length * 10;
            rtr.sizeDelta = new Vector2(length, 20);
        }
        
        //2020.10.8  坐标轴刻度的x坐标 TimeAxis.transform.localPosition.x+ (60f * i),60f=时间长度60s 存在误差
        Transform textsize = obj.transform.GetChild(0);
        textsize.GetComponent<RectTransform>().sizeDelta = obj.GetComponent<RectTransform>().sizeDelta;


        //ActTag.ActTime = textsize.GetComponent<RectTransform>().sizeDelta.x;
        //ActTag.ActForce =  Force;

        
        // RgtClc.pointOverEnter.AddListener(ActPointOverEnter);
        // RgtClc.pointOverExit.AddListener(ActPointOverExit);
        // DuritionScript dursrc = obj.GetComponent<DuritionScript>();
        // dursrc.inputField = InputField.Find("Time1");
    }
    public void CombBtnClicked()
    {
        CombBtnClick = true;
    }

    //public  void ActImport()//导入json时调用，用于导入动素层数据
    //{
    //    for (int j = 1; j < 4; j++)
    //    {
    //        string json_text = File.ReadAllText(Application.dataPath + "/Resources/JsonFile/Soldier" + j.ToString() + ".json");
    //        JsonFile[] json = JsonHelper.FromJson<JsonFile>(json_text);
    //        string soldierName = json[0].AnimationName;            
    //        if (soldierName == "soldier1")
    //        {
    //            track = GameObject.Find("DZTrack1").transform;
    //        }
    //        else if (soldierName == "soldier2")
    //        {
    //            track = GameObject.Find("DZTrack2").transform;
    //        }
    //        else if (soldierName == "soldier3")
    //        {
    //            track = GameObject.Find("DZTrack3").transform;
    //        }
    //        for (int i = 1; i < json.Length; i++)
    //        {
    //            //Image obj;
    //            if (json[i].ActionName == "等待")
    //            {
    //                Image WaitActImagePrefab = Resources.Load<Image>("Prefabs/WaitActImage");
    //                Image obj = Instantiate(WaitActImagePrefab, transform.position, Quaternion.identity) as Image;
    //                obj.transform.SetParent(track);
    //                Text text = obj.GetComponentInChildren<Text>();
    //                text.text = json[i].ActionName;
    //                //按钮显示的位置                                                                                                                                                            
    //                RectTransform rtr = obj.GetComponent<RectTransform>();
    //                //定义控件大小                                                                                                                                                           
    //                rtr.sizeDelta = new Vector2(json[i].ActTime, 20);
    //                Transform textsize = obj.transform.GetChild(0);
    //                textsize.GetComponent<RectTransform>().sizeDelta = obj.GetComponent<RectTransform>().sizeDelta;

    //                ActTagScript ActTag = obj.GetComponent<ActTagScript>();
    //                ActTag.ActTag = json[i].ActTag;
    //                ActTag.ActType = json[i].ActType;
    //                ActTag.ActForce = json[i].ActForce;
    //                //设置右键菜单
    //                rightclick RgtClc = obj.GetComponent<rightclick>();
    //                RgtClc.rightClick.AddListener(TimeLineEditor);
    //            }
    //            else
    //            {
    //                Image ImagePrefab = Resources.Load<Image>("Prefabs/Image");
    //                print(ImagePrefab.name);
    //                Image obj = Instantiate(ImagePrefab, transform.position, Quaternion.identity) as Image;
    //                obj.transform.SetParent(track);
    //                Text text = obj.GetComponentInChildren<Text>();
    //                text.text = json[i].ActionName;
    //                //按钮显示的位置                                                                                                                                                            
    //                RectTransform rtr = obj.GetComponent<RectTransform>();
    //                //定义控件大小                                                                                                                                                           
    //                rtr.sizeDelta = new Vector2(json[i].ActTime, 20);
    //                Transform textsize = obj.transform.GetChild(0);
    //                textsize.GetComponent<RectTransform>().sizeDelta = obj.GetComponent<RectTransform>().sizeDelta;

    //                ActTagScript ActTag = obj.GetComponent<ActTagScript>();
    //                ActTag.ActTag = json[i].ActTag;
    //                ActTag.ActType = json[i].ActType;
    //                ActTag.ActForce = json[i].ActForce;
    //                //设置右键菜单
    //                rightclick RgtClc = obj.GetComponent<rightclick>();
    //                RgtClc.rightClick.AddListener(TimeLineEditor);
    //            }

    //        }

    //    }

    //}
    public void AddAfterClicked()
    {
        CreateNewActBotton();
    }
    public void DuritionClicked()//时间按钮点击事件
    {
        DurSetPanel.SetActive(true);
    }

    public void ActPropertyClickd()//右键菜单中属性按钮点击事件
    {
        ActPropertySetPanel.SetActive(true);
        GameObject CurRgtClcAct = rightclick.CurrentRightClickedAct;
        if (CurRgtClcAct.GetComponentsInChildren<Transform>(true).Length > 1)
        {
            GameObject soldier;
            Text text = CurRgtClcAct.GetComponentInChildren<Text>();
            Pname.GetComponent<Text>().text = text.text;
            float acttimeWidth = CurRgtClcAct.GetComponent<RectTransform>().sizeDelta.x;
            float Realacttime = acttimeWidth / 10;
            Ptime.GetComponent<Text>().text = Realacttime.ToString("#0.000") + "s";
            Ptools.GetComponent<Text>().text = CurRgtClcAct.GetComponent<ActTagScript>().ActTools;
            Pforce.GetComponent<Text>().text = CurRgtClcAct.GetComponent<ActTagScript>().ActForce + "N";

            if (CurRgtClcAct.transform.parent.name == "Track1")
            {
                soldier = GameObject.Find("soldier1");
                Pposition.GetComponent<Text>().text = "X:" + soldier.GetComponent<Transform>().position.x.ToString("#0.000")
                    + " Y:" + soldier.GetComponent<Transform>().position.y.ToString("#0.000")
                    + " Z:" + soldier.GetComponent<Transform>().position.z.ToString("#0.000");
                print(Pposition.GetComponent<Text>().text);
            }
            else if (CurRgtClcAct.transform.parent.name == "Track2")
            {
                soldier = GameObject.Find("soldier2");
                Pposition.GetComponent<Text>().text = "X:" + soldier.GetComponent<Transform>().position.x.ToString("#0.000")
                    + " Y:" + soldier.GetComponent<Transform>().position.y.ToString("#0.000")
                    + " Z:" + soldier.GetComponent<Transform>().position.z.ToString("#0.000");

            }
            else if (CurRgtClcAct.transform.parent.name == "Track3")
            {
                soldier = GameObject.Find("soldier3");
                Pposition.GetComponent<Text>().text = "X:" + soldier.GetComponent<Transform>().position.x.ToString("#0.000")
                    + " Y:" + soldier.GetComponent<Transform>().position.y.ToString("#0.000")
                    + " Z:" + soldier.GetComponent<Transform>().position.z.ToString("#0.000");
            }

        }
        else
        {
            GameObject soldier;
            Text text = CurRgtClcAct.transform.parent.GetComponentInChildren<Text>();
            Pname.GetComponent<Text>().text = text.text;
            float acttimeWidth = CurRgtClcAct.GetComponent<RectTransform>().sizeDelta.x;
            float Realacttime = acttimeWidth / 10;
            Ptime.GetComponent<Text>().text = Realacttime.ToString("#0.000") + "s";
            //Ptime.GetComponent<Text>().text = CurRgtClcAct.transform.parent.GetComponent<RectTransform>().sizeDelta.x.ToString() + "s";
            Ptools.GetComponent<Text>().text = CurRgtClcAct.transform.parent.GetComponent<ActTagScript>().ActTools;
            Pforce.GetComponent<Text>().text = CurRgtClcAct.transform.parent.GetComponent<ActTagScript>().ActForce + "N";

            if (CurRgtClcAct.transform.parent.transform.parent.name == "Track1")
            {
                soldier = GameObject.Find("soldier1");
                Pposition.GetComponent<Text>().text = "X:" + soldier.GetComponent<Transform>().position.x.ToString("#0.000")
                    + " Y:" + soldier.GetComponent<Transform>().position.y.ToString("#0.000")
                    + " Z:" + soldier.GetComponent<Transform>().position.z.ToString("#0.000");
            }
            else if (CurRgtClcAct.transform.parent.transform.parent.name == "Track2")
            {
                soldier = GameObject.Find("soldier2");
                Pposition.GetComponent<Text>().text = "X:" + soldier.GetComponent<Transform>().position.x.ToString("#0.000")
                    + " Y:" + soldier.GetComponent<Transform>().position.y.ToString("#0.000")
                    + " Z:" + soldier.GetComponent<Transform>().position.z.ToString("#0.000");
            }
            else if (CurRgtClcAct.transform.parent.transform.parent.name == "Track3")
            {
                soldier = GameObject.Find("soldier3");
                Pposition.GetComponent<Text>().text = "X:" + soldier.GetComponent<Transform>().position.x.ToString("#0.000")
                    + " Y:" + soldier.GetComponent<Transform>().position.y.ToString("#0.000")
                    + " Z:" + soldier.GetComponent<Transform>().position.z.ToString("#0.000");
            }
        }


    }

    public void ActPropertyPanelClose()//属性页面关闭按钮点击事件
    {
        ActPropertySetPanel.SetActive(false);
    }
    public void ForceClicked()//操作力按钮点击事件
    {
        ForceSetPanel.SetActive(true);
    }

    public void ForceSureClicked()//操作力panel中的确定按钮点击事件
    {
        Force = Convert.ToSingle(ForceSetPanel.transform.GetChild(0).GetComponent<InputField>().text);
        GameObject CurRgtClcAct = rightclick.CurrentRightClickedAct;
        if (CurRgtClcAct.GetComponentsInChildren<Transform>(true).Length > 1)
        {
            CurRgtClcAct.GetComponent<ActTagScript>().ActForce = Force;
            ForceSetPanel.SetActive(false);
        }
        else
        {
            CurRgtClcAct.transform.parent.GetComponent<ActTagScript>().ActForce = Force;
            ForceSetPanel.SetActive(false);
        }
    }
    public void ForceCanelClicked()//时间panel中的取消按钮点击事件
    {
        ForceSetPanel.SetActive(false);
    }

    public void DuritionSureClicked()//时间panel中的确定按钮点击事件
    {
        TimeWidth = 10*Convert.ToSingle(GameObject.Find("Time1").GetComponent<InputField>().text);
        GameObject CurRgtClcAct = rightclick.CurrentRightClickedAct;
        if (CurRgtClcAct.GetComponentsInChildren<Transform>(true).Length > 1)
        {
            CurRgtClcAct.GetComponent<RectTransform>().sizeDelta = new Vector2(TimeWidth, 20);
            Transform text = CurRgtClcAct.transform.GetChild(0);
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(TimeWidth, 20);
            //TimeWidthFlag = true;

            DurSetPanel.SetActive(false);
        }
        else
        {
            CurRgtClcAct.GetComponent<RectTransform>().sizeDelta = new Vector2(TimeWidth, 20);
            Transform text = CurRgtClcAct.transform.parent;
            text.GetComponent<RectTransform>().sizeDelta = new Vector2(TimeWidth, 20);
            // TimeWidthFlag = true;

            DurSetPanel.SetActive(false);
        }
    }
    public void DuritionCanelClicked()//时间panel中的取消按钮点击事件
    {
        //TimeWidthFlag = false;
        DurSetPanel.SetActive(false);

    }
    public void DeleteClicked()//删除按钮点击事件
    {
        GameObject CurRgtClcAct = rightclick.CurrentRightClickedAct;
        if (CurRgtClcAct.GetComponentsInChildren<Transform>(true).Length > 1)
        {
            Destroy(CurRgtClcAct);
            //CurRgtClcAct.SetActive(false);           
            //DurSetPanel.SetActive(false);
        }
        else
        {
            Destroy(CurRgtClcAct);
            Destroy(CurRgtClcAct.transform.parent.gameObject);
            //CurRgtClcAct.transform.parent.gameObject.SetActive(false);
            //CurRgtClcAct.SetActive(false);
            //DurSetPanel.SetActive(false);
        }

    }
    public void AddWaitActClicked()//添加等待时间按钮的点击事件
    {
        WaitTimeSetPanel.SetActive(true);
    }
    public void WaitCanelClicked()//等待时间panel中的取消按钮点击事件
    {
        WaitTimeSetPanel.SetActive(false);
    }
    public void WaitSureClicked()//等待时间panel中的确定按钮点击事件
    {
        WaitTimeWidth = Convert.ToSingle(GameObject.Find("WaitTime").GetComponent<InputField>().text);
        CreateWaitActImage();
        //TimeWidthFlag = true;
        WaitTimeSetPanel.SetActive(false);
    }



    private void CreateWaitActImage()//创建等待的动素矩形
    {
        if (HumanDropdown.captionText.text == "数字人1")
        {
            track = GameObject.Find("Track1").transform;
        }
        else if (HumanDropdown.captionText.text == "数字人2")
        {
            track = GameObject.Find("Track2").transform;
        }
        else if (HumanDropdown.captionText.text == "数字人3")
        {
            track = GameObject.Find("Track3").transform;
        }
        Image Waitobj = Instantiate(WaitActImagePrefab, transform.position, Quaternion.identity) as Image;
        Waitobj.transform.SetParent(track);
        //按钮显示的位置                                                                                                                                                            
        RectTransform rtr = Waitobj.GetComponent<RectTransform>();
        //定义控件大小                                                                                                                                                           
        rtr.sizeDelta = new Vector2(WaitTimeWidth, 14);//此处的50应为实际动素时间长度的转化值！待修改

        //设置右键菜单
        rightclick RgtClc = Waitobj.GetComponent<rightclick>();
        RgtClc.rightClick.AddListener(TimeLineEditor);
        ActTagScript ActTag = Waitobj.GetComponent<ActTagScript>();
        ActTag.ActTag = DZTimelineEditScript.CurDZTag;
        ActTag.ActType = "等待";
    }

    //创建将要输出为json file的类
    [Serializable]
    public class JsonFile
    {
        public int Index;
        public string MotionName;
        public string AnimationName;
        public string ActionName;
        public float DZTime;
        public float ActTime;
        public int DZTag;
        public int ActTag;
        public string ActType;
        public int CurDZTag;
        public string CurDZNameText;
        public float ActForce;
    }
    //封装JsonHelper类, 用以使ToJson可以输出Array
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

    //public void ActPointOverEnter()//鼠标悬浮在动素矩形上显示动素全称
    //{
    //    GameObject canvas = GameObject.Find("Canvas");
    //    Vector2 localPoint = Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);       
    //    ActNameUI.transform.localPosition = localPoint / canvas.transform.localScale.x;
    //    GameObject CurPointAct = rightclick.CurrenPointAct;
    //    ActNameUI.SetActive(true);        
    //    Text text = CurPointAct.GetComponentInChildren<Text>();       
    //    ActNameUI.GetComponentInChildren<Text>().text = text.text;        
    //}
    //public void ActPointOverExit()//鼠标悬浮在动素矩形上动素全称消失
    //{
    //    ActNameUI.SetActive(false);
    //}


}
