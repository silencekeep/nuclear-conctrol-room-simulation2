using CNCC.Panels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CNCC.UI
{
    public class PanelSplicingUI : MonoBehaviour
    {
        [SerializeField] InputField TargetInput;
        [SerializeField] InputField ReferenceInput;
        [SerializeField] Button TargetButton;
        [SerializeField] Button ReferenceButton;
        [SerializeField] Toggle Left;
        [SerializeField] Toggle Right;
        [SerializeField] Button Yes;
        [SerializeField] Button Cancel;
        [SerializeField] Text Result;

        private GameObject TargetObject;
        private GameObject ReferenceObject;

        bool iSTargetSelect = false;
        bool isReferenceSelect = false;
        // Start is called before the first frame update
        void Start()
        {
            TargetButton.GetComponent<Button>().onClick.AddListener(TargetButtonClick);
            ReferenceButton.GetComponent<Button>().onClick.AddListener(ReferenceButtonClick);

            Yes.GetComponent<Button>().onClick.AddListener(MontagePanel);
            Cancel.GetComponent<Button>().onClick.AddListener(UICanel);

            TargetButtonClick();
            ReferenceButtonClick();
        }
        void Update()
        {
            TargetChoose();
            ReferenceChoose();
        }

        private void OnEnable()
        {
            TargetButtonClick();
            ReferenceButtonClick();
        }

        //合并:关键：所要拼接的盘台左右端点的三维坐标系指向相同
        private void MontagePanel()
        {
            //print("盘台合并");
            if (isReferenceSelect && iSTargetSelect && TargetObject.GetComponent<ISpliceable>() != null && TargetObject != ReferenceObject)
            {
                GameManager.Instance.Save();
                //bool MontageSuccess = TargetObject.GetComponent<ISpliceable>().IsSplice(ReferenceObject, IsLeftMontage());
                bool MontageSuccess = TargetObject.GetComponent<ISpliceable>().IsSplice(ReferenceObject, IsLeftMontage());
                if (!MontageSuccess)
                {
                    GameManager.Instance.StackPop();
                }
                ErrorUI(MontageSuccess);
            }
            else
            {
                print("未选择盘台或选择的盘台无法合并");
                Result.text = "未选择盘台或选择的盘台无法合并";
            }
        }

        private void ReferenceChoose()
        {
            if (!isReferenceSelect && Input.GetMouseButton(0))
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (!EventSystem.current.IsPointerOverGameObject())//判断点击目标是否为UI
                    {
                        if (hit.transform.gameObject.GetComponent<Panel>())
                        {
                            ReferenceObject = hit.transform.gameObject;
                            ReferenceInput.text = ReferenceObject.GetComponent<Panel>().ID;
                            isReferenceSelect = true;
                        }
                    }
                }
            }
        }

        private void TargetChoose()
        {
            if (!iSTargetSelect && Input.GetMouseButton(0))
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (!EventSystem.current.IsPointerOverGameObject())//判断点击目标是否为UI
                    {
                        if (hit.transform.gameObject.GetComponent<Panel>())
                        {
                            TargetObject = hit.transform.gameObject;
                            TargetInput.text = TargetObject.GetComponent<Panel>().ID;
                            iSTargetSelect = true;
                        }
                    }
                }
            }
        }

        // Update is called once per frame


        private void UICanel()
        {
            gameObject.SetActive(false);
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        void TargetButtonClick()
        {
            iSTargetSelect = false;
            TargetInput.text = "";
            Result.text = "";
        }

        void ReferenceButtonClick()
        {
            isReferenceSelect = false;
            ReferenceInput.text = "";
            Result.text = "";
        }

        //左右拼接
        bool IsLeftMontage()
        {
            if (Left.isOn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void ErrorUI(bool isSuccess)
        {
            if (!isSuccess)
            {
                print("选择的两个盘台不能合并");
                Result.text = "选择的两个盘台不能合并";
            }
            else
            {
                Result.text = "拼接成功";
            }
        }
    }
}
