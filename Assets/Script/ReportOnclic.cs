using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportOnclic : MonoBehaviour
{
    public GameObject Report_Popup;
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnClick()
    {

        if (!Report_Popup.activeInHierarchy)
        {
            Report_Popup.SetActive(true);
        }
        else
        {
            Report_Popup.SetActive(false);
        }

    }

}
