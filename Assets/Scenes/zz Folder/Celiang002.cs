using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Celiang002 : MonoBehaviour
{

    /// <summary>
    /// 测量距离工具
    /// </summary>

    LineRenderer line;
    Ray ray;
    RaycastHit hit;
    public Text tex;
    Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            line.SetPosition(0, hit.point);
            startPos = hit.point;
        }
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100) && Input.GetMouseButton(0))
        {
            line.SetPosition(1, hit.point);
        }
        tex.text = Vector3.Distance(startPos, hit.point).ToString("0.00")+"m";
        tex.transform.position = Camera.main.WorldToScreenPoint(hit.point);

    }
}