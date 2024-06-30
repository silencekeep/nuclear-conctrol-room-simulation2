using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayTest2 : MonoBehaviour
{
    private Text tex;
    public GameObject eye;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GameObject.Find("CaseRoom/GameObject/3").GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

        }
        float distance = 0f;
        if (hit.collider == null)
        {
            distance = 0f;
        }
        else
        {
            distance = Vector3.Distance(transform.position, hit.collider.gameObject.GetComponent<Collider>().bounds.center) - hit.collider.gameObject.GetComponent<Collider>().bounds.size.z/2 -1.0f;
        }

        //Debug.DrawRay(ray.origin, new Vector3(0, 0, 1) * distance, Color.red);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position+new Vector3(0, 0, 1) * distance);
        tex = GameObject.Find("2DUI/Canvas111/Text1").GetComponent<Text>();
        tex.text = distance.ToString("0.00") + "m";
        tex.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
}
