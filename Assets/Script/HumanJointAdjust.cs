using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SuperTreeView
{
    public class HumanJointAdjust : MonoBehaviour
    {
        public GameObject Head;
        public GameObject Neck;
        public GameObject Chest;
        public GameObject Shoulder_L;
        public GameObject Shoulder_R;
        public GameObject Elbow_L;
        public GameObject Elbow_R;
        public GameObject Wrist_L;
        public GameObject Wrist_R;
        public GameObject Hip_L;
        public GameObject Hip_R;
        public GameObject Knee_L;
        public GameObject Knee_R;
        public GameObject Ankle_L;
        public GameObject Ankle_R;

        public Slider Slider_x;
        public Slider Slider_y;
        public Slider Slider_z;
        public Text Slider_x_Value;
        public Text Slider_y_Value;
        public Text Slider_z_Value;
        List<GameObject> humanList = new List<GameObject>();
        static string str;
        //Head
        static float Head_x_max = 48;
        static float Head_x_min = -57;
        static float Head_y_max = 0;
        static float Head_y_min = -1;
        static float Head_z_max = 26;
        static float Head_z_min = -26;

        //neck
        static float Neck_x_max = 0;
        static float Neck_x_min = -1;
        static float Neck_y_max = 63;
        static float Neck_y_min = -63;
        static float Neck_z_max = 0;
        static float Neck_z_min = -1;

        //chest
        static float Chest_x_max = 80;
        static float Chest_x_min = -25;
        static float Chest_y_max = 20;
        static float Chest_y_min = -20;
        static float Chest_z_max = 50;
        static float Chest_z_min = -50;

        //shoulder_L
        static float Shoulder_L_x_max = 61;
        static float Shoulder_L_x_min = -180;
        static float Shoulder_L_y_max = 140;
        static float Shoulder_L_y_min = -34;
        static float Shoulder_L_z_max = 134;
        static float Shoulder_L_z_min = -48;

        //shoulder_R 
        static float Shoulder_R_x_max = 61;
        static float Shoulder_R_x_min = -180;
        static float Shoulder_R_y_max = 34;
        static float Shoulder_R_y_min = -140;
        static float Shoulder_R_z_max = 48;
        static float Shoulder_R_z_min = -134;

        //Elbow_L_Model1
        static float Elbow_L_x_max = 78;
        static float Elbow_L_x_min = -119;
        static float Elbow_L_y_max = 141;
        static float Elbow_L_y_min = 0;
        static float Elbow_L_z_max = 0;
        static float Elbow_L_z_min = -1;

        //Elbow_R_Model1
        static float Elbow_R_x_max = 78;
        static float Elbow_R_x_min = -119;
        static float Elbow_R_y_max = 0;
        static float Elbow_R_y_min = -141;
        static float Elbow_R_z_max = 0;
        static float Elbow_R_z_min = -1;

        //Wrist_L_Model1
        static float Wrist_L_x_max = 0;
        static float Wrist_L_x_min = -1;
        static float Wrist_L_y_max = 30;
        static float Wrist_L_y_min = -44;
        static float Wrist_L_z_max = 87;
        static float Wrist_L_z_min = -104;

        //Wrist_R_Model1
        static float Wrist_R_x_max = 0;
        static float Wrist_R_x_min = -1;
        static float Wrist_R_y_max = 44;
        static float Wrist_R_y_min = -30;
        static float Wrist_R_z_max = 104;
        static float Wrist_R_z_min = -87;

        //Hip_L_Model1
        static float Hip_L_x_max = 20;
        static float Hip_L_x_min = -109;
        static float Hip_L_y_max = 43;
        static float Hip_L_y_min = -30;
        static float Hip_L_z_max = 28;
        static float Hip_L_z_min = -53;

        //Hip_R_Model1
        static float Hip_R_x_max = 20;
        static float Hip_R_x_min = -109;
        static float Hip_R_y_max = 30;
        static float Hip_R_y_min = -43;
        static float Hip_R_z_max = 53;
        static float Hip_R_z_min = -28;

        //Knee_L_Model1
        static float Knee_L_x_max = 130;
        static float Knee_L_x_min = 0;
        static float Knee_L_y_max = 0;
        static float Knee_L_y_min = -1;
        static float Knee_L_z_max = 0;
        static float Knee_L_z_min = -1;

        //Knee_R_Model1
        static float Knee_R_x_max = 130;
        static float Knee_R_x_min = 0;
        static float Knee_R_y_max = 0;
        static float Knee_R_y_min = -1;
        static float Knee_R_z_max = 0;
        static float Knee_R_z_min = -1;

        //Ankle_L_Model1
        static float Ankle_L_x_max = 40;
        static float Ankle_L_x_min = -47;
        static float Ankle_L_y_max = 30;
        static float Ankle_L_y_min = -42;
        static float Ankle_L_z_max = 30;
        static float Ankle_L_z_min = -28;

        //Ankle_R_Model1
        static float Ankle_R_x_max = 40;
        static float Ankle_R_x_min = -47;
        static float Ankle_R_y_max = 42;
        static float Ankle_R_y_min = -30;
        static float Ankle_R_z_max = 28;
        static float Ankle_R_z_min = -30;

        #region 当前关节角度的值
        static float Head_x_Value_Angle = 0;
        static float Head_y_Value_Angle = 0;
        static float Head_z_Value_Angle = 0;

        static float Neck_x_Value_Angle = 0;
        static float Neck_y_Value_Angle = 0;
        static float Neck_z_Value_Angle = 0;

        static float Chest_x_Value_Angle = 0;
        static float Chest_y_Value_Angle = 0;
        static float Chest_z_Value_Angle = 0;

        static float Shoulder_L_x_Value_Angle = 0;
        static float Shoulder_L_y_Value_Angle = 0;
        static float Shoulder_L_z_Value_Angle = 0;

        static float Shoulder_R_x_Value_Angle = 0;
        static float Shoulder_R_y_Value_Angle = 0;
        static float Shoulder_R_z_Value_Angle = 0;

        static float Elbow_L_x_Value_Angle = 0;
        static float Elbow_L_y_Value_Angle = 0;
        static float Elbow_L_z_Value_Angle = 0;

        static float Elbow_R_x_Value_Angle = 0;
        static float Elbow_R_y_Value_Angle = 0;
        static float Elbow_R_z_Value_Angle = 0;

        static float Wrist_L_x_Value_Angle = 0;
        static float Wrist_L_y_Value_Angle = 0;
        static float Wrist_L_z_Value_Angle = 0;

        static float Wrist_R_x_Value_Angle = 0;
        static float Wrist_R_y_Value_Angle = 0;
        static float Wrist_R_z_Value_Angle = 0;

        static float Hip_L_x_Value_Angle = 0;
        static float Hip_L_y_Value_Angle = 0;
        static float Hip_L_z_Value_Angle = 0;

        static float Hip_R_x_Value_Angle = 0;
        static float Hip_R_y_Value_Angle = 0;
        static float Hip_R_z_Value_Angle = 0;

        static float Knee_L_x_Value_Angle = 0;
        static float Knee_L_y_Value_Angle = 0;
        static float Knee_L_z_Value_Angle = 0;

        static float Knee_R_x_Value_Angle = 0;
        static float Knee_R_y_Value_Angle = 0;
        static float Knee_R_z_Value_Angle = 0;

        static float Ankle_L_x_Value_Angle = 0;
        static float Ankle_L_y_Value_Angle = 0;
        static float Ankle_L_z_Value_Angle = 0;

        static float Ankle_R_x_Value_Angle = 0;
        static float Ankle_R_y_Value_Angle = 0;
        static float Ankle_R_z_Value_Angle = 0;

        #endregion
        void Start()
        {
            // Slider_x.value = (Head_x_Value - Head_x_max) / (Head_x_max - Head_x_min) + 1;
            str = "";
        }

        private void Update()
        {
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //print("头部x:" + Head_x_Value_Angle);
            //print("头部y:" + Head_y_Value_Angle);
            //print("头部z:" + Head_z_Value_Angle);

            //print("当前str：" + str);
            //print("当前ItemScript1.JointInfo：" + ItemScript1.JointInfo);
            if (str != ItemScript1.JointInfo)
            {
                str = ItemScript1.JointInfo;
                switch (ItemScript1.JointInfo)
                {
                    case "头":
                        HeadSliderTextUpdata();
                        break;

                    case "颈椎":
                        NeckSliderTextUpdata();

                        break;

                    case "胸椎":
                        ChestSliderTextUpdata();
                        break;

                    case "左肩关节":
                        Shoulder_LSliderTextUpdata();
                        break;

                    case "右肩关节":
                        Shoulder_RSliderTextUpdata();
                        break;

                    case "左肘关节":
                        Elbow_LSliderTextUpdata();

                        break;

                    case "右肘关节":
                        Elbow_RSliderTextUpdata();
                        break;

                    case "左腕关节":
                        Wrist_LSliderTextUpdata();
                        break;

                    case "右腕关节":
                        Wrist_RSliderTextUpdata();
                        break;

                    case "左髋关节":
                        Hip_LSliderTextUpdata();
                        break;

                    case "右髋关节":
                        Hip_RSliderTextUpdata();
                        break;

                    case "左膝关节":
                        Knee_LSliderTextUpdata();
                        break;

                    case "右膝关节":
                        Knee_RSliderTextUpdata();
                        break;

                    case "左踝关节":
                        Ankle_LSliderTextUpdata();
                        break;

                    case "右踝关节":
                        Ankle_RSliderTextUpdata();
                        break;
                }
            }

        }


        public void Bone_LocalEulerAngles()
        {
            switch (ItemScript1.JointInfo)
            {
                case "头":
                    //HeadSliderTextUpdata();
                    Head_localEulerAngles();
                    HeadText();

                    break;

                case "颈椎":
                    //NeckSliderTextUpdata();
                    Neck_localEulerAngles();
                    NeckText();

                    break;

                case "胸椎":
                    //ChestSliderTextUpdata();
                    Chest_localEulerAngles();
                    ChestText();

                    break;

                case "左肩关节":
                    Shoulder_L_localEulerAngles();
                    Shoulder_LText();

                    break;

                case "右肩关节":
                    Shoulder_R_localEulerAngles();
                    Shoulder_RText();

                    break;

                case "左肘关节":
                    Elbow_L_localEulerAngles();
                    Elbow_LText();

                    break;

                case "右肘关节":
                    Elbow_R_localEulerAngles();
                    Elbow_RText();

                    break;

                case "左腕关节":
                    Wrist_L_localEulerAngles();
                    Wrist_LText();

                    break;

                case "右腕关节":
                    Wrist_R_localEulerAngles();
                    Wrist_RText();

                    break;

                case "左髋关节":
                    Hip_L_localEulerAngles();
                    Hip_LText();

                    break;

                case "右髋关节":
                    Hip_R_localEulerAngles();
                    Hip_RText();

                    break;

                case "左膝关节":
                    Knee_L_localEulerAngles();
                    Knee_LText();

                    break;

                case "右膝关节":
                    Knee_R_localEulerAngles();
                    Knee_RText();

                    break;

                case "左踝关节":
                    Ankle_L_localEulerAngles();
                    Ankle_LText();

                    break;

                case "右踝关节":
                    Ankle_R_localEulerAngles();
                    Ankle_RText();

                    break;
            }

        }
        void Head_localEulerAngles()
        {
            Head.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Head_x_max - Head_x_min) + Head_x_max), (float)((Slider_y.value - 1) * (Head_y_max - Head_y_min) + Head_y_max), (float)((Slider_z.value - 1) * (Head_z_max - Head_z_min) + Head_z_max));
            Head_x_Value_Angle = (Slider_x.value - 1) * (Head_x_max - Head_x_min) + Head_x_max;
            //Head_y_Value_Angle = (Slider_y.value - 1) * (Head_y_max - Head_y_min) + Head_y_max;
            Head_y_Value_Angle = 0;
            Head_z_Value_Angle = (Slider_z.value - 1) * (Head_z_max - Head_z_min) + Head_z_max;
        }

        void Neck_localEulerAngles()
        {
            Neck.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Neck_x_max - Neck_x_min) + Neck_x_max), (float)((Slider_y.value - 1) * (Neck_y_max - Neck_y_min) + Neck_y_max), (float)((Slider_z.value - 1) * (Neck_z_max - Neck_z_min) + Neck_z_max));
            Neck_x_Value_Angle = (Slider_x.value - 1) * (Neck_x_max - Neck_x_min) + Neck_x_max;
            Neck_y_Value_Angle = (Slider_y.value - 1) * (Neck_y_max - Neck_y_min) + Neck_y_max;
            Neck_z_Value_Angle = (Slider_z.value - 1) * (Neck_z_max - Neck_z_min) + Neck_z_max;
        }

        void Chest_localEulerAngles()
        {
            Chest.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Chest_x_max - Chest_x_min) + Chest_x_max), (float)((Slider_y.value - 1) * (Chest_y_max - Chest_y_min) + Chest_y_max), (float)((Slider_z.value - 1) * (Chest_z_max - Chest_z_min) + Chest_z_max));
            Chest_x_Value_Angle = (Slider_x.value - 1) * (Chest_x_max - Chest_x_min) + Chest_x_max;
            Chest_y_Value_Angle = (Slider_y.value - 1) * (Chest_y_max - Chest_y_min) + Chest_y_max;
            Chest_z_Value_Angle = (Slider_z.value - 1) * (Chest_z_max - Chest_z_min) + Chest_z_max;
        }

        void Shoulder_L_localEulerAngles()
        {
            Shoulder_L.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Shoulder_L_x_max - Shoulder_L_x_min) + Shoulder_L_x_max), (float)((Slider_y.value - 1) * (Shoulder_L_y_max - Shoulder_L_y_min) + Shoulder_L_y_max), (float)((Slider_z.value - 1) * (Shoulder_L_z_max - Shoulder_L_z_min) + Shoulder_L_z_max));
            Shoulder_L_x_Value_Angle = (Slider_x.value - 1) * (Shoulder_L_x_max - Shoulder_L_x_min) + Shoulder_L_x_max;
            Shoulder_L_y_Value_Angle = (Slider_y.value - 1) * (Shoulder_L_y_max - Shoulder_L_y_min) + Shoulder_L_y_max;
            Shoulder_L_z_Value_Angle = (Slider_z.value - 1) * (Shoulder_L_z_max - Shoulder_L_z_min) + Shoulder_L_z_max;
        }

        void Shoulder_R_localEulerAngles()
        {
            Shoulder_R.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Shoulder_R_x_max - Shoulder_R_x_min) + Shoulder_R_x_max), (float)((Slider_y.value - 1) * (Shoulder_R_y_max - Shoulder_R_y_min) + Shoulder_R_y_max), (float)((Slider_z.value - 1) * (Shoulder_R_z_max - Shoulder_R_z_min) + Shoulder_R_z_max));
            Shoulder_R_x_Value_Angle = (Slider_x.value - 1) * (Shoulder_R_x_max - Shoulder_R_x_min) + Shoulder_R_x_max;
            Shoulder_R_y_Value_Angle = (Slider_y.value - 1) * (Shoulder_R_y_max - Shoulder_R_y_min) + Shoulder_R_y_max;
            Shoulder_R_z_Value_Angle = (Slider_z.value - 1) * (Shoulder_R_z_max - Shoulder_R_z_min) + Shoulder_R_z_max;
        }

        void Elbow_L_localEulerAngles()
        {
            Elbow_L.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Elbow_L_x_max - Elbow_L_x_min) + Elbow_L_x_max), (float)((Slider_y.value - 1) * (Elbow_L_y_max - Elbow_L_y_min) + Elbow_L_y_max), (float)((Slider_z.value - 1) * (Elbow_L_z_max - Elbow_L_z_min) + Elbow_L_z_max));
            Elbow_L_x_Value_Angle = (Slider_x.value - 1) * (Elbow_L_x_max - Elbow_L_x_min) + Elbow_L_x_max;
            Elbow_L_y_Value_Angle = (Slider_y.value - 1) * (Elbow_L_y_max - Elbow_L_y_min) + Elbow_L_y_max;
            Elbow_L_z_Value_Angle = (Slider_z.value - 1) * (Elbow_L_z_max - Elbow_L_z_min) + Elbow_L_z_max;
        }

        void Elbow_R_localEulerAngles()
        {
            Elbow_R.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Elbow_R_x_max - Elbow_R_x_min) + Elbow_R_x_max), (float)((Slider_y.value - 1) * (Elbow_R_y_max - Elbow_R_y_min) + Elbow_R_y_max), (float)((Slider_z.value - 1) * (Elbow_R_z_max - Elbow_R_z_min) + Elbow_R_z_max));
            Elbow_R_x_Value_Angle = (Slider_x.value - 1) * (Elbow_R_x_max - Elbow_R_x_min) + Elbow_R_x_max;
            Elbow_R_y_Value_Angle = (Slider_y.value - 1) * (Elbow_R_y_max - Elbow_R_y_min) + Elbow_R_y_max;
            Elbow_R_z_Value_Angle = (Slider_z.value - 1) * (Elbow_R_z_max - Elbow_R_z_min) + Elbow_R_z_max;
        }

        void Wrist_L_localEulerAngles()
        {
            Wrist_L.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Wrist_L_x_max - Wrist_L_x_min) + Wrist_L_x_max), (float)((Slider_y.value - 1) * (Wrist_L_y_max - Wrist_L_y_min) + Wrist_L_y_max), (float)((Slider_z.value - 1) * (Wrist_L_z_max - Wrist_L_z_min) + Wrist_L_z_max));
            Wrist_L_x_Value_Angle = (Slider_x.value - 1) * (Wrist_L_x_max - Wrist_L_x_min) + Wrist_L_x_max;
            Wrist_L_y_Value_Angle = (Slider_y.value - 1) * (Wrist_L_y_max - Wrist_L_y_min) + Wrist_L_y_max;
            Wrist_L_z_Value_Angle = (Slider_z.value - 1) * (Wrist_L_z_max - Wrist_L_z_min) + Wrist_L_z_max;
        }

        void Wrist_R_localEulerAngles()
        {
            Wrist_R.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Wrist_R_x_max - Wrist_R_x_min) + Wrist_R_x_max), (float)((Slider_y.value - 1) * (Wrist_R_y_max - Wrist_R_y_min) + Wrist_R_y_max), (float)((Slider_z.value - 1) * (Wrist_R_z_max - Wrist_R_z_min) + Wrist_R_z_max));
            Wrist_R_x_Value_Angle = (Slider_x.value - 1) * (Wrist_R_x_max - Wrist_R_x_min) + Wrist_R_x_max;
            Wrist_R_y_Value_Angle = (Slider_y.value - 1) * (Wrist_R_y_max - Wrist_R_y_min) + Wrist_R_y_max;
            Wrist_R_z_Value_Angle = (Slider_z.value - 1) * (Wrist_R_z_max - Wrist_R_z_min) + Wrist_R_z_max;
        }

        void Hip_L_localEulerAngles()
        {
            Hip_L.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Hip_L_x_max - Hip_L_x_min) + Hip_L_x_max), (float)((Slider_y.value - 1) * (Hip_L_y_max - Hip_L_y_min) + Hip_L_y_max), (float)((Slider_z.value - 1) * (Hip_L_z_max - Hip_L_z_min) + Hip_L_z_max));
            Hip_L_x_Value_Angle = (Slider_x.value - 1) * (Hip_L_x_max - Hip_L_x_min) + Hip_L_x_max;
            Hip_L_y_Value_Angle = (Slider_y.value - 1) * (Hip_L_y_max - Hip_L_y_min) + Hip_L_y_max;
            Hip_L_z_Value_Angle = (Slider_z.value - 1) * (Hip_L_z_max - Hip_L_z_min) + Hip_L_z_max;
        }

        void Hip_R_localEulerAngles()
        {
            Hip_R.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Hip_R_x_max - Hip_R_x_min) + Hip_R_x_max), (float)((Slider_y.value - 1) * (Hip_R_y_max - Hip_R_y_min) + Hip_R_y_max), (float)((Slider_z.value - 1) * (Hip_R_z_max - Hip_R_z_min) + Hip_R_z_max));
            Hip_R_x_Value_Angle = (Slider_x.value - 1) * (Hip_R_x_max - Hip_R_x_min) + Hip_R_x_max;
            Hip_R_y_Value_Angle = (Slider_y.value - 1) * (Hip_R_y_max - Hip_R_y_min) + Hip_R_y_max;
            Hip_R_z_Value_Angle = (Slider_z.value - 1) * (Hip_R_z_max - Hip_R_z_min) + Hip_R_z_max;
        }

        void Knee_L_localEulerAngles()
        {
            Knee_L.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Knee_L_x_max - Knee_L_x_min) + Knee_L_x_max), (float)((Slider_y.value - 1) * (Knee_L_y_max - Knee_L_y_min) + Knee_L_y_max), (float)((Slider_z.value - 1) * (Knee_L_z_max - Knee_L_z_min) + Knee_L_z_max));
            Knee_L_x_Value_Angle = (Slider_x.value - 1) * (Knee_L_x_max - Knee_L_x_min) + Knee_L_x_max;
            Knee_L_y_Value_Angle = (Slider_y.value - 1) * (Knee_L_y_max - Knee_L_y_min) + Knee_L_y_max;
            Knee_L_z_Value_Angle = (Slider_z.value - 1) * (Knee_L_z_max - Knee_L_z_min) + Knee_L_z_max;
        }

        void Knee_R_localEulerAngles()
        {
            Knee_R.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Knee_R_x_max - Knee_R_x_min) + Knee_R_x_max), (float)((Slider_y.value - 1) * (Knee_R_y_max - Knee_R_y_min) + Knee_R_y_max), (float)((Slider_z.value - 1) * (Knee_R_z_max - Knee_R_z_min) + Knee_R_z_max));
            Knee_R_x_Value_Angle = (Slider_x.value - 1) * (Knee_R_x_max - Knee_R_x_min) + Knee_R_x_max;
            Knee_R_y_Value_Angle = (Slider_y.value - 1) * (Knee_R_y_max - Knee_R_y_min) + Knee_R_y_max;
            Knee_R_z_Value_Angle = (Slider_z.value - 1) * (Knee_R_z_max - Knee_R_z_min) + Knee_R_z_max;
        }

        void Ankle_L_localEulerAngles()
        {
            Ankle_L.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Ankle_L_x_max - Ankle_L_x_min) + Ankle_L_x_max), (float)((Slider_y.value - 1) * (Ankle_L_y_max - Ankle_L_y_min) + Ankle_L_y_max), (float)((Slider_z.value - 1) * (Ankle_L_z_max - Ankle_L_z_min) + Ankle_L_z_max));
            Ankle_L_x_Value_Angle = (Slider_x.value - 1) * (Ankle_L_x_max - Ankle_L_x_min) + Ankle_L_x_max;
            Ankle_L_y_Value_Angle = (Slider_y.value - 1) * (Ankle_L_y_max - Ankle_L_y_min) + Ankle_L_y_max;
            Ankle_L_z_Value_Angle = (Slider_z.value - 1) * (Ankle_L_z_max - Ankle_L_z_min) + Ankle_L_z_max;
        }

        void Ankle_R_localEulerAngles()
        {
            Ankle_R.transform.localEulerAngles = new Vector3((float)((Slider_x.value - 1) * (Ankle_R_x_max - Ankle_R_x_min) + Ankle_R_x_max), (float)((Slider_y.value - 1) * (Ankle_R_y_max - Ankle_R_y_min) + Ankle_R_y_max), (float)((Slider_z.value - 1) * (Ankle_R_z_max - Ankle_R_z_min) + Ankle_R_z_max));
            Ankle_R_x_Value_Angle = (Slider_x.value - 1) * (Ankle_R_x_max - Ankle_R_x_min) + Ankle_R_x_max;
            Ankle_R_y_Value_Angle = (Slider_y.value - 1) * (Ankle_R_y_max - Ankle_R_y_min) + Ankle_R_y_max;
            Ankle_R_z_Value_Angle = (Slider_z.value - 1) * (Ankle_R_z_max - Ankle_R_z_min) + Ankle_R_z_max;
        }

        #region 文本框显示内容
        void HeadText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Head_x_max - Head_x_min) + Head_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Head_y_max - Head_y_min) + Head_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Head_z_max - Head_z_min) + Head_z_max).ToString("F2");
        }

        void NeckText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Neck_x_max - Neck_x_min) + Neck_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Neck_y_max - Neck_y_min) + Neck_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Neck_z_max - Neck_z_min) + Neck_z_max).ToString("F2");
        }

        void ChestText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Chest_x_max - Chest_x_min) + Chest_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Chest_y_max - Chest_y_min) + Chest_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Chest_z_max - Chest_z_min) + Chest_z_max).ToString("F2");
        }
        void Shoulder_LText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Shoulder_L_x_max - Shoulder_L_x_min) + Shoulder_L_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Shoulder_L_y_max - Shoulder_L_y_min) + Shoulder_L_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Shoulder_L_z_max - Shoulder_L_z_min) + Shoulder_L_z_max).ToString("F2");
        }
        void Shoulder_RText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Shoulder_R_x_max - Shoulder_R_x_min) + Shoulder_R_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Shoulder_R_y_max - Shoulder_R_y_min) + Shoulder_R_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Shoulder_R_z_max - Shoulder_R_z_min) + Shoulder_R_z_max).ToString("F2");
        }
        void Elbow_LText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Elbow_L_x_max - Elbow_L_x_min) + Elbow_L_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Elbow_L_y_max - Elbow_L_y_min) + Elbow_L_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Elbow_L_z_max - Elbow_L_z_min) + Elbow_L_z_max).ToString("F2");
        }
        void Elbow_RText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Elbow_R_x_max - Elbow_R_x_min) + Elbow_R_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Elbow_R_y_max - Elbow_R_y_min) + Elbow_R_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Elbow_R_z_max - Elbow_R_z_min) + Elbow_R_z_max).ToString("F2");
        }
        void Wrist_LText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Wrist_L_x_max - Wrist_L_x_min) + Wrist_L_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Wrist_L_y_max - Wrist_L_y_min) + Wrist_L_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Wrist_L_z_max - Wrist_L_z_min) + Wrist_L_z_max).ToString("F2");
        }
        void Wrist_RText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Wrist_R_x_max - Wrist_R_x_min) + Wrist_R_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Wrist_R_y_max - Wrist_R_y_min) + Wrist_R_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Wrist_R_z_max - Wrist_R_z_min) + Wrist_R_z_max).ToString("F2");
        }
        void Hip_LText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Hip_L_x_max - Hip_L_x_min) + Hip_L_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Hip_L_y_max - Hip_L_y_min) + Hip_L_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Hip_L_z_max - Hip_L_z_min) + Hip_L_z_max).ToString("F2");
        }
        void Hip_RText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Hip_R_x_max - Hip_R_x_min) + Hip_R_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Hip_R_y_max - Hip_R_y_min) + Hip_R_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Hip_R_z_max - Hip_R_z_min) + Hip_R_z_max).ToString("F2");
        }
        void Knee_LText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Knee_L_x_max - Knee_L_x_min) + Knee_L_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Knee_L_y_max - Knee_L_y_min) + Knee_L_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Knee_L_z_max - Knee_L_z_min) + Knee_L_z_max).ToString("F2");
        }
        void Knee_RText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Knee_R_x_max - Knee_R_x_min) + Knee_R_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Knee_R_y_max - Knee_R_y_min) + Knee_R_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Knee_R_z_max - Knee_R_z_min) + Knee_R_z_max).ToString("F2");
        }
        void Ankle_LText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Ankle_L_x_max - Ankle_L_x_min) + Ankle_L_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Ankle_L_y_max - Ankle_L_y_min) + Ankle_L_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Ankle_L_z_max - Ankle_L_z_min) + Ankle_L_z_max).ToString("F2");
        }
        void Ankle_RText()
        {
            Slider_x_Value.text = ((Slider_x.value - 1) * (Ankle_R_x_max - Ankle_R_x_min) + Ankle_R_x_max).ToString("F2");
            Slider_y_Value.text = ((Slider_y.value - 1) * (Ankle_R_y_max - Ankle_R_y_min) + Ankle_R_y_max).ToString("F2");
            Slider_z_Value.text = ((Slider_z.value - 1) * (Ankle_R_z_max - Ankle_R_z_min) + Ankle_R_z_max).ToString("F2");
        }
        #endregion


        #region 滑块值重赋
        void HeadSliderTextUpdata()
        {
            //if (str.Equals(ItemScript1.JointInfo))
            //{
            //}
            //else
            //{

            //Slider_y.value = (Head_y_Value_Angle - Head_y_max) / (Head_y_max - Head_y_min) + 1;
            Slider_z.value = (Head_z_Value_Angle - Head_z_max) / (Head_z_max - Head_z_min) + 1;
            str = ItemScript1.JointInfo;
            Slider_y.value = 1;

            Slider_x.value = (Head_x_Value_Angle - Head_x_max) / (Head_x_max - Head_x_min) + 1;
            //}
        }

        void NeckSliderTextUpdata()
        {
            //if (str.Equals(ItemScript1.JointInfo))
            //{
            //}
            //else
            //{
            Slider_x.value = (Neck_x_Value_Angle - Neck_x_max) / (Neck_x_max - Neck_x_min) + 1;
            Slider_y.value = (Neck_y_Value_Angle - Neck_y_max) / (Neck_y_max - Neck_y_min) + 1;
            Slider_z.value = (Neck_z_Value_Angle - Neck_z_max) / (Neck_z_max - Neck_z_min) + 1;
            str = ItemScript1.JointInfo;
            //}
        }
        void ChestSliderTextUpdata()
        {
            //if (str.Equals(ItemScript1.JointInfo))
            //{
            //}
            //else
            //{
            Slider_x.value = (Chest_x_Value_Angle - Chest_x_max) / (Chest_x_max - Chest_x_min) + 1;
            Slider_y.value = (Chest_y_Value_Angle - Chest_y_max) / (Chest_y_max - Chest_y_min) + 1;
            Slider_z.value = (Chest_z_Value_Angle - Chest_z_max) / (Chest_z_max - Chest_z_min) + 1;
            str = ItemScript1.JointInfo;
            //}
        }
        void Shoulder_LSliderTextUpdata()
        {           
            Slider_x.value = (Shoulder_L_x_Value_Angle - Shoulder_L_x_max) / (Shoulder_L_x_max - Shoulder_L_x_min) + 1;
            Slider_y.value = (Shoulder_L_y_Value_Angle - Shoulder_L_y_max) / (Shoulder_L_y_max - Shoulder_L_y_min) + 1;
            Slider_z.value = (Shoulder_L_z_Value_Angle - Shoulder_L_z_max) / (Shoulder_L_z_max - Shoulder_L_z_min) + 1;
            str = ItemScript1.JointInfo;
        }

        void Shoulder_RSliderTextUpdata()
        {
            Slider_x.value = (Shoulder_R_x_Value_Angle - Shoulder_R_x_max) / (Shoulder_R_x_max - Shoulder_R_x_min) + 1;
            Slider_y.value = (Shoulder_R_y_Value_Angle - Shoulder_R_y_max) / (Shoulder_R_y_max - Shoulder_R_y_min) + 1;
            Slider_z.value = (Shoulder_R_z_Value_Angle - Shoulder_R_z_max) / (Shoulder_R_z_max - Shoulder_R_z_min) + 1;
            str = ItemScript1.JointInfo;
        }
        void Elbow_LSliderTextUpdata()
        {
            Slider_x.value = (Elbow_L_x_Value_Angle - Elbow_L_x_max) / (Elbow_L_x_max - Elbow_L_x_min) + 1;
            Slider_y.value = (Elbow_L_y_Value_Angle - Elbow_L_y_max) / (Elbow_L_y_max - Elbow_L_y_min) + 1;
            Slider_z.value = (Elbow_L_z_Value_Angle - Elbow_L_z_max) / (Elbow_L_z_max - Elbow_L_z_min) + 1;
            str = ItemScript1.JointInfo;
        }

        void Elbow_RSliderTextUpdata()
        {
            Slider_x.value = (Elbow_R_x_Value_Angle - Elbow_R_x_max) / (Elbow_R_x_max - Elbow_R_x_min) + 1;
            Slider_y.value = (Elbow_R_y_Value_Angle - Elbow_R_y_max) / (Elbow_R_y_max - Elbow_R_y_min) + 1;
            Slider_z.value = (Elbow_R_z_Value_Angle - Elbow_R_z_max) / (Elbow_R_z_max - Elbow_R_z_min) + 1;
            str = ItemScript1.JointInfo;
        }
        void Wrist_LSliderTextUpdata()
        {
            Slider_x.value = (Wrist_L_x_Value_Angle - Wrist_L_x_max) / (Wrist_L_x_max - Wrist_L_x_min) + 1;
            Slider_y.value = (Wrist_L_y_Value_Angle - Wrist_L_y_max) / (Wrist_L_y_max - Wrist_L_y_min) + 1;
            Slider_z.value = (Wrist_L_z_Value_Angle - Wrist_L_z_max) / (Wrist_L_z_max - Wrist_L_z_min) + 1;
            str = ItemScript1.JointInfo;
        }
        void Wrist_RSliderTextUpdata()
        {
            Slider_x.value = (Wrist_R_x_Value_Angle - Wrist_R_x_max) / (Wrist_R_x_max - Wrist_R_x_min) + 1;
            Slider_y.value = (Wrist_R_y_Value_Angle - Wrist_R_y_max) / (Wrist_R_y_max - Wrist_R_y_min) + 1;
            Slider_z.value = (Wrist_R_z_Value_Angle - Wrist_R_z_max) / (Wrist_R_z_max - Wrist_R_z_min) + 1;
            str = ItemScript1.JointInfo;
        }
        void Hip_LSliderTextUpdata()
        {
            Slider_x.value = (Hip_L_x_Value_Angle - Hip_L_x_max) / (Hip_L_x_max - Hip_L_x_min) + 1;
            Slider_y.value = (Hip_L_y_Value_Angle - Hip_L_y_max) / (Hip_L_y_max - Hip_L_y_min) + 1;
            Slider_z.value = (Hip_L_z_Value_Angle - Hip_L_z_max) / (Hip_L_z_max - Hip_L_z_min) + 1;
        }
        void Hip_RSliderTextUpdata()
        {
            Slider_x.value = (Hip_R_x_Value_Angle - Hip_R_x_max) / (Hip_R_x_max - Hip_R_x_min) + 1;
            Slider_y.value = (Hip_R_y_Value_Angle - Hip_R_y_max) / (Hip_R_y_max - Hip_R_y_min) + 1;
            Slider_z.value = (Hip_R_z_Value_Angle - Hip_R_z_max) / (Hip_R_z_max - Hip_R_z_min) + 1;
        }
        void Knee_LSliderTextUpdata()
        {
            Slider_x.value = (Knee_L_x_Value_Angle - Knee_L_x_max) / (Knee_L_x_max - Knee_L_x_min) + 1;
            Slider_y.value = (Knee_L_y_Value_Angle - Knee_L_y_max) / (Knee_L_y_max - Knee_L_y_min) + 1;
            Slider_z.value = (Knee_L_z_Value_Angle - Knee_L_z_max) / (Knee_L_z_max - Knee_L_z_min) + 1;
        }
        void Knee_RSliderTextUpdata()
        {
            Slider_x.value = (Knee_R_x_Value_Angle - Knee_R_x_max) / (Knee_R_x_max - Knee_R_x_min) + 1;
            Slider_y.value = (Knee_R_y_Value_Angle - Knee_R_y_max) / (Knee_R_y_max - Knee_R_y_min) + 1;
            Slider_z.value = (Knee_R_z_Value_Angle - Knee_R_z_max) / (Knee_R_z_max - Knee_R_z_min) + 1;
        }
        void Ankle_LSliderTextUpdata()
        {
            Slider_x.value = (Ankle_L_x_Value_Angle - Ankle_L_x_max) / (Ankle_L_x_max - Ankle_L_x_min) + 1;
            Slider_y.value = (Ankle_L_y_Value_Angle - Ankle_L_y_max) / (Ankle_L_y_max - Ankle_L_y_min) + 1;
            Slider_z.value = (Ankle_L_z_Value_Angle - Ankle_L_z_max) / (Ankle_L_z_max - Ankle_L_z_min) + 1;
        }
        void Ankle_RSliderTextUpdata()
        {
            Slider_x.value = (Ankle_R_x_Value_Angle - Ankle_R_x_max) / (Ankle_R_x_max - Ankle_R_x_min) + 1;
            Slider_y.value = (Ankle_R_y_Value_Angle - Ankle_R_x_max) / (Ankle_R_y_max - Ankle_R_y_min) + 1;
            Slider_z.value = (Ankle_R_z_Value_Angle - Ankle_R_x_max) / (Ankle_R_z_max - Ankle_R_z_min) + 1;
        }

    }
    #endregion




}
