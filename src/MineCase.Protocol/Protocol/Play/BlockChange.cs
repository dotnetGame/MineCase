using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x0C)]
    public sealed class BlockChange : ISerializablePacket
    {
        [SerializeAs(DataType.Position)]
        public Position Location;

        [SerializeAs(DataType.VarInt)]
        public uint BlockId;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsPosition(Location);
            bw.WriteAsVarInt(BlockId, out _);
        }
    }
}
