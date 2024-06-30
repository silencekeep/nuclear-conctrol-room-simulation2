using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delete_option : MonoBehaviour
{
    // Start is called before the first frame update
    Dropdown.OptionData m_TempData;
    public Dropdown Drd_IPList;
  
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnClick()
    {
        int a = Drd_IPList.value;
        m_TempData = Drd_IPList.options[a];
        Drd_IPList.options.Remove(m_TempData);
        Drd_IPList.captionText.text = "";
    }
}
