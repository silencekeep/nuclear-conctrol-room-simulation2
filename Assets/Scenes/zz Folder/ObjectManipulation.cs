using UnityEngine;

public class ObjectManipulation : MonoBehaviour
{
    // 缩放比例限制
    public float MinScale = 0.2f;
    public float MaxScale = 3.0f;
    // 缩放速率
    private float scaleRate = 1f;
    // 新尺寸
    private float newScale;

    // 射线
    private Ray ray;
    private RaycastHit hitInfo;

    private bool isDragging = false;
    private Vector3 offset;

    // 旋转
    private float rotationSpeed = 5.0f;

    private void OnMouseDown()
    {
        isDragging = true;
        offset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        // 拖拽
        if (isDragging)
        {
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            transform.position = newPosition;
        }

        // 旋转
        if (Input.GetMouseButton(1))
        {
            float mousX = Input.GetAxis("Mouse X");
            float mousY = Input.GetAxis("Mouse Y");
            transform.Rotate(mousY * rotationSpeed, -mousX * rotationSpeed, 0, Space.World);
        }

        // 缩放
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                newScale += Input.GetAxis("Mouse ScrollWheel") * scaleRate;
                newScale = Mathf.Clamp(newScale, MinScale, MaxScale);
                transform.localScale = new Vector3(newScale, newScale, newScale);
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
