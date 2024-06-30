using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;


namespace CNCC.Models
{
    public class HumanChange : MonoBehaviour
    {
        private bool target;
        public static double seegleFloat = 0;//to 张政
        public InputField targetName;
        public GameObject human, humanLeftArm_Man, humanLeftArm1_Man, humanRightArm_Man, humanRightArm1_Man, humanLeftHand_Man, humanRightHand_Man,
                          humanLeftArm_Female, humanLeftArm1_Female, humanRightArm_Female, humanRightArm1_Female, humanLeftHand_Female, humanRightHand_Female;
        public GameObject HumanDataPanel;
        public Slider heightSlider, armSlider, handSlider, seegleSlider;
        public Dropdown humanSelect;
        //public Dropdown CreatedHUmanDropDown;
        public Text factorText, HeigtMinText, HeigtMaxText;
        private Vector3 heightValue, armValue, handValue, seegleValue;
        public float height1, height2, height3, arm1, arm2, arm3, hand1, hand2, hand3, height, arm, hand, factorMin, factorMax;
        //string humanName;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            ////下拉菜单赋值
            //if (Model.createdModel.Count == 0)
            //{
            //    if (CreatedHUmanDropDown.options.Count == 0)
            //    {
            //        return;
            //    }
            //}
            //else if (Model.createdModel.Count != CreatedHUmanDropDown.options.Count)
            //{
            //    CreatedHUmanDropDownRefresh();
            //}
            //humanName = CreatedHUmanDropDown.options[CreatedHUmanDropDown.value].text;

            //鼠标点击检测
            if (target && Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                    if (hit.transform.gameObject.tag.Equals("human"))
                    {
                        {
                            targetName.text = hit.transform.name;
                            human = hit.transform.gameObject;
                        }
                    }
                target = false;

            }
        }


        public void HeightSliderChange()
        {
            //if (humanSelect.value == 0)
            //{
            //    //human = GameObject.Find("Worker_Man(Clone)");
            //    //human.transform.localPosition = new Vector3(12.454f, -0.4f, 4.11f);
            //}
            //else
            //{
            //    //human = GameObject.Find("Worker_Woman(Clone)");
            //}

            heightValue = new Vector3(heightSlider.value, heightSlider.value, heightSlider.value);

            seegleSlider.value = heightSlider.value;
            human.transform.localScale = heightValue;
        }

        public void ArmSliderChange()
        {           

            if (humanSelect.value == 0)//选了男性
            {
                //human = GameObject.Find("Male 4");
                humanLeftArm_Man = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftUpperArm).gameObject;
                humanRightArm_Man = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightUpperArm).gameObject;
                humanLeftArm1_Man = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftLowerArm).gameObject;
                humanRightArm1_Man = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightLowerArm).gameObject;
                //human = GameObject.Find("人物1");
                humanLeftArm_Man.transform.localScale = armValue;
                humanRightArm_Man.transform.localScale = armValue;
                humanLeftArm1_Man.transform.localScale = armValue;
                humanRightArm1_Man.transform.localScale = armValue;
            }
            else//选了女性
            {
                //human = GameObject.Find("Female 4");
                humanLeftArm_Female = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftUpperArm).gameObject;
                humanRightArm_Female = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightUpperArm).gameObject;
                humanLeftArm1_Female = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftLowerArm).gameObject;
                humanRightArm1_Female = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightLowerArm).gameObject;
                //human = GameObject.Find("Female 4");
                humanLeftArm_Female.transform.localScale = armValue;
                humanRightArm_Female.transform.localScale = armValue;
                humanLeftArm1_Female.transform.localScale = armValue;
                humanRightArm1_Female.transform.localScale = armValue;
            }
            //armValue = new Vector3(armSlider.value, armSlider.value, armSlider.value);
            //humanLeftArm.transform.localScale = armValue;
            //humanRightArm.transform.localScale = armValue;
            //humanLeftArm1.transform.localScale = armValue;
            //humanRightArm1.transform.localScale = armValue;
            armValue = new Vector3(armSlider.value, armSlider.value, armSlider.value);
        }

        public void HandSliderChange()
        {            
            if (humanSelect.value == 0)
            {
                //human = GameObject.Find("Male 4");
                humanLeftHand_Man = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftHand).gameObject;
                humanRightHand_Man = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand).gameObject;
                //human = GameObject.Find("人物1");
                humanLeftHand_Man.transform.localScale = handValue;
                humanRightHand_Man.transform.localScale = handValue;
            }
            else
            {
                //human = GameObject.Find("Female 4");
                humanLeftHand_Female = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftHand).gameObject;
                humanRightHand_Female = human.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand).gameObject;
                //human = GameObject.Find("Female 4");
                humanLeftHand_Female.transform.localScale = handValue;
                humanRightHand_Female.transform.localScale = handValue;
            }
            //handValue = new Vector3(handSlider.value, handSlider.value, handSlider.value);
            //humanLeftHand.transform.localScale = handValue;
            //humanRightHand.transform.localScale = handValue;
            handValue = new Vector3(handSlider.value, handSlider.value, handSlider.value);
        }

        public void SeegleSliderChange()
        {

            //if (humanSelect.value == 0)
            //{
            //    human = GameObject.Find("人物1");
            //}
            //else
            //{
            //    human = GameObject.Find("Female 4");
            //}
            seegleValue = new Vector3(seegleSlider.value, seegleSlider.value, seegleSlider.value);
            heightSlider.value = seegleSlider.value;
            human.transform.localScale = seegleValue;
            
            if (PostureDB.humanStand == true)
            {
                seegleFloat = 100 * heightSlider.value + 59;
            }
            else
            {
                seegleFloat = 120 * heightSlider.value - 3;
            }
            double numTwice = Math.Round(seegleFloat * 2, 0);
            double num = numTwice / 2;
            string result = num.ToString("0.0");


            if (humanSelect.value == 0)
            {
                GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/SeegleNowText").GetComponent<Text>().text = result;
            }
            else
            {
                GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/SeegleNowText").GetComponent<Text>().text = result;
            }

        }

        public void SliderChange()
        {
            //if (aText.text!="" || bText.text != "")
            //{
            //    a = float.Parse(aText.text);
            //    b = float.Parse(bText.text);
            //}
            //else
            //{
            //    a = 100;
            //    b = 70;
            //}

            if (factorText.text != "")
            {
                heightSlider.minValue = 1 - float.Parse(factorText.text);
                heightSlider.maxValue = 1 + float.Parse(factorText.text);
                seegleSlider.minValue = 1 - float.Parse(factorText.text);
                seegleSlider.maxValue = 1 + float.Parse(factorText.text);
            }
            else
            {
                heightSlider.minValue = 0.9f;
                heightSlider.maxValue = 1.1f;
                seegleSlider.minValue = 0.9f;
                seegleSlider.maxValue = 1.1f;
            }

            //身高Canvas/
            double heightFloat = 100 * heightSlider.value + 70;
            double numTwice = Math.Round(heightFloat * 2, 0);
            double num = numTwice / 2;
            string result = num.ToString("0.0");
            string HeightMinValue = Convert.ToString(100 * heightSlider.minValue + 70);
            string HeightMaxValue = Convert.ToString(100 * heightSlider.maxValue + 70);


            //臂长
            double armLengthDouble = 50 * heightSlider.value + 6;
            double armLengthnum = armSlider.value;
            armLengthnum = 100 * armLengthnum + armLengthDouble - 100;
            double armLengthTwice = Math.Round(armLengthnum * 2, 0);
            armLengthnum = armLengthTwice / 2;
            string armLengthStr = armLengthnum.ToString("0.0");
            armLengthDouble = Math.Round(armLengthDouble * 2, 0) / 2;
            double armLengthMin = armLengthDouble - 5;
            double armLengthMax = armLengthDouble + 5;
            string ArmLenghtMinValue = armLengthMin.ToString("0.0");
            string ArmLenghtMaxValue = armLengthMax.ToString("0.0");

            //手长
            double handsSizeDouble = 15 * heightSlider.value + 9;
            double handsSizenum = handSlider.value;
            handsSizenum = 5 * handsSizenum + handsSizeDouble - 10;
            double handsSizeTwice = Math.Round(handsSizenum * 2, 0);
            handsSizenum = handsSizeTwice / 2;
            handsSizeDouble = Math.Round(handsSizeDouble * 2, 0) / 2 - 5;
            double handsSizeMin = handsSizeDouble - 1.5;
            double handsSizeMax = handsSizeDouble + 1.5;
            String HandsSizeStr = handsSizenum.ToString("0.0");
            String HandsSizeMinValue = handsSizeMin.ToString("0.0");
            String HandsSizeMaxValue = handsSizeMax.ToString("0.0");

            //视高随姿势变化而改变
            double seeSizeMin = 149;
            double seeSizeMax = 169;
            if (PostureDB.humanStand == false)
            {
                seeSizeMin = 120 * heightSlider.minValue - 3;
                seeSizeMax = 120 * heightSlider.maxValue - 3;

            }
            else
            {
                seeSizeMin = 100 * heightSlider.minValue + 59;
                seeSizeMax = 100 * heightSlider.maxValue + 59;

            }
            String seeSizeMinValue = seeSizeMin.ToString("0.0");
            String seeSizeMaxValue = seeSizeMax.ToString("0.0");

            if (humanSelect.value == 0)
            {
                GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HeigtNowText").GetComponent<Text>().text = result;
                GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/ArmNowText").GetComponent<Text>().text = armLengthStr;
                GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/ArmMinText").GetComponent<Text>().text = ArmLenghtMinValue;
                GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/ArmMaxText").GetComponent<Text>().text = ArmLenghtMaxValue;
                GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HandNowText").GetComponent<Text>().text = HandsSizeStr;
                GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HandMinText").GetComponent<Text>().text = HandsSizeMinValue;
                GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HandMaxText").GetComponent<Text>().text = HandsSizeMaxValue;
                GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/SeegleMinText").GetComponent<Text>().text = seeSizeMinValue;
                GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/SeegleMaxText").GetComponent<Text>().text = seeSizeMaxValue;
                //GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HeigtMinText").GetComponent<Text>().text = HeightMinValue;
                //GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HeigtMaxText").GetComponent<Text>().text = HeightMaxValue;
                HeigtMinText.text = HeightMinValue;
                HeigtMaxText.text = HeightMaxValue;

            }
            else
            {
                if (humanSelect.value == 1)
                {
                    GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HeigtNowText").GetComponent<Text>().text = result;
                    GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/ArmNowText").GetComponent<Text>().text = armLengthStr;
                    GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/ArmMinText").GetComponent<Text>().text = ArmLenghtMinValue;
                    GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/ArmMaxText").GetComponent<Text>().text = ArmLenghtMaxValue;
                    GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HandNowText").GetComponent<Text>().text = HandsSizeStr;
                    GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HandMinText").GetComponent<Text>().text = HandsSizeMinValue;
                    GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HandMaxText").GetComponent<Text>().text = HandsSizeMaxValue;
                    GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/SeegleMinText").GetComponent<Text>().text = seeSizeMinValue;
                    GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/SeegleMaxText").GetComponent<Text>().text = seeSizeMaxValue;
                    //GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HeigtMinText").GetComponent<Text>().text = HeightMinValue;
                    //GameObject.Find("Canvas/HumanDataPanel/HumanModerChange/HeigtMaxText").GetComponent<Text>().text = HeightMaxValue;
                    HeigtMinText.text = HeightMinValue;
                    HeigtMaxText.text = HeightMaxValue;
                }

                height = heightSlider.value;
                arm = armSlider.value;
                hand = handSlider.value;
            }
        }


        public void HumanOkCloseImageButtonClicked()
        {
            HumanDataPanel.SetActive(false);

        }

        //public void CreatedHUmanDropDownRefresh()
        //{
        //    CreatedHUmanDropDown.options.Clear();
        //    foreach (Model human in Model.createdModel)
        //    {
        //        CreatedHUmanDropDown.options.Add(new Dropdown.OptionData() { text = human.Name });

        //    }
        //    if (CreatedHUmanDropDown.options.Count != 0 && CreatedHUmanDropDown.value > CreatedHUmanDropDown.options.Count - 1)
        //    {
        //        CreatedHUmanDropDown.value = CreatedHUmanDropDown.value - 1;
        //    }
        //    if (CreatedHUmanDropDown.options.Count == 0)
        //    {
        //        CreatedHUmanDropDown.captionText.text = "";
        //    }
        //    else
        //    {
        //        CreatedHUmanDropDown.captionText.text = Model.createdModel[CreatedHUmanDropDown.value].Name;
        //    }
        //}

        public void targetChoose()
        {
            target = true;
        }
    }
}
