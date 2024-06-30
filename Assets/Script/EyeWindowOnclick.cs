using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeWindowOnclick : MonoBehaviour
{
    public Dropdown dropdown;
    public Dropdown whichPerson;
    public GameObject left;
    public GameObject right;
    public GameObject left2;
    public GameObject right2;
    public GameObject left3;
    public GameObject right3;
    //public GameObject head;
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnClick()
    {
        int dr = dropdown.value;
        if (whichPerson.value == 0)
        {
            switch (dr)
            {
                case 0:
                    left.SetActive(true);
                    right.SetActive(true);
                    break;
                case 1:
                    left.SetActive(true);
                    break;
                case 2:
                    right.SetActive(true);
                    break;
            }
        }
        if (whichPerson.value == 1)
        {
            switch (dr)
            {
                case 0:
                    left2.SetActive(true);
                    right2.SetActive(true);
                    break;
                case 1:
                    left2.SetActive(true);
                    break;
                case 2:
                    right2.SetActive(true);
                    break;
            }
        }
        if (whichPerson.value == 2)
        {
            switch (dr)
            {
                case 0:
                    left3.SetActive(true);
                    right3.SetActive(true);
                    break;
                case 1:
                    left3.SetActive(true);
                    break;
                case 2:
                    right3.SetActive(true);
                    break;
            }
        }

    }
}
