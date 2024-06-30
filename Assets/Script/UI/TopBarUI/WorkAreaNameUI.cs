using CNCC.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CNCC.UI
{
    public class WorkAreaNameUI : MonoBehaviour
    {
        [SerializeField] Text WorkAreaName;
        [SerializeField] Text UserName;
        [SerializeField] GameObject CaseRoom;

        [SerializeField] GameObject InitUI;//后期可删
        void Start()
        {
            //SavingSystem.CurrentWorkAreaName
            InitUI.gameObject.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            WorkAreaName.text = SavingSystem.CurrentWorkAreaName;
            SavingSystem.CurrentUserName = UserName.text;
            if (SavingSystem.CurrentWorkAreaName ==string.Empty)
            {
               CaseRoom.SetActive(false);
            }
            else
            {
                CaseRoom.SetActive(true);
            }
        }

        //加载初始化
        
    }
}