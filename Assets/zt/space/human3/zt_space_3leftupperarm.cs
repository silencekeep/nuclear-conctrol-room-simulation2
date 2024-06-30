using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zt_space_3leftupperarm : MonoBehaviour
{
    public string left_upperarm;
   public  List<string> left_upperarm1 = new List<string>();
    zt_space_click click1;

    void Start()
    {
        click1 = FindObjectOfType<zt_space_click>();
    }

    // Update is called once per frame
    void Update()
    {
        if (click1.lister == 1)
        {
            
            
                left_upperarm1.Add(left_upperarm);
            
        }
        if (click1.lister == 0)
        {


            left_upperarm1.Clear();

        }

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag != "s3"&& other.tag != "Untagged")
        {
            left_upperarm = other.tag;
        }
        if (click1.lister == 0)
        {
            left_upperarm = " ";
        }
    }
}
