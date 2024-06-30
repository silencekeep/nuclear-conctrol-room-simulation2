using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DrawSnap
{
    static LineRenderer lineRenderer = GameObject.Find("CaseRoom/GameObject/0").GetComponent<LineRenderer>();
    public static Vector3 p0 = new Vector3(0,0,0);
    public static Vector3 p1 = new Vector3(0, 0, 0);
    public static Vector3 p2 = new Vector3(0, 0, 0);
    public static Vector3 p3 = new Vector3(0, 0, 0);

    public static void Draw()
    {
        Debug.Log(p0);
        lineRenderer.SetPosition(0, p0);
        lineRenderer.SetPosition(1, p1);
        lineRenderer.SetPosition(2, p2);
        lineRenderer.SetPosition(3, p3);
    }
}
