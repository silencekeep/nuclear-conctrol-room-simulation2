using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryExample.UI
{
    public class ShowHideUI_Key : MonoBehaviour
    {
        [SerializeField] KeyCode toggleKey = KeyCode.Escape;
        [SerializeField] GameObject[] uiContainer = null;

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < uiContainer.Length; i++)
            {
                uiContainer[i].SetActive(false);
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                for (int i = 0; i < uiContainer.Length; i++)
                {
                    uiContainer[i].SetActive(!uiContainer[i].activeSelf);
                }
               
            }
        }
    }
}