using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x35)]
    public sealed class EntityHeadLook : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EID;

        [SerializeAs(DataType.Angle)]
        public byte Yaw;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EID, out _);
            bw.WriteAsByte((sbyte)Yaw);
        }
    }
}
