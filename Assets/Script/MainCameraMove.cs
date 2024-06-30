using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMove : MonoBehaviour
{
    public Camera Camera;
    public Light MainLight;
    void Start()
    {
        Camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float speed = 2.5f;//旋转跟随速度
            float OffsetX = Input.GetAxis("Mouse X");//获取鼠标x轴的偏移量
            float OffsetY = Input.GetAxis("Mouse Y");//获取鼠标y轴的偏移量
            Camera.transform.Rotate(new Vector3(-OffsetY, -OffsetX, 0) * speed, Space.World);//旋转物体
            MainLight.transform.rotation = Camera.transform.rotation;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.fieldOfView <= 100)
                Camera.fieldOfView += 2;
            if (Camera.orthographicSize <= 20)
                Camera.orthographicSize += 0.5F;
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.fieldOfView > 2)
                Camera.fieldOfView -= 2;
            if (Camera.orthographicSize >= 1)
                Camera.orthographicSize -= 0.5F;
        }
        if (Input.GetMouseButton(2))//ButtonMiddleClick
        {
            float speed = 0.2f;//旋转跟随速度
            float OffsetX = Input.GetAxis("Mouse X");//获取鼠标x轴的偏移量
            float OffsetY = Input.GetAxis("Mouse Y");//获取鼠标y轴的偏移量                                                     
            //Camera.transform.localPosition(new Vector3(-OffsetY, OffsetX, 0) * speed, Space.World);
            Camera.transform.Translate(new Vector3(-OffsetX, -OffsetY, 0) * speed);
        }
        MainLight.transform.rotation = Camera.transform.rotation;
    }
}

