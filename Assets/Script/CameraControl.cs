using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    public Transform CameraPosition;
    public Transform[] Camerapoints;
    public int CurrentPoints = 0;
    public Dropdown CameraList;
    public Transform LightPosition;

    //public Dropdown ThreeViewsList;

    [SerializeField] GameObject x0;
    [SerializeField] GameObject z0;
    //public static bool CouldMove = true;
    void Start()
    {

    }

    // Update is called once per frame
    public void CameraPointAdjust()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    CurrentPoints = 0;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    CurrentPoints = 1;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    CurrentPoints = 2;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    CurrentPoints = 3;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    CurrentPoints = 4;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    CurrentPoints = 5;
        //}
        CurrentPoints = CameraList.value;


        if (CurrentPoints == 6) //正视图
        {
            Camerapoints[CurrentPoints].position = new Vector3(z0.transform.localScale.x / 2, 1.5f, 0);
        }
        else if (CurrentPoints == 7) //侧视图
        {
            Camerapoints[CurrentPoints].position = new Vector3(0, 1.5f, x0.transform.localScale.z / 2);
        }
        else if (CurrentPoints == 8)//俯视图
        {
            Camerapoints[CurrentPoints].position = new Vector3(z0.transform.position.x, z0.transform.localScale.y / 2, x0.transform.position.z );
        
        }
        CameraPosition.position = Camerapoints[CurrentPoints].position;
        CameraPosition.eulerAngles = Camerapoints[CurrentPoints].eulerAngles;
        LightPosition.position = Camerapoints[CurrentPoints].position;
        LightPosition.eulerAngles = Camerapoints[CurrentPoints].eulerAngles;

    }

    public void ThreeViewsPointAdjust()
    {

    }

    void SetPointPosition()
    {
        gameObject.transform.position = new Vector3(z0.transform.localScale.x / 2, 0, x0.transform.localScale.z / 2);
    }
}
