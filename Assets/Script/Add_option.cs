using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Add_option : MonoBehaviour
{
    // Start is called before the first frame update
    Dropdown.OptionData m_TempData;
    public Dropdown Drd_IPList;
    public InputField ifd;
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

        m_TempData = new Dropdown.OptionData();
        m_TempData.text = ifd.text;
        //m_TempData.text = "新添加的节点";
        Drd_IPList.options.Add(m_TempData);
        
    }
}
