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
        public List<UnityVoxelEngine.game.VertexPositionColorNormal> vertices = new List<UnityVoxelEngine.game.VertexPositionColorNormal>();
        public List<int> triangles = new List<int>();
        public List<Vector2> uv = new List<Vector2>();

        public List<Vector3> colVertices = new List<Vector3>();
        public List<int> colTriangles = new List<int>();

        public bool useRenderDataForCol;

        public MeshData() 
        {
        }

        public void AddQuadTriangles(Chunk chunk, short i1, short i2, short i3, short i4, short i5, short i6)
        {
            triangles.Add((short)(chunk.Index + i1));
            triangles.Add((short)(chunk.Index + i2));
            triangles.Add((short)(chunk.Index + i3));
            triangles.Add((short)(chunk.Index + i4));
            triangles.Add((short)(chunk.Index + i5));
            triangles.Add((short)(chunk.Index + i6));

            if (useRenderDataForCol)
            {
                colTriangles.Add((short)(chunk.Index + i1));
                colTriangles.Add((short)(chunk.Index + i2));
                colTriangles.Add((short)(chunk.Index + i3));
                colTriangles.Add((short)(chunk.Index + i4));
                colTriangles.Add((short)(chunk.Index + i5));
                colTriangles.Add((short)(chunk.Index + i6));
            }
            chunk.Index += 4;
        }

        public void AddVertex(Vector3 vertex)
        {
            if(useRenderDataForCol)
            {
                colVertices.Add(vertex);
            }
        }

        public void AddTriangle(int tri)
        {
            if(useRenderDataForCol)
            {
                colTriangles.Add(tri - (vertices.Count - colVertices.Count));
            }
        }
    }
}