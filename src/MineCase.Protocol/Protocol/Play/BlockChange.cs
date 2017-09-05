using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x0B)]
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
