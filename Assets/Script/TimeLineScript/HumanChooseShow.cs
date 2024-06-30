using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanChooseShow : MonoBehaviour
{
    public GameObject TimelinePanel;
    public GameObject DZTimelinePanel;
    public GameObject HumanText;
    public Dropdown HumanDropdown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TimelinePanel.activeInHierarchy == true)
        {
            HumanText.SetActive(false);
            HumanDropdown.gameObject.SetActive(false);
        }
        else if (DZTimelinePanel.activeInHierarchy == true)
        {
            HumanText.SetActive(true);
            HumanDropdown.gameObject.SetActive(true);
        }
    }
}
