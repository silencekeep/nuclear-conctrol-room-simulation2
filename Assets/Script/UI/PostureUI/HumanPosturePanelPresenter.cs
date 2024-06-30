using CNCC.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CNCC.UI
{
    public class HumanPosturePanelPresenter : MonoBehaviour
    {
        [SerializeField] Dropdown humanSelectDropdown;
        [SerializeField] Button standingButton;
        [SerializeField] Button sittingingButton;
        [SerializeField] Button TPoseButton;
        Model Currentmodel;        
        void Start()
        {
            humanSelectDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(humanSelectDropdown); });
            //humanSelectDropdown
            standingButton.onClick.AddListener(Stand);
            sittingingButton.onClick.AddListener(SitDown);
            TPoseButton.onClick.AddListener(TPose);
            if (humanSelectDropdown.options.Count == 0)
            {
                humanSelectDropdown.captionText.text = "";
                Currentmodel = null;
                print("没有新建的人物模型");
            }
            else
            {
                Currentmodel = CNCC.Models.Model.createdModel[humanSelectDropdown.value];
            }
            DropdownItemSelected(humanSelectDropdown);
        }

        private void OnEnable()
        {
            DropdownItemSelected(humanSelectDropdown);
        }
        void Update()
        {

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
        }

        public void CheckHuamnNum()
        {
            DropdownItemSelected(humanSelectDropdown);
        }

        #region 姿势触发
        void TPose()
        {
            //Currentmodel.SetPosutre(Currentmodel, PostureDB.PostureJointAngle(PostureDB.TPose()));
            GetPosture(PostureDB.TPose());
        }

        void Stand()
        {
            GetPosture(PostureDB.ZhanLi());
        }
        void SitDown()
        {
            GetPosture(PostureDB.SitDown());
        }

        void GetPosture(Vector3[] vector3)
        {
            if (Currentmodel == null)
            {
                return;
            }
            Currentmodel.SetPosutre(Currentmodel, PostureDB.PostureJointAngle(vector3));
        }
        #endregion
    }
}
