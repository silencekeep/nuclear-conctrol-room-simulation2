using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActImageContent : MonoBehaviour
{
    public GameObject dragableImage;
    public GameObject emptyImage;
    private GameObject e_Button;
    private void Start()
    {        
        e_Button = Instantiate(emptyImage, transform);
        e_Button.GetComponent<Outline>().enabled = false;
        e_Button.SetActive(false);

    }

    public void SetEmpty(int index)
    {
        e_Button.transform.SetSiblingIndex(index);       
        e_Button.SetActive(true);
    }
    public void DeActivedEmpty()
    {
        e_Button.SetActive(false);
    }
}
