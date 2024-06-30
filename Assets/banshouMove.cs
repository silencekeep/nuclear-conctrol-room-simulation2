using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class banshouMove: MonoBehaviour
{
    public GameObject obj;
    public Material Selected;
    public Camera objCamera;
    public bool IsThisObj = false;
    private Color mouseOverColor = Color.blue;//声明变量为蓝色
    private Color originalColor;//声明变量来存储本来颜色
    public GameObject Texure_Popup;
    public Text text;
    public Text text2;
    public GameObject quan;
    string name;
    string name2;
    public Button button;
    public Dropdown mater;
    public Material mental, plastic;
    Vector3 scale;
    public float offset = 0.1f;
    float maxSize = 100000.0f;
    float minSize = 0.001f;
    float speed = 0.5f;
    public GameObject Delete_Popup;
    public GameObject gra;
    public GameObject sizePanel;
    public InputField sizeInput;
    // Use this for initialization 



    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        //originalColor = GetComponent<MeshRenderer>().sharedMaterial.color;//开始时得到物体着色
        scale = this.transform.localScale;
    }
    void OnMouseDown()
    {
        name = gameObject.name;
        if (quan.activeInHierarchy)
        {
            IsThisObj = true;
            Debug.Log(name);
            text.text = gameObject.name;
            text2.text = gameObject.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!quan.activeInHierarchy)
        {
            IsThisObj = false;
            if (obj.GetComponent<HighLightControl>() != null)
            {
                HighlightableObject ho = obj.GetComponent<HighlightableObject>();
                HighLightControl hc = obj.GetComponent<HighLightControl>();
                Destroy(ho, 0);
                Destroy(hc, 0);
            }
        }

        if (IsThisObj && quan.activeInHierarchy)
        {
            //鼠标放上去高亮
            if (obj.GetComponent<HighLightControl>() == null)
            {
                obj.AddComponent<HighLightControl>();
            }
            if (obj.GetComponent<HighlightableObject>() == null)
            {
                obj.AddComponent<HighlightableObject>();
            }

            //左键拖拽移动
            if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.M))
            {
                Vector3 targetScreenSpace = objCamera.WorldToScreenPoint(obj.transform.position);
                Vector3 point = Input.mousePosition;
                obj.transform.position += Vector3.right * Time.deltaTime * Input.GetAxis("Mouse X") * speed;
            }
            if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.M))
            {

                Vector3 targetScreenSpace = objCamera.WorldToScreenPoint(obj.transform.position);
                Vector3 point = Input.mousePosition;
                obj.transform.position += Vector3.up * Time.deltaTime * Input.GetAxis("Mouse Y") * speed;
            }

            if (Input.GetMouseButton(2) && !Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.M))
            {
                Vector3 targetScreenSpace = objCamera.WorldToScreenPoint(obj.transform.position);
                Vector3 point = Input.mousePosition;
                obj.transform.position += Vector3.forward * Time.deltaTime * Input.GetAxis("Mouse Y") * speed;
            }

            //鼠标滚轮的效果（缩放）
            //Zoom out  
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.R))
            {
                sizePanel.SetActive(true);
                if (scale.x <= maxSize)
                {
                    scale.x += offset;
                    scale.y += offset;
                    scale.z += offset;
                    //obj.transform.position = obj.GetComponent<MeshRenderer>().bounds.center;
                    obj.transform.localScale = scale;
                    float result1 = scale.x / 22.36925f;
                    string result = result1.ToString("0.00");
                    GameObject.Find("PiXYZ_Runtime_Import/Canvas/SizePanel/SizeText").GetComponent<Text>().text = result;
                }
            }
            //Zoom in  
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && !Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.R))
            {
                sizePanel.SetActive(true);
                //center = obj.GetComponent<MeshRenderer>().bounds.center;
                if (scale.x > minSize)
                {
                    scale.x -= offset;
                    scale.y -= offset;
                    scale.z -= offset;
                    //obj.transform.scale = obj.GetComponent<MeshRenderer>().bounds.center;
                    obj.transform.localScale = scale; 
                    float result1 = scale.x / 22.36925f;
                    string result = result1.ToString("0.00");
                    GameObject.Find("PiXYZ_Runtime_Import/Canvas/SizePanel/SizeText").GetComponent<Text>().text = result;
                }
               // this.transform.RotateAround(center, new Vector3(1, 0, 0), Time.deltaTime * 500f * axis);
            }

            //删除
            if (Input.GetKey(KeyCode.Delete))
            {
                sizePanel.SetActive(false);
                if (!Delete_Popup.activeInHierarchy)
                {
                    Delete_Popup.SetActive(true);
                    Delete_Popup.GetComponent<DeleteModel>().deleteGo = obj;
                }
            }

            //表面属性
            if (Input.GetMouseButtonUp(1) && !Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.M) && !Input.GetKey(KeyCode.LeftControl))
            {
                gra.GetComponent<grasp>().go = obj;
                ray = objCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    name2 = hit.collider.gameObject.name;
                    if (hit.collider.gameObject == obj)
                    {

                        if (!Texure_Popup.activeInHierarchy)
                        {
                            text2.text = gameObject.name;
                            Texure_Popup.SetActive(true);
                        }
                        else
                        {
                            Texure_Popup.SetActive(false);
                        }
                    }
                }
            }

            void Test(GameObject go)
            {
                Transform parent = go.transform;
                Vector3 postion = parent.position;
                Quaternion rotation = parent.rotation;
                Vector3 scale = parent.localScale;
                parent.position = Vector3.zero;
                parent.rotation = Quaternion.Euler(Vector3.zero);
                parent.localScale = Vector3.one;

                Vector3 center = Vector3.zero;
                Renderer[] renders = parent.GetComponentsInChildren<Renderer>();
                foreach (Renderer child in renders)
                {
                    center += child.bounds.center;
                }
                center /= parent.GetComponentsInChildren<Transform>().Length;
                Bounds bounds = new Bounds(center, Vector3.zero);
                foreach (Renderer child in renders)
                {
                    bounds.Encapsulate(child.bounds);
                }

                parent.position = postion;
                parent.rotation = rotation;
                parent.localScale = scale;

                foreach (Transform t in parent)
                {
                    t.position = t.position - bounds.center;
                }
                parent.transform.position = bounds.center + parent.position;

                //return parent.position;
            }


            //旋转
            if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.LeftControl))
            {
                Vector3 center = new Vector3();
                //if (obj.GetComponentInChildren<Transform>())
                //{
                //    //把所有子物体的中心设为中心
                //    Test(obj);
                //    center = obj.transform.position;
                //}
                //else if (obj.transform.childCount == 0)
                //{
                center = obj.GetComponent<MeshRenderer>().bounds.center;
                //}

                float axis = Input.GetAxis("Mouse Y");
                //this.transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * 1000f * axis);
                this.transform.RotateAround(center, new Vector3(1, 0, 0), Time.deltaTime * 500f * axis);

            }
            if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.LeftControl))
            {
                Vector3 center = new Vector3();
                //if (obj.GetComponentInChildren<Transform>())
                //{
                //    //把所有子物体的中心设为中心
                //    Test(obj);
                //    center = obj.transform.position;
                //}
                //else if (obj.transform.childCount == 0)
                //{
                center = obj.GetComponent<MeshRenderer>().bounds.center;
                //}

                float axis = Input.GetAxis("Mouse X");
                //this.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * 1000f * axis);
                this.transform.RotateAround(center, new Vector3(0, 1, 0), Time.deltaTime * 500f * axis);
            }
            if (Input.GetMouseButton(2) && Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.LeftControl))
            {
                Vector3 center = new Vector3();
                //if (obj.GetComponentInChildren<Transform>())
                //{
                //    //把所有子物体的中心设为中心
                //    Test(obj);
                //    center = obj.transform.position;
                //}
                //else if (obj.transform.childCount == 0)
                //{
                center = obj.GetComponent<MeshRenderer>().bounds.center;
                //}
                float axis = Input.GetAxis("Mouse X");
                //this.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 1000f * axis);
                this.transform.RotateAround(center, new Vector3(0, 0, 1), Time.deltaTime * 500f * axis);
            }
        }
        //自由旋转
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.F) && !Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.M) && !Input.GetKey(KeyCode.LeftControl))
        {
            Vector3 center = new Vector3();
            center = obj.GetComponent<BoxCollider>().bounds.center;
            float axis = Input.GetAxis("Mouse X") * 10;
            float axis2 = Input.GetAxis("Mouse Y") * 10;
            //this.transform.Rotate(Vector3.up, -Input.GetAxis("Mouse X") * 10, Space.World);
            //this.transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * 10, Space.World);
            obj.transform.RotateAround(center, Vector3.up, Time.deltaTime * 500f * axis);
            obj.transform.RotateAround(center, Vector3.right, Time.deltaTime * 500f * axis2);
        }
        //}
    }

    public void DeleteMod()
    {
        obj.SetActive(false);
    }

    public void btn_Size()
    {
        String sizeStr = sizeInput.text;
        GameObject.Find("PiXYZ_Runtime_Import/Canvas/SizePanel/SizeText").GetComponent<Text>().text = sizeStr;
        float result = float.Parse(sizeStr);
        Vector3 startSize = new Vector3(22.36925f*result,18.038f*result,25*result);
        //heightValue = new Vector3(heightSlider.value, heightSlider.value, heightSlider.value);
        obj.transform.localScale = startSize;
        sizePanel.SetActive(false);

    }

}




