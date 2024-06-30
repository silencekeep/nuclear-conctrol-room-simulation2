using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class DoubleClick : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    public UnityEvent doubleClick = new UnityEvent();
    public float Interval = 0.5f;
    public static GameObject CurrentDoubleClickedDZAct;
    public static GameObject CurrenPointDZAct;

    private float firstClicked = 0;
    private float secondClicked = 0;


    public void OnPointerDown(PointerEventData eventData)
    {
        secondClicked = Time.realtimeSinceStartup;

        if (secondClicked - firstClicked < Interval)
        {
           
            CurrentDoubleClickedDZAct = eventData.pointerEnter;
            //print("当前点击的UI是：" + eventData.pointerEnter);
            doubleClick.Invoke();
        }
        else
        {
            firstClicked = secondClicked;
        }
    }
    
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
}
