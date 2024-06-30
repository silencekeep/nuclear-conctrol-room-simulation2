using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zt_space_2leftForearm : MonoBehaviour
{
    public string left_Forearm;
    public List<string> left_Forearm1 = new List<string>();
    zt_space_click click1;
    void Start()
    {
        click1 = FindObjectOfType<zt_space_click>();
    }

    // Update is called once per frame
    void Update()
    {
        if(click1.lister==1)
        {
            
            
                left_Forearm1.Add(left_Forearm);
            
        }

        if (click1.lister == 0)
        {


            left_Forearm1.Clear();

        }




    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag != "s2"&& other.tag != "Untagged"&& click1.lister == 1)
        {
            left_Forearm = other.tag;
        }
        if (click1.lister == 0)
        {
            left_Forearm = " ";
        }

    }
}
