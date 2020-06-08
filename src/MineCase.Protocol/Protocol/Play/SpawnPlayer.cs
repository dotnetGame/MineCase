using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x05)]
    public sealed class SpawnPlayer : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EntityId;

        [SerializeAs(DataType.UUID)]
        public Guid PlayerUUID;

        [SerializeAs(DataType.Double)]
        public double X;

        [SerializeAs(DataType.Double)]
        public double Y;

        [SerializeAs(DataType.Double)]
        public double Z;

        [SerializeAs(DataType.Angle)]
        public Angle Yaw;

        [SerializeAs(DataType.Angle)]
        public Angle Pitch;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EntityId, out _);
            bw.WriteAsUUID(PlayerUUID);
            bw.WriteAsDouble(X);
            bw.WriteAsDouble(Y);
            bw.WriteAsDouble(Z);
            bw.WriteAsAngle(Yaw);
            bw.WriteAsAngle(Pitch);
        }
    }
}
