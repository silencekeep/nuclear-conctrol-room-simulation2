using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// 这个脚本用于点击空白区域  干掉或隐藏此物体(或者其他方法  需自己传入回调方法)
/// </summary>
public class Blocker : MonoBehaviour
{
    //Canvas通过它接受点击事件  官网介绍   https://docs.unity3d.com/Manual/script-GraphicRaycaster.html
    public GraphicRaycaster RaycastInCanvas;
    //如果它之下还有多个嵌套 比如 点击A，出现界面PanelA  PanelA中有三个按钮 B  C  D 分别对应PanelB  PanelC  PanelD  这三个Panel也同样添加了此脚本且有自己的Canvas  当点击这个三个Panel时  为了不让PanelA被隐藏或干掉 需要把这三个Panel拖入此List中
    public List<Blocker> blockers;
    EventSystem eventSystem;

    //为了避面判断下级时 多执行一次回调方法
    bool state;

    void Start()
    {
        eventSystem = EventSystem.current;
        if (RaycastInCanvas == null)
            RaycastInCanvas = GetComponentInParent<GraphicRaycaster>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            state = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (state)
                CheckGuiRaycastObjects();
        }
    }

    Callback callback;
    //public void Callback callback()
    /// <summary>
    /// 注册点击空白区域时的事件（比如  隐藏  干掉   需自己传入执行方法）
    /// </summary>
    /// <param name="callback"></param>
    public void RegisterEvent(Callback callback)
    {
        this.callback = callback;
    }

    /// <summary>
    /// 是否点击在空白区域
    /// </summary>
    /// <returns></returns>
    public bool CheckGuiRaycastObjects()
    {
        //当执行了一次之后  本次鼠标点击就不再执行此方法
        state = false;

        //获取到所有鼠标射线检测到的UI
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;
        List<RaycastResult> list = new List<RaycastResult>();
        RaycastInCanvas.Raycast(eventData, list);
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                GameObject go = list[i].gameObject;
                if (go.GetComponentInParent<Blocker>() != null)
                {
                    return false;
                }
            }
        }
        //递归调用子级 看是否点击在空白区域
        if (blockers != null)
        {
            foreach (var item in blockers)
            {
                if (item.gameObject.activeSelf && !item.CheckGuiRaycastObjects())
                {
                    return false;
                }
            }
        }
        //callback?.Invoke();
        return true;
    }

}