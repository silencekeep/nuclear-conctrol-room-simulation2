using CNCC.Panels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayTest : MonoBehaviour
{
    public LayerMask mask;
    public float dis; 
    private Text tex;
    private Text tex_max;

    LineRenderer lineRenderer1;
    LineRenderer lineRenderer2;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer1 = GameObject.Find("CaseRoom/GameObject/1").GetComponent<LineRenderer>();
        lineRenderer2 = GameObject.Find("CaseRoom/GameObject/2").GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            Debug.Log("物体"+hit.transform.name);
            
        }
        float distance = 0f;
        if (hit.collider == null)
        {
            GameObject ping = GameObject.Find("CaseRoom/大屏幕45度拼角台(Clone)");
            distance = Vector3.Distance(transform.position, new Vector3(transform.position.x, transform.position.y, ping.transform.position.z));
        }
        else
        {
            distance = Vector3.Distance(transform.position, hit.collider.gameObject.transform.position);
        }

        //Debug.DrawRay(ray.origin, new Vector3(0, 0, 1) * distance, Color.red);
        lineRenderer1.SetPosition(0, transform.position);
        lineRenderer1.SetPosition(1, transform.position+new Vector3(0, 0, 1) * distance);
        tex = GameObject.Find("2DUI/Canvas111/Text").GetComponent<Text>();
        tex.text = distance.ToString("0.00") + "m";
        tex.transform.position = Camera.main.WorldToScreenPoint(transform.position);

        GameObject left = GameObject.Find("CaseRoom/大屏幕45度拼角台(Clone)");
        GameObject right = GameObject.Find("CaseRoom/大屏幕45度拼角台(Clone)");
        int n = Panel.AllPanels.Count;
        int cnt = 0;
        for(int i = 0; i < n; i++)
        {
            if (Panel.AllPanels[i].name.Contains("大屏幕45度拼角台"))
            {
                if (cnt == 0)
                {
                    left = Panel.AllPanels[i].gameObject;
                    cnt++;
                }
                if (cnt == 1)
                {
                    right = Panel.AllPanels[i].gameObject;
                }
            }
        }
        
        float disLeft = Vector3.Distance(left.transform.position, transform.position);
        float disRight = Vector3.Distance(right.transform.position, transform.position);
        Vector3 target;
        if(disLeft > disRight)
        {
            Ray ray2 = new Ray(transform.position, transform.forward);
            //Debug.DrawRay(ray.origin, left.transform.position-transform.position, Color.red);
            target = left.transform.position;
            tex_max = GameObject.Find("2DUI/Canvas111/Text2").GetComponent<Text>();
            tex_max.text = disLeft.ToString("0.00") + "m";
            tex_max.transform.position = Camera.main.WorldToScreenPoint((left.transform.position+transform.position)/2);
            dis = disLeft;
        }
        else
        {
            Ray ray2 = new Ray(transform.position, transform.forward);
            //Debug.DrawRay(ray.origin, right.transform.position - transform.position, Color.red);
            target = right.transform.position;
            tex_max = GameObject.Find("2DUI/Canvas111/Text2").GetComponent<Text>();
            tex_max.text = disRight.ToString("0.00") + "m";
            tex_max.transform.position = Camera.main.WorldToScreenPoint((right.transform.position + transform.position) / 2);
            dis = disRight;
        }
        lineRenderer2.SetPosition(0, transform.position);
        lineRenderer2.SetPosition(1, target);

    }
}
