using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityVoxelEngine
{
    public class MeshData
    {
        public List<VertexPositionColor> vertices = new List<VertexPositionColor>();
        public List<int> triangles = new List<int>();
        public List<Vector2> uv = new List<Vector2>();

        public List<Vector3> colVertices = new List<Vector3>();
        public List<int> colTriangles = new List<int>();

        public MeshData() 
        {
        }

        public void AddQuadTriangles()
        {
            Block myBlock = new Block();
            List<int> indices = new List<int>();

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < triangles.Count; i++)
                {
                    Vector3 p1 = vertices[triangles[i]].Position;
                    int p1I = (int)i++;
                    Vector3 p2 = vertices[triangles[i]].Position;
                    int p2I = (int)i++;
                    Vector3 p3 = vertices[triangles[i]].Position;
                    int p3I = (int)i;

                    int a = (int)vertices.Count;
                    vertices = myBlock.AddColorVertex(vertices, new Vector3((p1.X + p2.X) / 2,(p1.Y + p2.Y) / 2, (p1.Z + p2.Z) / 2), Color.Gray);

                    int b = (int)vertices.Count;
                    vertices = myBlock.AddColorVertex(vertices, new Vector3((p2.X + p3.X) / 2, (p2.Y + p3.Y) / 2, (p2.Z + p3.Z) / 2), Color.Gray);

                    int c = (int)vertices.Count;
                    vertices = myBlock.AddColorVertex(vertices, new Vector3((p1.X + p3.X) / 2, (p1.Y + p3.Y) / 2, (p1.Z + p3.Z) / 2), Color.Gray);

                    indices.Add(triangles[p1I]); indices.Add(a); indices.Add(c);
                    indices.Add(triangles[p2I]); indices.Add(b); indices.Add(a);
                    indices.Add(triangles[p3I]); indices.Add(c); indices.Add(b);
                    indices.Add(a); indices.Add(b); indices.Add(c);
                }

                triangles.Clear();
                foreach (int index in indices)
                {
                    triangles.Add(index);
                }
            }
        }
    }
}