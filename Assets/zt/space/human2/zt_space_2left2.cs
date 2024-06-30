using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zt_space_2left2 : MonoBehaviour
{
    public int left2=0;
    zt_space_click click1;
    void Start()
    {
        click1 = FindObjectOfType<zt_space_click>();
    }

    // Update is called once per frame
    void Update()
    {
        if (click1.lister == 0)
        {
            left2 = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
                if (other.tag != "s2" && other.tag != "Untagged" && click1.lister == 1)
                {
                    
                            left2 = 1;
                   
                 }
                        
        
       


    }
}
