using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.World.Chunk
{
    public interface IBlockStatePalette<T>
    {
        int IdFor(T state);

        bool Contains(T obj);

        T IBlockStatePalette(int indexKey);

        T Get(int indexKey);

        // void Read(PacketBuffer buf);

        // void Write(PacketBuffer buf);

        int GetSerializedSize();

        void Read(Nbt.Tags.NbtList nbt);
    }
}
