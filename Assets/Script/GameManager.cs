using CNCC.Models;
using CNCC.Panels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //List<GameObject> MoveObjs = new List<GameObject>(); //所有当前活动的，可移动的物体
    //Stack<List<GameObject>> stack = new Stack<List<GameObject>>(); //存储所有操作的上一个操作

    //Stack<List<Model>> modelStack = new Stack<List<Model>>();
    //Stack<List<Panel>> panelStack = new Stack<List<Panel>>();
    Stack<Dictionary<string[], Vector3[]>> modelStack = new Stack<Dictionary<string[], Vector3[]>>();
    Stack<Dictionary<string[], Vector3[]>> BUPStack = new Stack<Dictionary<string[], Vector3[]>>();
    Stack<Dictionary<string[], Vector3[]>> BUP90Stack = new Stack<Dictionary<string[], Vector3[]>>();
    Stack<Dictionary<string[], Vector3[]>> ECPNStack = new Stack<Dictionary<string[], Vector3[]>>();
    Stack<Dictionary<string[], Vector3[]>> ECPSStack = new Stack<Dictionary<string[], Vector3[]>>();
    Stack<Dictionary<string[], Vector3[]>> OWPtwoStack = new Stack<Dictionary<string[], Vector3[]>>();
    Stack<Dictionary<string[], Vector3[]>> OWPthreeStack = new Stack<Dictionary<string[], Vector3[]>>();
    Stack<Dictionary<string[], Vector3[]>> OWPconnerStack = new Stack<Dictionary<string[], Vector3[]>>();
    Stack<Dictionary<string[], Vector3[]>> SVDUStack = new Stack<Dictionary<string[], Vector3[]>>();
    Stack<Dictionary<string[], Vector3[]>> LargeScreenStack = new Stack<Dictionary<string[], Vector3[]>>();
    Stack<Dictionary<string[], Vector3[]>> LargeScreen45Stack = new Stack<Dictionary<string[], Vector3[]>>();
    Stack<Dictionary<string[], Vector3[]>> ChairStack = new Stack<Dictionary<string[], Vector3[]>>();

    [Header("按钮")]
    [SerializeField] Button 撤销button;
    [SerializeField] Button Closebutton;
    [Header("人物生成")]
    //人物预制体
    [SerializeField] GameObject manPrefab;
    [SerializeField] GameObject womanPrefab;
    //生成位置
    [SerializeField] Transform spawnHumanPosition;
    GameObject selectedManPrefab;
    GameObject selectedWomanPrefab;
    [Header("盘台")]
    [SerializeField] GameObject BUPprefab;
    [SerializeField] GameObject BUP90Assemblyprefab;
    [SerializeField] GameObject ECP_Nprefab;
    [SerializeField] GameObject ECP_Sprefab;
    [SerializeField] GameObject OWP_threeprefab;
    [SerializeField] GameObject OWP_twoprefab;
    [SerializeField] GameObject OWP_cornerprefab;
    [SerializeField] GameObject SVDUprefab;
    [SerializeField] GameObject LargeScreenprefab;
    [SerializeField] GameObject LargeScreen45Assemblyprefab;
    [SerializeField] GameObject Chairprefab;

    [Header("盘台位置")]
    [SerializeField] Transform originTansform;
    [SerializeField] Transform spawnTansform;
    //场景
    GameObject BUPpanel;
    GameObject BUP90Assemblypanel;
    GameObject ECP_Npanel;
    GameObject ECP_Spanel;
    GameObject OWP_threepanel;
    GameObject OWP_twopanel;
    GameObject OWP_cornerpanel;
    GameObject SVDUpanel;
    GameObject LargeScreenpanel;
    GameObject LargeScreen45Assemblypanel;
    GameObject Chairpanel;
    [Header("撤回事件发生")]
    public UnityEvent OnUndo;
    public UnityEvent OnExit;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        撤销button.onClick.AddListener(UnDo);
        Closebutton.onClick.AddListener(ExitGame);
        selectedManPrefab = manPrefab;
        selectedWomanPrefab = womanPrefab;
        //
        BUPpanel = BUPprefab;
        BUP90Assemblypanel = BUP90Assemblyprefab;
        ECP_Npanel = ECP_Nprefab;
        ECP_Spanel = ECP_Sprefab;
        OWP_threepanel = OWP_threeprefab;
        OWP_twopanel = OWP_twoprefab;
        OWP_cornerpanel = OWP_cornerprefab;
        SVDUpanel = SVDUprefab;
        LargeScreenpanel = LargeScreenprefab;
        LargeScreen45Assemblypanel = LargeScreen45Assemblyprefab;
        Chairpanel = Chairprefab;
    }

    private void Update()
    {
        //ErrorShow();
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnDo();
        }
        if (modelStack.Count > 0)
        {
            撤销button.gameObject.SetActive(true);
        }
        else
        {
            撤销button.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }
    #region UnDo
    public void Rigist(GameObject obj)  //将物体注册
    {
        // MoveObjs.Add(obj);
    }

    public void Remove(GameObject obj) //将已经删除掉的物品移除链表
    {
        // MoveObjs.Remove(obj);
    }

    public void ClearAllStack()
    {
        modelStack.Clear();
        BUPStack.Clear();
        BUP90Stack.Clear();
        ECPNStack.Clear();
        ECPSStack.Clear();
        OWPtwoStack.Clear();
        OWPthreeStack.Clear();
        OWPconnerStack.Clear();
        SVDUStack.Clear();
        LargeScreenStack.Clear();
        LargeScreenStack.Clear();
        ChairStack.Clear();
    }

    public void Save()  //每次移动前执行一次调用保存
    {
        //当前人物入栈
        //List<Model> modelTemp = new List<Model>();
        Dictionary<string[], Vector3[]> modelTemp = new Dictionary<string[], Vector3[]>();
        foreach (var item in Model.createdModel)    //遍历当前所有可活动的物体并创建副本
        {
            //GameObject tp = Instantiate(item.gameObject, item.transform.position, Quaternion.identity);
            //tp.SetActive(false); //将所有副本设置为不活动，并将其添加到临时链表里面
            string[] itemstring = new string[2];
            itemstring[0] = item.Name;
            itemstring[1] = item.gender;
            Vector3[] itemVector2 = new Vector3[3];
            itemVector2[0] = item.gameObject.transform.position;
            itemVector2[1] = item.gameObject.transform.eulerAngles;
            itemVector2[2] = item.gameObject.transform.localScale;
            modelTemp.Add(itemstring, itemVector2);
        }
        modelStack.Push(modelTemp);   //入栈

        //BUP入栈
        Dictionary<string[], Vector3[]> BUPTemp = new Dictionary<string[], Vector3[]>();
        foreach (var item in BUP.AllBUPs)    //遍历当前所有可活动的物体并创建副本
        {
            string[] itemstring = new string[2];
            itemstring[0] = item.ID;
            itemstring[1] = "BUP";
            Vector3[] itemVector2 = new Vector3[3];
            itemVector2[0] = item.gameObject.transform.position;
            itemVector2[1] = item.gameObject.transform.eulerAngles;
            itemVector2[2] = item.gameObject.transform.localScale;
            BUPTemp.Add(itemstring, itemVector2);
        }
        BUPStack.Push(BUPTemp);   //入栈

        //BUP90入栈
        Dictionary<string[], Vector3[]> BUP45Temp = new Dictionary<string[], Vector3[]>();
        foreach (var item in BUP90Assembly.AllBUP90s)    //遍历当前所有可活动的物体并创建副本
        {
            string[] itemstring = new string[2];
            itemstring[0] = item.ID;
            itemstring[1] = "BUP90Assembly";
            Vector3[] itemVector2 = new Vector3[3];
            itemVector2[0] = item.gameObject.transform.position;
            itemVector2[1] = item.gameObject.transform.eulerAngles;
            itemVector2[2] = item.gameObject.transform.localScale;
            BUP45Temp.Add(itemstring, itemVector2);
        }
        BUP90Stack.Push(BUP45Temp);   //入栈

        //ECP_N入栈
        Dictionary<string[], Vector3[]> ECPN = new Dictionary<string[], Vector3[]>();
        foreach (var item in ECP_N.ALLECP_Ns)    //遍历当前所有可活动的物体并创建副本
        {
            string[] itemstring = new string[2];
            itemstring[0] = item.ID;
            itemstring[1] = "ECP_N";
            Vector3[] itemVector2 = new Vector3[3];
            itemVector2[0] = item.gameObject.transform.position;
            itemVector2[1] = item.gameObject.transform.eulerAngles;
            itemVector2[2] = item.gameObject.transform.localScale;
            ECPN.Add(itemstring, itemVector2);
        }
        ECPNStack.Push(ECPN);   //入栈

        //ECPS入栈
        Dictionary<string[], Vector3[]> ECPS = new Dictionary<string[], Vector3[]>();
        foreach (var item in ECP_S.ALLECP_Ss)    //遍历当前所有可活动的物体并创建副本
        {
            string[] itemstring = new string[2];
            itemstring[0] = item.ID;
            itemstring[1] = "ECP_S";
            Vector3[] itemVector2 = new Vector3[3];
            itemVector2[0] = item.gameObject.transform.position;
            itemVector2[1] = item.gameObject.transform.eulerAngles;
            itemVector2[2] = item.gameObject.transform.localScale;
            ECPS.Add(itemstring, itemVector2);
        }
        ECPSStack.Push(ECPS);   //入栈

        //OWPtwo入栈
        Dictionary<string[], Vector3[]> OWPtwo = new Dictionary<string[], Vector3[]>();
        foreach (var item in OWP_two.AllOWP_two)    //遍历当前所有可活动的物体并创建副本
        {
            string[] itemstring = new string[2];
            itemstring[0] = item.ID;
            itemstring[1] = "OWP_two";
            Vector3[] itemVector2 = new Vector3[3];
            itemVector2[0] = item.gameObject.transform.position;
            itemVector2[1] = item.gameObject.transform.eulerAngles;
            itemVector2[2] = item.gameObject.transform.localScale;
            OWPtwo.Add(itemstring, itemVector2);
        }
        OWPtwoStack.Push(OWPtwo);   //入栈

        //OWPthree入栈
        Dictionary<string[], Vector3[]> OWPthree = new Dictionary<string[], Vector3[]>();
        foreach (var item in OWP_three.AllOWP_three)    //遍历当前所有可活动的物体并创建副本
        {
            string[] itemstring = new string[2];
            itemstring[0] = item.ID;
            itemstring[1] = "OWP_three";
            Vector3[] itemVector2 = new Vector3[3];
            itemVector2[0] = item.gameObject.transform.position;
            itemVector2[1] = item.gameObject.transform.eulerAngles;
            itemVector2[2] = item.gameObject.transform.localScale;
            OWPthree.Add(itemstring, itemVector2);
        }
        OWPthreeStack.Push(OWPthree);   //入栈

        //OWPconner入栈
        Dictionary<string[], Vector3[]> OWPconner = new Dictionary<string[], Vector3[]>();
        foreach (var item in OWP_corner.AllOWP_corner)    //遍历当前所有可活动的物体并创建副本
        {
            string[] itemstring = new string[2];
            itemstring[0] = item.ID;
            itemstring[1] = "OWP_corner";
            Vector3[] itemVector2 = new Vector3[3];
            itemVector2[0] = item.gameObject.transform.position;
            itemVector2[1] = item.gameObject.transform.eulerAngles;
            itemVector2[2] = item.gameObject.transform.localScale;
            OWPconner.Add(itemstring, itemVector2);
        }
        OWPconnerStack.Push(OWPconner);   //入栈

        //SVDU
        Dictionary<string[], Vector3[]> SVDUs = new Dictionary<string[], Vector3[]>();
        foreach (var item in SVDU.AllSVDU)    //遍历当前所有可活动的物体并创建副本
        {
            string[] itemstring = new string[2];
            itemstring[0] = item.ID;
            itemstring[1] = "SVDU";
            Vector3[] itemVector2 = new Vector3[3];
            itemVector2[0] = item.gameObject.transform.position;
            itemVector2[1] = item.gameObject.transform.eulerAngles;
            itemVector2[2] = item.gameObject.transform.localScale;
            SVDUs.Add(itemstring, itemVector2);
        }
        SVDUStack.Push(SVDUs);   //入栈

        //largeScreen
        Dictionary<string[], Vector3[]> largeScreenS = new Dictionary<string[], Vector3[]>();
        foreach (var item in LargeScreen.AllLargeScreen)    //遍历当前所有可活动的物体并创建副本
        {
            string[] itemstring = new string[2];
            itemstring[0] = item.ID;
            itemstring[1] = "LargeScreen";
            Vector3[] itemVector2 = new Vector3[3];
            itemVector2[0] = item.gameObject.transform.position;
            itemVector2[1] = item.gameObject.transform.eulerAngles;
            itemVector2[2] = item.gameObject.transform.localScale;
            largeScreenS.Add(itemstring, itemVector2);
        }
        LargeScreenStack.Push(largeScreenS);   //入栈

        //LargeScreen45Stack 入栈
        Dictionary<string[], Vector3[]> LargeScreen45S = new Dictionary<string[], Vector3[]>();
        foreach (var item in LargeScreen45Assembly.AllLargeScreen45Assembly)    //遍历当前所有可活动的物体并创建副本
        {
            string[] itemstring = new string[2];
            itemstring[0] = item.ID;
            itemstring[1] = "LargeScreen45Assembly";
            Vector3[] itemVector2 = new Vector3[3];
            itemVector2[0] = item.gameObject.transform.position;
            itemVector2[1] = item.gameObject.transform.eulerAngles;
            itemVector2[2] = item.gameObject.transform.localScale;
            LargeScreen45S.Add(itemstring, itemVector2);
        }
        LargeScreen45Stack.Push(LargeScreen45S);   //入栈

        //ChairStack 
        Dictionary<string[], Vector3[]> ChairS = new Dictionary<string[], Vector3[]>();
        foreach (var item in Chair.ALLChairs)    //遍历当前所有可活动的物体并创建副本
        {
            string[] itemstring = new string[2];
            itemstring[0] = item.ID;
            itemstring[1] = "Chair";
            Vector3[] itemVector2 = new Vector3[3];
            itemVector2[0] = item.gameObject.transform.position;
            itemVector2[1] = item.gameObject.transform.eulerAngles;
            itemVector2[2] = item.gameObject.transform.localScale;
            ChairS.Add(itemstring, itemVector2);
        }
        ChairStack.Push(ChairS);   //入栈
    }

    public void UnDo()
    {
        if (modelStack.Count <= 0)
        {
            print("当前栈数：" + (modelStack.Count));
            return;
        }
        if (modelStack.Count != 0)
        {
            Dictionary<string[], Vector3[]> modelTemp = modelStack.Pop();
            Dictionary<string[], Vector3[]> BUPTemp = BUPStack.Pop();
            Dictionary<string[], Vector3[]> BUP90Temp = BUP90Stack.Pop();
            Dictionary<string[], Vector3[]> ECPNSTemp = ECPNStack.Pop();
            Dictionary<string[], Vector3[]> ECPSSTemp = ECPSStack.Pop();
            Dictionary<string[], Vector3[]> OWPtwoTemp = OWPtwoStack.Pop();
            Dictionary<string[], Vector3[]> OWPthreeTemp = OWPthreeStack.Pop();
            Dictionary<string[], Vector3[]> OWPconnerTemp = OWPconnerStack.Pop();
            Dictionary<string[], Vector3[]> SVDUTemp = SVDUStack.Pop();
            Dictionary<string[], Vector3[]> LargeScrenceTemp = LargeScreenStack.Pop();
            Dictionary<string[], Vector3[]> LargeScrence45Temp = LargeScreen45Stack.Pop();
            Dictionary<string[], Vector3[]> ChairTemp = ChairStack.Pop();

            print("当前栈数：" + (modelStack.Count));
            //人物数量没有改变
            if (modelTemp.Count == Model.createdModel.Count)
            {
                int i = 0;
                foreach (var item in modelTemp.Keys)
                {
                    if (ModelSameJudge(Model.createdModel[i], item, modelTemp[item])) { i++; }

                    else
                    { ModelRollBACK(Model.createdModel[i], item, modelTemp[item]); i++; }
                }
                //return;
            }
            //EqualUndo(Model.createdModel,modelTemp)
            EqualUndo(BUP.AllBUPs, BUPTemp);
            EqualUndo(BUP90Assembly.AllBUP90s, BUP90Temp);
            EqualUndo(ECP_N.ALLECP_Ns, ECPNSTemp);
            EqualUndo(ECP_S.ALLECP_Ss, ECPSSTemp);
            EqualUndo(OWP_two.AllOWP_two, OWPtwoTemp);
            EqualUndo(OWP_three.AllOWP_three, OWPthreeTemp);
            EqualUndo(OWP_corner.AllOWP_corner, OWPconnerTemp);
            EqualUndo(SVDU.AllSVDU, SVDUTemp);
            EqualUndo(LargeScreen.AllLargeScreen, LargeScrenceTemp);
            EqualUndo(LargeScreen45Assembly.AllLargeScreen45Assembly, LargeScrence45Temp);
            EqualUndo(Chair.ALLChairs, ChairTemp);


            //栈的数量多余人物列表数量
            if (modelTemp.Count > Model.createdModel.Count)  //执行了删除人物的操作，需要将被删除的人物还原
            {
                //将被删除的人物重新添加回链表相应的位置
                int i = 0;
                foreach (var item in modelTemp.Keys)
                {
                    if (Model.createdModel.Count == 0)  //人物列表数量为空，即场景中无人
                    {
                        ModelDeleteRollBACK(item, modelTemp[item]);
                    }
                    else//人物列表数量不为空，即场景中有人
                    {
                        if (ModelSameJudge(Model.createdModel[i], item, modelTemp[item]))
                        {
                            if (i == Model.createdModel.Count - 1)
                            {
                                int iIndex = 0; //标识第i个元素，i为常量
                                foreach (var itemLast in modelTemp.Keys)
                                {
                                    iIndex++;

                                    if (iIndex == modelTemp.Keys.Count)
                                    {
                                        ModelDeleteRollBACK(itemLast, modelTemp[itemLast]);
                                    }
                                }
                                break;
                            }
                            else
                            {
                                i++;
                            }
                        }
                        else
                        {
                            ModelDeleteRollBACK(item, modelTemp[item], i);
                            //print("被删除的重新恢复");
                            i++;
                            break;
                        }
                    }
                }
            }
            PanelDeleteUndo(BUP.AllBUPs, BUPTemp);
            PanelDeleteUndo(BUP90Assembly.AllBUP90s, BUP90Temp);
            PanelDeleteUndo(ECP_N.ALLECP_Ns, ECPNSTemp);
            PanelDeleteUndo(ECP_S.ALLECP_Ss, ECPSSTemp);
            PanelDeleteUndo(OWP_two.AllOWP_two, OWPtwoTemp);
            PanelDeleteUndo(OWP_three.AllOWP_three, OWPthreeTemp);
            PanelDeleteUndo(OWP_corner.AllOWP_corner, OWPconnerTemp);
            PanelDeleteUndo(SVDU.AllSVDU, SVDUTemp);
            PanelDeleteUndo(LargeScreen.AllLargeScreen, LargeScrenceTemp);
            PanelDeleteUndo(LargeScreen45Assembly.AllLargeScreen45Assembly, LargeScrence45Temp);
            PanelDeleteUndo(Chair.ALLChairs, ChairTemp);

            //栈的数量少于人物列表的数量
            if (modelTemp.Count < Model.createdModel.Count) //执行了新建人物的操作，需要将新建的人物删除
            {
                //删除当前modelList末尾的人物
                int i = Model.createdModel.Count - 1;
                Destroy(Model.createdModel[i].gameObject);
                Model.createdModel.RemoveAt(i);
                print("删除后人物数" + Model.createdModel.Count);
                //ModelNewRollBACK(Model.createdModel[i])   //为什么把model当作参数传入就会报错？
            }
            PanelNewUndo(BUP.AllBUPs, BUPTemp);
            PanelNewUndo(BUP90Assembly.AllBUP90s, BUP90Temp);
            PanelNewUndo(ECP_N.ALLECP_Ns, ECPNSTemp);
            PanelNewUndo(ECP_S.ALLECP_Ss, ECPSSTemp);
            PanelNewUndo(OWP_two.AllOWP_two, OWPtwoTemp);
            PanelNewUndo(OWP_three.AllOWP_three, OWPthreeTemp);
            PanelNewUndo(OWP_corner.AllOWP_corner, OWPconnerTemp);
            PanelNewUndo(SVDU.AllSVDU, SVDUTemp);
            PanelNewUndo(LargeScreen.AllLargeScreen, LargeScrenceTemp);
            PanelNewUndo(LargeScreen45Assembly.AllLargeScreen45Assembly, LargeScrence45Temp);
            PanelNewUndo(Chair.ALLChairs, ChairTemp);
        }
        OnUndo.Invoke();
    }

    #region 人
    //人物列表当中没有人时
    private void ModelDeleteRollBACK(string[] item, Vector3[] transfrom)
    {
        if (item[1] == "男")
        {
            Model newModel1 = new Model(selectedManPrefab, spawnHumanPosition, item[0], "男");
            foreach (var newmdel in Model.createdModel)
            {
                if (newmdel.Name == item[0] && newmdel.gender == "男")
                {
                    newmdel.gameObject.transform.position = transfrom[0];
                    newmdel.gameObject.transform.eulerAngles = transfrom[1];
                    newmdel.gameObject.transform.localScale = transfrom[2];
                }
            }
            //swap(Model.createdModel, i, Model.createdModel.Count - 1);
        }
        else
        {
            Model newModel1 = new Model(selectedWomanPrefab, spawnHumanPosition, item[0], "女");
            foreach (var newmdel in Model.createdModel)
            {
                if (newmdel.Name == item[0])
                {
                    newmdel.gameObject.transform.position = transfrom[0];
                    newmdel.gameObject.transform.eulerAngles = transfrom[1];
                    newmdel.gameObject.transform.localScale = transfrom[2];
                }
            }
            //swap(Model.createdModel, i, Model.createdModel.Count - 1);
        }
    }

    //人物列表有人
    private void ModelDeleteRollBACK(string[] item, Vector3[] transfrom, int i)
    {
        if (item[1] == "男")
        {
            new Model(selectedManPrefab, spawnHumanPosition, item[0], "男");
            foreach (var newmdel in Model.createdModel)
            {
                if (newmdel.Name == item[0])
                {
                    newmdel.gameObject.transform.position = transfrom[0];
                    newmdel.gameObject.transform.eulerAngles = transfrom[1];
                    newmdel.gameObject.transform.localScale = transfrom[2];
                }
            }
            swap(Model.createdModel, i, Model.createdModel.Count - 1);
        }
        else//女
        {
            new Model(selectedWomanPrefab, spawnHumanPosition, item[0], "女");
            foreach (var newmdel in Model.createdModel)
            {
                if (newmdel.Name == item[0])
                {
                    newmdel.gameObject.transform.position = transfrom[0];
                    newmdel.gameObject.transform.eulerAngles = transfrom[1];
                    newmdel.gameObject.transform.localScale = transfrom[2];
                }
            }
            swap(Model.createdModel, i, Model.createdModel.Count - 1);
        }
    }

    private void ModelRollBACK(Model modelnow, string[] strandgender, Vector3[] transfrom)
    {
        modelnow.Name = strandgender[0];
        modelnow.gender = strandgender[1];
        modelnow.gameObject.transform.position = transfrom[0];
        modelnow.gameObject.transform.eulerAngles = transfrom[1];
        modelnow.gameObject.transform.localScale = transfrom[2];
    }

    private bool ModelSameJudge(Model modelnow, string[] strandgender, Vector3[] transfrom)   //判断当前场景人物的信息和字典当中的信息是否相同
    {
        if (modelnow == null)
        {
            return false;
        }
        if (modelnow.Name == strandgender[0] && modelnow.gender == strandgender[1] && IsSameDistance(modelnow.gameObject.transform.position, transfrom[0]) && IsSameDistance(modelnow.gameObject.transform.eulerAngles, transfrom[1]) && IsSameDistance(modelnow.gameObject.transform.localScale, transfrom[2]))
        {
            return true;
        }
        return false;
    }
    #endregion

    #region 盘台
    private void PanelRollBACK(Panel panelnow, string[] strandgender, Vector3[] transfrom)
    {
        panelnow.ID = strandgender[0];
        panelnow.gameObject.transform.position = transfrom[0];
        panelnow.gameObject.transform.eulerAngles = transfrom[1];
        panelnow.gameObject.transform.localScale = transfrom[2];
    }

    private void PanelDeleteRollBACK(string[] item, Vector3[] transfrom)
    {
        switch (item[1])
        {
            case "BUP":
                CreatBUP(item, transfrom, originTansform, spawnTansform);
                break;

            case "BUP90Assembly":
                CreatBUP45Assembly(item, transfrom, originTansform, spawnTansform);
                break;

            case "ECP_N":
                CreatECP_N(item, transfrom, originTansform, spawnTansform);
                break;

            case "ECP_S":
                CreatECP_S(item, transfrom, originTansform, spawnTansform);
                break;

            case "LargeScreen":
                CreatLargeScreen(item, transfrom, originTansform, spawnTansform);
                break;

            case "LargeScreen45Assembly":
                CreatLargeScreen45Assembly(item, transfrom, originTansform, spawnTansform);
                break;

            case "OWP_corner":
                CreatOWP_cornerButton(item, transfrom, originTansform, spawnTansform);
                break;

            case "OWP_three":
                CreatOWP_threeButton(item, transfrom, originTansform, spawnTansform);
                break;

            case "OWP_two":
                CreatOWP_twoButton(item, transfrom, originTansform, spawnTansform);
                break;

            case "SVDU":
                CreatSVDU(item, transfrom, originTansform, spawnTansform);
                break;

            case "Chair":
                CreatChair(item, transfrom, originTansform, spawnTansform);
                break;
        }
    }

    private bool PanelSameJudge(Panel panelnow, string[] strandgender, Vector3[] transfrom)   //判断当前场景人物的信息和字典当中的信息是否相同
    {
        if (panelnow == null)
        {
            return false;
        }
        if (panelnow.ID == strandgender[0] && IsSameDistance(panelnow.gameObject.transform.position, transfrom[0]) && IsSameDistance(panelnow.gameObject.transform.eulerAngles, transfrom[1]) && IsSameDistance(panelnow.gameObject.transform.localScale, transfrom[2]))
        {
            return true;
        }
        return false;
    }

    private void PanelPush(List<Panel> panels, Stack<Dictionary<string[], Vector3[]>> stack)
    {
        Dictionary<string[], Vector3[]> modelTemp = new Dictionary<string[], Vector3[]>();
        foreach (var item in panels)    //遍历当前所有可活动的物体并创建副本
        {
            string[] itemstring = new string[1];
            itemstring[0] = item.ID;
            Vector3[] itemVector2 = new Vector3[3];
            itemVector2[0] = item.gameObject.transform.position;
            itemVector2[1] = item.gameObject.transform.eulerAngles;
            itemVector2[2] = item.gameObject.transform.localScale;
            modelTemp.Add(itemstring, itemVector2);
        }
        stack.Push(modelTemp);   //入栈
    }

    void CreatBUP(string[] item, Vector3[] transfrom, Transform originTansform, Transform spawnTansform)
    {
        new BUP(BUPpanel, originTansform, spawnTansform, item[0]);
        foreach (var newBUP in BUP.AllBUPs)
        {
            if (newBUP.ID == item[0])
            {
                newBUP.gameObject.transform.position = transfrom[0];
                newBUP.gameObject.transform.eulerAngles = transfrom[1];
                newBUP.gameObject.transform.localScale = transfrom[2];
            }
        }
    }

    void CreatBUP45Assembly(string[] item, Vector3[] transfrom, Transform originTansform, Transform spawnTansform)
    {
        new BUP90Assembly(BUP90Assemblypanel, originTansform, spawnTansform, item[0]);
        foreach (var renewpanel in BUP90Assembly.AllBUP90s)
        {
            if (renewpanel.ID == item[0])
            {
                renewpanel.gameObject.transform.position = transfrom[0];
                renewpanel.gameObject.transform.eulerAngles = transfrom[1];
                renewpanel.gameObject.transform.localScale = transfrom[2];
            }
        }
    }

    void CreatECP_N(string[] item, Vector3[] transfrom, Transform originTansform, Transform spawnTansform)
    {
        ECP_N ecp_N = new ECP_N(ECP_Npanel, originTansform, spawnTansform, item[0]);
        foreach (var renewpanel in ECP_N.ALLECP_Ns)
        {
            if (renewpanel.ID == item[0])
            {
                renewpanel.gameObject.transform.position = transfrom[0];
                renewpanel.gameObject.transform.eulerAngles = transfrom[1];
                renewpanel.gameObject.transform.localScale = transfrom[2];
            }
        }
    }

    void CreatECP_S(string[] item, Vector3[] transfrom, Transform originTansform, Transform spawnTansform)
    {
        ECP_S ecp_S = new ECP_S(ECP_Spanel, originTansform, spawnTansform, item[0]);
        foreach (var renewpanel in ECP_S.ALLECP_Ss)
        {
            if (renewpanel.ID == item[0])
            {
                renewpanel.gameObject.transform.position = transfrom[0];
                renewpanel.gameObject.transform.eulerAngles = transfrom[1];
                renewpanel.gameObject.transform.localScale = transfrom[2];
            }
        }
    }

    void CreatOWP_threeButton(string[] item, Vector3[] transfrom, Transform originTansform, Transform spawnTansform)
    {
        OWP_three owp_three = new OWP_three(OWP_threepanel, originTansform, spawnTansform, item[0]);
        foreach (var renewpanel in OWP_three.AllOWP_three)
        {
            if (renewpanel.ID == item[0])
            {
                renewpanel.gameObject.transform.position = transfrom[0];
                renewpanel.gameObject.transform.eulerAngles = transfrom[1];
                renewpanel.gameObject.transform.localScale = transfrom[2];
            }
        }
    }

    void CreatOWP_twoButton(string[] item, Vector3[] transfrom, Transform originTansform, Transform spawnTansform)
    {
        OWP_two owp_two = new OWP_two(OWP_twopanel, originTansform, spawnTansform, item[0]);
        foreach (var renewpanel in OWP_two.AllOWP_two)
        {
            if (renewpanel.ID == item[0])
            {
                renewpanel.gameObject.transform.position = transfrom[0];
                renewpanel.gameObject.transform.eulerAngles = transfrom[1];
                renewpanel.gameObject.transform.localScale = transfrom[2];
            }
        }
    }

    void CreatOWP_cornerButton(string[] item, Vector3[] transfrom, Transform originTansform, Transform spawnTansform)
    {
        OWP_corner owp_corner = new OWP_corner(OWP_cornerpanel, originTansform, spawnTansform, item[0]);
        foreach (var renewpanel in OWP_corner.AllOWP_corner)
        {
            if (renewpanel.ID == item[0])
            {
                renewpanel.gameObject.transform.position = transfrom[0];
                renewpanel.gameObject.transform.eulerAngles = transfrom[1];
                renewpanel.gameObject.transform.localScale = transfrom[2];
            }
        }
    }

    void CreatSVDU(string[] item, Vector3[] transfrom, Transform originTansform, Transform spawnTansform)
    {
        SVDU svdu = new SVDU(SVDUpanel, originTansform, spawnTansform, item[0]);
        foreach (var renewpanel in SVDU.AllSVDU)
        {
            if (renewpanel.ID == item[0])
            {
                renewpanel.gameObject.transform.position = transfrom[0];
                renewpanel.gameObject.transform.eulerAngles = transfrom[1];
                renewpanel.gameObject.transform.localScale = transfrom[2];
            }
        }
    }

    void CreatLargeScreen(string[] item, Vector3[] transfrom, Transform originTansform, Transform spawnTansform)
    {
        LargeScreen laegerscreen = new LargeScreen(LargeScreenpanel, originTansform, spawnTansform, item[0]);
        foreach (var renewpanel in LargeScreen.AllLargeScreen)
        {
            if (renewpanel.ID == item[0])
            {
                renewpanel.gameObject.transform.position = transfrom[0];
                renewpanel.gameObject.transform.eulerAngles = transfrom[1];
                renewpanel.gameObject.transform.localScale = transfrom[2];
            }
        }
    }

    void CreatLargeScreen45Assembly(string[] item, Vector3[] transfrom, Transform originTansform, Transform spawnTansform)
    {
        LargeScreen45Assembly assembly = new LargeScreen45Assembly(LargeScreen45Assemblypanel, originTansform, spawnTansform, item[0]);
        foreach (var renewpanel in LargeScreen45Assembly.AllLargeScreen45Assembly)
        {
            if (renewpanel.ID == item[0])
            {
                renewpanel.gameObject.transform.position = transfrom[0];
                renewpanel.gameObject.transform.eulerAngles = transfrom[1];
                renewpanel.gameObject.transform.localScale = transfrom[2];
            }
        }
    }

    void CreatChair(string[] item, Vector3[] transfrom, Transform originTansform, Transform spawnTansform)
    {
        Chair chair = new Chair(Chairpanel, originTansform, spawnTansform, item[0]);
        foreach (var renewpanel in Chair.ALLChairs)
        {
            if (renewpanel.ID == item[0])
            {
                renewpanel.gameObject.transform.position = transfrom[0];
                renewpanel.gameObject.transform.eulerAngles = transfrom[1];
                renewpanel.gameObject.transform.localScale = transfrom[2];
            }
        }
    }
    #endregion
    #region 通用项
    //移动后撤回
    private void EqualUndo(IList list, Dictionary<string[], Vector3[]> temp)
    {
        if (list.Count == temp.Count)
        {
            int i = 0;
            foreach (var item in temp.Keys)
            {
                if (PanelSameJudge((Panel)list[i], item, temp[item])) { i++; }

                else
                { PanelRollBACK((Panel)list[i], item, temp[item]); i++; }
            }
            return;
        }
    }

    //删除后撤回
    private void PanelDeleteUndo(IList list, Dictionary<string[], Vector3[]> temp)
    {
        if (list.Count < temp.Count)
        {
            int i = 0;
            foreach (var item in temp.Keys)
            {
                if (list.Count == 0)  //盘台列表数量为空，即场景中无该类盘台
                {
                    //ModelDeleteRollBACK(item, temp[item]);
                    PanelDeleteRollBACK(item, temp[item]);
                }
                else//人物列表数量不为空，即场景中有人
                {
                    if (PanelSameJudge((Panel)list[i], item, temp[item]))
                    {
                        if (i == list.Count - 1)
                        {
                            int iIndex = 0; //标识第i个元素，i为常量
                            foreach (var itemLast in temp.Keys)
                            {
                                iIndex++;

                                if (iIndex == temp.Keys.Count)
                                {
                                    // ModelDeleteRollBACK(itemLast, temp[itemLast]);
                                    PanelDeleteRollBACK(itemLast, temp[itemLast]);
                                }
                            }
                            break;
                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {
                        // ModelDeleteRollBACK(item, temp[item], i);
                        PanelDeleteRollBACK(item, temp[item]);
                        swap(list, i, list.Count - 1);
                        swapInPanelList(item[0]);
                        i++;
                        break;
                    }
                }
            }
        }
    }

    //新建后撤回
    private void PanelNewUndo(IList list, Dictionary<string[], Vector3[]> temp)
    {
        if (temp.Count < list.Count)
        {
            int i = list.Count - 1;
            Destroy(((Panel)list[i]).gameObject);
            Panel.AllPanels.Remove((Panel)list[i]);
            list.RemoveAt(i);
        }
    }
    #endregion

    #endregion
    //判断两个向量是否相同
    private bool IsSameDistance(Vector3 vector1, Vector3 vector2)
    {
        if (Math.Abs(vector1.x - vector2.x) < 0.00001 && Math.Abs(vector1.y - vector2.y) < 0.00001 && Math.Abs(vector1.z - vector2.z) < 0.00001)
        {
            return true;
        }
        return false;
    }
    private static void swap(List<Model> list, int index1, int index2)
    {
        Model temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;

    }
    private static void swap(IList list, int index1, int index2)
    {
        var temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;

    }
    private void swapInPanelList(string ID)
    {
        int i;
        for (i = 0; i < Panel.AllPanels.Count; i++)
        {
            if (Panel.AllPanels[i].ID == ID)
            {
                swap(Panel.AllPanels, i, Panel.AllPanels.Count - 1);
            }
        }
    }

    public void ExitGame()
    {
        OnExit.Invoke();
        //预处理
#if UNITY_EDITOR    //在编辑器模式下
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void StackPop()
    {
        if (modelStack.Count <= 0) return;
        modelStack.Pop();
        BUPStack.Pop();
        BUP90Stack.Pop();
        ECPNStack.Pop();
        ECPSStack.Pop();
        OWPtwoStack.Pop();
        OWPthreeStack.Pop();
        OWPconnerStack.Pop();
        SVDUStack.Pop();
        LargeScreenStack.Pop();
        LargeScreen45Stack.Pop();
        ChairStack.Pop();
    }

    //void ErrorShow()
    //{
    //    if (BUP90Assembly.AllBUP90s.Count == 0)
    //    {
    //        print("正常");
    //    }
    //    else if(BUP90Assembly.AllBUP90s.Count == 1)
    //    {
    //        print("变成1");
    //    }
    //    else
    //    {
    //        print(BUP90Assembly.AllBUP90s.Count);
    //    }
    //}
}
