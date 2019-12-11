using System;
using System.Text;
using MineCase.Util;

namespace MineCase.World.Chunk
{
    public class BlockStateContainer<T>
    {
        protected BitArray _storage;

        // private IBlockStatePalette<T> palette;

        public int Count { get => _storage.Size(); }

        public int Length { get => _storage.Size(); }

        public int GetSerializedSize()
        {
            // return 1 + this.palette.GetSerializedSize() + VarInt.SizeOf((uint)_storage.Count) + _storage.GetBackingData().length * 8;
            return 0;
        }
    }
}
