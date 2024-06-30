using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zt_comfort_open2 : MonoBehaviour
{
    zt_comfort_open1 open1;
    public GameObject comfort;
   
    zt_comfort_click2 click;
    public int p = 0;
    zt_comfort comfort1;
    zt_comfort5 comfort5;
    zt_comfort2 comfort2;
    zt_comfort_all all;
    
    void Start()
    {
        open1 = FindObjectOfType<zt_comfort_open1>();

        comfort.SetActive(false);
        
        click = FindObjectOfType<zt_comfort_click2>();
        comfort1 = FindObjectOfType<zt_comfort>();
        comfort5 = FindObjectOfType<zt_comfort5>();
        comfort2 = FindObjectOfType<zt_comfort2>();
        all = FindObjectOfType<zt_comfort_all>();
    }

    // Update is called once per frame
    void Update()
    {
        if (open1.lister_comfort == 1)
        {
            
                comfort.SetActive(true);
                
           
                open1.lister_comfort = 0;
                p = 1;
            comfort1.txt11.GetComponent<Text>().text = " ";
            GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h4/TXT13").GetComponent<Image>().color = new Color((176 / 255f), (176 / 255f), (176 / 255f), (255 / 255f));
            GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT22").GetComponent<Text>().text = " ";
            GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h8/TXT23").GetComponent<Image>().color = new Color((176 / 255f), (176 / 255f), (176/ 255f), (255/ 255f));
            comfort5.txt16.GetComponent<Text>().text = " ";
            GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h6/TXT17").GetComponent<Image>().color = new Color((176 / 255f), (176 / 255f), (176/ 255f), (255 / 255f));
            comfort2.txt12.GetComponent<Text>().text = " ";
            GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h5/TXT14").GetComponent<Image>().color = new Color((176 / 255f), (176 / 255f), (176/ 255f), (255 / 255f));
            all.txt25.GetComponent<Text>().text = " ";
            GameObject.Find("zt_comfort_panel/Canvas/Panel/v1/h9/TXT26").GetComponent<Image>().color = new Color((176 / 255f), (176 / 255f), (176 / 255f), (255 / 255f));
            GameObject.Find("report").GetComponent<Text>().text = " ";
            GameObject.Find("jieguo_text").GetComponent<Text>().text = " ";

        }
        if (open1.lister_comfort1 == 1)
        {
            comfort.SetActive(false);
          
            open1.lister_comfort1 = 0;
            click.lister1 = 0;
            p = 0;
        }
    }
}
