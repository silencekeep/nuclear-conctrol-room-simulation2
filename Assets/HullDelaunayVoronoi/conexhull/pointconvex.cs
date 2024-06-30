using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using HullDelaunayVoronoi.Hull;
using HullDelaunayVoronoi.Primitives;
namespace HullDelaunayVoronoi
{

    public class pointconvex : MonoBehaviour
    {
        Vertex3[] vertices = new Vertex3[500];

        Vector3[] meshVerts = new Vector3[500];
        int[] meshIndices = new int[500];



        private int seed = 0;

        private Material lineMaterial;

        private int Id = 0;

        private Mesh mesh;

        private Matrix4x4 rotation = Matrix4x4.identity;
        //定义凸面体中心点
       public Transform ConvexCenterPoint;
        //定义跟随末端物体
        private Transform ToolPoint;
        private float size = 0.2f;
        public static int time;
        int i = time * 30;
        private ConvexHull3 hull;
        public static int j = 0;
        public static Vector3[] Poitn = new Vector3[1001];
        Vector3[] speed = new Vector3[1001];
        float[] MoveTime = new float[1001];
        CollisionDetection1 collision1;
        click click2;
        input input1;
        lineconvex convex;
        public  int t = 0;
         int d = 0;
        public int p = 0;
        input2 speed1;
        click1 click3;
        private void Start()
        {
            click3 = FindObjectOfType<click1>();
            speed1 = FindObjectOfType<input2>();
            convex = FindObjectOfType<lineconvex>();
            input1 = FindObjectOfType<input>();
            click2 = FindObjectOfType<click>();
            
            collision1 = FindObjectOfType<CollisionDetection1>();
            
                //定义包络体
            lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
                
            mesh = new Mesh();
           

            UnityEngine.Random.InitState(seed);
            //以ConvexCenterPoint点为中心向外发散得到随机点集空间（该空间为ToolPoint运动空间）
            for (int i = 0; i < 500; i++)
            {
                float x = ConvexCenterPoint.position.x + size * UnityEngine.Random.Range(-1.0f, 1.0f);
                float y = ConvexCenterPoint.position.y + size * UnityEngine.Random.Range(-1.0f, 1.0f);
                float z = ConvexCenterPoint.position.z + size * UnityEngine.Random.Range(-1.0f, 1.0f);

                vertices[i] = new Vertex3(x, y, z);

                meshVerts[i] = new Vector3(x, y, z);
                meshIndices[i] = i;
            }

            mesh.vertices = meshVerts;
            mesh.SetIndices(meshIndices, MeshTopology.Points, 0);
            //根据随机点来求解得到包络体
            hull = new ConvexHull3();
            hull.Generate(vertices);

            foreach (Simplex<Vertex3> f in hull.Simplexs)
            {

                j++;
            }

            //取得凸包络体的基础上得到凸包络体的顶点坐标
            
           
        }
        

        //根据凸包络体顶点生成ToolPoint运动轨迹
        void Update()
        {
            //if (click2.lister == 1)
            //{
            //    if (click3.lister == 0)
            //    {
            //        if (p == 0)
            //        {
            //            for (int k = 1; k < j; k++)
            //            {
            //                Simplex<Vertex3> v = hull.Simplexs[0];
            //                Poitn[0] = new Vector3(v.Vertices[0].X, v.Vertices[0].Y, v.Vertices[0].Z);
            //                Simplex<Vertex3> h = hull.Simplexs[k];

            //                MoveTime[k] = k / speed1.endValue3;
            //                Poitn[k] = new Vector3(h.Vertices[0].X, h.Vertices[0].Y, h.Vertices[0].Z);
            //                speed[k] = (Poitn[k] - Poitn[k - 1]) / MoveTime[k] / speed1.endValue3;

            //            }
            //            p++;
            //        }
            //        if (Id < j)
            //        {

            //            if (MoveTime[Id] > 0)
            //            {

            //                MoveTime[Id] -= Time.deltaTime;
            //                Poitn[Id] += speed[Id] * Time.deltaTime;
            //            }


            //            else
            //            {

            //                GameObject.Find(input1.endValue1).GetComponent<Transform>().position = Poitn[Id];
            //                if (collision1.collisionname.Length == 0)
            //                {

            //                    Id++;

            //                }
            //            }

            //        }
            //    }
            //}
        }


        private void OnPostRender()
        {
            if (convex.i >= 5000)
            {
                if (hull.Vertices.Count == 0) return;
                //控制包络体
                GL.PushMatrix();

                GL.LoadIdentity();
                GL.MultMatrix(GetComponent<Camera>().worldToCameraMatrix * rotation);
                GL.LoadProjectionMatrix(GetComponent<Camera>().projectionMatrix);

                lineMaterial.SetPass(0);
                GL.Begin(GL.LINES);
                GL.Color(Color.blue);
                //画出包络体
                foreach (Simplex<Vertex3> f in hull.Simplexs)
                {
                    DrawSimplex(f);

                }

                GL.End();

                GL.PopMatrix();
            }
        }
        

        private void DrawSimplex(Simplex<Vertex3> f)
        {
            GL.Vertex3(f.Vertices[0].X, f.Vertices[0].Y, f.Vertices[0].Z);
            GL.Vertex3(f.Vertices[1].X, f.Vertices[1].Y, f.Vertices[1].Z);

            GL.Vertex3(f.Vertices[0].X, f.Vertices[0].Y, f.Vertices[0].Z);
            GL.Vertex3(f.Vertices[2].X, f.Vertices[2].Y, f.Vertices[2].Z);

            GL.Vertex3(f.Vertices[1].X, f.Vertices[1].Y, f.Vertices[1].Z);
            GL.Vertex3(f.Vertices[2].X, f.Vertices[2].Y, f.Vertices[2].Z);
        }
        void OnDrawGizmos()
        {
            
                Gizmos.color = Color.red;
                for (int i = 0; i < j - 1; i++)
                {

                    Gizmos.DrawLine(Poitn[i], Poitn[i + 1]);

                }
              
        }

    }
}






      
    


