using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityVoxelEngine
{
    public class BlockDirt : Block
    {
        public BlockDirt()
            : base()
        { }

        public override MeshData Blockdata(Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            return meshData;
        }

        public override bool IsSolid(Block.Direction direction)
        {
            return false;
        }
    }
}