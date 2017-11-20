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
