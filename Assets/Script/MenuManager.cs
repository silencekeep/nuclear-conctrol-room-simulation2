using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject Posture,盘台拼接;
    public GameObject analyseMenu, keshiMenu, canshuhuaMenu, NewPeople, HumanDataPanel, RequireDataMenu, RequireDataPanel, NewHuman, 
                      JointsAdjust, ActDatabase, RelationPanel, CreatPeopleButton, NewPerson,WorkArea, CaiBiaoPanel;
    // Start is called before the first frame update
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Function1()
    {
        if (!Posture.activeInHierarchy)
        {
            Posture.SetActive(true);
        }
        else
        {
            Posture.SetActive(false);
        }
        if (!盘台拼接.activeInHierarchy)
        {
            盘台拼接.SetActive(true);
        }
        else
        {
            盘台拼接.SetActive(false);
        }
    }

    public void analyseMenu_onClick()
    {
        if (!analyseMenu.activeInHierarchy)
        {
            analyseMenu.SetActive(true);
        }
        else
        {
            analyseMenu.SetActive(false);
        }
    }

    public void HumanDataMenu_onClick()
    {
        if (!canshuhuaMenu.activeInHierarchy)
        {
            canshuhuaMenu.SetActive(true);            
        }
        else
        {
            canshuhuaMenu.SetActive(false);           
        }

        if (!NewHuman.activeInHierarchy)
        {            
            NewHuman.SetActive(true);
        }
        else
        {            
            NewHuman.SetActive(false);
        }

        if (!JointsAdjust.activeInHierarchy)
        {
            JointsAdjust.SetActive(true);
        }
        else
        {
            JointsAdjust.SetActive(false);
        }

        if (CreatPeopleButton.activeInHierarchy)
        {
            CreatPeopleButton.SetActive(true);
            //NewPerson.SetActive(true);
        }
        else
        {
            CreatPeopleButton.SetActive(false);
        }
    }
    public void RequireDataMenu_onClick()
    {
        if (!RequireDataMenu.activeInHierarchy)
        {
            RequireDataMenu.SetActive(true);
        }
        else
        {
            RequireDataMenu.SetActive(false);
        }
    }

    //public void CreatPeopleButton_onClick()
    //{
    //    if (!CreatPeopleButton.activeInHierarchy)
    //    {
    //        CreatPeopleButton.SetActive(true);
    //    }
    //    else
    //    {
    //        CreatPeopleButton.SetActive(false);
    //    }
    //}
    public void analyseMenuExit()
    {
        analyseMenu.SetActive(false);
    }

    public void keshiMenuShow()
    {
        keshiMenu.SetActive(true);
    }
    public void HumanDataPanelShow()
    {
        HumanDataPanel.SetActive(true);
    }
    //退出数字人参数化
    public void HumanDataPanelExit()
    {
        canshuhuaMenu.SetActive(false);
        NewHuman.SetActive(false);
        JointsAdjust.SetActive(false);
        //CreatPeopleButton.SetActive(false);
    }

    //需求数据新增界面
    public void RequireDataPanelShow()
    {
        RequireDataPanel.SetActive(true);

    }
    public void RequireDataPanelExit()
    {
        RequireDataMenu.SetActive(false);
    }

    //需求数据查询界面
    public void ActDatabaseShow()
    {
        ActDatabase.SetActive(true);

    }
    public void RelationPanelShow()
    {
        RelationPanel.SetActive(true);
    }
    public void CaiBiaoPanelShow()
    {
        CaiBiaoPanel.SetActive(true);

    }
}
