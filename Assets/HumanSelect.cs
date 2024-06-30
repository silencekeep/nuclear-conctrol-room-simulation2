using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanSelect : MonoBehaviour
{
    public GameObject[] Human;
    public Dropdown dropdown;
    void Start()
    {
        dropdown = this.transform.GetComponent<Dropdown>();
        dropdown.options.Clear();
        List<string> items = new List<string>();
        for (int i = 0; i < Human.Length; i++)
        {
            items.Add(Human[i].name);
        }

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;

        //TextBox.text = dropdown.options[index].text;
    }
}
