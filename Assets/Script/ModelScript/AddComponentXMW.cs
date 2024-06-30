using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddComponentXMW : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public Light Dlight;
    public Material m;
    public GameObject go;     //给所有的子元素的物体添加一个脚本 该物体是所有元素的父元素
    private int count;     //计算所有子元素的个数
    private int[] IsAdd;
    int i = 1;
    public Material selected;
    public GameObject quan;
    public GameObject Texure_Popup;
    public Text text;
    public Text text2;
    // Start is called before the first frame update
    void Start()
    {
        count = go.transform.childCount;    //获取所有子物体的个数
        cam.gameObject.AddComponent<IsSavableCompo>();
        Dlight.gameObject.AddComponent<IsSavableCompo>();
        //if (count == 1 & i == 1)
        //{
        //    //for (int i = 0; i < count; i++)
        //    //{
        //    go.transform.GetChild(0).gameObject.AddComponent<MeshRenderer>().material = m;
        //    go.transform.GetChild(0).gameObject.AddComponent<BoxCollider>();
        //    //go.transform.GetChild(0).gameObject.AddComponent<BoxCollider>().size = new Vector3(10,10,10);
        //    //go.transform.GetChild(0).gameObject.AddComponent<MouseMove>();
        //    //go.transform.GetChild(i).gameObject.GetComponent<MouseMove>().objCamera = cam;
        //    go.transform.GetChild(0).gameObject.AddComponent<IsSavableCompo>();
        //    i++;
        //    //}
        //}

    }

    // Update is called once per frame
    void Update()
    {
        count = go.transform.childCount;    //获取所有子物体的个数
        if (count == 1 & i == 1)
        {

            go.transform.GetChild(0).gameObject.AddComponent<MeshRenderer>().material = m;
            go.transform.GetChild(0).gameObject.AddComponent<BoxCollider>();
            //go.transform.GetChild(0).gameObject.AddComponent<BoxCollider>().size = new Vector3(10,10,10);
            //go.transform.GetChild(0).gameObject.AddComponent<MouseMove>();
            //go.transform.GetChild(i).gameObject.GetComponent<MouseMove>().objCamera = cam;
            go.transform.GetChild(0).gameObject.AddComponent<IsSavableCompo>();
            go.transform.GetChild(0).gameObject.AddComponent<NewMouse>();
            go.transform.GetChild(0).gameObject.GetComponent<NewMouse>().obj = go.transform.GetChild(0).gameObject;
            go.transform.GetChild(0).gameObject.GetComponent<NewMouse>().IsThisObj = false;
            go.transform.GetChild(0).gameObject.GetComponent<NewMouse>().Selected = selected;
            go.transform.GetChild(0).gameObject.GetComponent<NewMouse>().quan = quan;
            go.transform.GetChild(0).gameObject.GetComponent<NewMouse>().Texure_Popup = Texure_Popup;
            go.transform.GetChild(0).gameObject.GetComponent<NewMouse>().text = text;
            go.transform.GetChild(0).gameObject.GetComponent<NewMouse>().text2 = text2;
            go.transform.GetChild(0).gameObject.GetComponent<NewMouse>().objCamera = cam;
            i = 3;

        }
        if (count == 2 & i == 3)
        {
            //for (int i = 0; i < count; i++)
            //{
            go.transform.GetChild(1).gameObject.AddComponent<MeshRenderer>().material = m;
            go.transform.GetChild(1).gameObject.AddComponent<BoxCollider>();
            //go.transform.GetChild(1).gameObject.AddComponent<MouseMove>();
            go.transform.GetChild(1).gameObject.AddComponent<IsSavableCompo>();
            go.transform.GetChild(1).gameObject.AddComponent<NewMouse>();
            go.transform.GetChild(1).gameObject.GetComponent<NewMouse>().obj = go.transform.GetChild(0).gameObject;
            go.transform.GetChild(1).gameObject.GetComponent<NewMouse>().IsThisObj = false;
            go.transform.GetChild(1).gameObject.GetComponent<NewMouse>().Selected = selected;
            go.transform.GetChild(1).gameObject.GetComponent<NewMouse>().quan = quan;
            go.transform.GetChild(1).gameObject.GetComponent<NewMouse>().Texure_Popup = Texure_Popup;
            go.transform.GetChild(1).gameObject.GetComponent<NewMouse>().text = text;
            go.transform.GetChild(1).gameObject.GetComponent<NewMouse>().text2 = text2;
            go.transform.GetChild(1).gameObject.GetComponent<NewMouse>().objCamera = cam;
            i++;
            //}
        }
    }
}
