using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddComponent : MonoBehaviour
{
    public Camera cam;
    public Light Dlight;
    public Material m;
    public GameObject go;     //给所有的子元素的物体添加一个脚本 该物体是所有元素的父元素
    public int count;     //计算所有子元素的个数
    private int[] IsAdd;
    public int i = 1;
    public Material selected;
    public GameObject quan;
    public GameObject Texure_Popup;
    public Text text;
    public Text text2;
    public GameObject s1;
    public GameObject Delete_Popup;
    public GameObject sizePanel;
    public InputField sizeInput;
    public Button btn_sizeyangji;
    // Start is called before the first frame update
    //public GameObject gameob;
    void Start()
    {
        count = go.transform.childCount;    //获取所有子物体的个数
        cam.gameObject.AddComponent<IsSavableCompo>();
        Dlight.gameObject.AddComponent<IsSavableCompo>();
        
    }

    // Update is called once per frame
    void Update()
    {
        count = go.transform.childCount;    //获取所有子物体的个数           

        while (i == count)
        {
            //GetChildest(go.transform.GetChild(i - 1).gameObject);
            Test(go.transform.GetChild(i - 1).gameObject);
            //go.transform.GetChild(i - 1).gameObject.GetComponent<BoxCollider>().isTrigger = true;
           // go.transform.GetChild(i - 1).gameObject.AddComponent<MeshRenderer>().material = m;
            go.transform.GetChild(i - 1).gameObject.AddComponent<IsSavableCompo>();
            go.transform.GetChild(i - 1).gameObject.AddComponent<NewMouse>();
            go.transform.GetChild(i - 1).gameObject.GetComponent<NewMouse>().obj = go.transform.GetChild(i - 1).gameObject;
            go.transform.GetChild(i - 1).gameObject.GetComponent<NewMouse>().IsThisObj = false;
            go.transform.GetChild(i - 1).gameObject.GetComponent<NewMouse>().Selected = selected;
            go.transform.GetChild(i - 1).gameObject.GetComponent<NewMouse>().quan = quan;
            go.transform.GetChild(i - 1).gameObject.GetComponent<NewMouse>().Texure_Popup = Texure_Popup;
            go.transform.GetChild(i - 1).gameObject.GetComponent<NewMouse>().Delete_Popup = Delete_Popup;
            go.transform.GetChild(i - 1).gameObject.GetComponent<NewMouse>().text = text;
            go.transform.GetChild(i - 1).gameObject.GetComponent<NewMouse>().text2 = text2;
            go.transform.GetChild(i - 1).gameObject.GetComponent<NewMouse>().objCamera = cam;
            go.transform.GetChild(i - 1).gameObject.GetComponent<NewMouse>().sizePanel = sizePanel;
            go.transform.GetChild(i - 1).gameObject.GetComponent<NewMouse>().sizeInput = sizeInput;
            go.transform.GetChild(i - 1).gameObject.GetComponent<NewMouse>().btn_sizeyangji = btn_sizeyangji;
            go.transform.GetChild(i - 1).gameObject.AddComponent<shiquer>();
            go.transform.GetChild(i - 1).gameObject.GetComponent<shiquer>().cam = cam;
            go.transform.GetChild(i - 1).gameObject.GetComponent<shiquer>().Sphere1 = s1;
            go.transform.GetChild(i - 1).gameObject.AddComponent<Rigidbody>();
            go.transform.GetChild(i - 1).gameObject.GetComponent<Rigidbody>().isKinematic = true;
            go.transform.GetChild(i - 1).gameObject.GetComponent<Rigidbody>().useGravity = false;
            i++;
        }

        for (int t = 0; t < count; t++)
        {
            if (!go.transform.GetChild(t).gameObject.activeInHierarchy)
            {
                Destroy(go.transform.GetChild(t).gameObject, 0);
                i--;
            }
        }

    }

    //找到trans这个gameobject的没有子物体的子物体（最底层的子物体） 给最底层子物体加上包围盒
    static void GetChildest(GameObject trans)
    {
        int num = trans.transform.childCount;
        if (num > 0)
        {
            for (int i = 0; i < num; i++)
            {
                GetChildest(trans.transform.GetChild(i).gameObject);
            }
        }
        else
        {
            //Debug.Log(trans.name);
            trans.AddComponent<BoxCollider>();
            //trans.GetComponent<MeshCollider>().convex = true;
            trans.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    //给整个物体加包围盒
    public void Test(GameObject gameObject)
    {
        Transform parent = gameObject.transform;
        Vector3 postion = parent.position;
        Quaternion rotation = parent.rotation;
        Vector3 scale = parent.localScale;
        parent.position = Vector3.zero;
        parent.rotation = Quaternion.Euler(Vector3.zero);
        parent.localScale = Vector3.one;

        Collider[] colliders = parent.GetComponentsInChildren<Collider>();
        foreach (Collider child in colliders)
        {
            DestroyImmediate(child);
        }
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
        BoxCollider boxCollider = parent.gameObject.AddComponent<BoxCollider>();
        boxCollider.center = bounds.center - parent.position;
        boxCollider.size = bounds.size;

        parent.position = postion;
        parent.rotation = rotation;
        parent.localScale = scale;
    }

    //Transform FindUpParent(Transform zi)
    //{
    //    if (zi.parent == null)
    //        return zi;
    //    else
    //        return FindUpParent(zi.parent);
    //}


}
