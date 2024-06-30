using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using HullDelaunayVoronoi.Hull;
using HullDelaunayVoronoi.Primitives;


namespace HullDelaunayVoronoi
{

    public class ExampleConvexHull3D : MonoBehaviour
    {

        public int NumberOfVertices = 1000;

        public float size = 5;

        public int seed = 0;

        private Material lineMaterial;

        

        private Mesh mesh;

        private Matrix4x4 rotation = Matrix4x4.identity;

        public Transform target;

        private ConvexHull3 hull;

        private void Start()
        {
           
            
            lineMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));

            mesh = new Mesh();
            Vertex3[] vertices = new Vertex3[NumberOfVertices];

            Vector3[] meshVerts = new Vector3[NumberOfVertices];
            int[] meshIndices = new int[NumberOfVertices];

            Random.InitState(seed);
            for (int i = 0; i < NumberOfVertices; i++)
            {
                float x =target.position.x+size * Random.Range(-1.0f, 1.0f);
                float y = target.position.y+size * Random.Range(-1.0f, 1.0f);
                float z = target.position.z+size * Random.Range(-1.0f, 1.0f);

                vertices[i] = new Vertex3(x, y, z);

                meshVerts[i] = new Vector3(x, y, z);
                meshIndices[i] = i;
            }

            mesh.vertices = meshVerts;
            mesh.SetIndices(meshIndices, MeshTopology.Points, 0);
            //根据随机点来求解得到包络体
            hull = new ConvexHull3();
            hull.Generate(vertices);
           

        }

        private void Update()
        {
           
            //Graphics.DrawMesh(mesh, rotation, lineMaterial, 0,Camera.main);

        }
        
        //画出包络体
        private void OnPostRender()
        {

            if (hull == null || hull.Simplexs.Count == 0 || hull.Vertices.Count == 0) return;
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



















