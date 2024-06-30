using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zt_space_rightupperarm : MonoBehaviour
{
    public string right_upperarm;
    public List<string> right_upperarm1 = new List<string>();
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
           
            
                right_upperarm1.Add(right_upperarm);
            
        }
        if (click1.lister == 0)
        {


            right_upperarm1.Clear();

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag != "s1"&& other.tag != "Untagged" && click1.lister == 1)
        {
            right_upperarm = other.tag;
        }
        if (click1.lister == 0)
        {
            right_upperarm = " ";
        }

    }
}
