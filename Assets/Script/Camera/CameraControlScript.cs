using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour
{
    [Tooltip("code auto get")]
    [SerializeField]
    private Camera mainCamera;
    private Transform cameraTrans;

    private int xAxisCoefficient = 1;
    private int yAsixCoefficient = 1;

    private string _mouseXString = "Mouse X";
    private string _mouseYString = "Mouse Y";
    private string _mouseScrollWheel = "Mouse ScrollWheel";
    private Vector3 _currentMouseRotate = Vector3.zero;

    public float fieldOfViewMin = 20.0f;
    public float fieldOfviewMax = 100.0f;
    public float moveSpeed = 3;
    public float sensitivityDrag = 1;
    public float sensitivityRotate = 3;
    public float sensitivetyMouseWheel = 30;

    #region 面板修改该选项后要重新启动
    public bool xAxisinversion = false;
    public bool yAsixinversion = false;
    #endregion

    private void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        if (mainCamera == null)
        {
            Debug.LogErrorFormat("摄像机为空!");
            return;
        }
        cameraTrans = mainCamera.transform;

        xAxisCoefficient = xAxisinversion ? -1 : 1;
        yAsixCoefficient = yAsixinversion ? -1 : 1;
    }

    private void Update()
    {
        if (cameraTrans == null) return;

        KeyBoardControl();

        MouseLeftDragControl();

        MouseRightRotateControl();

        MouseScrollwheelScale();
    }

    /// <summary>
    /// 键盘移动
    /// </summary>
    private void KeyBoardControl()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cameraTrans.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            cameraTrans.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            cameraTrans.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            cameraTrans.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
    }

    /// <summary>
    /// 鼠标左键拖拽摄像机
    /// </summary>
    private void MouseLeftDragControl()
    {
        if (Input.GetMouseButton(2))
        {
            Vector3 p1 = cameraTrans.position - cameraTrans.right * Input.GetAxisRaw(_mouseXString) * sensitivityDrag * Time.timeScale;
            Vector3 p2 = p1 - cameraTrans.up * Input.GetAxisRaw(_mouseYString) * sensitivityDrag * Time.timeScale;
            cameraTrans.position = p2;
        }
    }

    /// <summary>
    /// 鼠标右键旋转
    /// </summary>
    private void MouseRightRotateControl()
    {
        if (Input.GetMouseButton(1))
        {
            _currentMouseRotate.x = (Input.GetAxis(_mouseYString) * sensitivityRotate) * xAxisCoefficient;
            _currentMouseRotate.y = (Input.GetAxis(_mouseXString) * sensitivityRotate) * yAsixCoefficient;

            cameraTrans.rotation = Quaternion.Euler(cameraTrans.eulerAngles + _currentMouseRotate);
        }
    }

    /// <summary>
    /// 鼠标滚轮缩放
    /// </summary>
    private void MouseScrollwheelScale()
    {
        if (Input.GetAxis(_mouseScrollWheel) == 0) return;

        mainCamera.fieldOfView = mainCamera.fieldOfView - Input.GetAxis(_mouseScrollWheel) * sensitivetyMouseWheel;
        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, fieldOfViewMin, fieldOfviewMax);
    }
}
