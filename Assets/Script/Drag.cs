////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;
////using UnityEngine.EventSystems;

////public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler
////{
////    //记录初始鼠标与元素的偏移量
////    private Vector3 offset;

////    public void OnBeginDrag(PointerEventData eventData)
////    {
////        offset = transform.position - new Vector3(eventData.position.x, eventData.position.y, transform.position.z);
////    }

////    public void OnDrag(PointerEventData eventData)
////    {
////        transform.position = offset + new Vector3(eventData.position.x, eventData.position.y, transform.position.z);
////    }
////    //public void OnDrag(PointerEventData eventData)
////    //{
////    //    RectTransform rect = GetComponent<RectTransform>();
////    //    Vector3 pos = Vector3.zero;
////    //    RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, eventData.position, eventData.enterEventCamera, out pos);
////    //    rect.position = pos;
////    //}
////}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;


//public class DragUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IEndDragHandler, IPointerEnterHandler
//{
//    public RectTransform canvas;//得到canvas的ugui坐标
//    private RectTransform imgRect;//得到图片的ugui坐标
//    Vector2 offset = new Vector3();//用来得到鼠标和图片的差值

//    private Vector2 startPos;

//    private GameObject cam;

//    void Start()
//    {
//        imgRect = GetComponent<RectTransform>();//得到组件
//        startPos = imgRect.position;
//        cam = GameObject.FindGameObjectWithTag("CamRay");//找到摄像机
//    }

//    void Update()
//    {
//        //当前触摸在UI上
//        // print(EventSystem.current.IsPointerOverGameObject());
//    }
    
//    //当鼠标按下时调用 
//    public void OnPointerDown(PointerEventData eventData)
//    {

//        Vector2 mouseDown = eventData.position;//记录鼠标按下时的屏幕坐标
//        Vector2 mouseUguiPos = new Vector2();//定义一个接收返回的ugui坐标

//        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown, eventData.enterEventCamera, out mouseUguiPos);
//        //RectTransformUtility.ScreenPointToLocalPointInRectangle()：把屏幕坐标转化成ugui坐标
//        //canvas：坐标要转换到哪一个物体上，这里img父类是Canvas，我们就用Canvas
//        //eventData.enterEventCamera：这个事件是由哪个摄像机执行的
//        //out mouseUguiPos：返回转换后的ugui坐标
//        //isRect：方法返回一个bool值，判断鼠标按下的点是否在要转换的物体上

//        if (isRect)//如果在
//        { //计算图片中心和鼠标点的差值


//            offset = imgRect.anchoredPosition - mouseUguiPos;
//        }
//        cam.GetComponent<CameraMove>().enabled = false;
//    }


//    //当鼠标拖动时调用 
//    public void OnDrag(PointerEventData eventData)
//    {
//        Vector2 mouseDrage = eventData.position;//当鼠标拖动时的屏幕坐标
//        Vector2 uguiPos = new Vector2();//用来接收转换后的拖动坐标
//        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDrage, eventData.enterEventCamera, out uguiPos);
//        if (isRect)
//        {
//            //设置图片的ugui坐标与鼠标的ugui坐标保持不变
//            imgRect.anchoredPosition = offset + uguiPos;
//        }
//        cam.GetComponent<CameraMove>().enabled = false;
//    }
//    ////当鼠标抬起时调用
//    public void OnPointerUp(PointerEventData eventData)
//    {
//        offset = Vector2.zero;
//        imgRect.position = startPos;
//        cam.GetComponent<CameraMove>().enabled = true;
//    }
//    //当鼠标结束拖动时调用 
//    public void OnEndDrag(PointerEventData eventData)
//    {
//        offset = Vector2.zero;
//        imgRect.position = startPos;
//        cam.GetComponent<CameraMove>().enabled = true;
//    }
//    //当鼠标进入图片时调用 
//    public void OnPointerEnter(PointerEventData eventData)
//    {
//        print("进入");
//        cam.GetComponent<CameraMove>().enabled = false;
//    }


//}