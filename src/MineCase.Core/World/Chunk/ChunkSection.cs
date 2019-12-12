using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Block;
using MineCase.World.Chunk;

namespace MineCase.World.Chunk
{
    public class ChunkSection
    {
        public static ChunkSection EmptySection = null;

        private BlockStateContainer<BlockState> _data;

        public bool IsEmpty()
        {
            return _data.Length == 0;
        }

        public int GetSize()
        {
            return 2 + _data.GetSerializedSize();
        }
    }
}
