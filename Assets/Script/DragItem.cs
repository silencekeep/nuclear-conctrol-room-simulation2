using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// UI面板拖拽,本脚本挂在想要拖拽的面板即可
/// </summary>
public class DragItem : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private RectTransform dragTarget;
    private Canvas canvas;

    public void Start()
    {
        dragTarget = this.GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

    }
    public void OnDrag(PointerEventData eventData)
    {
        dragTarget.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //面板显示在最上层
        dragTarget.SetAsLastSibling();
    }

}
