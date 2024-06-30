using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CadImport : MonoBehaviour
{
    public GameObject Import_Popup;
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

        if (!Import_Popup.activeInHierarchy)
        {
            Import_Popup.SetActive(true);
        }
        else
        {
            Import_Popup.SetActive(false);
        }

    }
}
