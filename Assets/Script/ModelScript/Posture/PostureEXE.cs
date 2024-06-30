using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CNCC.Models
{
    public class PostureEXE : MonoBehaviour
    {
        public GameObject[] Human;
        public Dropdown dropdown;
        GameObject _currentHuman;

        GameObject Model;

        GameObject Head_M;


        GameObject Neck_M;


        GameObject Scapula_L;
        GameObject Scapula_R1;


        GameObject Shoulder_L;
        GameObject Shoulder_R;



        GameObject Elbow_L;
        GameObject Elbow_R;



        GameObject Wrist_L;
        GameObject Wrist_R;



        GameObject Spine1_M;

        GameObject Chest_M;


        GameObject Hip_L;
        GameObject Hip_R;



        GameObject Knee_L;
        GameObject Knee_R;


        GameObject Ankle_L;
        GameObject Ankle_R;
        //Transform transform1;
        //Transform transform2;
        //Transform transform3;
        Vector3 transform1;
        Vector3 transform2;
        Vector3 transform3;
        private void Awake()
        {
            this.gameObject.SetActive(true);
        }
        void Start()
        {
            _currentHuman = Human[1];//最初下拉列表不换时为人物1
            dropdown.GetComponent<Dropdown>();
            dropdown.options.Clear();
            List<string> HumanList = new List<string>();
            for (int i = 0; i < Human.Length; i++)
            {
                HumanList.Add(Human[i].name);
            }

            foreach (var Huamn in HumanList)
            {
                dropdown.options.Add(new Dropdown.OptionData() { text = Huamn });
            }

            dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });

            transform1 = new Vector3(11.4f, 0, 2.67f);
            transform2 = new Vector3(13.725f, 0, 2.257f);
            transform3 = new Vector3(15.992f, 0, 2.257f);

            Init();     //初始化
            _currentHuman = Human[0];

            this.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ActionEXE()
        {
            SelectActionHUman();
            PostureAngleDesign();
            ActionPosition();
        }

        public void ActionPosition()
        {
            if (_currentHuman == Human[0])
            {
                if (PostureDB.x_Knee_L_Angle == 0)
                {
                    _currentHuman.transform.localPosition = transform1;
                    transform1 = _currentHuman.transform.localPosition;
                }
                else
                {
                    _currentHuman.transform.position = new Vector3(12.454f, -0.4f, 4.1f);
                }

            }
            if (_currentHuman == Human[1])
            {
                if (PostureDB.x_Knee_L_Angle == 0)
                {
                    _currentHuman.transform.localPosition = transform2;
                    transform2 = _currentHuman.transform.localPosition;
                }
                else
                {
                    _currentHuman.transform.position = new Vector3(14.88f, -0.4f, 4.02f);
                }

            }
            if (_currentHuman == Human[2])
            {
                if (PostureDB.x_Spine1_M_Angle == 0)
                {
                    _currentHuman.transform.localPosition = transform3;
                    transform3 = _currentHuman.transform.localPosition;
                }
                else
                {
                    DiSanGeRen();
                    //_currentHuman.transform.position = new Vector3(12.354f, -0.341f, 2.6f);
                }

            }

        }

        void PostureAngleDesign()
        {
            //mdeol
            //Model.transform.localRotation = new Quaternion(PostureDB.x_Rotation, PostureDB.y_Rotation, PostureDB.z_Rotation,0);
            //Model.transform.rotation = Quaternion.Euler(PostureDB.x_Rotation, PostureDB.y_Rotation, PostureDB.z_Rotation);//new Vector3(PostureDB.x_Rotation, PostureDB.y_Rotation, PostureDB.z_Rotation);
            //Model.transform.localEulerAngles = new Vector3(PostureDB.x_Rotation, PostureDB.y_Rotation, PostureDB.z_Rotation);
            //头
            Head_M.transform.localEulerAngles = new Vector3(PostureDB.x_HeadAngle, PostureDB.y_HeadAngle, PostureDB.z_HeadAngle);

            //颈
            Neck_M.transform.localEulerAngles = new Vector3(PostureDB.x_NeckAngle, PostureDB.y_NeckAngle, PostureDB.z_NeckAngle);
            //胸椎
            Chest_M.transform.localEulerAngles = new Vector3(PostureDB.x_Chest_M_Angle, PostureDB.y_Chest_M_Angle, PostureDB.z_Chest_M_Angle);

            //左肩甲骨
            Scapula_L.transform.localEulerAngles = new Vector3(PostureDB.x_Scapula_L_Angle, PostureDB.y_Scapula_L_Angle, PostureDB.z_Scapula_L_Angle);

            //右肩甲骨
            Scapula_R1.transform.localEulerAngles = new Vector3(PostureDB.x_Scapula_R_Angle, PostureDB.y_Scapula_R_Angle, PostureDB.z_Scapula_R_Angle);

            //左肩
            Shoulder_L.transform.localEulerAngles = new Vector3(PostureDB.x_Shoulder_L_Angle, PostureDB.y_Shoulder_L_Angle, PostureDB.z_Shoulder_L_Angle);

            //右肩
            Shoulder_R.transform.localEulerAngles = new Vector3(PostureDB.x_Shoulder_R_Angle, PostureDB.y_Shoulder_R_Angle, PostureDB.z_Shoulder_R_Angle);

            //左肘
            Elbow_L.transform.localEulerAngles = new Vector3(PostureDB.x_Elbow_L_Angle, PostureDB.y_Elbow_L_Angle, PostureDB.z_Elbow_L_Angle);

            //右肘
            Elbow_R.transform.localEulerAngles = new Vector3(PostureDB.x_Elbow_R_Angle, PostureDB.y_Elbow_R_Angle, PostureDB.z_Elbow_R_Angle);

            //左腕
            Wrist_L.transform.localEulerAngles = new Vector3(PostureDB.x_Wrist_L_Angle, PostureDB.y_Wrist_L_Angle, PostureDB.z_Wrist_L_Angle);

            //右腕
            Wrist_R.transform.localEulerAngles = new Vector3(PostureDB.x_Wrist_R_Angle, PostureDB.y_Wrist_R_Angle, PostureDB.z_Wrist_R_Angle);

            //脊椎
            Spine1_M.transform.localEulerAngles = new Vector3(PostureDB.x_Spine1_M_Angle, PostureDB.y_Spine1_M_Angle, PostureDB.z_Spine1_M_Angle);

            //左大腿
            Hip_L.transform.localEulerAngles = new Vector3(PostureDB.x_Hip_L_Angle, PostureDB.y_Hip_L_Angle, PostureDB.z_Hip_L_Angle);

            //右大腿
            Hip_R.transform.localEulerAngles = new Vector3(PostureDB.x_Hip_R_Angle, PostureDB.y_Hip_R_Angle, PostureDB.z_Hip_R_Angle);

            //左膝盖
            Knee_L.transform.localEulerAngles = new Vector3(PostureDB.x_Knee_L_Angle, PostureDB.y_Knee_L_Angle, PostureDB.z_Knee_L_Angle);

            //右膝盖
            Knee_R.transform.localEulerAngles = new Vector3(PostureDB.x_Knee_R_Angle, PostureDB.y_Knee_R_Angle, PostureDB.z_Knee_R_Angle);

            //左脚踝
            Ankle_L.transform.localEulerAngles = new Vector3(PostureDB.x_Ankle_L_Angle, PostureDB.y_Ankle_L_Angle, PostureDB.z_Ankle_L_Angle);

            //右脚踝
            Ankle_R.transform.localEulerAngles = new Vector3(PostureDB.x_Ankle_R_Angle, PostureDB.y_Ankle_R_Angle, PostureDB.z_Ankle_R_Angle);
        }

        void SelectActionHUman()
        {
            //Model = FindChildGameObjectByName(_currentHuman, Model);
            Head_M = FindChildGameObjectByName(_currentHuman, "Head_M");
            Neck_M = FindChildGameObjectByName(_currentHuman, "Neck_M");
            Scapula_L = FindChildGameObjectByName(_currentHuman, "Scapula_L");
            Scapula_R1 = FindChildGameObjectByName(_currentHuman, "Scapula_R1");
            Shoulder_L = FindChildGameObjectByName(_currentHuman, "Shoulder_L");
            Shoulder_R = FindChildGameObjectByName(_currentHuman, "Shoulder_R");
            Elbow_L = FindChildGameObjectByName(_currentHuman, "Elbow_L");
            Elbow_R = FindChildGameObjectByName(_currentHuman, "Elbow_R");

            Wrist_L = FindChildGameObjectByName(_currentHuman, "Wrist_L");
            Wrist_R = FindChildGameObjectByName(_currentHuman, "Wrist_R");
            Spine1_M = FindChildGameObjectByName(_currentHuman, "Spine1_M");
            Chest_M = FindChildGameObjectByName(_currentHuman, "Chest_M");

            Hip_L = FindChildGameObjectByName(_currentHuman, "Hip_L");
            Hip_R = FindChildGameObjectByName(_currentHuman, "Hip_R");

            Knee_L = FindChildGameObjectByName(_currentHuman, "Knee_L");
            Knee_R = FindChildGameObjectByName(_currentHuman, "Knee_R");

            Ankle_L = FindChildGameObjectByName(_currentHuman, "Ankle_L");
            Ankle_R = FindChildGameObjectByName(_currentHuman, "Ankle_R");
        }


        GameObject FindChildGameObjectByName(GameObject currentGamobject, string gameObjectName)
        {

            for (int i = 0; i < currentGamobject.transform.childCount; i++)
            {
                if (currentGamobject.transform.GetChild(i).name == gameObjectName)
                {
                    return currentGamobject.transform.GetChild(i).gameObject;
                }

                GameObject tmp = FindChildGameObjectByName(currentGamobject.transform.GetChild(i).gameObject, gameObjectName);
                if (tmp != null)
                {
                    return tmp;
                }
            }
            return null;
        }
        GameObject FindChildGameObjectByName(GameObject currentGamobject, GameObject gameObject)
        {
            for (int i = 0; i < currentGamobject.transform.childCount; i++)
            {
                if (currentGamobject.transform.GetChild(i).name == gameObject.name)
                {
                    return currentGamobject.transform.GetChild(i).gameObject;
                }

                GameObject tmp = FindChildGameObjectByName(currentGamobject.transform.GetChild(i).gameObject, gameObject);
                if (tmp != null)
                {
                    return tmp;
                }
            }
            return null;
        }

        void DropdownItemSelected(Dropdown dropdown)
        {
            int index = dropdown.value;
            _currentHuman = Human[index];
            //TextBox.text = dropdown.options[index].text;
        }

        void Init()
        {
            SitDown();
            SelectActionHUman();
            PostureAngleDesign();
            ActionPosition();
        }
        /// <summary>
        /// 
        /// </summary>
        void DiSanGeRen()
        {
            //朝向
            PostureDB.x_Rotation = 0;
            PostureDB.y_Rotation = 180;
            PostureDB.z_Rotation = 0;
            //头
            PostureDB.x_HeadAngle = 0;
            PostureDB.y_HeadAngle = 0;
            PostureDB.z_HeadAngle = 0;
            //脖子
            PostureDB.x_NeckAngle = 0;
            PostureDB.y_NeckAngle = 0;
            PostureDB.z_NeckAngle = 0;
            //胸骨
            PostureDB.x_Chest_M_Angle = 10;
            PostureDB.y_Chest_M_Angle = 15;
            PostureDB.z_Chest_M_Angle = 0;
            //左肩
            PostureDB.x_Shoulder_L_Angle = -95;
            PostureDB.y_Shoulder_L_Angle = 0;
            PostureDB.z_Shoulder_L_Angle = 85;
            //右肩
            PostureDB.x_Shoulder_R_Angle = -50;
            PostureDB.y_Shoulder_R_Angle = 0;
            PostureDB.z_Shoulder_R_Angle = -42;
            //左肩胛骨
            PostureDB.x_Scapula_L_Angle = 0;
            PostureDB.y_Scapula_L_Angle = 0;
            PostureDB.z_Scapula_L_Angle = 0;
            //右肩胛骨
            PostureDB.x_Scapula_R_Angle = 0;
            PostureDB.y_Scapula_R_Angle = 0;
            PostureDB.z_Scapula_R_Angle = 0;
            //左肘
            PostureDB.x_Elbow_L_Angle = 0;
            PostureDB.y_Elbow_L_Angle = 0;
            PostureDB.z_Elbow_L_Angle = 0;
            //右肘
            PostureDB.x_Elbow_R_Angle = 0;
            PostureDB.y_Elbow_R_Angle = 0;
            PostureDB.z_Elbow_R_Angle = 0;
            //左腕
            PostureDB.x_Wrist_L_Angle = 0;
            PostureDB.y_Wrist_L_Angle = 0;
            PostureDB.z_Wrist_L_Angle = 0;
            //右腕
            PostureDB.x_Wrist_R_Angle = 0;
            PostureDB.y_Wrist_R_Angle = 0;
            PostureDB.z_Wrist_R_Angle = 0;
            //脊椎
            PostureDB.x_Spine1_M_Angle = 20;
            PostureDB.y_Spine1_M_Angle = 30;
            PostureDB.z_Spine1_M_Angle = 0;
            //左大腿
            PostureDB.x_Hip_L_Angle = 0;
            PostureDB.y_Hip_L_Angle = 0;
            PostureDB.z_Hip_L_Angle = -5;
            //右大腿
            PostureDB.x_Hip_R_Angle = 0;
            PostureDB.y_Hip_R_Angle = 0;
            PostureDB.z_Hip_R_Angle = 5;
            //左膝盖
            PostureDB.x_Knee_L_Angle = 0;
            PostureDB.y_Knee_L_Angle = 0;
            PostureDB.z_Knee_L_Angle = 0;
            //右膝盖
            PostureDB.x_Knee_R_Angle = 0;
            PostureDB.y_Knee_R_Angle = 0;
            PostureDB.z_Knee_R_Angle = 0;
            //左脚踝
            PostureDB.x_Ankle_L_Angle = 0;
            PostureDB.y_Ankle_L_Angle = 0;
            PostureDB.z_Ankle_L_Angle = 0;
            //右脚踝
            PostureDB.x_Ankle_R_Angle = 0;
            PostureDB.y_Ankle_R_Angle = 0;
            PostureDB.z_Ankle_R_Angle = 0;
        }

        /// <summary>
        /// 坐下
        /// </summary>
        void SitDown()
        {
            PostureDB.x_Rotation = 0;
            PostureDB.y_Rotation = 180;
            PostureDB.z_Rotation = 0;
            //头
            PostureDB.x_HeadAngle = 0;
            PostureDB.y_HeadAngle = 0;
            PostureDB.z_HeadAngle = 0;
            //脖子
            PostureDB.x_NeckAngle = 15;
            PostureDB.y_NeckAngle = 0;
            PostureDB.z_NeckAngle = 0;
            //胸骨
            PostureDB.x_Chest_M_Angle = 0;
            PostureDB.y_Chest_M_Angle = 0;
            PostureDB.z_Chest_M_Angle = 0;
            //左肩
            PostureDB.x_Shoulder_L_Angle = -25;
            PostureDB.y_Shoulder_L_Angle = 10;
            PostureDB.z_Shoulder_L_Angle = 75;
            //右肩
            PostureDB.x_Shoulder_R_Angle = -25;
            PostureDB.y_Shoulder_R_Angle = -10;
            PostureDB.z_Shoulder_R_Angle = -75;
            //左肩胛骨
            PostureDB.x_Scapula_L_Angle = 0;
            PostureDB.y_Scapula_L_Angle = 0;
            PostureDB.z_Scapula_L_Angle = 0;
            //右肩胛骨
            PostureDB.x_Scapula_R_Angle = 0;
            PostureDB.y_Scapula_R_Angle = 0;
            PostureDB.z_Scapula_R_Angle = 0;
            //左肘
            PostureDB.x_Elbow_L_Angle = 75;
            PostureDB.y_Elbow_L_Angle = 60;
            PostureDB.z_Elbow_L_Angle = 0;
            //右肘
            PostureDB.x_Elbow_R_Angle = 75;
            PostureDB.y_Elbow_R_Angle = -60;
            PostureDB.z_Elbow_R_Angle = 0;
            //左腕
            PostureDB.x_Wrist_L_Angle = 0;
            PostureDB.y_Wrist_L_Angle = 0;
            PostureDB.z_Wrist_L_Angle = 0;
            //右腕
            PostureDB.x_Wrist_R_Angle = 0;
            PostureDB.y_Wrist_R_Angle = 0;
            PostureDB.z_Wrist_R_Angle = 0;
            //脊椎
            PostureDB.x_Spine1_M_Angle = 5;
            PostureDB.y_Spine1_M_Angle = 0;
            PostureDB.z_Spine1_M_Angle = 0;
            //左大腿
            PostureDB.x_Hip_L_Angle = -90;
            PostureDB.y_Hip_L_Angle = 0;
            PostureDB.z_Hip_L_Angle = 0;
            //右大腿
            PostureDB.x_Hip_R_Angle = -90;
            PostureDB.y_Hip_R_Angle = 0;
            PostureDB.z_Hip_R_Angle = 0;
            //左膝盖
            PostureDB.x_Knee_L_Angle = 85;
            PostureDB.y_Knee_L_Angle = 0;
            PostureDB.z_Knee_L_Angle = 0;
            //右膝盖
            PostureDB.x_Knee_R_Angle = 85;
            PostureDB.y_Knee_R_Angle = 0;
            PostureDB.z_Knee_R_Angle = 0;
            //左脚踝
            PostureDB.x_Ankle_L_Angle = 0;
            PostureDB.y_Ankle_L_Angle = 0;
            PostureDB.z_Ankle_L_Angle = 0;
            //右脚踝
            PostureDB.x_Ankle_R_Angle = 0;
            PostureDB.y_Ankle_R_Angle = 0;
            PostureDB.z_Ankle_R_Angle = 0;
        }
    }
}