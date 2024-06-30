using CNCC.Models;
using SuperTreeView;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CNCC.UI
{
    public class UIHumanJointAdjustPresenter : MonoBehaviour
    {
        [SerializeField] UnityEvent onjointClick;
        [SerializeField] OnSolidChange onSolidChange;
        [SerializeField] Dropdown humanSelectDropdown;
        string str = "1";
        string selectJointed = "";

        //public List<Model> ModelsList;
        Model Currentmodel;
        Model solidControlModel;

        public Slider Slider_x;
        public Slider Slider_y;
        public Slider Slider_z;
        public Text Current_x_Value;
        public Text Current_y_Value;
        public Text Current_z_Value;
        public Text x_max;
        public Text x_min;
        public Text y_max;
        public Text y_min;
        public Text z_max;
        public Text z_min;

        [SerializeField] InputField x_Value;
        [SerializeField] InputField y_Value;
        [SerializeField] InputField z_Value;

        [SerializeField] InputField inputText_x_max;
        [SerializeField] InputField inputText_x_min;
        [SerializeField] InputField inputText_y_max;
        [SerializeField] InputField inputText_y_min;
        [SerializeField] InputField inputText_z_max;
        [SerializeField] InputField inputText_z_min;

        IJoint CurrentJoint;

        float maxValue;
        float minValue;
        float currentValue;
        // Start is called before the first frame update
        [System.Serializable]
        public class OnSolidChange : UnityEvent<float>
        {

        }

        void Start()
        {
            humanSelectDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(humanSelectDropdown); });
            // CurrentJoint = Currentmodel.GetComponent<Head>();
            inputText_x_max.onEndEdit.AddListener(delegate { LockInput(inputText_x_max); });
            inputText_x_min.onEndEdit.AddListener(delegate { LockInput(inputText_x_min); });
            inputText_y_max.onEndEdit.AddListener(delegate { LockInput(inputText_y_max); });
            inputText_y_min.onEndEdit.AddListener(delegate { LockInput(inputText_y_min); });
            inputText_z_max.onEndEdit.AddListener(delegate { LockInput(inputText_z_max); });
            inputText_z_min.onEndEdit.AddListener(delegate { LockInput(inputText_z_min); });

            x_Value.onEndEdit.AddListener(delegate { LockInputCurrent(x_Value); });
            y_Value.onEndEdit.AddListener(delegate { LockInputCurrent(y_Value); });
            z_Value.onEndEdit.AddListener(delegate { LockInputCurrent(z_Value); });
            if (humanSelectDropdown.options.Count == 0)
            {
                humanSelectDropdown.captionText.text = "";
                Currentmodel = null;
                print("没有新建的人物模型");
            }
            else
            {
                solidControlModel = Currentmodel = CNCC.Models.Model.createdModel[humanSelectDropdown.value];
            }
            DropdownItemSelected(humanSelectDropdown);
        }

        private void OnEnable()
        {
            ShowUImaxmin();
            DropdownItemSelected(humanSelectDropdown);
            UpdateSolid();
        }
        private void DropdownItemSelected(Dropdown humanSelectDropdown)
        {
            humanSelectDropdown.options.Clear();
            foreach (Model human in CNCC.Models.Model.createdModel)
            {
                humanSelectDropdown.options.Add(new Dropdown.OptionData() { text = human.Name });

            }
            if (humanSelectDropdown.options.Count == 0)
            {
                humanSelectDropdown.captionText.text = "";
                Currentmodel = null;
                print("没有新建的人物模型");
            }
            else
            {
                humanSelectDropdown.captionText.text = CNCC.Models.Model.createdModel[humanSelectDropdown.value].Name;
                Currentmodel = CNCC.Models.Model.createdModel[humanSelectDropdown.value];
            }
            //CheckJointSelected();
            CheckHumanSelected();
        }
        void CheckHumanSelected()
        {
            if (Currentmodel == null) return;
            if (selectJointed == "" || selectJointed == null) return;

            FalseAllSilderText();
            GetJoint(selectJointed);
            ShowUImaxmin();
            UpdateSolid();
            UpdateTextUI();
            //OnSolidValueChange();
            OnSoliderXChange();
            OnSoliderYChange();
            OnSoliderZChange();
        }

        public void CheckJointSelected()
        {
            if (Currentmodel == null)
            {
                return;
            }
            string current = selectJointed;
            if (!String.Equals(current, ItemScript1.JointInfo))
            {
                selectJointed = ItemScript1.JointInfo;
                //onjointClick.Invoke();
                GetJoint(selectJointed);
                ShowUImaxmin();
                UpdateSolid();
                UpdateTextUI();
                //OnSolidValueChange();
                OnSoliderXChange();
                OnSoliderYChange();
                OnSoliderZChange();
            }
        }

        // Update is called once per frame
        void Update()
        {
            CheckJointSelected();
            //ShowUImaxmin();   
        }

        public void CheckHuamnNum()
        {
            DropdownItemSelected(humanSelectDropdown);
        }

        public void GetJoint(string selectJointed)
        {
            if (selectJointed == "头")
            {
                SliderAllSetActive();
                SliderTextFalseActiveY();
                CurrentJoint = Currentmodel.GetComponent<Head>();
            }
            else if (selectJointed == "颈椎")
            {
                SliderAllSetActive();
                SliderTextFalseActiveX();
                SliderTextFalseActiveZ();
                CurrentJoint = Currentmodel.GetComponent<Neck>();
            }
            else if (selectJointed == "左肩关节")
            {
                SliderAllSetActive();
                CurrentJoint = Currentmodel.GetComponent<Shoulder_L>();
            }
            else if (selectJointed == "右肩关节")
            {
                SliderAllSetActive();
                CurrentJoint = Currentmodel.GetComponent<Shoulder_R>();
            }
            else if (selectJointed == "胸椎")
            {
                SliderAllSetActive();
                CurrentJoint = Currentmodel.GetComponent<Chest_M>();
            }
            else if (selectJointed == "左肘关节")
            {
                SliderAllSetActive();
                SliderTextFalseActiveZ();
                CurrentJoint = Currentmodel.GetComponent<Elbow_L>();
            }
            else if (selectJointed == "右肘关节")
            {
                SliderAllSetActive();
                SliderTextFalseActiveZ();
                CurrentJoint = Currentmodel.GetComponent<Elbow_R>();
            }
            else if (selectJointed == "左腕关节")
            {
                SliderAllSetActive();
                SliderTextFalseActiveX();
                CurrentJoint = Currentmodel.GetComponent<Wrist_L>();
            }
            else if (selectJointed == "右腕关节")
            {
                SliderAllSetActive();
                SliderTextFalseActiveX();
                CurrentJoint = Currentmodel.GetComponent<Wrist_R>();
            }
            else if (selectJointed == "脊椎")
            {
                SliderAllSetActive();
                CurrentJoint = Currentmodel.GetComponent<Spine1_M>();
            }
            else if (selectJointed == "左髋关节")
            {
                SliderAllSetActive();
                CurrentJoint = Currentmodel.GetComponent<Hip_L>();
            }
            else if (selectJointed == "右髋关节")
            {
                SliderAllSetActive();
                CurrentJoint = Currentmodel.GetComponent<Hip_R>();
            }
            else if (selectJointed == "左膝关节")
            {
                SliderAllSetActive();
                SliderTextFalseActiveY();
                SliderTextFalseActiveZ();
                CurrentJoint = Currentmodel.GetComponent<Knee_L>();
            }
            else if (selectJointed == "右膝关节")
            {
                SliderAllSetActive();
                SliderTextFalseActiveY();
                SliderTextFalseActiveZ();
                CurrentJoint = Currentmodel.GetComponent<Knee_R>();
            }
            else if (selectJointed == "左踝关节")
            {
                SliderAllSetActive();
                CurrentJoint = Currentmodel.GetComponent<Ankle_L>();
            }
            else if (selectJointed == "右踝关节")
            {
                SliderAllSetActive();
                CurrentJoint = Currentmodel.GetComponent<Ankle_R>();
            }
            else
            {
                FalseAllSilderText();
                CurrentJoint = null;
            }
            UpdateSolid();
        }


        private void UpdateTextUI()
        {
            if (CurrentJoint == null)
            {
                return;
            }
            Current_x_Value.text = ((Slider_x.value - 1) * (CurrentJoint.GetMaxAngle().x - CurrentJoint.GetMinAngle().x) + CurrentJoint.GetMaxAngle().x).ToString("F2");
            Current_y_Value.text = ((Slider_y.value - 1) * (CurrentJoint.GetMaxAngle().y - CurrentJoint.GetMinAngle().y) + CurrentJoint.GetMaxAngle().y).ToString("F2");
            Current_z_Value.text = ((Slider_z.value - 1) * (CurrentJoint.GetMaxAngle().z - CurrentJoint.GetMinAngle().z) + CurrentJoint.GetMaxAngle().z).ToString("F2");

            x_Value.text = ((Slider_x.value - 1) * (CurrentJoint.GetMaxAngle().x - CurrentJoint.GetMinAngle().x) + CurrentJoint.GetMaxAngle().x).ToString("F2");
            y_Value.text = ((Slider_y.value - 1) * (CurrentJoint.GetMaxAngle().y - CurrentJoint.GetMinAngle().y) + CurrentJoint.GetMaxAngle().y).ToString("F2");
            z_Value.text = ((Slider_z.value - 1) * (CurrentJoint.GetMaxAngle().z - CurrentJoint.GetMinAngle().z) + CurrentJoint.GetMaxAngle().z).ToString("F2");
        }

        //public void OnSolidValueChange()
        //{

        //    if (str != selectJointed || solidControlModel != Currentmodel)   //如果选择的人物不同也需要刷新
        //    {                
        //        str = selectJointed;
        //        solidControlModel = Currentmodel;
        //        return;
        //    }
        //    //if (currentRotateX > 180)
        //    //{
        //    //    currentRotateX = currentRotateX - 360;//这样-5度还是-5度 而不是355！
        //    //}
        //    float x = CurrentJoint.GetCurrentAngle().x;
        //    float y = CurrentJoint.GetCurrentAngle().y;
        //    float z = CurrentJoint.GetCurrentAngle().z;
        //    //print("当前滑块值: x为" + Slider_x.value + " y为" + Slider_y.value + " z为" + Slider_z.value);
        //    if (float.IsNaN(x) ||Slider_x.value == float.NaN || !Slider_x.gameObject.activeSelf)   //Slider_x.value == float.NaN||!Slider_x.gameObject.activeSelf
        //    {
        //        //x = (float)((Slider_x.value - 1) * (CurrentJoint.GetMaxAngle().x - CurrentJoint.GetMinAngle().x) + CurrentJoint.GetMaxAngle().x);
        //        x = CurrentJoint.GetCurrentAngle().x;
        //    }
        //    else
        //    {
        //        x = (float)((Slider_x.value - 1) * (CurrentJoint.GetMaxAngle().x - CurrentJoint.GetMinAngle().x) + CurrentJoint.GetMaxAngle().x);
        //    }


        //    if (float.IsNaN(y) ||Slider_y.value == float.NaN || !Slider_y.gameObject.activeSelf)
        //    {
        //        //y = (float)((Slider_y.value - 1) * (CurrentJoint.GetMaxAngle().y - CurrentJoint.GetMinAngle().y) + CurrentJoint.GetMaxAngle().y);
        //        y = CurrentJoint.GetCurrentAngle().y;
        //       // print("!Slider_z.gameObject.activeSelf :" + !Slider_z.gameObject.activeSelf);
        //    }
        //    else
        //    {
        //        y = (float)((Slider_y.value - 1) * (CurrentJoint.GetMaxAngle().y - CurrentJoint.GetMinAngle().y) + CurrentJoint.GetMaxAngle().y);
        //    }

        //    if (float.IsNaN(z)|| Slider_z.value == float.NaN || !Slider_z.gameObject.activeSelf)
        //    {
        //        //z = (float)((Slider_z.value - 1) * (CurrentJoint.GetMaxAngle().z - CurrentJoint.GetMinAngle().z) + CurrentJoint.GetMaxAngle().z);
        //        z = CurrentJoint.GetCurrentAngle().z;
        //    }
        //    else
        //    {
        //        z = (float)((Slider_z.value - 1) * (CurrentJoint.GetMaxAngle().z - CurrentJoint.GetMinAngle().z) + CurrentJoint.GetMaxAngle().z);
        //    }

        //    //print("当前传入关节角度： " + new Vector3(x, y, z) + " x为：" + x + " y为：" + y + " z为：" + z);
        //    CurrentJoint.SetCurrentAngle(Currentmodel.gameObject, x,y,z);
        //    Current_x_Value.text = ((Slider_x.value - 1) * (CurrentJoint.GetMaxAngle().x - CurrentJoint.GetMinAngle().x) + CurrentJoint.GetMaxAngle().x).ToString("F2");
        //    Current_y_Value.text = ((Slider_y.value - 1) * (CurrentJoint.GetMaxAngle().y - CurrentJoint.GetMinAngle().y) + CurrentJoint.GetMaxAngle().y).ToString("F2");
        //    Current_z_Value.text = ((Slider_z.value - 1) * (CurrentJoint.GetMaxAngle().z - CurrentJoint.GetMinAngle().z) + CurrentJoint.GetMaxAngle().z).ToString("F2");

        //    x_Value.text = ((Slider_x.value - 1) * (CurrentJoint.GetMaxAngle().x - CurrentJoint.GetMinAngle().x) + CurrentJoint.GetMaxAngle().x).ToString("F2");
        //    y_Value.text = ((Slider_y.value - 1) * (CurrentJoint.GetMaxAngle().y - CurrentJoint.GetMinAngle().y) + CurrentJoint.GetMaxAngle().y).ToString("F2");
        //    z_Value.text = ((Slider_z.value - 1) * (CurrentJoint.GetMaxAngle().z - CurrentJoint.GetMinAngle().z) + CurrentJoint.GetMaxAngle().z).ToString("F2");
        //}

        public void OnSoliderXChange()
        {
            if (CurrentJoint == null || str != selectJointed || solidControlModel != Currentmodel)   //如果选择的人物不同也需要刷新
            {
                str = selectJointed;
                solidControlModel = Currentmodel;
                return;
            }

            float x = CurrentJoint.GetCurrentAngle().x;
            if (float.IsNaN(x) || Slider_x.value == float.NaN || !Slider_x.gameObject.activeSelf)   //Slider_x.value == float.NaN||!Slider_x.gameObject.activeSelf
            {
                x = CurrentJoint.GetCurrentAngle().x;
            }
            else
            {
                x = (float)((Slider_x.value - 1) * (CurrentJoint.GetMaxAngle().x - CurrentJoint.GetMinAngle().x) + CurrentJoint.GetMaxAngle().x);
            }

            CurrentJoint.SetCurrentAngle(Currentmodel.gameObject, x, CurrentJoint.GetCurrentAngle().y, CurrentJoint.GetCurrentAngle().z);

            Current_x_Value.text = ((Slider_x.value - 1) * (CurrentJoint.GetMaxAngle().x - CurrentJoint.GetMinAngle().x) + CurrentJoint.GetMaxAngle().x).ToString("F2");
            x_Value.text = ((Slider_x.value - 1) * (CurrentJoint.GetMaxAngle().x - CurrentJoint.GetMinAngle().x) + CurrentJoint.GetMaxAngle().x).ToString("F2");
        }

        public void OnSoliderYChange()
        {
            if (CurrentJoint == null || str != selectJointed || solidControlModel != Currentmodel)   //如果选择的人物不同也需要刷新
            {
                str = selectJointed;
                solidControlModel = Currentmodel;
                return;
            }

            float y = CurrentJoint.GetCurrentAngle().y;
            if (float.IsNaN(y) || Slider_y.value == float.NaN || !Slider_y.gameObject.activeSelf)
            {
                y = CurrentJoint.GetCurrentAngle().y;
            }
            else
            {
                y = (float)((Slider_y.value - 1) * (CurrentJoint.GetMaxAngle().y - CurrentJoint.GetMinAngle().y) + CurrentJoint.GetMaxAngle().y);
            }
            CurrentJoint.SetCurrentAngle(Currentmodel.gameObject, CurrentJoint.GetCurrentAngle().x, y, CurrentJoint.GetCurrentAngle().z);

            Current_y_Value.text = ((Slider_y.value - 1) * (CurrentJoint.GetMaxAngle().y - CurrentJoint.GetMinAngle().y) + CurrentJoint.GetMaxAngle().y).ToString("F2");
            y_Value.text = ((Slider_y.value - 1) * (CurrentJoint.GetMaxAngle().y - CurrentJoint.GetMinAngle().y) + CurrentJoint.GetMaxAngle().y).ToString("F2");
        }

        public void OnSoliderZChange()
        {
            if (CurrentJoint == null || str != selectJointed || solidControlModel != Currentmodel)   //如果选择的人物不同也需要刷新
            {
                str = selectJointed;
                solidControlModel = Currentmodel;
                return;
            }

            float z = CurrentJoint.GetCurrentAngle().z;
            if (float.IsNaN(z) || Slider_z.value == float.NaN || !Slider_z.gameObject.activeSelf)
            {
                z = CurrentJoint.GetCurrentAngle().z;
            }
            else
            {
                z = (float)((Slider_z.value - 1) * (CurrentJoint.GetMaxAngle().z - CurrentJoint.GetMinAngle().z) + CurrentJoint.GetMaxAngle().z);//这句有问题
            }
            CurrentJoint.SetCurrentAngle(Currentmodel.gameObject, CurrentJoint.GetCurrentAngle().x, CurrentJoint.GetCurrentAngle().y, z);

            Current_z_Value.text = ((Slider_z.value - 1) * (CurrentJoint.GetMaxAngle().z - CurrentJoint.GetMinAngle().z) + CurrentJoint.GetMaxAngle().z).ToString("F2");
            z_Value.text = ((Slider_z.value - 1) * (CurrentJoint.GetMaxAngle().z - CurrentJoint.GetMinAngle().z) + CurrentJoint.GetMaxAngle().z).ToString("F2");
        }

        //每次修改编辑完更新一次最大最小的UI
        void ShowUImaxmin()
        {
            if (CurrentJoint == null)
            {
                return;
            }
            x_max.text = CurrentJoint.GetMaxAngle().x.ToString();
            y_max.text = CurrentJoint.GetMaxAngle().y.ToString();
            z_max.text = CurrentJoint.GetMaxAngle().z.ToString();
            x_min.text = CurrentJoint.GetMinAngle().x.ToString();
            y_min.text = CurrentJoint.GetMinAngle().y.ToString();
            z_min.text = CurrentJoint.GetMinAngle().z.ToString();

            inputText_x_max.text = CurrentJoint.GetMaxAngle().x.ToString("F2");
            inputText_x_min.text = CurrentJoint.GetMinAngle().x.ToString("F2");
            inputText_y_max.text = CurrentJoint.GetMaxAngle().y.ToString("F2");
            inputText_y_min.text = CurrentJoint.GetMinAngle().y.ToString("F2");
            inputText_z_max.text = CurrentJoint.GetMaxAngle().z.ToString("F2");
            inputText_z_min.text = CurrentJoint.GetMinAngle().z.ToString("F2");
        }


        void UpdateSolid()
        {
            if (CurrentJoint == null)
            {
                return;
            }
            Slider_x.value = (CurrentJoint.GetCurrentAngle().x - CurrentJoint.GetMaxAngle().x) / (CurrentJoint.GetMaxAngle().x - CurrentJoint.GetMinAngle().x) + 1;
            Slider_y.value = (CurrentJoint.GetCurrentAngle().y - CurrentJoint.GetMaxAngle().y) / (CurrentJoint.GetMaxAngle().y - CurrentJoint.GetMinAngle().y) + 1;
            Slider_z.value = (CurrentJoint.GetCurrentAngle().z - CurrentJoint.GetMaxAngle().z) / (CurrentJoint.GetMaxAngle().z - CurrentJoint.GetMinAngle().z) + 1;
        }

        #region UI显示与消失
        void SliderAllSetActive()
        {
            SliderTextSetActiveX();
            SliderTextSetActiveY();
            SliderTextSetActiveZ();
        }

        void SliderTextSetActiveX()
        {
            Slider_x.gameObject.SetActive(true);
            Current_x_Value.gameObject.SetActive(true);
            x_Value.gameObject.SetActive(true);
        }
        void SliderTextSetActiveY()
        {
            Slider_y.gameObject.SetActive(true);
            Current_y_Value.gameObject.SetActive(true);
            y_Value.gameObject.SetActive(true);
        }
        void SliderTextSetActiveZ()
        {
            Slider_z.gameObject.SetActive(true);
            Current_z_Value.gameObject.SetActive(true);
            z_Value.gameObject.SetActive(true);
        }

        void SliderTextFalseActiveX()
        {
            Slider_x.gameObject.SetActive(false);
            Current_x_Value.gameObject.SetActive(false);
            x_Value.gameObject.SetActive(false);
        }

        void SliderTextFalseActiveY()
        {
            Slider_y.gameObject.SetActive(false);
            Current_y_Value.gameObject.SetActive(false);
            y_Value.gameObject.SetActive(false);
        }

        void SliderTextFalseActiveZ()
        {
            Slider_z.gameObject.SetActive(false);
            Current_z_Value.gameObject.SetActive(false);
            z_Value.gameObject.SetActive(false);
        }

        void FalseAllSilderText()
        {
            SliderTextFalseActiveX();
            SliderTextFalseActiveY();
            SliderTextFalseActiveZ();
        }
        #endregion

        void LockInput(InputField input)
        {
            string str = input.text;
            if (input.text.Length > 0 && IsNumberic(input.text))
            {
                //Debug.Log("Text has been entered");
                //CurrentJoint.SetMaxAngle();
                EndEditInput_MaxMin();
                ShowUImaxmin();
                UpdateTextUI();
            }
            else if (input.text.Length == 0)
            {
                input.text = str;
            }
        }

        void LockInputCurrent(InputField input)
        {
            string str = input.text;
            if (input.text.Length > 0 && IsNumberic(input.text))
            {
                EndEditInput_Current();
                UpdateSolid();
                UpdateTextUI();

            }
            else if (input.text.Length == 0)
            {
                input.text = str;
            }
        }

        private void EndEditInput_Current()
        {
            CurrentJoint.SetCurrentAngle(Currentmodel.gameObject, new Vector3(float.Parse(x_Value.text), float.Parse(y_Value.text), float.Parse(z_Value.text)));
        }

        void EndEditInput_MaxMin()
        {
            //除法分母不为零
            CurrentJoint.SetMaxAngle(Currentmodel.gameObject, new Vector3(float.Parse(inputText_x_max.text), float.Parse(inputText_y_max.text), float.Parse(inputText_z_max.text)));
            CurrentJoint.SetMinAngle(Currentmodel.gameObject, new Vector3(float.Parse(inputText_x_min.text), float.Parse(inputText_y_min.text), float.Parse(inputText_z_min.text)));
            //print("已更新最大值");
            UpdateSolid();
        }

        public static bool IsNumberic(string str)
        {
            float vsNum;
            bool isNum;
            isNum = float.TryParse(str, System.Globalization.NumberStyles.Float,
                System.Globalization.NumberFormatInfo.InvariantInfo, out vsNum);
            return isNum;
        }

        void EnableSolider()
        {
            //Slider_x.gameObject.SetActive(false);
            //Slider_y.gameObject.SetActive(fl)
            //Slider_z
        }
    }

}
