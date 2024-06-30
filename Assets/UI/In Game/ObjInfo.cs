using CNCC.Models;
using CNCC.Panels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CNCC.UI
{
    public class ObjInfo : MonoBehaviour
    {
        [SerializeField] GameObject obj;
        [SerializeField] Text InGameText;
        // Start is called before the first frame update
        void Start()
        {
            if (obj.GetComponent<Model>())
            {
                InGameText.text = obj.GetComponent<Model>().Name;
            }
            else if (obj.GetComponent<Panel>())
            {
                InGameText.text = obj.GetComponent<Panel>().ID;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}