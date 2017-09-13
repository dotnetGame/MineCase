using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x28)]
    public sealed class EntityLook : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EID;

        [SerializeAs(DataType.Angle)]
        public float Yaw;

        [SerializeAs(DataType.Angle)]
        public float Pitch;

        [SerializeAs(DataType.Boolean)]
        public bool OnGround;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EID, out _);
            bw.WriteAsFloat(Yaw);
            bw.WriteAsFloat(Pitch);
            bw.WriteAsBoolean(OnGround);
        }
    }
}
