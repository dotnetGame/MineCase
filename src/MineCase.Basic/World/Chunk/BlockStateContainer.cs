using System;
using System.Text;
using MineCase.Util;

namespace MineCase.World.Chunk
{
    public class BlockStateContainer<T>
    {
        public BitArray Storage { get; set; }

        public int Bits { get; set; }

        // private IBlockStatePalette<T> palette;
        public int Count { get => Storage.Size(); }

        public int Length { get => Storage.Size(); }

        public int GetSerializedSize()
        {
            // return 1 + this.palette.GetSerializedSize() + VarInt.SizeOf((uint)_storage.Count) + _storage.GetBackingData().length * 8;
            return 0;
        }
    }
}
