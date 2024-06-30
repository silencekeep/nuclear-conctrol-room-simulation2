using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    private GameObject obj;
    private float x;
    private float y;
    private float z;
    private float x0;
    private float y0;
    private float z0;
    private bool isMoved = false;
    public InputField objName;

    private bool target;
    private GameObject myTarget;
    public InputField targetName;
    public InputField dis;

    public InputField xm;
    public InputField ym;
    public InputField zm;
    public InputField xr;
    public InputField yr;
    public InputField zr;

    private bool choose;

    // Start is called before the first frame update
    void Start()
    {
        choose = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (target && Input.GetMouseButton(0))
        {
            choose = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetName.text = hit.transform.name;
                myTarget = hit.transform.gameObject;
            }
            target = false;
            choose = true;
        }

        if (objName.text != "" && dis.text != "" && targetName.text != "")
        {
            float distance = float.Parse(dis.text);
            obj.GetComponent<Transform>().position = new Vector3(obj.GetComponent<Transform>().position.x,
                obj.GetComponent<Transform>().position.y, myTarget.GetComponent<Transform>().position.z-distance-0.7f);
        }

        if (choose && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                objName.text = hit.transform.name;
                obj = hit.transform.gameObject;
                init();
            }
        }
    }

    void init()
    {
        x0 = obj.GetComponent<Transform>().localRotation.x;
        y0 = obj.GetComponent<Transform>().localRotation.y;
        z0 = obj.GetComponent<Transform>().localRotation.z;
        x = obj.GetComponent<Transform>().position.x;
        y = obj.GetComponent<Transform>().position.y;
        z = obj.GetComponent<Transform>().position.z;
        isMoved = false;
    }

    public void objMove()
    {
        float x_m = x; 
        if(xm.text != null && xm.text != "")
        {
            x_m = float.Parse(xm.text);
        }
        float y_m = y;
        if (ym.text != null && ym.text != "")
        {
            y_m = float.Parse(ym.text);
        }
        float z_m = z;
        if (zm.text != null && zm.text != "")
        {
            z_m = float.Parse(zm.text);
        }

        float x_r = x0;
        if (xr.text != null && xr.text != "")
        {
            x_r = float.Parse(xr.text);
        }
        float y_r = y0;
        if (yr.text != null && yr.text != "")
        {
            y_r = float.Parse(yr.text);
        }
        float z_r = z0;
        if (zr.text != null && zr.text != "")
        {
            z_r = float.Parse(zr.text);
        }
        obj.GetComponent<Transform>().position = new Vector3(x+x_m, y+y_m, z+z_m);
        //obj.GetComponent<Transform>().localRotation = Quaternion.Euler(x0+x_r, y0+y_r, z0+z_r);
        isMoved = true;
    }

    public void reset()
    {
        if (isMoved)
        {
            obj.GetComponent<Transform>().position = new Vector3(x, y, z);
            obj.GetComponent<Transform>().localRotation = Quaternion.Euler(x0, y0, z0);
        }
        isMoved = false;
    }

    public void close()
    {
        target = false;
        isMoved = false;
        xm.text = "";
        ym.text = "";
        zm.text = "";
        xr.text = "";
        yr.text = "";
        zr.text = "";
    }
    public void targetChoose()
    {
        target = true;
    }
}
