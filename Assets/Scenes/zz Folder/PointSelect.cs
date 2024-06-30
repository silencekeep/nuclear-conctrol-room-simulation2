using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class PointSelect
{
    public static bool TryGetTriangle(Ray ray, out MeshCollider meshCollider, out int triIndex)
    {
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1000))
        {
            meshCollider = hit.collider as MeshCollider;
            if(meshCollider==null|| meshCollider.sharedMesh == null)
            {
                triIndex = -1;
                return false;
            }

            triIndex = hit.triangleIndex;
            return true;
        }
        else
        {
            meshCollider = null;
            triIndex = -1;
            return false;
        }
    }

    public static bool TryGetPoint(Ray ray, out MeshCollider meshCollider, out Vector3 point)
    {
        int triAngleIndex = -1;

        point = Vector3.zero;
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 1000))
        {
            meshCollider = hit.collider as MeshCollider;
            if(meshCollider == null || meshCollider.sharedMesh == null)
            {
                triAngleIndex = -1;
                return false;
            }
            triAngleIndex = hit.triangleIndex;

            Mesh mesh = meshCollider.sharedMesh;

            Vector3[] points = mesh.vertices;
            int[] triangles = mesh.triangles;
            Vector3 hitPoint = meshCollider.transform.InverseTransformPoint(hit.point);

            Vector3 p0 = points[triangles[triAngleIndex * 3]];
            Vector3 p1 = points[triangles[triAngleIndex * 3+1]];
            Vector3 p2 = points[triangles[triAngleIndex * 3+2]];

            Vector3 p3 = (p0 + p1) / 2;
            Vector3 p4 = (p0 + p2) / 2;
            Vector3 p5 = (p1 + p2) / 2;

            float dis = (hitPoint - p0).magnitude;
            point = p0;
            if((point - p1).magnitude < dis)
            {
                dis = (point - p1).magnitude;
                point = p1;
            }
            if ((point - p2).magnitude < dis)
            {
                dis = (point - p2).magnitude;
                point = p2;
            }
            if ((point - p3).magnitude < dis)
            {
                dis = (point - p3).magnitude;
                point = p3;
            }
            if ((point - p4).magnitude < dis)
            {
                dis = (point - p4).magnitude;
                point = p4;
            }
            if ((point - p5).magnitude < dis)
            {
                dis = (point - p5).magnitude;
                point = p5;
            }

            return true;
        }
        else
        {
            meshCollider = null;
            return false;
        }
    }
}
