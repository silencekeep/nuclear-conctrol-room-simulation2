using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CNCC.Models;
using UnityEngine.Events;
using CNCC.WorkArea;

namespace CNCC.UI
{
    public class HunmanIntanstiationUIPresenter : MonoBehaviour
    {
        // public static List<Model> createdModel = new List<Model>();

        //生成人物UI
        [SerializeField] InputField inputName;
        [SerializeField] Dropdown dropdownGender;

        [SerializeField] Button buttonYes;
        [SerializeField] Button buttonNo;

        //已生成人物UI
        [SerializeField] Dropdown createdHUmanDropDown;

        [SerializeField] Button buttonDelete;
        [SerializeField] Button buttonCancel;

        //重名判断UI
        [SerializeField] GameObject tipUI;

        //人物预制体
        [SerializeField] GameObject manPrefab;
        [SerializeField] GameObject womanPrefab;
        GameObject selectedManPrefab;
        GameObject selectedWomanPrefab;
        //生成位置
        [SerializeField] Transform spawnHumanPosition;
        [SerializeField] Transform person;

        //
        public UnityEvent OnHumanIntanstiat;
        void Start()
        {
            dropdownGender.GetComponent<Dropdown>();
            buttonYes.GetComponent<Button>().onClick.AddListener(CreatHuamnModel);
            createdHUmanDropDown.GetComponent<Dropdown>();
            //createdHUmanDropDown..AddListener(delegate { DeleteIntanstiationHuman(); });
            buttonDelete.GetComponent<Button>().onClick.AddListener(DeleteIntanstiationHuman);
            selectedManPrefab = manPrefab;
            selectedWomanPrefab = womanPrefab;
            CreatedHUmanDropDownRefresh();


        }

        private void OnEnable()
        {
            CreatedHUmanDropDownRefresh();
        }
        public void HumanListClear()
        {
            CreatedHUmanDropDownRefresh();
        }

        private void CreatHuamnModel()
        {
            //重名判断
            if (Model.IsRepeat(inputName.text.Trim()) || inputName.text.Trim().Length == 0)
            {
                print("名字重复或为空");
                tipUI.SetActive(true);
                return;
            }

            spawnHumanPosition.position = new Vector3(Wall.WallLength / 2, 0, Wall.WallWidth / 2);
            if (dropdownGender.value == 0)
            {
                if (inputName.text.Length != 0)
                {
                    GameManager.Instance.Save();
                    new Model(selectedManPrefab, person, inputName.text, "男");
                    foreach (var newmdel in Model.createdModel)
                    {
                        if (newmdel.Name == inputName.text && newmdel.gender == "男")
                        {
                            newmdel.gameObject.transform.position = spawnHumanPosition.position;
                            newmdel.gameObject.transform.eulerAngles = spawnHumanPosition.eulerAngles;
                            newmdel.gameObject.transform.localScale = spawnHumanPosition.localScale;
                        }
                    }
                }
                else
                {
                    GameManager.Instance.Save();
                    Model newModel1 = new Model(selectedManPrefab, spawnHumanPosition);
                    foreach (var newmdel in Model.createdModel)
                    {
                        if (newmdel.Name == "新增人物" + (Model.createdModel.Count + 1).ToString() && newmdel.gender == "男")
                        {
                            newmdel.gameObject.transform.position = spawnHumanPosition.position;
                            newmdel.gameObject.transform.eulerAngles = spawnHumanPosition.eulerAngles;
                            newmdel.gameObject.transform.localScale = spawnHumanPosition.localScale;
                        }
                    }
                }
            }
            else if (dropdownGender.value == 1)
            {
                if (inputName.text.Length != 0)
                {
                    GameManager.Instance.Save();
                    Model newModel1 = new Model(selectedWomanPrefab, person, inputName.text, "女");
                    foreach (var newmdel in Model.createdModel)
                    {
                        if (newmdel.Name == inputName.text && newmdel.gender == "女")
                        {
                            newmdel.gameObject.transform.position = spawnHumanPosition.position;
                            newmdel.gameObject.transform.eulerAngles = spawnHumanPosition.eulerAngles;
                            newmdel.gameObject.transform.localScale = spawnHumanPosition.localScale;
                        }
                    }
                }
                else
                {
                    GameManager.Instance.Save();
                    Model newModel1 = new Model(selectedWomanPrefab, spawnHumanPosition);
                    foreach (var newmdel in Model.createdModel)
                    {
                        if (newmdel.Name == "新增人物" + (Model.createdModel.Count + 1).ToString() && newmdel.gender == "女")
                        {
                            newmdel.gameObject.transform.position = spawnHumanPosition.position;
                            newmdel.gameObject.transform.eulerAngles = spawnHumanPosition.eulerAngles;
                            newmdel.gameObject.transform.localScale = spawnHumanPosition.localScale;
                        }
                    }
                }
            }
            CreatedHUmanDropDownRefresh();
            OnHumanIntanstiat.Invoke();
        }

        // Update is called once per frame
        void Update()
        {
            if (Model.createdModel.Count == 0)
            {
                if (createdHUmanDropDown.options.Count == 0)
                {
                    return;
                }
            }
            else if (Model.createdModel.Count != createdHUmanDropDown.options.Count)
            {
                //CreatHuamnModel();
                CreatedHUmanDropDownRefresh();
            }
        }

        public void CreatedHUmanDropDownRefresh()
        {
            createdHUmanDropDown.options.Clear();
            foreach (Model human in Model.createdModel)
            {
                createdHUmanDropDown.options.Add(new Dropdown.OptionData() { text = human.Name });

            }
            if (createdHUmanDropDown.options.Count != 0 && createdHUmanDropDown.value > createdHUmanDropDown.options.Count - 1)
            {
                createdHUmanDropDown.value = createdHUmanDropDown.value - 1;
            }
            if (createdHUmanDropDown.options.Count == 0)
            {
                createdHUmanDropDown.captionText.text = "";
            }
            else
            {
                createdHUmanDropDown.captionText.text = CNCC.Models.Model.createdModel[createdHUmanDropDown.value].Name;
            }
        }

        void DeleteIntanstiationHuman()
        {
            if (createdHUmanDropDown.options.Count == 0)
            {
                createdHUmanDropDown.captionText.text = "";
                return;
            }

            GameManager.Instance.Save();


            Destroy(CNCC.Models.Model.createdModel[createdHUmanDropDown.value].gameObject);//Destory照理说应该放到Model类当中            
            Model.createdModel.Remove(CNCC.Models.Model.createdModel[createdHUmanDropDown.value]);
            //createdHUmanDropDown.options.RemoveAt(createdHUmanDropDown.value);
            CreatedHUmanDropDownRefresh();
            OnHumanIntanstiat.Invoke();

        }
    }
}