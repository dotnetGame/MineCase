using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x00)]
    public sealed class SpawnObject : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EID;

        [SerializeAs(DataType.UUID)]
        public Guid ObjectUUID;

        [SerializeAs(DataType.VarInt)]
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

        [SerializeAs(DataType.Int)]
        public int Data;

        [SerializeAs(DataType.Short)]
        public short VelocityX;

        [SerializeAs(DataType.Short)]
        public short VelocityY;

        [SerializeAs(DataType.Short)]
        public short VelocityZ;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EID, out _);
            bw.WriteAsUUID(ObjectUUID);
            bw.WriteAsVarInt(Type, out _);
            bw.WriteAsDouble(X);
            bw.WriteAsDouble(Y);
            bw.WriteAsDouble(Z);
            bw.WriteAsByte(Pitch);
            bw.WriteAsByte(Yaw);
            bw.WriteAsInt(Data);
            bw.WriteAsShort(VelocityX);
            bw.WriteAsShort(VelocityY);
            bw.WriteAsShort(VelocityZ);
        }
    }
}
