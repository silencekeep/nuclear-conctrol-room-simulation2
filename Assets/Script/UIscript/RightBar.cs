using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RightBar : MonoBehaviour
{
    public Button ShowButton;
    public Button HideButton;
    public GameObject RightToolBar;
    public GameObject CreatedPanelTable;

    void Start()
    {
        ShowButton.GetComponent<Button>().onClick.AddListener(ShowButtonOnClick);
        HideButton.GetComponent<Button>().onClick.AddListener(HideButtonOnClick);
    }

    public void ShowButtonOnClick()
    {
        ShowButton.gameObject.SetActive(false);
        HideButton.gameObject.SetActive(true);
        RightToolBar.SetActive(true);
        CreatedPanelTable.SetActive(true);
    }

    public void HideButtonOnClick()
    {
        ShowButton.gameObject.SetActive(true);
        HideButton.gameObject.SetActive(false);
        RightToolBar.SetActive(false);
        CreatedPanelTable.SetActive(false);
    }

}
