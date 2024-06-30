using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiquer : MonoBehaviour
{
    public float pokeForce;
    public Camera cam;
    public GameObject Sphere1;
    // Use this for initialization

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10000))
                if (hit.rigidbody != null)
                    hit.rigidbody.AddForceAtPosition(ray.direction * pokeForce, hit.point);
            {
                Debug.Log(hit.point);
                //GameObject.Find("Sphere1").transform.position = hit.point;
                Sphere1.transform.position = hit.point;
            }
            //{
            //    //transform.position = hit.point;
            //    //拾取三角面前提是物体含有一个MeshCollider碰撞器
            //    MeshCollider collider = hit.collider as MeshCollider;
            //    if (collider == null || collider.sharedMesh == null)
            //        return;
            //    //获取碰撞器所在物体的Mesh网格
            //    Mesh mesh0 = collider.sharedMesh;
            //    //Mesh mesh1 = collider.sharedMesh;
            //    //获取Mesh网格的所有顶点
            //    Vector3[] vertices = mesh0.vertices;
            //    // Vector3[] vertices = mesh1.vertices;
            //    //获取mesh的三角形索引，这里的索引的就是模型顶点数组的下标
            //    int[] triangles = mesh0.triangles;
            //    //int[] triangles = mesh1.triangles;
            //    //然后通过hit.triangleIndex(摄像碰撞到的三角形的第一个点的索引)
            //    //然后+1 ，+2，获取三角形另外两个点的坐标
            //    Vector3 p0 = vertices[triangles[hit.triangleIndex * 3]];
            //    Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
            //    Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];
            //    Transform transform = collider.transform;
            //    //上面的三个顶点是Mesh的本地坐标，需要用模型的Transform进行转换到世界坐标
            //    p0 = transform.TransformPoint(p0);
            //    p1 = transform.TransformPoint(p1);
            //    p2 = transform.TransformPoint(p2);
            //    //然后设置三个小球的位置到这个三个点，方便调试，呵呵！
            //    GameObject.Find("Sphere1").transform.position = (p0 + p1 + p2)/3;
            //    //GameObject.Find("Sphere2").transform.position = p1;
            //    //GameObject.Find("Sphere3").transform.position = p2;
            //}
        }
        if (Input.GetMouseButtonUp(1))
        {

        }
    }
}
