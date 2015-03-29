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
        public struct Tile { public int x; public int y; }
        const float tileSize = 0.25f;

        public Block()
        {

        }

        public virtual Tile TexturePosition(Direction direction)
        {
            Tile tile = new Tile();
            tile.x = 0;
            tile.y = 0;

            return tile;
        }

        public virtual Vector2[] FaceUVs(Direction direction)
        {
            Vector2[] UVs = new Vector2[4];
            Tile tilePos = TexturePosition(direction);

            UVs[0] = new Vector2(tileSize * tilePos.x + tileSize, tileSize * tilePos.y);
            UVs[1] = new Vector2(tileSize * tilePos.x + tileSize, tileSize * tilePos.y + tileSize);
            UVs[2] = new Vector2(tileSize * tilePos.x, tileSize * tilePos.y + tileSize);
            UVs[3] = new Vector2(tileSize * tilePos.x, tileSize * tilePos.y);

            return UVs;
        }

        public virtual MeshData Blockdata(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            //meshData.useRenderDataForCol = true;

            if (!chunk.GetBlock(x, y + 1, z).IsSolid(Direction.down))
            {
                meshData = FaceDataUp(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y - 1, z).IsSolid(Direction.up))
            {
                meshData = FaceDataDown(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y, z + 1).IsSolid(Direction.south))
            {
                meshData = FaceDataNorth(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x, y, z - 1).IsSolid(Direction.north))
            {
                meshData = FaceDataSouth(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x + 1, y, z).IsSolid(Direction.west))
            {
                meshData = FaceDataEast(chunk, x, y, z, meshData);
            }

            if (!chunk.GetBlock(x - 1, y, z).IsSolid(Direction.east))
            {
                meshData = FaceDataWest(chunk, x, y, z, meshData);
            }
            return meshData;
        }

        public List<UnityVoxelEngine.game.VertexPositionColorNormal> AddColorVertex(List<UnityVoxelEngine.game.VertexPositionColorNormal> vertices, Vector3 vector, Color color, Vector3 normal, Vector2 textureCoordinate)
        {
            vertices.Add(new UnityVoxelEngine.game.VertexPositionColorNormal(vector, color, normal, textureCoordinate));
            return vertices;
        }

        protected MeshData FaceDataUp(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            AddColorVertex(meshData.vertices, new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), Color.Purple, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), Color.Blue, new Vector3(0, 0, 0), new Vector2(1.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), Color.Green, new Vector3(0, 0, 0), new Vector2(0.0f, 1.0f));
            AddColorVertex(meshData.vertices, new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), Color.Yellow, new Vector3(0, 0, 0), new Vector2(1.0f, 1.0f));

            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));

            meshData.AddQuadTriangles(chunk, 0, 1, 2, 0, 2, 3);

            meshData.uv.AddRange(FaceUVs(Direction.up));

            return meshData;
        }

        protected MeshData FaceDataDown(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            AddColorVertex(meshData.vertices, new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), Color.Pink, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), Color.Blue, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), Color.Green, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), Color.Yellow, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));

            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));

            meshData.AddQuadTriangles(chunk, 0, 1, 2, 0, 2, 3);

            meshData.uv.AddRange(FaceUVs(Direction.down));

            return meshData;
        }

        protected MeshData FaceDataNorth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            AddColorVertex(meshData.vertices, new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), Color.Orange, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), Color.Blue, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), Color.Green, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), Color.Yellow, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));

            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));

            meshData.AddQuadTriangles(chunk, 0, 1, 2, 0, 2, 3);

            meshData.uv.AddRange(FaceUVs(Direction.north));

            return meshData;
        }
        protected MeshData FaceDataSouth(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            AddColorVertex(meshData.vertices, new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), Color.Brown, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), Color.Blue, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), Color.Green, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), Color.Yellow, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));

            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles(chunk, 0, 1, 2, 0, 2, 3);

            meshData.uv.AddRange(FaceUVs(Direction.south));

            return meshData;
        }

        protected MeshData FaceDataEast(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            AddColorVertex(meshData.vertices, new Vector3(x + 0.5f, y - 0.5f, z - 0.5f), Color.White, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x + 0.5f, y + 0.5f, z - 0.5f), Color.Blue, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), Color.Green, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x + 0.5f, y - 0.5f, z + 0.5f), Color.Yellow, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));

            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

            meshData.AddQuadTriangles(chunk, 0, 1, 2, 0, 2, 3);

            meshData.uv.AddRange(FaceUVs(Direction.east));

            return meshData;
        }

        protected MeshData FaceDataWest(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            AddColorVertex(meshData.vertices, new Vector3(x - 0.5f, y - 0.5f, z - 0.5f), Color.Black, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x - 0.5f, y - 0.5f, z + 0.5f), Color.Blue, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x - 0.5f, y + 0.5f, z + 0.5f), Color.Green, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));
            AddColorVertex(meshData.vertices, new Vector3(x - 0.5f, y + 0.5f, z - 0.5f), Color.Yellow, new Vector3(0, 0, 0), new Vector2(0.0f, 0.0f));

            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
            meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));

            meshData.AddQuadTriangles(chunk, 0, 1, 2, 0, 2, 3);

            meshData.uv.AddRange(FaceUVs(Direction.west));

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