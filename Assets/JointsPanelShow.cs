using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JointsPanelShow : MonoBehaviour
{
    [SerializeField] GameObject HumanJointAdjustment;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowJointPanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ShowJointPanel()
    {
        HumanJointAdjustment.SetActive(true);
    }
}
