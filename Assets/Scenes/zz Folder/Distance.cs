using CNCC.Panels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    public Camera Camera;
    public InputField objName;
    public InputField referName;
    public Text distance;
    public Text centerDistance;
    public Text angle;
    private bool obj;
    private bool refer;
    private bool flag;
    private GameObject myObject;
    private GameObject myReference;
    public GameObject Room;
    private bool dis;
    private GameObject eye;
    private GameObject body;
    private Text tex;
    private Text tex_max;
    private Text text;
    public GameObject starter;
    // Start is called before the first frame update
    void Start()
    {
        Room.GetComponent<Celiang002>().enabled = false;
        Room.GetComponent<LineRenderer>().enabled = false;
        obj = false;
        refer = false;
        flag = true;
        dis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dis)
        {
            GameObject myHuman;
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag.Equals("human"))
                    {
                        myHuman = hit.transform.GetChild(0).gameObject;
                        
                        eye = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Neck_M/Head_M/Cube").gameObject;
                        body = myHuman.transform.Find("Root_M/Spine1_M/Chest_M/Cube").gameObject;
                        eye.GetComponent<RayTest>().enabled = true;
                        body.GetComponent<RayTest2>().enabled = true;
                    }
                }

            }
        }
        if (obj && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                objName.text = hit.transform.name;
                myObject = hit.transform.gameObject;
            }
            obj = false;
            
        }

        if (refer && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                referName.text = hit.transform.name;
                myReference = hit.transform.gameObject;
            }
            refer = false;

        }
        
        if(objName.text!="" && referName.text != "" && flag)
        {
            Vector3 start = myReference.transform.position;
            Vector3 end = myObject.transform.position;
            float res = (float)(((end - start).magnitude - 1.2) < 0 ? 0 : ((end - start).magnitude - 1.2));
            distance.text = res.ToString("0.00") + "米";
            //centerDistance.text = (end - start).magnitude.ToString("0.00") + "米"; 
            float ang = myReference.transform.eulerAngles.y;
            Vector3 project = Vector3.ProjectOnPlane((end - start), new Vector3((float)Math.Cos(ang), (float)Math.Sin(ang), 0));
            Debug.DrawLine(start, end);
            angle.text = (project.magnitude-1.2).ToString("0.00") + "米";
          
            /*
            if (myObject.GetComponent<Measurements>())
            {
                if (myReference.GetComponent<Collider>())
                {
                    myObject.GetComponent<Measurements>().DistanceObject = myReference;
                    Vector2 dis = myObject.GetComponent<Measurements>().getDistance();
                    Vector4 angles = myObject.GetComponent<Measurements>().getAngles();
                    distance.text = dis.y.ToString("0.00") + "米";
                    centerDistance.text = (dis.y-0.75).ToString("0.00") + "米";
                    angle.text = (Mathf.Abs((float)((myObject.transform.position.z-myReference.transform.position.z)*Math.Cos((myObject.transform.localEulerAngles.y+90) * Math.PI / 180))) ).ToString("0.00") + "米";
                }
                else
                {
                    myReference.AddComponent<Collider>();
                }
            }
            else
            {
                myObject.AddComponent<Measurements>();
            }
            */
        }
    }
    public void delBox()
    {
        int n = Panel.AllPanels.Count;
        for (int i=0;i<n;i++)
        {
            if (Panel.AllPanels[i].GetComponent<BoxCollider>() && Panel.AllPanels[i].GetComponent<BoxCollider>().enabled)
            {
                Panel.AllPanels[i].GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    public void reBox()
    {
        int n = Panel.AllPanels.Count;
        for (int i = 0; i < n; i++)
        {
            if (Panel.AllPanels[i].GetComponent<BoxCollider>())
            {
                Panel.AllPanels[i].GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    public void objChoose()
    {
        reBox();
        obj = true;
    }

    public void referChoose()
    {
        reBox();
        refer = true;
    }

    public void start()
    {
        flag = true;
    }
    public void trans()
    {
        Room.GetComponent<Celiang002>().enabled = !Room.GetComponent<Celiang002>().enabled;
        Room.GetComponent<LineRenderer>().enabled = !Room.GetComponent<LineRenderer>().enabled;
    }
    public void stop()
    {
        reBox();
        flag = false;
        objName.text = "";
        referName.text = "";
        Room.GetComponent<Celiang002>().enabled = false;
        Room.GetComponent<LineRenderer>().enabled = false;
        removeSnap();
        if (dis)
        {
            dis = false;
            eye.GetComponent<RayTest>().enabled = false;
            body.GetComponent<RayTest2>().enabled = false;
            text = GameObject.Find("2DUI/Canvas111/Text1").GetComponent<Text>();
            text.text = "";

            tex = GameObject.Find("2DUI/Canvas111/Text").GetComponent<Text>();
            tex.text = "";

            tex_max = GameObject.Find("2DUI/Canvas111/Text2").GetComponent<Text>();
            tex_max.text = "";

            LineRenderer lineRenderer1 = GameObject.Find("CaseRoom/GameObject/1").GetComponent<LineRenderer>();
            LineRenderer lineRenderer2 = GameObject.Find("CaseRoom/GameObject/2").GetComponent<LineRenderer>();
            LineRenderer lineRenderer3 = GameObject.Find("CaseRoom/GameObject/3").GetComponent<LineRenderer>();
            lineRenderer1.SetPosition(0, new Vector3(0, 0, 0));
            lineRenderer1.SetPosition(1, new Vector3(0, 0, 0));
            lineRenderer2.SetPosition(0, new Vector3(0,0,0));
            lineRenderer2.SetPosition(1, new Vector3(0, 0, 0));
            lineRenderer3.SetPosition(0, new Vector3(0, 0, 0));
            lineRenderer3.SetPosition(1, new Vector3(0, 0, 0));

        }
        int n = starter.transform.childCount;
        for(int i = 0; i < n; i++)
        {
            Destroy(starter.transform.GetChild(i).gameObject);
        }
    }
    public void humanDis()
    {
        dis = true;
    }

    public void addSnap()
    {
        delBox();
        if (Camera.gameObject.GetComponent<SnapPoint>())
        {
            Camera.gameObject.GetComponent<SnapPoint>().enabled = true;
        }
    }

    public void removeSnap()
    {
        reBox();
        if (Camera.gameObject.GetComponent<SnapPoint>())
        {
            Camera.gameObject.GetComponent<SnapPoint>().enabled = false;
        }
    }
}
