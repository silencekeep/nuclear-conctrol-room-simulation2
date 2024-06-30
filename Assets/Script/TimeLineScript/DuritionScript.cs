using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;


public class DuritionScript : MonoBehaviour ,IPointerClickHandler
{
    public UnityEvent leftClick;
   // public InputField inputField;
    /// <summary>
    ///设置动素时间实现尺寸变化(暂时不用）
    /// </summary>
    private float width;
    //private float TimeWidth;

    private void Start()
    {
        //leftClick.AddListener(new UnityAction(ButtonLeftClick));
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();


    }
    //public void ButtonLeftClick()
    //{
    //    //GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
    //    //Debug.Log(clickedBtn.name);
    //    // var obj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
    //    // Debug.Log(obj.name);
    //    if (timielinedemoscript.TimeWidthFlag==true)
    //    {
    //        //GameObject.Find("Time1").GetComponent<Text>().text 
    //        //timielinedemoscript tlds = new timielinedemoscript();
    //        //tlds.TimeWidth = Convert.ToSingle(GameObject.Find("Time1").GetComponent<InputField>().text);
    //        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(timielinedemoscript.TimeWidth, 20);
    //        transform.GetComponentInChildren < RectTransform > ().sizeDelta = new Vector2(timielinedemoscript.TimeWidth, 20);
    //        Transform  text = transform.GetChild(0);
    //        text.GetComponent<RectTransform>().sizeDelta = new Vector2(timielinedemoscript.TimeWidth, 20);
    //        timielinedemoscript.TimeWidthFlag = false;           
    //    }
    //    //if (timielinedemoscript.IsDeleted == true)
    //    //{

    //    //}

    //}
   
    private void Update()
    {
        //ButtonLeftClick();
    }
}
