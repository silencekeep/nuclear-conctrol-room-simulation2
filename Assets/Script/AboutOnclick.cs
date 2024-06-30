using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutOnclick : MonoBehaviour
{
    public GameObject About_Popup;
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
        if (!About_Popup.activeInHierarchy)
        {
            About_Popup.SetActive(true);
        }
        else
        {
            About_Popup.SetActive(false);
        }
    }
}
