using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityVoxelEngine
{
    public class MeshData
    {
        public VertexPositionColor[] vertices = new VertexPositionColor[20];
        public List<int> triangles = new List<int>();
        public List<Vector2> uv = new List<Vector2>();

        public List<Vector3> colVertices = new List<Vector3>();
        public List<int> colTriangles = new List<int>();

        public MeshData() 
        { 
        }

        public void AddQuadTriangles()
        {
            triangles.Add(vertices.Length - 4);
            triangles.Add(vertices.Length - 3);
            triangles.Add(vertices.Length - 2);
            
            triangles.Add(vertices.Length - 4);
            triangles.Add(vertices.Length - 2);
            triangles.Add(vertices.Length - 1);
                        
            //triangles[0] = (short)(vertices.Length - 4);
            //triangles[1] = (short)(vertices.Length - 3);
            //triangles[2] = (short)(vertices.Length - 2);

            //triangles[3] = (short)(vertices.Length - 4);
            //triangles[4] = (short)(vertices.Length - 2);
            //triangles[5] = (short)(vertices.Length - 1);
        }
    }
}