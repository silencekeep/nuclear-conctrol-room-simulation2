using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessShowScript : MonoBehaviour
{
    public GameObject AnimationProcessPanel,human1,human2,human3,humanSelect;
    private bool AnimationProcessShowSign,humanSign;
    public GameObject ShowPanel;
    public GameObject HideBtn;
    public GameObject ShowBtn;
    // Start is called before the first frame update
    void Start()
    {
        AnimationProcessShowSign = false;
        humanSign = false;
        ProcessShowPanelExit();
    }

    public void ProcessShowBtnClicked()
    {        
        AnimationProcessShowSign = true;
    }
    public void ProcessShowPanelEnter()
    {
        Image Color = ShowPanel.GetComponent<Image>();
        Color.color = new Color(Color.color.r, Color.color.g, Color.color.b, 100);
        ShowBtn.SetActive(true);
    }
    public void ProcessShowPanelExit()
    {
        Image Color = ShowPanel.GetComponent<Image>();
        Color.color = new Color(Color.color.r, Color.color.g, Color.color.b, 50);
        //ShowBtn.SetActive(false);
    }
    public void ProcessHideBtnClicked()
    {
        AnimationProcessShowSign = false;
        ProcessShowPanelExit();
    }

    // Update is called once per frame
    void Update()
    {
        if (AnimationProcessShowSign == true)
        {
            AnimationProcessPanel.SetActive(true);
            human1.SetActive(false);
            human2.SetActive(false);
            human3.SetActive(false);
            humanSelect.SetActive(false);
            ShowPanel.SetActive(false);
            //ShowBtn.SetActive(false);
        }
        else if (AnimationProcessShowSign == false)
        {
            AnimationProcessPanel.SetActive(false);
            ShowPanel.SetActive(true);

        }
        
    }

    
}
