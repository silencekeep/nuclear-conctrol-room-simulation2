using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keti3keshi : MonoBehaviour
{
    public GameObject yes, no;
    public Dropdown which;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ketisanKESHI()
    {
        System.Threading.Thread.Sleep(500);
        switch (which.value)
        {
            case 0:
                yes.SetActive(true);
                no.SetActive(true);
                break;
            case 1:
                yes.SetActive(true);
                no.SetActive(false);
                break;
            case 2:
                yes.SetActive(true);
                no.SetActive(false);
                break;
            case 3:
                yes.SetActive(true);
                no.SetActive(false);
                break;
            case 4:
                yes.SetActive(false);
                no.SetActive(true);
                break;
            case 5:
                yes.SetActive(false);
                no.SetActive(true);
                break;
        }
    }

}
