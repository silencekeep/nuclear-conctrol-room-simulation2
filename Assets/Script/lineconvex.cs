using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using HullDelaunayVoronoi.Hull;
using HullDelaunayVoronoi.Primitives;
namespace HullDelaunayVoronoi
{
    public class lineconvex : MonoBehaviour
    {
     
        public static int time;
      private Material lineMaterial;
      private Mesh mesh;
      private Matrix4x4 rotation = Matrix4x4.identity;
      private ConvexHull3 hull;

        click click3;
        Vertex3[] vertices = new Vertex3[5001];

        Vector3[] meshVerts = new Vector3[5001];
        int[] meshIndices = new int[5001];
        public int i = 0;
        dropdown dropdown1;
        private void Start()
        {
            dropdown1 = FindObjectOfType<dropdown>();
            lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));

            mesh = new Mesh();

            click3 = FindObjectOfType<click>();
                

            
            
           
            
        }
        private void Update()
        {
            if (i < 5001)
            {
                if (click3.lister == 1)
                {
                    float x = GameObject.Find(dropdown1.ValueChange).GetComponent<Transform>().position.x;
                    float y = GameObject.Find(dropdown1.ValueChange).GetComponent<Transform>().position.y;
                    float z = GameObject.Find(dropdown1.ValueChange).GetComponent<Transform>().position.z;
                    vertices[i] = new Vertex3(x, y, z);

                    meshVerts[i] = new Vector3(x, y, z);
                    meshIndices[i] = i;
                    i++;
                }
            }
            if (i >=5000)
            {
               hull = new ConvexHull3();
               hull.Generate(vertices);
             }
                    
            

        }


        
        //画出包络体
        private void OnPostRender()
        {

            //if (hull.Vertices.Count == 0) return;
               if (i<=5000) return;
            //控制包络体
               GL.PushMatrix();

                GL.LoadIdentity();
                GL.MultMatrix(GetComponent<Camera>().worldToCameraMatrix * rotation);
                GL.LoadProjectionMatrix(GetComponent<Camera>().projectionMatrix);

                lineMaterial.SetPass(0);
                GL.Begin(GL.LINES);
                GL.Color(Color.red);
                //画出包络体
                foreach (Simplex<Vertex3> f in hull.Simplexs)
                {
                    DrawSimplex(f);

                }

                GL.End();

                GL.PopMatrix();


            
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

    }
}

