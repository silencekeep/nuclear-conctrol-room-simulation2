using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TexureEdit : MonoBehaviour
{
    public GameObject Texure_Popup;
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
        
        if (!Texure_Popup.activeInHierarchy)
        {
            Texure_Popup.SetActive(true);
        }
        else
        {
            Texure_Popup.SetActive(false);
        }
       
    }
}
