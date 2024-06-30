using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CNCC.UI
{
    public class SaveUIPresenter : MonoBehaviour
    {
        [SerializeField] Button Save;
        [SerializeField] Button Reset;
        SaveAllInstantiations saveAll;
        // Start is called before the first frame update
        void Start()
        {
           // Save.onClick.AddListener(SaveClick);
        }

        // Update is called once per frame
        void Update()
        {

        }

        //void SaveClick()
        //{
        //    saveAll.Save();
        //}

    }
}