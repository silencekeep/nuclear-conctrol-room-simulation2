using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePopup : MonoBehaviour
{
    public GameObject Popup;
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

        //if (!Popup.activeInHierarchy)
        //{
        //    Popup.SetActive(true);
        //}
        //else
        //{
            Popup.SetActive(false);
        //}

    }
}
