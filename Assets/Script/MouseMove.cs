using UnityEngine;

using System.Collections;
using System.Collections.Generic;
public class MouseMove : MonoBehaviour
{
    //public GameObject obj;
    public Camera objCamera;
    //public GameObject cam;
    //鼠标经过时改变物体颜色
    private Color mouseOverColor = Color.blue;//声明变量为蓝色
    private Color originalColor;//声明变量来存储本来颜色

    Ray ray;
    RaycastHit hit;

    ////与旋转有关的参数
    //private float OffsetX = 0;
    //private float OffsetY = 0;
    //public float speed = 6f;//旋转速度

    //public enum RotationAxes
    //{
    //    MouseXAndY = 0, //上下左右滑动
    //    MouseX = 1, //左右滑动
    //    MouseY = 2  //上下滑动
    //}

    //public RotationAxes m_axes = RotationAxes.MouseXAndY;
    //[Range(1, 10)]
    //public int m_sensitivityX = 5; //视角转动的幅度
    //[Range(1, 10)]
    //public int m_sensitivityY = 5;
    //[Range(1, 10)]
    //public int m_fieldOfView = 5; //视角拉伸的幅度
    //// 垂直方向的镜头转向范围 
    //public float m_minimumY = -45f;
    //public float m_maximumY = 45f;

    //float m_rotationY = 0f;


    void Start()
    {
        originalColor = GetComponent<MeshRenderer>().sharedMaterial.color;//开始时得到物体着色
    }


    void OnMouseEnter()
    {
        GetComponent<MeshRenderer>().material.color = mouseOverColor;//当鼠标滑过时改变物体颜色为蓝色
        
    }


    void OnMouseExit()
    {
        GetComponent<MeshRenderer>().material.color = originalColor;//当鼠标滑出时恢复物体本来颜色
        //cam.SetActive(true);
    }

    void Update()
    {
        //objCamera = Camera;
        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name + "左键");
                Vector3 targetScreenSpace = objCamera.WorldToScreenPoint(hit.collider.gameObject.transform.position);//用于获取物体的z轴坐标(物体离相机距离不变)
                Vector3 point = Input.mousePosition;
                hit.collider.gameObject.transform.position = objCamera.ScreenToWorldPoint(new Vector3(point.x, point.y, targetScreenSpace.z));

            }

        }
        if (Input.GetMouseButton(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name + "右键");
                hit.collider.gameObject.transform.Rotate(Vector3.up, -Input.GetAxis("Mouse X") * 10, Space.World);
                hit.collider.gameObject.transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * 10, Space.World);
            }
            

        }
        if (Input.GetMouseButton(2))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                Debug.Log(hit.collider.gameObject.name + "中键");
        }
    }
    //void Update()
    //{
    //    //按住左键旋转物体
    //    if (Input.GetMouseButton(0) && obj != null)
    //    {
    //        if (Input.GetMouseButton(0))
    //        {
    //            obj.transform.Rotate(Vector3.up, -Input.GetAxis("Mouse X") * 10, Space.World);
    //            obj.transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * 10, Space.World);
    //        }
    //    }
    //    //按住右键拖动物体
    //    else if (Input.GetMouseButton(1) && obj != null)
    //    {
    //        Vector3 targetScreenSpace = objCamera.WorldToScreenPoint(obj.transform.position);//用于获取物体的z轴坐标(物体离相机距离不变)
    //        Vector3 point = Input.mousePosition;
    //        obj.transform.position = objCamera.ScreenToWorldPoint(new Vector3(point.x, point.y, targetScreenSpace.z));
    //    }
    //}


        //if (Input.GetMouseButton(0))
        //{
        //    Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);//三维物体坐标转屏幕坐标
        //                                                                             //将鼠标屏幕坐标转为三维坐标，再计算物体位置与鼠标之间的距离
        //    var offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
        //    print("down");
        //    //cam.SetActive(false);
        //    //while (Input.GetMouseButton(0))
        //    //{
        //        print("left");
        //        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        //        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
        //        transform.position = curPosition;
        //        //yield return new WaitForFixedUpdate();

        //    //}
        //}
        //if (Input.GetMouseButton(1))
        //{
        //    if (m_axes == RotationAxes.MouseXAndY)
        //    {
        //        float m_rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * m_sensitivityX;
        //        m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
        //        m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);

        //        transform.localEulerAngles = new Vector3(-m_rotationY, m_rotationX, 0);
        //    }
        //    else if (m_axes == RotationAxes.MouseX)
        //    {
        //        transform.Rotate(0, Input.GetAxis("Mouse X") * m_sensitivityX, 0);
        //    }
        //    else
        //    {
        //        m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;
        //        m_rotationY = Mathf.Clamp(m_rotationY, m_minimumY, m_maximumY);

        //        transform.localEulerAngles = new Vector3(-m_rotationY, transform.localEulerAngles.y, 0);
        //    }
        //}
        ////通过鼠标滚轮放大 和 缩放视角
        //if (Input.GetAxis("Mouse ScrollWheel") < 0)
        //{
        //    Camera.main.fieldOfView += m_fieldOfView;
        //}
        //if (Input.GetAxis("Mouse ScrollWheel") > 0)
        //{
        //    Camera.main.fieldOfView -= m_fieldOfView;
        //}
        //}
    //    IEnumerator OnMouseDown()
    //{
    //    Vector3 screenSpace = Camera.main.WorldToScreenPoint(transform.position);//三维物体坐标转屏幕坐标
    //    //将鼠标屏幕坐标转为三维坐标，再计算物体位置与鼠标之间的距离
    //    var offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    //    print("down");
    //    //cam.SetActive(false);
    //    while (Input.GetMouseButton(0))
    //    {
    //        print("left");
    //        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
    //        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
    //        transform.position = curPosition;
    //        yield return new WaitForFixedUpdate();

    //    }
    //}
        //    //cam.SetActive(true);
        //    //while (Input.GetMouseButton(1))
        //    //{
        //    //    print("right");
        //    //    OffsetX = Input.GetAxis("Mouse X");//获取鼠标x轴的偏移量
        //    //    OffsetY = Input.GetAxis("Mouse Y");//获取鼠标y轴的偏移量

        //    //    transform.Rotate(new Vector3(OffsetY, -OffsetX, 0) * speed, Space.World);

        //    //    yield return new WaitForFixedUpdate();
        //    //}
        //}
    }