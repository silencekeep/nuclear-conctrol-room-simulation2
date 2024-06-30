using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class post_active : MonoBehaviour
{
    //post_onclick onclick1;
    post_dropdown dropdown2;
     private  GameObject text, droplist, slider, text1, droplist1, slider1;
    void Start()
    {
       // onclick1 = FindObjectOfType<post_onclick>();
        dropdown2 = FindObjectOfType<post_dropdown>();
        text = GameObject.Find("post_text");
        droplist= GameObject.Find("post_droplist");
        slider = GameObject.Find("post_slider");
        text1 = GameObject.Find("post_text1");
        droplist1 = GameObject.Find("post_droplist");
        slider1 = GameObject.Find("post_slider1");
    }

    // Update is called once per frame
    void Update()
    {
        //if (onclick1.lister == 1)
        //{
            droplist.SetActive(true);
            if (dropdown2.a==1)
            {
                text.SetActive(true);
                
                slider.SetActive(true);
                text1.SetActive(false);
                
                slider1.SetActive(false);
            }
            if (dropdown2.a == 2)
            {
                text.SetActive(false);
                
                slider.SetActive(false);
                text1.SetActive(true);
               
                slider1.SetActive(true);
            }


        //}
        //if (onclick1.lister == 0)
        //{
        //    text.SetActive(false);
        //    droplist.SetActive(false);
        //    slider.SetActive(false);
        //    text1.SetActive(false);
        //    droplist1.SetActive(false);
        //    slider1.SetActive(false);

        //}
    }
}
