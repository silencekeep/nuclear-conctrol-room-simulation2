using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CNCC.Panels;
using UnityEngine.SceneManagement;
using CNCC.Saving;
using CNCC.WorkArea;
using UnityEngine.Events;

namespace CNCC.UI
{
    public class PanelIntanstiationUIPresenter : MonoBehaviour
    {
        [Header("命名UI")]
        [SerializeField] GameObject NameUI;
        [SerializeField] Text State;
        [SerializeField] InputField newNameText;
        [SerializeField] Button Yes;
        [SerializeField] Button Cannel;
        [Header("提示UI")]
        [SerializeField] GameObject TipUI;
        [Header("预制体")]
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
        [Header("下拉列表")]
        [SerializeField] Dropdown CreatedPanelDropDown;
        [Header("按钮")]
        [SerializeField] Button BUPButton;
        [SerializeField] Button BUP45AssemblyButton;
        [SerializeField] Button ECP_NButton;
        [SerializeField] Button ECP_SButton;
        [SerializeField] Button OWP_threeButton;
        [SerializeField] Button OWP_twoButton;
        [SerializeField] Button OWP_cornerButton;
        [SerializeField] Button SVDUButton;
        [SerializeField] Button LargeScreenButton;
        [SerializeField] Button LargeScreen45AssemblyButton;
        [SerializeField] Button ChairButton;
        [SerializeField] Button DeleteButton;

        [Header("生成位置")]
        [SerializeField] Transform originTansform; //(0,0,0) 
        [SerializeField] Transform spawnTansform;  //厂房中点

        [Header("盘台删除事件")]
        public UnityEvent OnPanelDelete;
        Panel currentPanel;

        GameObject BUPpanel;
        GameObject BUP45Assemblypanel;
        GameObject ECP_Npanel;
        GameObject ECP_Spanel;
        GameObject OWP_threepanel;
        GameObject OWP_twopanel;
        GameObject OWP_cornerpanel;
        GameObject SVDUpanel;
        GameObject LargeScreenpanel;
        GameObject LargeScreen45Assemblypanel;
        GameObject Chairpanel;

        string newPanelName = "";
        string Type = "";
        void Start()
        {
            CreatedPanelDropDown.onValueChanged.AddListener(delegate { DropdownItemSelected(CreatedPanelDropDown); });
            BUPButton.onClick.AddListener(BUPbottonclick);
            BUP45AssemblyButton.onClick.AddListener(BUP90Assemblybottonclick);
            ECP_NButton.onClick.AddListener(ECP_Nbottonclick);
            ECP_SButton.onClick.AddListener(ECP_Sbottonclick);
            OWP_threeButton.onClick.AddListener(OWP_threebottonclick);
            OWP_twoButton.onClick.AddListener(OWP_twobottonclick);
            OWP_cornerButton.onClick.AddListener(OWP_cornerbottonclick);
            SVDUButton.onClick.AddListener(SVDUbottonclick);
            LargeScreenButton.onClick.AddListener(LargeScreenbottonclick);
            LargeScreen45AssemblyButton.onClick.AddListener(LargeScreen45Assemblybottonclick);
            ChairButton.onClick.AddListener(Chairbottonclick);
            DeleteButton.onClick.AddListener(DeletePanel);
            //新建盘台名称UI
            Yes.onClick.AddListener(CreatedPanel);


            BUPpanel = BUPprefab;
            BUP45Assemblypanel = BUP90Assemblyprefab;
            ECP_Npanel = ECP_Nprefab;
            ECP_Spanel = ECP_Sprefab;
            OWP_threepanel = OWP_threeprefab;
            OWP_twopanel = OWP_twoprefab;
            OWP_cornerpanel = OWP_cornerprefab;
            SVDUpanel = SVDUprefab;
            LargeScreenpanel = LargeScreenprefab;
            LargeScreen45Assemblypanel = LargeScreen45Assemblyprefab;
            Chairpanel = Chairprefab;
            //OnPanelDelete.AddListener(PanelDataUpdate);
        }



        public void PanelsClear()
        {
            DropdownItemSelected(CreatedPanelDropDown);
        }

        private void DropdownItemSelected(Dropdown createdPanelDropDown)
        {
            createdPanelDropDown.options.Clear();
            foreach (Panel panel in CNCC.Panels.Panel.AllPanels)
            {
                createdPanelDropDown.options.Add(new Dropdown.OptionData() { text = panel.ID });

            }
            if (createdPanelDropDown.options.Count != 0 && createdPanelDropDown.value > createdPanelDropDown.options.Count - 1)
            {
                createdPanelDropDown.value = createdPanelDropDown.value - 1;
            }
            if (createdPanelDropDown.options.Count == 0)
            {
                createdPanelDropDown.captionText.text = "";

            }
            else
            {
                createdPanelDropDown.captionText.text = CNCC.Panels.Panel.AllPanels[createdPanelDropDown.value].ID;
                currentPanel = CNCC.Panels.Panel.AllPanels[createdPanelDropDown.value];
            }
        }

        void Update()
        {
            spawnTansform.position = new Vector3(Wall.WallLength / 2, 0, Wall.WallWidth / 2);
            if (Panel.AllPanels.Count == 0)
            {
                if (CreatedPanelDropDown.options.Count == 0)
                {
                    return;
                }
            }
            else if (Panel.AllPanels.Count != CreatedPanelDropDown.options.Count)
            {
                DropdownItemSelected(CreatedPanelDropDown);
            }
            OnDeleteKeyDown();

        }

        #region 左边盘台类别点击事件触发
        void BUPbottonclick()
        {
            NameUI.SetActive(true);
            Type = "BUP";
            State.text = "为该" + Type + "盘台命名";
        }

        void BUP90Assemblybottonclick()
        {
            NameUI.SetActive(true);
            Type = "BUP90Assembly";
            State.text = "为该BUP90°拼角台命名";
        }

        void ECP_Nbottonclick()
        {
            NameUI.SetActive(true);
            Type = "ECP_N";
            State.text = "为该" + Type + "盘台命名";
        }

        void ECP_Sbottonclick()
        {
            NameUI.SetActive(true);
            Type = "ECP_S";
            State.text = "为该" + Type + "盘台命名";
        }

        void OWP_threebottonclick()
        {
            NameUI.SetActive(true);
            Type = "OWP_three";
            State.text = "为该三平台命名";
        }

        void OWP_twobottonclick()
        {
            NameUI.SetActive(true);
            Type = "OWP_two";
            State.text = "为该两平台命名";
        }

        void OWP_cornerbottonclick()
        {
            NameUI.SetActive(true);
            Type = "OWP_corner";
            State.text = "为该拐角盘台命名";
        }

        void SVDUbottonclick()
        {
            NameUI.SetActive(true);
            Type = "SVDU";
            State.text = "为该" + Type + "盘台命名";
        }

        void LargeScreenbottonclick()
        {
            NameUI.SetActive(true);
            Type = "LargeScreen";
            State.text = "为该大屏幕盘台命名";
        }

        void LargeScreen45Assemblybottonclick()
        {
            NameUI.SetActive(true);
            Type = "LargeScreen45Assembly";
            State.text = "为该大屏幕拼角命名";
        }

        void Chairbottonclick()
        {
            NameUI.SetActive(true);
            Type = "Chair";
            State.text = "为该椅子命名";
        }

        #endregion

        #region 创造具体盘台方法
        void CreatBUP()
        {
            BUP bUP = new BUP(BUPpanel, originTansform, spawnTansform, newNameText.text);
            DropdownItemSelected(CreatedPanelDropDown);
        }

        void CreatBUP45Assembly()
        {
            BUP90Assembly bUP90Assembly = new BUP90Assembly(BUP45Assemblypanel, originTansform, spawnTansform, newNameText.text);
            DropdownItemSelected(CreatedPanelDropDown);
        }

        void CreatECP_N()
        {
            ECP_N ecp_N = new ECP_N(ECP_Npanel, originTansform, spawnTansform, newNameText.text);
            DropdownItemSelected(CreatedPanelDropDown);
        }

        void CreatECP_S()
        {
            ECP_S ecp_S = new ECP_S(ECP_Spanel, originTansform, spawnTansform, newNameText.text);
            DropdownItemSelected(CreatedPanelDropDown);
        }

        void CreatOWP_threeButton()
        {
            OWP_three owp_three = new OWP_three(OWP_threepanel, originTansform, spawnTansform, newNameText.text);
            //owp_three.gameObject.GetComponent<Transform>().position = spawnTansform.position;
            DropdownItemSelected(CreatedPanelDropDown);
        }

        void CreatOWP_twoButton()
        {
            OWP_two owp_two = new OWP_two(OWP_twopanel, originTansform, spawnTansform, newNameText.text);
            //owp_two.gameObject.GetComponent<Transform>().position = spawnTansform.position;

            DropdownItemSelected(CreatedPanelDropDown);
        }

        void CreatOWP_cornerButton()
        {
            OWP_corner owp_corner = new OWP_corner(OWP_cornerpanel, originTansform, spawnTansform, newNameText.text);
            DropdownItemSelected(CreatedPanelDropDown);
        }

        void CreatSVDU()
        {
            SVDU svdu = new SVDU(SVDUpanel, originTansform, spawnTansform, newNameText.text);
            DropdownItemSelected(CreatedPanelDropDown);
        }

        void CreatLargeScreen()
        {
            LargeScreen laegerscreen = new LargeScreen(LargeScreenpanel, originTansform, spawnTansform, newNameText.text);
            DropdownItemSelected(CreatedPanelDropDown);
        }

        void CreatLargeScreen45Assembly()
        {
            LargeScreen45Assembly assembly = new LargeScreen45Assembly(LargeScreen45Assemblypanel, originTansform, spawnTansform, newNameText.text);
            DropdownItemSelected(CreatedPanelDropDown);
        }

        void CreatChair()
        {
            Chair chair = new Chair(Chairpanel, originTansform, spawnTansform, newNameText.text);
            DropdownItemSelected(CreatedPanelDropDown);
        }

        void DeletePanel()
        {
            GameManager.Instance.Save();
            if (currentPanel is BUP)
            {
                BUP.AllBUPs.Remove((BUP)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is BUP90Assembly)
            {
                BUP90Assembly.AllBUP90s.Remove((BUP90Assembly)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is ECP_N)
            {
                ECP_N.ALLECP_Ns.Remove((ECP_N)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is ECP_S)
            {
                ECP_S.ALLECP_Ss.Remove((ECP_S)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is OWP_three)
            {
                OWP_three.AllOWP_three.Remove((OWP_three)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is OWP_two)
            {
                OWP_two.AllOWP_two.Remove((OWP_two)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is OWP_corner)
            {
                OWP_corner.AllOWP_corner.Remove((OWP_corner)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is SVDU)
            {
                SVDU.AllSVDU.Remove((SVDU)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is LargeScreen)
            {
                LargeScreen.AllLargeScreen.Remove((LargeScreen)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is LargeScreen45Assembly)
            {
                LargeScreen45Assembly.AllLargeScreen45Assembly.Remove((LargeScreen45Assembly)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is Chair)
            {
                Chair.ALLChairs.Remove((Chair)currentPanel);
                PanelDataUpdate();
            }
            OnPanelDelete.Invoke();
        }

        void DeletePanel(Panel currentPanel)
        {
            GameManager.Instance.Save();
            if (currentPanel is BUP)
            {
                BUP.AllBUPs.Remove((BUP)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is BUP90Assembly)
            {
                BUP90Assembly.AllBUP90s.Remove((BUP90Assembly)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is ECP_N)
            {
                ECP_N.ALLECP_Ns.Remove((ECP_N)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is ECP_S)
            {
                ECP_S.ALLECP_Ss.Remove((ECP_S)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is OWP_three)
            {
                OWP_three.AllOWP_three.Remove((OWP_three)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is OWP_two)
            {
                OWP_two.AllOWP_two.Remove((OWP_two)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is OWP_corner)
            {
                OWP_corner.AllOWP_corner.Remove((OWP_corner)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is SVDU)
            {
                SVDU.AllSVDU.Remove((SVDU)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is LargeScreen)
            {
                LargeScreen.AllLargeScreen.Remove((LargeScreen)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is LargeScreen45Assembly)
            {
                LargeScreen45Assembly.AllLargeScreen45Assembly.Remove((LargeScreen45Assembly)currentPanel);
                PanelDataUpdate();
            }
            else if (currentPanel is Chair)
            {
                Chair.ALLChairs.Remove((Chair)currentPanel);
                PanelDataUpdate();
            }
            OnPanelDelete.Invoke();
        }
        #endregion

        private void PanelDataUpdate()
        {
            if (CreatedPanelDropDown.options.Count == 0)
            {
                CreatedPanelDropDown.captionText.text = "";
                return;
            }
            Panel.AllPanels.Remove(currentPanel);
            Destroy(currentPanel.gameObject);
            //CreatedPanelDropDown.options.RemoveAt(CreatedPanelDropDown.value);
            DropdownItemSelected(CreatedPanelDropDown);
        }

        private void CreatedPanel()
        {
            if (Panel.IsRepeat(newNameText.text))
            {
                TipUI.SetActive(true);
                return;
            }
            GameManager.Instance.Save();
            switch (Type)
            {
                case "":
                    break;

                case "BUP":
                    CreatBUP();
                    break;

                case "BUP90Assembly":
                    CreatBUP45Assembly();
                    break;

                case "ECP_N":
                    CreatECP_N();
                    break;

                case "ECP_S":
                    CreatECP_S();
                    break;

                case "LargeScreen":
                    CreatLargeScreen();
                    break;

                case "LargeScreen45Assembly":
                    CreatLargeScreen45Assembly();
                    break;

                case "OWP_corner":
                    CreatOWP_cornerButton();
                    break;

                case "OWP_three":
                    CreatOWP_threeButton();
                    break;

                case "OWP_two":
                    CreatOWP_twoButton();
                    break;

                case "SVDU":
                    CreatSVDU();
                    break;

                case "Chair":
                    CreatChair();
                    break;
            }
        }

        private void OnDeleteKeyDown()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                if (RightDownBarUI.clickObj.GetComponent<Panel>().Equals(currentPanel) && currentPanel != null)
                {
                    DeletePanel(currentPanel);
                }
                else
                {
                    DeletePanel(RightDownBarUI.clickObj.GetComponent<Panel>());
                }
            }
        }
    }
}