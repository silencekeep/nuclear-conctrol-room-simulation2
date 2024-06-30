using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    private GameObject[] spheres = new GameObject[100];
    private int count = 0;
    public Material matSelest;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    void OnMouseEnter()
    {
        print(gameObject.name);
        Transform[] allChild;
        allChild = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChild)
        {
            if (child.gameObject.GetComponent<MeshRenderer>())
            {
                if ((child.name.Contains("P")) && !child.name.Contains("Point") && !child.name.Contains("显示器"))
                {
                    Vector3[] verts = new Vector3[8];
                    BoxCollider b = child.transform.GetComponent<BoxCollider>();
                    verts[0] = b.gameObject.transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f);
                    verts[1] = b.gameObject.transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f);
                    verts[2] = b.gameObject.transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f);
                    verts[3] = b.gameObject.transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f);
                    verts[4] = b.gameObject.transform.TransformPoint(b.center + new Vector3(-b.size.x, b.size.y, -b.size.z) * 0.5f);
                    verts[5] = b.gameObject.transform.TransformPoint(b.center + new Vector3(b.size.x, b.size.y, -b.size.z) * 0.5f);
                    verts[6] = b.gameObject.transform.TransformPoint(b.center + new Vector3(b.size.x, b.size.y, b.size.z) * 0.5f);
                    verts[7] = b.gameObject.transform.TransformPoint(b.center + new Vector3(-b.size.x, b.size.y, b.size.z) * 0.5f);
                    for (int i = 0; i < 8; i++)
                    {
                        GameObject obj1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        //设置物体的位置Vector3三个参数分别代表x,y,z的坐标数  
                        obj1.transform.position = new Vector3(verts[i].x, verts[i].y, verts[i].z);
                        obj1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        obj1.GetComponent<MeshRenderer>().material.color = Color.green;
                        spheres[count] = obj1;
                        count++;
                    }
                }
                
            }
        }

    }
    */
    void OnMouseExit()
    {
        //destroy();
    }

    public void destroy()
    {
        for (int i = 0; i < count; i++)
        {
            Destroy(spheres[i]);
        }
        count = 0;
    }

    private void OnPostRender()
    {
        drawSelectPoint();
    }

    public void drawSelectPoint()
    {
        matSelest.SetPass(0);
        MeshCollider meshCollider;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(PointSelect.TryGetPoint(ray, out meshCollider, out Vector3 vertice))
        {

            BeginLocalDraw(GL.QUADS, meshCollider.transform, Color.yellow);
            Vector3 p0 = vertice + new Vector3(-0.02f, 0.02f);
            Vector3 p1 = vertice + new Vector3(0.02f, 0.02f);
            Vector3 p2 = vertice + new Vector3(0.02f, -0.02f);
            Vector3 p3 = vertice + new Vector3(-0.02f, -0.02f);
            GL.Vertex3(p0.x, p0.y, p0.z);
            GL.Vertex3(p1.x, p1.y, p1.z);
            GL.Vertex3(p2.x, p2.y, p2.z);
            GL.Vertex3(p3.x, p3.y, p3.z);
            endDraw();

        }
    }

    public void BeginLocalDraw(int mode, Transform transform, Color c)
    {
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);
        GL.Begin(mode);
        GL.Color(c);
    }

    public void endDraw()
    {
        GL.End();
        GL.PopMatrix();
    }
}
