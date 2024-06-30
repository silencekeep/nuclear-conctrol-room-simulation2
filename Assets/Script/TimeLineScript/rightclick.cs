using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class rightclick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{   /// <summary>
/// 鼠标左、中、右事件触发，挂载到对应的物体上
/// </summary>
      //  public UnityEvent leftClick;
        //public UnityEvent middleClick;
        public UnityEvent rightClick;
        //public UnityEvent pointOverEnter;
       // public UnityEvent pointOverExit;
        public static  GameObject CurrentRightClickedAct;        
        public static GameObject CurrenPointAct;

    private void Start()
        {
          //  leftClick.AddListener(new UnityAction(ButtonLeftClick));
         //   middleClick.AddListener(new UnityAction(ButtonMiddleClick));
            rightClick.AddListener(new UnityAction(ButtonRightClick));
        }


        public void OnPointerClick(PointerEventData eventData)
        {
        //if (eventData.button == PointerEventData.InputButton.Left)
        //    leftClick.Invoke();
        //else if (eventData.button == PointerEventData.InputButton.Middle)
        //    middleClick.Invoke();
        //else if (eventData.button == PointerEventData.InputButton.Right)
        //    rightClick.Invoke();
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            rightClick.Invoke();
           // print("当前点击的UI是：" + eventData.pointerEnter);
            CurrentRightClickedAct = eventData.pointerEnter;
            // var buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            // print(buttonSelf.name+"11111111111" );
        }
        
        }


        private void ButtonLeftClick()
        {
            Debug.Log("Button Left Click");
        }

        private void ButtonMiddleClick()
        {
            Debug.Log("Button Middle Click");
        }

        private void ButtonRightClick()
        {
            //Debug.Log("Button Right Click");
        }

    public void OnPointerEnter(PointerEventData eventData)//当鼠标悬浮触发的事件，显示动素名称
    {
        //CurrenPointAct = eventData.pointerEnter;
       // pointOverEnter.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       // pointOverExit.Invoke();        
    }
}

