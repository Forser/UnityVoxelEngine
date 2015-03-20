using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityVoxelEngine
{
    public class Chunk
    {
        Block[, ,] blocks;
        public static int chunkSize = 16;
        public bool update = true;
        public short Index;

        public MeshData Start()
        {
            blocks = new Block[chunkSize, chunkSize, chunkSize];

            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    for (int z = 0; z < chunkSize; z++)
                    {
                        blocks[x, y, z] = new BlockAir();
                    }
                }
            }

            blocks[1, 1, 1] = new BlockDirt();
            //blocks[2, 1, 1] = new BlockStone();

            return UpdateChunk();
        }

        public Block GetBlock(int x, int y, int z)
        {
            return blocks[x, y, z];
        }

        MeshData UpdateChunk()
        {
            MeshData meshData = new MeshData();

            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    for (int z = 0; z < chunkSize; z++)
                    {
                        meshData = blocks[x, y, z].Blockdata(this, x, y, z, meshData);
                    }
                }
            }

            return meshData;
        }
    }
}