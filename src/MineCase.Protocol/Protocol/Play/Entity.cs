using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x25)]
    public sealed class Entity : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EID;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EID, out _);
        }
    }
}
