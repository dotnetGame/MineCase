using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x31)]
    public sealed class DestroyEntities : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint Count;

        [SerializeAs(DataType.Array)]
        public uint[] EntityIds;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(Count, out _);
            foreach (var eid in EntityIds)
                bw.WriteAsVarInt(eid, out _);
        }
    }
}
