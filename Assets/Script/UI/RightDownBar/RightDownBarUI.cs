using CNCC.Models;
using CNCC.Panels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CNCC.UI
{
    public class RightDownBarUI : MonoBehaviour
    {
        [Header("左栏")]
        [SerializeField] Text TypeText;
        [SerializeField] InputField ObjName;
        [SerializeField] InputField Xposition;
        [SerializeField] InputField Yposition;
        [SerializeField] InputField Zposition;
        [SerializeField] InputField RotXposition;
        [SerializeField] InputField RotYposition;
        [SerializeField] InputField RotZposition;
        [SerializeField] InputField ScaXposition;
        [SerializeField] InputField ScaYposition;
        [SerializeField] InputField ScaZposition;
        [Header("右栏")]
        [SerializeField] GameObject Bar;
        [SerializeField] Text MonitorLeft;
        [SerializeField] Text MonitorMiddle;
        [SerializeField] Text MonitorRight;
        [SerializeField] Text StateLeft;
        [SerializeField] Text StateMiddle;
        [SerializeField] Text StateRight;
        [SerializeField] Button buttonLeft;
        [SerializeField] Button buttonMiddle;
        [SerializeField] Button buttonRight;
        [Header("标定")]
        [SerializeField] Transform Zero;
        public Slider Slider_x;
        public Slider Slider_y;
        public Slider Slider_z;
        [SerializeField] public InputField x_max;
        [SerializeField] public InputField x_min;
        [SerializeField] public InputField y_max;
        [SerializeField] public InputField y_min;
        [SerializeField] public InputField z_max;
        [SerializeField] public InputField z_min;

        public Slider RotSlider_x;
        public Slider RotSlider_y;
        public Slider RotSlider_z;
        [SerializeField] public InputField Rotx_max;
        [SerializeField] public InputField Rotx_min;
        [SerializeField] public InputField Roty_max;
        [SerializeField] public InputField Roty_min;
        [SerializeField] public InputField Rotz_max;
        [SerializeField] public InputField Rotz_min;

        public Slider ScaSlider_x;
        public Slider ScaSlider_y;
        public Slider ScaSlider_z;
        [SerializeField] public InputField Scax_max;
        [SerializeField] public InputField Scax_min;
        [SerializeField] public InputField Scay_max;
        [SerializeField] public InputField Scay_min;
        [SerializeField] public InputField Scaz_max;
        [SerializeField] public InputField Scaz_min;
        GameObject obj;
        Vector3 lastPosition;
        Vector3 lastRotation;
        Vector3 lastScale;
        public enum State
        {
            Show = 1,
            Hide = 0
        };
        public static GameObject clickObj;
        void Start()
        {
            ObjName.onEndEdit.AddListener(delegate { LockInputName(ObjName); });
            Xposition.onEndEdit.AddListener(delegate { LockInputPosition(Xposition); });
            Yposition.onEndEdit.AddListener(delegate { LockInputPosition(Yposition); });
            Zposition.onEndEdit.AddListener(delegate { LockInputPosition(Zposition); });

            RotXposition.onEndEdit.AddListener(delegate { LockInputPosition(RotXposition); });
            RotYposition.onEndEdit.AddListener(delegate { LockInputPosition(RotYposition); });
            RotZposition.onEndEdit.AddListener(delegate { LockInputPosition(RotZposition); });

            ScaXposition.onEndEdit.AddListener(delegate { LockInputPosition(ScaXposition); });
            ScaYposition.onEndEdit.AddListener(delegate { LockInputPosition(ScaYposition); });
            ScaZposition.onEndEdit.AddListener(delegate { LockInputPosition(ScaZposition); });
            //Xposition.c
            buttonLeft.onClick.AddListener(MonitorBarLeftButtonOnclick);
            buttonMiddle.onClick.AddListener(MonitorBarMiddleButtonOnclick);
            buttonRight.onClick.AddListener(MonitorBarRightButtonOnclick);

            x_max.text = "17.00";
            y_max.text = "8.00";
            z_max.text = "10.00";
            x_min.text = "5.00";
            y_min.text = "0.00";
            z_min.text = "0.00";

            Rotx_max.text = "60.00";
            Roty_max.text = "360.00";
            Rotz_max.text = "60.00";
            Rotx_min.text = "0.00";
            Roty_min.text = "0.00";
            Rotz_min.text = "-60.00";

            Scax_max.text = "3.00";
            Scay_max.text = "0.00";
            Scaz_max.text = "3.00";
            Scax_min.text = "0.00";
            Scay_min.text = "0.00";
            Scaz_min.text = "0.00";

            Slider_x.maxValue = float.Parse(x_max.text);
            Slider_y.maxValue = float.Parse(y_max.text);
            Slider_z.maxValue = float.Parse(z_max.text);
            Slider_x.minValue = float.Parse(x_min.text);
            Slider_y.minValue = float.Parse(y_min.text);
            Slider_z.minValue = float.Parse(z_min.text);

            RotSlider_x.maxValue = float.Parse(Rotx_max.text);
            RotSlider_y.maxValue = float.Parse(Roty_max.text);
            RotSlider_z.maxValue = float.Parse(Rotz_max.text);
            RotSlider_x.minValue = float.Parse(Rotx_min.text);
            RotSlider_y.minValue = float.Parse(Roty_min.text);
            RotSlider_z.minValue = float.Parse(Rotz_min.text);

            ScaSlider_x.maxValue = float.Parse(Scax_max.text);
            ScaSlider_y.maxValue = float.Parse(Scay_max.text);
            ScaSlider_z.maxValue = float.Parse(Scaz_max.text);
            ScaSlider_x.minValue = float.Parse(Scax_min.text);
            ScaSlider_y.minValue = float.Parse(Scay_min.text);
            ScaSlider_z.minValue = float.Parse(Scaz_min.text);

            Debug.Log("RDSliderUI.cs Start() called");
            Xposition.onValueChanged.AddListener(delegate { SliderAlternate(Xposition, Slider_x); });
            Yposition.onValueChanged.AddListener(delegate { SliderAlternate(Yposition, Slider_y); });
            Zposition.onValueChanged.AddListener(delegate { SliderAlternate(Zposition, Slider_z); });

            RotXposition.onValueChanged.AddListener(delegate { SliderAlternate(RotXposition, RotSlider_x); });
            RotYposition.onValueChanged.AddListener(delegate { SliderAlternate(RotYposition, RotSlider_y); });
            RotZposition.onValueChanged.AddListener(delegate { SliderAlternate(RotZposition, RotSlider_z); });

            ScaXposition.onValueChanged.AddListener(delegate { SliderAlternate(ScaXposition, ScaSlider_x); });
            ScaYposition.onValueChanged.AddListener(delegate { SliderAlternate(ScaYposition, ScaSlider_y); });
            ScaZposition.onValueChanged.AddListener(delegate { SliderAlternate(ScaZposition, ScaSlider_z); });

            x_max.onEndEdit.AddListener(delegate { Slider_x.maxValue = float.Parse(x_max.text); });
            y_max.onEndEdit.AddListener(delegate { Slider_y.maxValue = float.Parse(y_max.text); });
            z_max.onEndEdit.AddListener(delegate { Slider_z.maxValue = float.Parse(z_max.text); });
            x_min.onEndEdit.AddListener(delegate { Slider_x.minValue = float.Parse(x_min.text); });
            y_min.onEndEdit.AddListener(delegate { Slider_y.minValue = float.Parse(y_min.text); });
            z_min.onEndEdit.AddListener(delegate { Slider_z.minValue = float.Parse(z_min.text); });

            Rotx_max.onEndEdit.AddListener(delegate { RotSlider_x.maxValue = float.Parse(Rotx_max.text); });
            Roty_max.onEndEdit.AddListener(delegate { RotSlider_y.maxValue = float.Parse(Roty_max.text); });
            Rotz_max.onEndEdit.AddListener(delegate { RotSlider_z.maxValue = float.Parse(Rotz_max.text); });
            Rotx_min.onEndEdit.AddListener(delegate { RotSlider_x.minValue = float.Parse(Rotx_min.text); });
            Roty_min.onEndEdit.AddListener(delegate { RotSlider_y.minValue = float.Parse(Roty_min.text); });
            Rotz_min.onEndEdit.AddListener(delegate { RotSlider_z.minValue = float.Parse(Rotz_min.text); });

            Scax_max.onEndEdit.AddListener(delegate { ScaSlider_x.maxValue = float.Parse(Scax_max.text); });
            Scay_max.onEndEdit.AddListener(delegate { ScaSlider_y.maxValue = float.Parse(Scay_max.text); });
            Scaz_max.onEndEdit.AddListener(delegate { ScaSlider_z.maxValue = float.Parse(Scaz_max.text); });
            Scax_min.onEndEdit.AddListener(delegate { ScaSlider_x.minValue = float.Parse(Scax_min.text); });
            Scay_min.onEndEdit.AddListener(delegate { ScaSlider_y.minValue = float.Parse(Scay_min.text); });
            Scaz_min.onEndEdit.AddListener(delegate { ScaSlider_z.minValue = float.Parse(Scaz_min.text); });
            Slider_x.onValueChanged.AddListener(delegate {
                Xposition.text = Slider_x.value.ToString("F3");
                LockInputPosition(Xposition);
            });
            Slider_y.onValueChanged.AddListener(delegate {
                Yposition.text = Slider_y.value.ToString("F3");
                LockInputPosition(Yposition);
            });
            Slider_z.onValueChanged.AddListener(delegate {
                Zposition.text = Slider_z.value.ToString("F3");
                LockInputPosition(Zposition);
            });

            RotSlider_x.onValueChanged.AddListener(delegate
            {
                RotXposition.text = RotSlider_x.value.ToString("F3");
                LockInputPosition(RotXposition);
            });
            RotSlider_y.onValueChanged.AddListener(delegate
            {
                RotYposition.text = RotSlider_y.value.ToString("F3");
                LockInputPosition(RotYposition);
            });
            RotSlider_z.onValueChanged.AddListener(delegate
            {
                RotZposition.text = RotSlider_z.value.ToString("F3");
                LockInputPosition(RotZposition);
            });

            ScaSlider_x.onValueChanged.AddListener(delegate
            {
                ScaXposition.text = ScaSlider_x.value.ToString("F3");
                LockInputPosition(ScaXposition);
            });
            ScaSlider_y.onValueChanged.AddListener(delegate
            {
                ScaYposition.text = ScaSlider_y.value.ToString("F3");
                LockInputPosition(ScaYposition);
            });
            ScaSlider_z.onValueChanged.AddListener(delegate
            {
                ScaZposition.text = ScaSlider_z.value.ToString("F3");
                LockInputPosition(ScaZposition);
            });
        }
        private void SliderAlternate(InputField xpon, Slider sldr)
        {
            try
            {
                float f = float.Parse(xpon.text);
                sldr.value = f;
            }
            catch { }
        }

        //输入锁
        private void LockInputPosition(InputField positiontext)
        {
            string str = positiontext.text;
            if (obj == null)
            {
                return;
            }
            GameManager.Instance.Save();    //保存当前位置
            try
            {
                if (obj.GetComponent<Panel>())
                {

                    if (positiontext.text.Length > 0 && IsNumberic(positiontext.text))
                    {
                        float x = float.Parse(Xposition.text);
                        float y = float.Parse(Yposition.text) + obj.GetComponent<Panel>().Hcompensation(0);
                        float z = float.Parse(Zposition.text);

                        float xx = float.Parse(RotXposition.text);
                        float yy = float.Parse(RotYposition.text) + obj.GetComponent<Panel>().Hcompensation(0);
                        float zz = float.Parse(RotZposition.text);

                        Debug.Log(obj.GetComponent<Panel>().transform.rotation.eulerAngles);

                        float xs = float.Parse(ScaXposition.text);
                        float ys = float.Parse(ScaYposition.text) + obj.GetComponent<Panel>().Hcompensation(0);
                        float zs = float.Parse(ScaZposition.text);
                        //obj.GetComponent<Panel>().SetPosition(new Vector3(x, y, z));
                        obj.GetComponent<Panel>().transform.position = new Vector3(x, y, z);
                        obj.GetComponent<Panel>().transform.rotation = Quaternion.Euler(xx, yy, zz);
                        obj.GetComponent<Panel>().transform.localScale = new Vector3(xs, ys, zs);

                    }
                    else if (positiontext.text.Length == 0)
                    {
                        positiontext.text = str;
                    }
                }
                else if (obj.GetComponent<Model>())
                {
                    if (positiontext.text.Length > 0 && IsNumberic(positiontext.text))
                    {
                        float x = float.Parse(Xposition.text);
                        float y = float.Parse(Yposition.text) + obj.GetComponent<Panel>().Hcompensation(0);
                        float z = float.Parse(Zposition.text);

                        float xx = float.Parse(RotXposition.text);
                        float yy = float.Parse(RotYposition.text) + obj.GetComponent<Panel>().Hcompensation(0);
                        float zz = float.Parse(RotZposition.text);

                        float xs = float.Parse(ScaXposition.text);
                        float ys = float.Parse(ScaYposition.text) + obj.GetComponent<Panel>().Hcompensation(0);
                        float zs = float.Parse(ScaZposition.text);
                        //obj.GetComponent<Model>().SetPosition(new Vector3(x, y, z));
                        obj.GetComponent<Panel>().transform.position = new Vector3(x, y, z);
                        obj.GetComponent<Model>().transform.rotation = Quaternion.Euler(xx, yy, zz);
                        obj.GetComponent<Model>().transform.localScale = new Vector3(xs, ys, zs);

                    }
                    else if (positiontext.text.Length == 0)
                    {
                        positiontext.text = str;
                    }
                }
                UpdataPosition();
            }
            catch
            {
                //Debug.Log("坐标框的值不正确。");
            }
        }

        //设置名称
        private void LockInputName(InputField inputtext)
        {
            if (obj == null)
            {

            }
            string str = inputtext.text;
            GameManager.Instance.Save();
            if (obj.GetComponent<Panel>())
            {
                if (inputtext.text.Length > 0) //物体名称
                {
                    obj.GetComponent<Panel>().ID = str;
                }
                else if (inputtext.text.Length == 0)
                {
                    inputtext.text = str;
                }
            }
            else if (obj.GetComponent<Model>())
            {
                if (inputtext.text.Length > 0) //物体名称
                {
                    obj.GetComponent<Model>().Name = str;
                }
                else if (inputtext.text.Length == 0)
                {
                    inputtext.text = str;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            //UpdataPosition();
            ObjChoose();
            OnPositionChange();

        }

        static bool IsNumberic(string str)
        {
            float vsNum;
            bool isNum;
            isNum = float.TryParse(str, System.Globalization.NumberStyles.Float,
                System.Globalization.NumberFormatInfo.InvariantInfo, out vsNum);
            return isNum;
        }

        private void ObjChoose()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Xposition.text = Yposition.text = Zposition.text = string.Empty;

                    RotXposition.text = RotYposition.text = RotZposition.text = string.Empty;
                    ScaXposition.text = ScaYposition.text = ScaZposition.text = string.Empty;

                    if (!EventSystem.current.IsPointerOverGameObject())//判断点击目标是否为UI
                    {
                        obj = null;
                        if (hit.transform.gameObject.GetComponent<Panel>())
                        {
                            obj = hit.transform.gameObject;
                            SetTypeAndNameText("盘台", obj.GetComponent<Panel>().ID);
                            UpdataPosition();
                        }
                        else if (hit.transform.gameObject.GetComponent<Model>())
                        {
                            obj = hit.transform.gameObject;
                            SetTypeAndNameText("人", obj.GetComponent<Model>().Name);
                            UpdataPosition();
                            //print(obj.GetComponent<Model>().GetPosition().ToString("0.000"));
                            //print("y坐标差" + (obj.GetComponent<Model>().GetPosition().y - Zero.position.y).ToString("0.0000"));
                        }
                        clickObj = obj;
                        MonitorControlUI();
                    }
                    else
                    {
                        print("点击到UI");
                    }
                }
            }
        }

        private void SetTypeAndNameText(string type, string name)
        {
            TypeText.text = type;
            ObjName.text = name;
        }

        void UpdataPosition()
        {
            if (obj == null)
            {
                return;
            }
            if (obj.GetComponent<Panel>())
            {
                Xposition.text = obj.GetComponent<Panel>().GetPosition().x.ToString("F4");
                Yposition.text = (obj.GetComponent<Panel>().GetPosition().y - obj.GetComponent<Panel>().Hcompensation(0)).ToString("F4");
                Zposition.text = obj.GetComponent<Panel>().GetPosition().z.ToString("F4");

                RotXposition.text = (obj.GetComponent<Panel>().transform.rotation.eulerAngles.x).ToString("F4");
                RotYposition.text = ((obj.GetComponent<Panel>().transform.rotation.eulerAngles.y - obj.GetComponent<Panel>().Hcompensation(0))).ToString("F4");
                RotZposition.text = (obj.GetComponent<Panel>().transform.rotation.eulerAngles.z).ToString("F4");

                ScaXposition.text = obj.GetComponent<Panel>().transform.localScale.x.ToString("F4");
                ScaYposition.text = (obj.GetComponent<Panel>().transform.localScale.y - obj.GetComponent<Panel>().Hcompensation(0)).ToString("F4");
                ScaZposition.text = obj.GetComponent<Panel>().transform.localScale.z.ToString("F4");
            }
            else if (obj.GetComponent<Model>())
            {
                if (obj.GetComponent<Model>().gender == "男")
                {
                    Xposition.text = obj.GetComponent<Model>().GetPosition().x.ToString("F4");
                    //Yposition.text = (obj.GetComponent<Model>().GetPosition().y - obj.GetComponent<Model>().Hcompensation()).ToString("F4");
                    Yposition.text = (obj.GetComponent<Model>().GetPosition().y).ToString("F4");
                    Zposition.text = obj.GetComponent<Model>().GetPosition().z.ToString("F4");

                    RotXposition.text = (obj.GetComponent<Panel>().transform.rotation.eulerAngles.x).ToString("F4");
                    RotYposition.text = ((obj.GetComponent<Panel>().transform.rotation.eulerAngles.y - obj.GetComponent<Panel>().Hcompensation(0))).ToString("F4");
                    RotZposition.text = (obj.GetComponent<Panel>().transform.rotation.eulerAngles.z).ToString("F4");

                    ScaXposition.text = obj.GetComponent<Panel>().transform.localScale.x.ToString("F4");
                    ScaYposition.text = (obj.GetComponent<Panel>().transform.localScale.y - obj.GetComponent<Panel>().Hcompensation(0)).ToString("F4");
                    ScaZposition.text = obj.GetComponent<Panel>().transform.localScale.z.ToString("F4");
                }
                else if (obj.GetComponent<Model>().gender == "女")
                {
                    if (Math.Abs(obj.GetComponent<Model>().GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightUpperLeg).localRotation.x) < -70)
                    {
                        Xposition.text = obj.GetComponent<Model>().GetPosition().x.ToString("F4");
                        Yposition.text = (obj.GetComponent<Model>().GetPosition().y - 0.0309).ToString("F4");
                        Zposition.text = obj.GetComponent<Model>().GetPosition().z.ToString("F4");

                        RotXposition.text = obj.GetComponent<Panel>().transform.rotation.eulerAngles.x.ToString("F4");
                        RotYposition.text = (obj.GetComponent<Panel>().transform.rotation.eulerAngles.y - obj.GetComponent<Panel>().Hcompensation(0)).ToString("F4");
                        RotZposition.text = obj.GetComponent<Panel>().transform.rotation.eulerAngles.z.ToString("F4");

                        ScaXposition.text = obj.GetComponent<Panel>().transform.localScale.x.ToString("F4");
                        ScaYposition.text = (obj.GetComponent<Panel>().transform.localScale.y - obj.GetComponent<Panel>().Hcompensation(0)).ToString("F4");
                        ScaZposition.text = obj.GetComponent<Panel>().transform.localScale.z.ToString("F4");
                    }
                    else
                    {
                        Xposition.text = obj.GetComponent<Model>().GetPosition().x.ToString("F4");
                        Yposition.text = (obj.GetComponent<Model>().GetPosition().y).ToString("F4");
                        Zposition.text = obj.GetComponent<Model>().GetPosition().z.ToString("F4");

                        RotXposition.text = obj.GetComponent<Panel>().transform.rotation.x.ToString("F4");
                        RotYposition.text = (obj.GetComponent<Panel>().transform.rotation.y - obj.GetComponent<Panel>().Hcompensation(0)).ToString("F4");
                        RotZposition.text = obj.GetComponent<Panel>().transform.rotation.z.ToString("F4");

                        ScaXposition.text = obj.GetComponent<Panel>().transform.localScale.x.ToString("F4");
                        ScaYposition.text = (obj.GetComponent<Panel>().transform.localScale.y - obj.GetComponent<Panel>().Hcompensation(0)).ToString("F4");
                        ScaZposition.text = obj.GetComponent<Panel>().transform.localScale.z.ToString("F4");
                    }

                }

            }
        }

        void OnPositionChange()
        {
            if (obj == null)
            {
                return;
            }
            if (Math.Abs(obj.transform.position.x - lastPosition.x) > 0.0001 || Math.Abs(obj.transform.position.y - lastPosition.y) > 0.0001 || Math.Abs(obj.transform.position.z - lastPosition.z) > 0.0001)
            {
                UpdataPosition();
                lastPosition = obj.transform.position;
                lastRotation = obj.transform.rotation.eulerAngles;
                lastScale = obj.transform.localScale;
            }

        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        public void UITextClear()
        {
            TypeText.text = null;
            ObjName.text = null;
            Xposition.text = null;
            Yposition.text = null;
            Zposition.text = null;

            RotXposition.text = null;
            RotYposition.text = null;
            RotZposition.text = null;

            ScaXposition.text = null;
            ScaYposition.text = null;
            ScaZposition.text = null;
        }

        void DeleteKeyDown()
        {
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                if (obj == null)
                {
                    return;
                }
                if (obj.GetComponent<Panel>())
                {
                    obj.GetComponent<Panel>().DeletePanel(obj.GetComponent<Panel>());

                }
                else if (obj.GetComponent<Model>())
                {

                }
            }
        }

        void MonitorControlUI()
        {
            if (obj == null) return;
            if (obj.GetComponent<OWP_three>())
            {
                Bar.gameObject.SetActive(true);
                MonitorControlUIShowHide(1);
                StateLeft.text = StateJudge(obj.GetComponent<OWP_three>().monitorLeft);
                StateMiddle.text = StateJudge(obj.GetComponent<OWP_three>().monitorMiddle);
                StateRight.text = StateJudge(obj.GetComponent<OWP_three>().monitorRight);
                buttonLeft.GetComponentInChildren<Text>().text = InvertStateJudge(obj.GetComponent<OWP_three>().monitorLeft);
                buttonMiddle.GetComponentInChildren<Text>().text = InvertStateJudge(obj.GetComponent<OWP_three>().monitorMiddle);
                buttonRight.GetComponentInChildren<Text>().text = InvertStateJudge(obj.GetComponent<OWP_three>().monitorRight);
            }
            else if (obj.GetComponent<OWP_two>())
            {
                Bar.gameObject.SetActive(true);
                MonitorControlUIShowHide(2);
                StateLeft.text = StateJudge(obj.GetComponent<OWP_two>().monitorLeft);
                StateRight.text = StateJudge(obj.GetComponent<OWP_two>().monitorRight);
                buttonLeft.GetComponentInChildren<Text>().text = InvertStateJudge(obj.GetComponent<OWP_two>().monitorLeft);
                buttonRight.GetComponentInChildren<Text>().text = InvertStateJudge(obj.GetComponent<OWP_two>().monitorRight);
            }
            else if (obj.GetComponent<OWP_corner>())
            {
                Bar.gameObject.SetActive(true);
                MonitorControlUIShowHide(3);
                StateMiddle.text = StateJudge(obj.GetComponent<OWP_corner>().monitorMiddle);
                buttonMiddle.GetComponentInChildren<Text>().text = InvertStateJudge(obj.GetComponent<OWP_corner>().monitorMiddle);
            }
            else
            {
                Bar.gameObject.SetActive(false);
            }
        }

        void MonitorControlUIShowHide(int i)
        {
            switch (i)
            {
                case 1: //三平台
                    MonitorLeft.gameObject.SetActive(true);
                    MonitorMiddle.gameObject.SetActive(true);
                    MonitorRight.gameObject.SetActive(true);
                    StateLeft.gameObject.SetActive(true);
                    StateMiddle.gameObject.SetActive(true);
                    StateRight.gameObject.SetActive(true);
                    buttonLeft.gameObject.SetActive(true);
                    buttonMiddle.gameObject.SetActive(true);
                    buttonRight.gameObject.SetActive(true);

                    break;
                case 2: //两平台
                    MonitorLeft.gameObject.SetActive(true);
                    MonitorMiddle.gameObject.SetActive(false);
                    MonitorRight.gameObject.SetActive(true);
                    StateLeft.gameObject.SetActive(true);
                    StateMiddle.gameObject.SetActive(false);
                    StateRight.gameObject.SetActive(true);
                    buttonLeft.gameObject.SetActive(true);
                    buttonMiddle.gameObject.SetActive(false);
                    buttonRight.gameObject.SetActive(true);
                    break;
                case 3: //拐角台
                    MonitorLeft.gameObject.SetActive(false);
                    MonitorMiddle.gameObject.SetActive(true);
                    MonitorRight.gameObject.SetActive(false);
                    StateLeft.gameObject.SetActive(false);
                    StateMiddle.gameObject.SetActive(true);
                    StateRight.gameObject.SetActive(false);
                    buttonLeft.gameObject.SetActive(false);
                    buttonMiddle.gameObject.SetActive(true);
                    buttonRight.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }

        }

        void MonitorBarLeftButtonOnclick()
        {
            if (obj == null) return;
            if (obj.GetComponent<OWP_three>())
            {
                obj.GetComponent<OWP_three>().monitorLeft.SetActive(!obj.GetComponent<OWP_three>().monitorLeft.activeInHierarchy);
            }
            else if (obj.GetComponent<OWP_two>())
            {
                obj.GetComponent<OWP_two>().monitorLeft.SetActive(!obj.GetComponent<OWP_two>().monitorLeft.activeInHierarchy);
            }
            else
            {
                return;
            }
            MonitorControlUI();
        }
        void MonitorBarMiddleButtonOnclick()
        {
            if (obj == null) return;
            if (obj.GetComponent<OWP_three>())
            {
                obj.GetComponent<OWP_three>().monitorMiddle.SetActive(!obj.GetComponent<OWP_three>().monitorMiddle.activeInHierarchy);
            }
            else if (obj.GetComponent<OWP_corner>())
            {
                obj.GetComponent<OWP_corner>().monitorMiddle.SetActive(!obj.GetComponent<OWP_corner>().monitorMiddle.activeInHierarchy);
            }
            else
            {
                return;
            }
            MonitorControlUI();
        }
        void MonitorBarRightButtonOnclick()
        {
            if (obj == null) return;
            if (obj.GetComponent<OWP_three>())
            {
                obj.GetComponent<OWP_three>().monitorRight.SetActive(!obj.GetComponent<OWP_three>().monitorRight.activeInHierarchy);
            }
            else if (obj.GetComponent<OWP_two>())
            {
                obj.GetComponent<OWP_two>().monitorRight.SetActive(!obj.GetComponent<OWP_two>().monitorRight.activeInHierarchy);
            }
            else
            {
                return;
            }
            MonitorControlUI();
        }

        string StateJudge(GameObject obj)
        {
            if (obj.activeInHierarchy)
            {
                return "显示";
            }
            return "隐藏";
        }

        string InvertStateJudge(GameObject obj)
        {
            if (obj.activeInHierarchy)
            {
                return "隐藏";
            }
            return "显示";
        }


    }


}

