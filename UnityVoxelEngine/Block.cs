using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityVoxelEngine
{
    public class Block
    {
        public enum Direction { north, east, south, west, up, down };

        public Block()
        {

        }

        public virtual MeshData Blockdata(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            if (!chunk.GetBlock(x, y+1, z).IsSolid(Direction.down))
            {
                meshData = FaceDataUp(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y-1, z).IsSolid(Direction.up))
            {
                meshData = FaceDataDown(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y, z+1).IsSolid(Direction.south))
            {
                meshData = FaceDataNorth(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y, z-1).IsSolid(Direction.north))
            {
                meshData = FaceDataSouth(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x+1, y, z).IsSolid(Direction.west))
            {
                meshData = FaceDataEast(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x-1, y, z).IsSolid(Direction.east))
            {
                meshData = FaceDataWest(chunk, x, y, z, meshData);
            }
            return meshData;
        }

        protected MeshData FaceDataUp(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices[0].Position = new Vector3(x - 0.5f, y + 0.5f, z + 0.5f);
            meshData.vertices[0].Color = Color.Purple;
            meshData.vertices[1].Position = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
            meshData.vertices[1].Color = Color.Blue;
            meshData.vertices[2].Position = new Vector3(x + 0.5f, y + 0.5f, z - 0.5f);
            meshData.vertices[2].Color = Color.Green;
            meshData.vertices[3].Position = new Vector3(x - 0.5f, y + 0.5f, z - 0.5f);
            meshData.vertices[3].Color = Color.Yellow;
                        
            meshData.AddQuadTriangles();

            return meshData;
        }

        protected MeshData FaceDataDown(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices[4] = new VertexPositionColor(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), Color.Pink);
            meshData.vertices[5] = new VertexPositionColor(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), Color.Blue);
            meshData.vertices[6] = new VertexPositionColor(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), Color.Green);
            meshData.vertices[7] = new VertexPositionColor(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), Color.Yellow);

            meshData.AddQuadTriangles();

            return meshData;
        }

        protected MeshData FaceDataNorth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices[0] = new VertexPositionColor(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), Color.Orange);
            meshData.vertices[1] = new VertexPositionColor(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), Color.Blue);
            meshData.vertices[2] = new VertexPositionColor(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), Color.Green);
            meshData.vertices[3] = new VertexPositionColor(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), Color.Yellow);

            meshData.AddQuadTriangles();

            return meshData;
        }

        protected MeshData FaceDataEast(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices[0] = new VertexPositionColor(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), Color.White);
            meshData.vertices[1] = new VertexPositionColor(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), Color.Blue);
            meshData.vertices[2] = new VertexPositionColor(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), Color.Green);
            meshData.vertices[3] = new VertexPositionColor(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), Color.Yellow);

            meshData.AddQuadTriangles();

            return meshData;
        }

        protected MeshData FaceDataSouth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices[0] = new VertexPositionColor(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), Color.Brown);
            meshData.vertices[1] = new VertexPositionColor(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), Color.Blue);
            meshData.vertices[2] = new VertexPositionColor(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), Color.Green);
            meshData.vertices[3] = new VertexPositionColor(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), Color.Yellow);

            meshData.AddQuadTriangles();

            return meshData;
        }

        protected MeshData FaceDataWest(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.vertices[0] = new VertexPositionColor(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), Color.Black);
            meshData.vertices[1] = new VertexPositionColor(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), Color.Blue);
            meshData.vertices[2] = new VertexPositionColor(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), Color.Green);
            meshData.vertices[3] = new VertexPositionColor(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), Color.Yellow);

            meshData.AddQuadTriangles();

            return meshData;
        }

        public virtual bool IsSolid(Direction direction)
        {
            switch(direction)
            {
                case Direction.north:
                    return true;
                case Direction.east:
                    return true;
                case Direction.south:
                    return true;
                case Direction.west:
                    return true;
                case Direction.up:
                    return true;
                case Direction.down:
                    return true;
            }

            return false;
        }
    }
}