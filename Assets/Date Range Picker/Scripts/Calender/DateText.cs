using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateText : MonoBehaviour
{
    [SerializeField] DateRangePicker m_DatePicker;
    [SerializeField] Text m_DateText;

    private void Start()
    {
        m_DatePicker.CalendersUpdated += CalenderUpdated;
    }

    public void CalenderUpdated(DateTime? selectedStartDate, DateTime? selectedEndDate)
    {
        string text = "";

        if (selectedStartDate != null)
        {
            text += selectedStartDate.Value.ToShortDateString();
        }

        if (selectedEndDate != null)
        {
            text += " - " + selectedEndDate.Value.ToShortDateString();
        }
        m_DateText.text = text;
        //自己添加
        if (text.Length > 18)
        {
            Destroy(transform.gameObject);
            GameObject query = GameObject.Find("2DUI/Canvas/JournalManagementPanel/QueryPanel/background2/GameObject/Time/inputback/startInputField (TMP)");
            query.GetComponent<TMP_Text>().text = text;
        }
    }
}
