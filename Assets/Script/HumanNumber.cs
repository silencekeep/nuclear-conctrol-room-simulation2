using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanNumber : MonoBehaviour
{
    public Dropdown humanNumber;
    public static int humanNum;
    public GameObject experimentalPanel;
    // Start is called before the first frame update
    void Start()
    {
        humanNum = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
        //GameObject.Find("Canvas/Panel/Text").GetComponent<Text>().text = humanNum.ToString();
    }

    public void Btn_humanNum()
    {
        //humanNum = humanNumber.value + 1;
        experimentalPanel.SetActive(false);
       
    }

}
