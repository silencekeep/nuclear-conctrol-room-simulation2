using CNCC.Panels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldSpaceTransitions;

public class TargetSelect : MonoBehaviour
{
    private bool flag = false;
    public GameObject panel;
    public Material mat;
    private Vector3 offset;
    public Camera cam;
    private GameObject[] tars;
    private int count = 0;
    private GameObject target;
    private Material oldMat;
    // Start is called before the first frame update
    void Start()
    {
        tars = new GameObject[20];
    }

    // Update is called once per frame
    void Update()
    {

        if (flag)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    panel.GetComponent<SectionSetup>().model = hit.transform.gameObject;
                    panel.GetComponent<Planar_xyzClippingSection>().model = hit.transform.gameObject;
                    oldMat = hit.transform.gameObject.GetComponent<MeshRenderer>().material;
                    target = hit.transform.gameObject;
                    tars[count] = target;
                    count++;
                    hit.transform.gameObject.GetComponent<MeshRenderer>().material = mat;
                    if (hit.transform.gameObject.GetComponent<Panel>())
                    {
                        hit.transform.gameObject.GetComponent<Panel>().ChangeMesh(mat);
                    }
                    flag = false;
                }

            }
        }
    }

    public void Active()
    {
        flag = true;
    }
    public void stop()
    {
        for(int i = 0; i < 20; i++)
        {
            target = tars[i];
            if (target != null)
            {
                target.GetComponent<MeshRenderer>().material = oldMat;
                if (target.GetComponent<Panel>())
                {
                    target.GetComponent<Panel>().ChangeMesh(oldMat);
                }
            }
        }
        
        
    }
}
