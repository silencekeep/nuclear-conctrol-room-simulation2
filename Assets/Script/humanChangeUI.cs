using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class humanChangeUI : MonoBehaviour
{
    public GameObject humanChange1, humanChange2, humanChange3;
    public Dropdown humanSelect;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HumanSelectMethod()
    {
        //humanChange2.SetActive(false);
        //humanChange3.SetActive(false);
        if (humanSelect.value == 0)
        {
            humanChange2.SetActive(false);
            humanChange3.SetActive(false);
            humanChange1.SetActive(true);
        }
        else
        {
            if (humanSelect.value == 1)
            {
                humanChange1.SetActive(false);
                humanChange3.SetActive(false);
                humanChange2.SetActive(true);
            }
            else
            {
                humanChange1.SetActive(false);
                humanChange2.SetActive(false);
                humanChange3.SetActive(true);
            }
        }


    }
}
