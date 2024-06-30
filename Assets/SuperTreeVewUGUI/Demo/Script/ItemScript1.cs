using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace SuperTreeView
{
    public class ItemScript1 : MonoBehaviour
    {
        public Button mExpandBtn;
        public Image mIcon;
        public Image mSelectImg;
        public Button mClickBtn;
        public Text mLabelText;
        string mData = "";

        public Slider Slider_x;
        public Slider Slider_y;
        public Slider Slider_z;
        public Text Slider_x_Value;
        public Text Slider_y_Value;
        public Text Slider_z_Value;

        public static string JointInfo;
        public static string JointData;

        public string Data
        {
            get
            {
                return mData;
            }
            set
            {
                mData = value;
            }
        }

        public string Text
        {
            get
            {
                return mLabelText.text;
            }
            set
            {
                mLabelText.text = value;
            }
        }
        void Start()
        {
            mExpandBtn.onClick.AddListener(OnExpandBtnClicked);
            mClickBtn.onClick.AddListener(OnItemClicked);
        }

        public void Init()
        {
            SetExpandBtnVisible(false);
            SetExpandStatus(true);
            IsSelected = false;
        }

        void OnExpandBtnClicked()
        {
            TreeViewItem item = GetComponent<TreeViewItem>();
            item.DoExpandOrCollapse();
        }


        public void SetItemInfo(string iconSpriteName,string labelTxt,string data = "")
        {
            Init();
            mIcon.sprite = ResManager.Instance.GetSpriteByName(iconSpriteName);
            mLabelText.text = labelTxt;
            mData = data;

        }

        void OnItemClicked()
        {
            TreeViewItem item = GetComponent<TreeViewItem>();
            item.RaiseCustomEvent(CustomEvent.ItemClicked, null);
            Debug.Log("TreeViewItem Clicked " + Text + Data);            
            JointInfo = Text;
            JointData = Data;
           // Bone_LocalEulerAngles(Text);

        }

        public void SetExpandBtnVisible(bool visible)
        {
            if (visible)
            {
                mExpandBtn.gameObject.SetActive(true);
            }
            else
            {
                mExpandBtn.gameObject.SetActive(false);
            }
        }

        public bool IsSelected
        {
            get
            {
                return mSelectImg.gameObject.activeSelf;
            }
            set
            {
                mSelectImg.gameObject.SetActive(value);
            }
        }
        public void SetExpandStatus(bool expand)
        {
            if (expand)
            {
                mExpandBtn.transform.localEulerAngles = new Vector3(0, 0, -90);
            }
            else
            {
                mExpandBtn.transform.localEulerAngles = new Vector3(0, 0, 0);

            }
        }

        //void Bone_LocalEulerAngles(string JointInfo)
        //{
        //    switch (JointInfo)
        //    {
        //        case "头":
        //            SliderAllSetActive();
        //            SliderTextFalseActiveY();
        //            break;

        //        case "颈椎":
        //            SliderAllSetActive();
        //            SliderTextFalseActiveX();
        //            SliderTextFalseActiveZ();
        //            break;

        //        case "胸椎":
        //            SliderAllSetActive();
        //            break;

        //        case "左肩关节":
        //            SliderAllSetActive();
        //            break;

        //        case "右肩关节":
        //            SliderAllSetActive();
        //            break;

        //        case "左肘关节":
        //            SliderAllSetActive();
        //            SliderTextFalseActiveZ();
        //            break;

        //        case "右肘关节":
        //            SliderAllSetActive();
        //            SliderTextFalseActiveZ();
        //            break;

        //        case "左腕关节":
        //            SliderAllSetActive();
        //            SliderTextFalseActiveX();
        //            break;

        //        case "右腕关节":
        //            SliderAllSetActive();
        //            SliderTextFalseActiveX();
        //            break;

        //        case "左髋关节":
        //            SliderAllSetActive();
        //            break;

        //        case "右髋关节":
        //            SliderAllSetActive();
        //            break;

        //        case "左膝关节":
        //            SliderAllSetActive();
        //            SliderTextFalseActiveY();
        //            SliderTextFalseActiveZ();
        //            break;

        //        case "右膝关节":
        //            SliderAllSetActive();
        //            SliderTextFalseActiveY();
        //            SliderTextFalseActiveZ();
        //            break;

        //        case "左踝关节":
        //            SliderAllSetActive();
        //            break;

        //        case "右踝关节":
        //            SliderAllSetActive();
        //            break;

        //        default:
        //            FalseAllSilderText();
        //            break;
        //    }

        }


    }

