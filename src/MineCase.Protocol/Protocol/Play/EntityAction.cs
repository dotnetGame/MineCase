using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x15)]
    public sealed class EntityAction : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EntityId;

        [SerializeAs(DataType.VarInt)]
        public uint ActionId;

        [SerializeAs(DataType.VarInt)]
        public uint JumpBoost;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EntityId, out _);
            bw.WriteAsVarInt((uint)ActionId, out _);
            bw.WriteAsVarInt(JumpBoost, out _);
        }
    }
}
