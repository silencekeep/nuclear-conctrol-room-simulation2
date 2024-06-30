using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using System.IO;

public class DZTimelineEditScript : MonoBehaviour
{
    /// <summary>
    /// 动作UI以及其他事件代码
    /// </summary>
    //public Text DoubleClickHumText;
    public Text StageText;
    public GameObject TimelinePanel;
    public GameObject DZTimelinePanel;
    public GameObject TimeLineEditorMenu;
    public GameObject DZNameSetPanel;
    public GameObject RenameSetPanel;
    public GameObject DataBasePanel;
    //public GameObject DZNameSetFaildPanel;
    public Button AddBefore;
    public Button AddAfter;
    public Button Name;
    public Button Delete;   
    private float TimeWidth;
    public Image DZImagePrefab;
    private Transform track;
    public GameObject HumanText;
    public Dropdown HumanDropdown;

    //public GameObject HumanText1;
    public GameObject DZHumanText2;
    public GameObject DZHumanText3;
    public GameObject DZHumanTimeText2;
    public GameObject DZHumanTimeText3;
    public GameObject DZTrack1;
    public GameObject DZTrack2;
    public GameObject DZTrack3;
    
    public InputField WaitTimeinputField;
    public GameObject WaitTimeSetPanel;
    public Image WaitActImagePrefab;
    private float WaitTimeWidth;
    public InputField DZNameinputField;
    private string DZName;
    private string Rename;
    private bool DZNameinputFlag;
    private bool DZNamecancelFlag;
    public static string CurDZNameText;
    public static int CurDZTag;
    private int DZ_i;
    private float DZWidth;
    public static GameObject CurTrack;
    private GameObject tracki;
    private GameObject Dragtracki;

    //创建list用于输出json文件,也用于记录动素顺序
    public List<JsonFile> jsonFile1 = new List<JsonFile>();
    public List<JsonFile> jsonFile2 = new List<JsonFile>();
    public List<JsonFile> jsonFile3 = new List<JsonFile>();
    void Start()
    {
        DZ_i = 0;
        DZWidth = 0;
        //ImagePrefab.AddListener(EventTriggerType.PointerClick, Clicked);
        //AddBefore.GetComponent<Button>().onClick.AddListener(Flag);
        AddAfter.GetComponent<Button>().onClick.AddListener(Flag);
        Name.GetComponent<Button>().onClick.AddListener(Flag);
        Delete.GetComponent<Button>().onClick.AddListener(Flag);
        Delete.GetComponent<Button>().onClick.AddListener(DeleteClicked);

        if (HumanNumber.humanNum == 1)
        {
            DZHumanText2.SetActive(false);
            DZTrack2.SetActive(false);
            DZHumanTimeText2.SetActive(false);
            GameObject.Find("soldier2").SetActive(false);

            DZHumanText3.SetActive(false);
            DZTrack3.SetActive(false);
            DZHumanTimeText3.SetActive(false);
            GameObject.Find("soldier3").SetActive(false);
            HumanDropdown.options.Clear();
            List<string> humtext = new List<string> { "数字人1" };
            HumanDropdown.AddOptions(humtext);

        }
        else if (HumanNumber.humanNum == 2)
        {
            DZHumanText3.SetActive(false);
            DZTrack3.SetActive(false);
            DZHumanTimeText3.SetActive(false);
            GameObject.Find("soldier3").SetActive(false);
            HumanDropdown.options.Clear();
            List<string> humtext = new List<string> { "数字人1", "数字人2" };
            HumanDropdown.AddOptions(humtext);
        }
        DataBasePanel.SetActive(false);
    }
    private void Awake()
    {
      
    }

    public void TimeLineEditor()//右键点击事件：在鼠标位置出现右键菜单
    {
        GameObject canvas = GameObject.Find("Canvas");
        Vector2 localPoint = Input.mousePosition - new Vector3(Screen.width * 0.5f - 50.0f, Screen.height * 0.5f - 50.0f);
        TimeLineEditorMenu.transform.localPosition = localPoint / canvas.transform.localScale.x;
        TimeLineEditorMenu.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (TimelinePanel.activeInHierarchy == true)
        {
            HumanText.SetActive(false);
            HumanDropdown.gameObject.SetActive(false);
        }
        else if (DZTimelinePanel.activeInHierarchy == true)
        {
            HumanText.SetActive(true);
            HumanDropdown.gameObject.SetActive(true);
        }

        if (DZImageDragDrop.DragTrackFlag == true)
        {
            GetAllActTag();
        }

        
    }

    public void RightMenu()//右键点击事件：在鼠标位置出现右键菜单
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
    private void CreateNewDZBotton() //在数字人时间轴Track1上动态创建动作方块
    {
        DZNameSetPanel.SetActive(true);
        
    }
    public void DZNameEditSureClicked()//添加新动作
    {
        //创建动作方块
        DZName=DZNameinputField.text;
        DZNameinputFlag = true;
        DZNamecancelFlag = false;
        DZNameSetPanel.SetActive(false);
        if (DZNameinputFlag == true && DZNamecancelFlag == false)//输入动作名字且未点取消
        {
           //if (HumanDropdown.captionText.text == "数字人1")
            if (HumanDropdown.value == 0)
            {
                track = GameObject.Find("DZTrack1").transform;

            }
            else if (HumanDropdown.value == 1)
            {
                track = GameObject.Find("DZTrack2").transform;
            }
            else if (HumanDropdown.value == 2)
            {
                track = GameObject.Find("DZTrack3").transform;
            }
            CurTrack = track.gameObject;
            Image obj = Instantiate(DZImagePrefab, transform.position, Quaternion.identity) as Image;
            obj.transform.SetParent(track);
            //按钮显示的位置                                                                                                                                                            
            RectTransform rtr = obj.GetComponent<RectTransform>();
            rtr.sizeDelta = new Vector2(60f, 20);//此处的50应为实际动素时间长度的转化值！待修改  

            Text text = obj.GetComponentInChildren<Text>();
            text.text = DZName;
            Transform textsize = obj.transform.GetChild(0);
            textsize.GetComponent<RectTransform>().sizeDelta = obj.GetComponent<RectTransform>().sizeDelta;

            DZ_i = DZ_i + 1;//动作的生成数量（序号）

            //添加右击事件
            rightclick RgtClc = obj.GetComponent<rightclick>();
            RgtClc.rightClick.AddListener(RightMenu);

            //添加双击事件
            DoubleClick DblClc = obj.GetComponent<DoubleClick>();
            DblClc.doubleClick.AddListener(DZImageDoubleClicked);

            //添加动作标签
            DZTagScript DZTag = obj.GetComponent<DZTagScript>();
            DZTag.DZActTag = DZ_i;
            //添加动作的阶段标签
            DZTag.DZStageTag = StageText.text;

            CurDZTag = DZTag.DZActTag;           
            CurDZNameText = DZName;
            DZNameinputFlag = false;

            //动作界面消失，显示动素编辑界面
            TimelinePanel.SetActive(true);
            DataBasePanel.SetActive(true);
            DZTimelinePanel.SetActive(false);
            SelectActImage();//遍历所有的动素image，显示Tag一致的动素        

        }
        else if (DZNameinputFlag == false && DZNamecancelFlag == true)//未输入动作名字但点了取消
        {
            DZNamecancelFlag = false;
        }
        else if (DZNameinputFlag == false && DZNamecancelFlag == false)//未输入动作名字也没点取消
        {
            //DZNameSetFaildPanel.SetActive(true);
           
        }
    }

    public void DZJsonImport()//导入json时调用，用于导入动作层数据
    {
        for (int j = 1; j < 4; j++)
        {
            string json_text = File.ReadAllText(System.Environment.CurrentDirectory + "/Json" + j.ToString() + ".json");
            JsonFile[] json = JsonHelper.FromJson<JsonFile>(json_text);            
            string soldierName = json[0].AnimationName;
            CurDZTag = json[0].CurDZTag;
            //CurDZNameText = json[0].CurDZNameText;
            if (soldierName == "数字人1")
            {
                track = DZTrack1.transform;
            }
            else if (soldierName == "数字人2")
            {
                track = DZTrack2.transform;
            }
            else if (soldierName == "数字人3")
            {
                track = DZTrack3.transform;
            }
            CurTrack = track.gameObject;
            int tmp = json.Length;
            int Cur_Tag = 0;
            int DZ_Index = 0;
            float [] DZtime =new float[json.Length];
            int[] DZtag = new int[json.Length];
            string[] DZname = new string[json.Length];
            for (int i = 1; i < tmp; i++)
            {

                if (Cur_Tag != json[i].DZTag)
                {
                    DZtime[DZ_Index] = json[i].DZTime;
                    DZtag[DZ_Index] = json[i].DZTag;
                    DZname[DZ_Index] = json[i].MotionName;
                    DZ_Index = DZ_Index + 1;                   
                    Cur_Tag = json[i].DZTag;
                }
            }            
            for (int i = 1; i < DZ_Index+1; i++)
            {
                //string DZName = json[i].MotionName;
                Image obj = Instantiate(DZImagePrefab, transform.position, Quaternion.identity) as Image;
                obj.transform.SetParent(track);

                //按钮显示的位置                                                                                                                                                            
                RectTransform rtr = obj.GetComponent<RectTransform>();
                rtr.sizeDelta = new Vector2(DZtime[i-1], 20);
                Text text = obj.GetComponentInChildren<Text>();
                text.text = DZname[i-1];
                Transform textsize = obj.transform.GetChild(0);
                textsize.GetComponent<RectTransform>().sizeDelta = obj.GetComponent<RectTransform>().sizeDelta;
                //DZ_i = DZ_i + 1;//动作的生成数量（序号）
                //添加右击事件
                rightclick RgtClc = obj.GetComponent<rightclick>();
                RgtClc.rightClick.AddListener(RightMenu);
                //添加双击事件
                DoubleClick DblClc = obj.GetComponent<DoubleClick>();
                DblClc.doubleClick.AddListener(DZImageDoubleClicked);
                //添加动作标签
                DZTagScript DZTag = obj.GetComponent<DZTagScript>();
                DZTag.DZActTag = DZtag[i - 1];
                DZTag.DZTime = DZtime[i - 1];

                DZ_i = DZ_i > DZtag[i - 1] ? DZ_i : DZtag[i - 1];
            }
        }        
    }

    public void ActImport()//导入json时调用，用于导入动素层数据
    {
        for (int j = 1; j < 4; j++)
        {
            string json_text = File.ReadAllText(System.Environment.CurrentDirectory + "/Json" + j.ToString() + ".json");
            JsonFile[] json = JsonHelper.FromJson<JsonFile>(json_text);
            string soldierName = json[0].AnimationName;
            if (soldierName == "数字人1")
            {
                track = GameObject.Find("Track1").transform;
            }
            else if (soldierName == "数字人2")
            {
                track = GameObject.Find("Track2").transform;
            }
            else if (soldierName == "数字人3")
            {
                track = GameObject.Find("Track3").transform;
            }
            for (int i = 1; i < json.Length; i++)
            {
                //Image obj;
                if (json[i].ActionName == "等待")
                {                    
                    Image obj = Instantiate(WaitActImagePrefab, transform.position, Quaternion.identity) as Image;
                    obj.transform.SetParent(track);
                    Text text = obj.GetComponentInChildren<Text>();
                    text.text = json[i].ActionName;
                    //按钮显示的位置                                                                                                                                                            
                    RectTransform rtr = obj.GetComponent<RectTransform>();
                    //定义控件大小                                                                                                                                                           
                    rtr.sizeDelta = new Vector2(json[i].ActTime*10, 20);
                    Transform textsize = obj.transform.GetChild(0);
                    textsize.GetComponent<RectTransform>().sizeDelta = obj.GetComponent<RectTransform>().sizeDelta;

                    ActTagScript ActTag = obj.GetComponent<ActTagScript>();
                    ActTag.ActTag = json[i].ActTag;
                    ActTag.ActType = json[i].ActType;
                    ActTag.ActForce = json[i].ActForce;
                    //设置右键菜单
                    rightclick RgtClc = obj.GetComponent<rightclick>();
                    RgtClc.rightClick.AddListener(TimeLineEditor);
                }
                else
                {
                    Image ImagePrefab = Resources.Load<Image>("Prefabs/Image");
                    print(ImagePrefab.name);
                    Image obj = Instantiate(ImagePrefab, transform.position, Quaternion.identity) as Image;
                    obj.transform.SetParent(track);
                    Text text = obj.GetComponentInChildren<Text>();
                    text.text = json[i].ActionName;
                    //按钮显示的位置                                                                                                                                                            
                    RectTransform rtr = obj.GetComponent<RectTransform>();
                    //定义控件大小                                                                                                                                                           
                    rtr.sizeDelta = new Vector2(json[i].ActTime*10, 20);
                    Transform textsize = obj.transform.GetChild(0);
                    textsize.GetComponent<RectTransform>().sizeDelta = obj.GetComponent<RectTransform>().sizeDelta;

                    ActTagScript ActTag = obj.GetComponent<ActTagScript>();
                    ActTag.ActTag = json[i].ActTag;
                    ActTag.ActType = json[i].ActType;
                    ActTag.ActForce = json[i].ActForce;
                    ActTag.ActTime = json[i].ActTime;
                    //设置右键菜单
                    rightclick RgtClc = obj.GetComponent<rightclick>();
                    RgtClc.rightClick.AddListener(TimeLineEditor);
                }

            }

        }

    }
    public void TimelineImport()//导入json时调用
    {
        DZJsonImport();
        TimelinePanel.SetActive(true);
        ActImport();
        TimelinePanel.SetActive(false);
    }
    public void DZNameEditCancelClicked()
    {
        DZNamecancelFlag = true;
        DZNameSetPanel.SetActive(false);
    }
    public void AddAfterClicked()
    {
        DZNameSetPanel.SetActive(true);
    }

    public void DZImageDoubleClicked()//动作Image双击事件
    {
        GameObject CurDblClcAct = DoubleClick.CurrentDoubleClickedDZAct;
        if (CurDblClcAct.GetComponentsInChildren<Transform>(true).Length > 1)//双击的是image
        {
            Text text = CurDblClcAct.GetComponentInChildren<Text>();
            CurDZNameText = text.text;
            DZTagScript DblClcDZTag = CurDblClcAct.GetComponent<DZTagScript>();
            CurDZTag = DblClcDZTag.DZActTag;
            CurTrack = CurDblClcAct.transform.parent.gameObject;
            RenameSetPanel.SetActive(false);
            //DoubleClickHumText.text = CurTrack.name;
        }
        else//双击的是text
        {
            Text text = CurDblClcAct.transform.parent.GetComponentInChildren<Text>();
            CurDZNameText = text.text;
            DZTagScript DblClcDZTag = CurDblClcAct.GetComponentInParent<DZTagScript>();
            CurDZTag = DblClcDZTag.DZActTag;
            CurTrack = CurDblClcAct.transform.parent.gameObject.transform.parent.gameObject;
            RenameSetPanel.SetActive(false);
            //DoubleClickHumText.text = CurTrack.name;
        }
        DZTimelinePanel.SetActive(false);
        TimelinePanel.SetActive(true);
        DataBasePanel.SetActive(true); 
        SelectActImage();//遍历所有的动素image，显示Tag一致的动素        
    }

    private void SelectActImage()//遍历所有的动素image，显示Tag一致的动素（ActTag=CurDZTag）
    {
        GameObject track1 = GameObject.Find("Track1");//将track1中的image有选择的显示        
        for (int i = 0; i<track1.transform.childCount; i++)
        {
            Transform Track1Childi = track1.transform.GetChild(i);
            ActTagScript ActTag = Track1Childi.GetComponent<ActTagScript>();
            if (ActTag.ActTag == CurDZTag)
            {
                Track1Childi.gameObject.SetActive(true);
                Track1Childi.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                ShowHideImage SHI = Track1Childi.GetComponent<ShowHideImage>();
                SHI.Show();
            }
            else
            {
                if (ActTag.ActTag != 0)
                {
                    ShowHideImage SHI = Track1Childi.GetComponent<ShowHideImage>();
                    SHI.Hide();
                }
            }
        }
        if (HumanNumber.humanNum >1)
        {
            GameObject track2 = GameObject.Find("Track2");//将track2中的image有选择的显示
            for (int i = 0; i < track2.transform.childCount; i++)
            {
                Transform Track2Childi = track2.transform.GetChild(i);
                ActTagScript ActTag = Track2Childi.GetComponent<ActTagScript>();
                if (ActTag.ActTag == CurDZTag)
                {
                    Track2Childi.gameObject.SetActive(true);
                    Track2Childi.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    ShowHideImage SHI = Track2Childi.GetComponent<ShowHideImage>();
                    SHI.Show();
                }
                else
                {
                    if (ActTag.ActTag != 0)
                    {
                        ShowHideImage SHI = Track2Childi.GetComponent<ShowHideImage>();
                        SHI.Hide();
                    }
                }
            }
            if (HumanNumber.humanNum >2)
            {
                GameObject track3 = GameObject.Find("Track3");//将track3中的image有选择的显示
                for (int i = 0; i < track3.transform.childCount; i++)
                {
                    Transform Track3Childi = track3.transform.GetChild(i);
                    ActTagScript ActTag = Track3Childi.GetComponent<ActTagScript>();
                    if (ActTag.ActTag == CurDZTag)
                    {
                        Track3Childi.gameObject.SetActive(true);
                        Track3Childi.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        ShowHideImage SHI = Track3Childi.GetComponent<ShowHideImage>();
                        SHI.Show();
                    }
                    else
                    {
                        if (ActTag.ActTag != 0)
                        {
                            ShowHideImage SHI = Track3Childi.GetComponent<ShowHideImage>();
                            SHI.Hide();
                        }
                    }
                }
            }
            
        }
        
    }
    public void BackToDZPanel()//返回动作层按钮触发的事件
    {
        //DZWidth = 0;
        //GameObject track1 = GameObject.Find("Track1");//将track1中相应的动作的image宽度求和     
        if (CurTrack.name == "DZTrack1")
        {
             tracki = GameObject.Find("Track1");

        }
        else if (CurTrack.name == "DZTrack2")
        {
             tracki = GameObject.Find("Track2");
        }
        else if (CurTrack.name == "DZTrack3")
        {
             tracki = GameObject.Find("Track3");
        }
        for (int i = 0; i < tracki.transform.childCount; i++)
        {
            Transform TrackiChildi = tracki.transform.GetChild(i);
            ActTagScript ActTag = TrackiChildi.GetComponent<ActTagScript>();
            if (ActTag.ActTag == CurDZTag)
            {
                DZWidth= DZWidth+ TrackiChildi.GetComponent<RectTransform>().sizeDelta.x;
            }
        }
        DataBasePanel.SetActive(false);
        TimelinePanel.SetActive(false);
        DZTimelinePanel.SetActive(true);
        //GameObject DZtrack1 = GameObject.Find("DZTrack1");//修改DZTrack1中对应动作的宽度      
        for (int i = 0; i < CurTrack.transform.childCount; i++)
        {
            Transform CurTrackChildi = CurTrack.transform.GetChild(i);
            DZTagScript DZActTag = CurTrackChildi.GetComponent<DZTagScript>();
            
            if (DZActTag.DZActTag == CurDZTag)
            {
                CurTrackChildi.GetComponent<RectTransform>().sizeDelta = new Vector2(DZWidth/10, 20);
                CurTrackChildi.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(DZWidth / 10, 20);
                float DZtime = DZWidth/10;
                DZActTag.DZTime = (float)Math.Round(DZtime, 3);
            }            
        }
        DZWidth = 0;
    }
    
    public void RenameClicked()//重命名按钮点击事件
    {
        RenameSetPanel.SetActive(true);
    }
    public void RenameSureClicked()//重命名panel中的确定按钮点击事件
    {
        Rename = GameObject.Find("DZNameInput").GetComponent<InputField>().text;
        GameObject CurRgtClcAct = rightclick.CurrentRightClickedAct;
        if (CurRgtClcAct.GetComponentsInChildren<Transform>(true).Length > 1)
        {
            Text text = CurRgtClcAct.GetComponentInChildren<Text>();
            text.text = Rename;
            RenameSetPanel.SetActive(false);
        }
        else
        {
            Text text = CurRgtClcAct.transform.parent.GetComponentInChildren<Text>();
            text.text = Rename;
            CurDZNameText = Rename;
            RenameSetPanel.SetActive(false);
        }

    }
    public void RenameCancelClicked()
    {
        RenameSetPanel.SetActive(false);
    }
    public void DuritionCanelClicked()//时间panel中的取消按钮点击事件
    {
        RenameSetPanel.SetActive(false);

    }
    public void DeleteClicked()//删除按钮点击事件
    {
        
        GameObject CurRgtClcAct = rightclick.CurrentRightClickedAct;
        GameObject curDeletetrack;
        if (CurRgtClcAct.GetComponentsInChildren<Transform>(true).Length > 1)//点到的是Image(父物体)
        {
            curDeletetrack = CurRgtClcAct.transform.parent.gameObject;
            DZTagScript CurRgtClcDZTag = CurRgtClcAct.GetComponent<DZTagScript>();
            CurDZTag = CurRgtClcDZTag.DZActTag;
            //获取当前要删除的动作是其所在动作阶段的第几个动作，删除对应树状结构中的物体。12.9
            //遍历当前track中所有子物体，如果其阶段tag==当前动作阶段tag，则序号=序号+1
            //int childnum = CurRgtClcAct.transform.GetSiblingIndex();
            //Transform curtrack = CurRgtClcAct.transform.parent;
            string curstage = CurRgtClcDZTag.DZStageTag;//获取当前动作的阶段
            int num = 0;
            for (int i = 0; i < curDeletetrack.transform.childCount; i++)//DZTrack3
            {
                Transform TrackChildi = curDeletetrack.transform.GetChild(i);
                if (TrackChildi.name != "Image Empty(Clone)")
                {
                    DZTagScript StageTag = TrackChildi.GetComponent<DZTagScript>();
                    string stageTagi = StageTag.DZStageTag;
                    if (stageTagi==curstage)
                    {
                        num = num + 1;
                    }
                }
            }
            
            if (curDeletetrack.name == "DZTrack1")
            {
                GameObject curdelete = GameObject.Find(curstage).transform.GetChild(0).transform.GetChild(num-1).gameObject;
                print(curdelete.name);
                Destroy(curdelete);
            }
            else  if(curDeletetrack.name == "DZTrack2")
            {
                GameObject curdelete = GameObject.Find(curstage).transform.GetChild(1).transform.GetChild(num - 1).gameObject;
                print(curdelete.name);
                Destroy(curdelete);
            }
            else if(curDeletetrack.name == "DZTrack3")
            {
                GameObject curdelete = GameObject.Find(curstage).transform.GetChild(2).transform.GetChild(num - 1).gameObject;
                print(curdelete.name);
                Destroy(curdelete);
            }

            Destroy(CurRgtClcAct); 

        }
        else//点到的是文字（子物体）
        {
            curDeletetrack = CurRgtClcAct.transform.parent.gameObject.transform.parent.gameObject;
            DZTagScript CurRgtClcDZTag = CurRgtClcAct.GetComponentInParent<DZTagScript>();
            CurDZTag = CurRgtClcDZTag.DZActTag;

            //Transform curtrack = CurRgtClcAct.transform.parent;
            string curstage = CurRgtClcDZTag.DZStageTag;//获取当前动作的阶段
            int num = 0;
            for (int i = 0; i < curDeletetrack.transform.childCount; i++)//DZTrack3
            {
                Transform TrackChildi = curDeletetrack.transform.GetChild(i);
                if (TrackChildi.name != "Image Empty(Clone)")
                {
                    DZTagScript StageTag = TrackChildi.GetComponent<DZTagScript>();
                    string stageTagi = StageTag.DZStageTag;
                    if (stageTagi == curstage)
                    {
                        num = num + 1;
                    }
                }
            }

            if (curDeletetrack.name == "DZTrack1")
            {
                GameObject curdelete = GameObject.Find(curstage).transform.GetChild(0).transform.GetChild(num - 1).gameObject;
                print(curdelete.name);
                Destroy(curdelete);
            }
            else if (curDeletetrack.name == "DZTrack2")
            {
                GameObject curdelete = GameObject.Find(curstage).transform.GetChild(1).transform.GetChild(num - 1).gameObject;
                print(curdelete.name);
                Destroy(curdelete);
            }
            else if (curDeletetrack.name == "DZTrack3")
            {
                GameObject curdelete = GameObject.Find(curstage).transform.GetChild(2).transform.GetChild(num - 1).gameObject;
                print(curdelete.name);
                Destroy(curdelete);
            }




            Destroy(CurRgtClcAct.transform.parent.gameObject);
            Destroy(CurRgtClcAct);
           
        }
        TimelinePanel.SetActive(true);
        if (curDeletetrack.name == "DZTrack1")
        {
            tracki = GameObject.Find("Track1");

        }
        else if (curDeletetrack.name == "DZTrack2")
        {
            tracki = GameObject.Find("Track2");
        }
        else if (curDeletetrack.name == "DZTrack3")
        {
            tracki = GameObject.Find("Track3");
        }
        for (int i = 0; i < tracki.transform.childCount; i++)
        {
            Transform TrackiChildi = tracki.transform.GetChild(i);
            ActTagScript ActTag = TrackiChildi.GetComponent<ActTagScript>();
            if (ActTag.ActTag == CurDZTag)
            {
               Destroy(tracki.transform.GetChild(i).gameObject);
            }
        }
        TimelinePanel.SetActive(false);
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
        if (HumanDropdown.value == 0)
        {
            track = GameObject.Find("Track1").transform;
        }
        else if (HumanDropdown.value == 1)
        {
            track = GameObject.Find("Track2").transform;
        }
        else if (HumanDropdown.value == 2)
        {
            track = GameObject.Find("Track3").transform;
        }
        Image Waitobj = Instantiate(WaitActImagePrefab, transform.position, Quaternion.identity) as Image;
        Waitobj.transform.SetParent(track);
        //按钮显示的位置                                                                                                                                                            
        RectTransform rtr = Waitobj.GetComponent<RectTransform>();
        //定义控件大小                                                                                                                                                           
        rtr.sizeDelta = new Vector2(WaitTimeWidth, 14);
        //设置右键菜单
        rightclick RgtClc = Waitobj.GetComponent<rightclick>();
        RgtClc.rightClick.AddListener(RightMenu);
    }

    public void GetAllActTag()//遍历拖拽的track中，对应的动素track中的所有动素并重排序
    {
        TimelinePanel.SetActive(true);
        if (TimelinePanel.activeInHierarchy == true)
        {
            if (DZImageDragDrop.DragTrack.name == "DZTrack1")
            {
                Dragtracki = GameObject.Find("Track1");

            }
            else if (DZImageDragDrop.DragTrack.name == "DZTrack2")
            {
                Dragtracki = GameObject.Find("Track2");
            }
            else if (DZImageDragDrop.DragTrack.name == "DZTrack3")
            {
                Dragtracki = GameObject.Find("Track3");
            }
            List<Transform> ChildActImageBuffer = new List<Transform>();//将改变顺序前的动素存入一个list
            ChildActImageBuffer.Clear();
            for (int i = 0; i < Dragtracki.transform.childCount; i++)
            {
                Transform ChildActImagei = Dragtracki.transform.GetChild(i);
                ChildActImageBuffer.Add(ChildActImagei);
            }

            for (int i = 0; i < DZImageDragDrop.DragDZTagList.Count; i++)//遍历动作层的Tag和改变顺序前的动素list，重排序
            {
                for (int j = 0; j < Dragtracki.transform.childCount; j++)
                {
                    Transform TrackiChildi = ChildActImageBuffer[j];
                    ActTagScript ActTag = TrackiChildi.GetComponent<ActTagScript>();
                    if (ActTag.ActTag == DZImageDragDrop.DragDZTagList[i])
                    {
                        TrackiChildi.transform.SetAsLastSibling();
                    }
                }
            }
            DZImageDragDrop.DragDZTagList.Clear();
            DZImageDragDrop.DragTrackFlag = false;
            TimelinePanel.SetActive(false);
        }
        
        
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
}



