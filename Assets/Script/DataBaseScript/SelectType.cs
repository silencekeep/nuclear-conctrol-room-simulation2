using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectType : MonoBehaviour
{
    public GameObject MainPanel, MainPanel2;
    public Dropdown posSelect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectMethod()
    {
        if (posSelect.value == 0)
        {
            MainPanel.SetActive(true);
            MainPanel2.SetActive(false);
            
        }
        else
        {
            if (posSelect.value == 1)
            {
                MainPanel2.SetActive(true);
                MainPanel.SetActive(false);
             
            }
        }
    }
}
