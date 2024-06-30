using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCurDZName : MonoBehaviour
{
    public GameObject TimelinePanel;
    public Text CurDZName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TimelinePanel.activeInHierarchy == true)
        {
            CurDZName.text = DZTimelineEditScript.CurDZNameText;
        }
    }
}
