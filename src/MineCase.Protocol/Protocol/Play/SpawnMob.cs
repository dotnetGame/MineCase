using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x03)]
    public sealed class SpawnMob : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EID;

        [SerializeAs(DataType.UUID)]
        public Guid EntityUUID;

        [SerializeAs(DataType.Byte)]
        public byte Type;

        [SerializeAs(DataType.Double)]
        public double X;

        [SerializeAs(DataType.Double)]
        public double Y;

        [SerializeAs(DataType.Double)]
        public double Z;

        [SerializeAs(DataType.Angle)]
        public byte Pitch;

        [SerializeAs(DataType.Angle)]
        public byte Yaw;

        [SerializeAs(DataType.Angle)]
        public byte HeadPitch;

        [SerializeAs(DataType.Short)]
        public short VelocityX;

        [SerializeAs(DataType.Short)]
        public short VelocityY;

        [SerializeAs(DataType.Short)]
        public short VelocityZ;

        [SerializeAs(DataType.ByteArray)]
        public byte[] Metadata;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EID, out _);
            bw.WriteAsUUID(EntityUUID);
            bw.WriteAsByte(Type);
            bw.WriteAsDouble(X);
            bw.WriteAsDouble(Y);
            bw.WriteAsDouble(Z);
            bw.WriteAsByte(Yaw);
            bw.WriteAsByte(Pitch);
            bw.WriteAsByte(HeadPitch);
            bw.WriteAsShort(VelocityX);
            bw.WriteAsShort(VelocityY);
            bw.WriteAsShort(VelocityZ);
            bw.WriteAsByteArray(Metadata);
        }
    }
}
