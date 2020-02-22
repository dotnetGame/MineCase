using MineCase.Block;
using MineCase.Util.Palette;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.World.Chunk
{
    public class ChunkSection
    {
        private readonly PalettedData<BlockState> _data = new PalettedData<BlockState>();

        public bool Empty { get; }
        public BlockState this[int x, int y, int z]
        {
            get => _data[GetIndex(x, y, z)];
            set => _data[GetIndex(x, y, z)] = value;
        }

        private static int GetIndex(int x, int y, int z)
        {
            return y << 8 | z << 4 | x;
        }
    }
}
