using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuExit : MonoBehaviour
{
    public GameObject menu1, menu11,menu2,menu21, menu3, menu31, menu4, menu41, menu5, menu51, menu6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void space_onClick()
    {

    }

    public void menu1_onClick()
    {
        if (!menu11.activeInHierarchy)
        {
            menu11.SetActive(true);
        }
        else
        {
            menu11.SetActive(false);
        }
        menu21.SetActive(false);
        menu31.SetActive(false);
        menu41.SetActive(false);
        menu51.SetActive(false);
    }

    public void menu2_onClick()
    {
        if (!menu21.activeInHierarchy)
        {
            menu21.SetActive(true);
        }
        else
        {
            menu21.SetActive(false);
        }
        menu11.SetActive(false);
        menu31.SetActive(false);
        menu41.SetActive(false);
        menu51.SetActive(false);
    }

    public void menu3_onClick()
    {
        if (!menu31.activeInHierarchy)
        {
            menu31.SetActive(true);
        }
        else
        {
            menu31.SetActive(false);
        }
        menu11.SetActive(false);
        menu21.SetActive(false);
        menu41.SetActive(false);
        menu51.SetActive(false);
    }

    public void menu4_onClick()
    {
        if (!menu41.activeInHierarchy)
        {
            menu41.SetActive(true);
        }
        else
        {
            menu41.SetActive(false);
        }
        menu11.SetActive(false);
        menu21.SetActive(false);
        menu31.SetActive(false);
        menu51.SetActive(false);
    }

    public void menu5_onClick()
    {
        if (!menu51.activeInHierarchy)
        {
            menu51.SetActive(true);
        }
        else
        {
            menu51.SetActive(false);
        }
        menu11.SetActive(false);
        menu21.SetActive(false);
        menu31.SetActive(false);
        menu41.SetActive(false);
    }

    public void menu6_onClick()
    {
        menu11.SetActive(false);
        menu21.SetActive(false);
        menu31.SetActive(false);
        menu41.SetActive(false);
        menu51.SetActive(false);
    }



    public void menu1small_onClick()
    {
        menu11.SetActive(false);
    }

    public void menu2small_onClick()
    {
        menu21.SetActive(false);
    }

    public void menu3small_onClick()
    {
        menu31.SetActive(false);
    }

    public void menu4small_onClick()
    {
        menu41.SetActive(false);
    }

    public void menu5small_onClick()
    {
        menu51.SetActive(false);
    }
    
  
}
