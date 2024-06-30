using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CreatedPanelTable : MonoBehaviour
{
    public Dropdown dropdown;
    void Start()
    {
        //GameEvents.current.onSelectDrawTriggerEnter += CreatedPanelTableDropDownRefresh;
        dropdown = dropdown.transform.GetComponent<Dropdown>();
        dropdown.options.Clear();
        //PanelBarIntanstiation.CreatedPanel;

        foreach (var item in PanelBarIntanstiation.CreatedPanel)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item.name });
        }

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
        CreatedPanelTableDropDownRefresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    ///刷新列表
    public void CreatedPanelTableDropDownRefresh()
    {
        dropdown.options.Clear();// dropdown.OnSelect();
        foreach (GameObject paneltable in PanelBarIntanstiation.CreatedPanel)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = paneltable.name });
        }
    }
    int DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        return index;
    }
    //删除
    public void DeleteIntanstiationPanelTable()
    {
        CreatedPanelTableDropDownRefresh();
        int DeletePanelTableNumber = DropdownItemSelected(dropdown);
        dropdown.options.RemoveAt(DeletePanelTableNumber);        
        Destroy(PanelBarIntanstiation.CreatedPanel[DeletePanelTableNumber]);
        PanelBarIntanstiation.CreatedPanel.RemoveAt(DeletePanelTableNumber);//????
        CreatedPanelTableDropDownRefresh();
        //dropdown.RefreshShownValue();
    }
    //void OnSelect()
}
