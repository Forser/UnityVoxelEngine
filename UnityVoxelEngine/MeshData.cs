using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityVoxelEngine
{
    public class MeshData
    {
        public List<Vector3> vertices = new List<Vector3>();
        public List<int> triangles = new List<int>();
        public List<Vector2> uv = new List<Vector2>();

        public List<Vector3> colVertices = new List<Vector3>();
        public List<int> colTriangles = new List<int>();

        public MeshData() { }
    }
}