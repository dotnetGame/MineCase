using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x03)]
    [GenerateSerializer]
    public sealed partial class SpawnLivingEntity : IPacket
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
        public Angle Pitch;

        [SerializeAs(DataType.Angle)]
        public Angle Yaw;

        [SerializeAs(DataType.Angle)]
        public Angle HeadPitch;

        [SerializeAs(DataType.Short)]
        public short VelocityX;

        [SerializeAs(DataType.Short)]
        public short VelocityY;

        [SerializeAs(DataType.Short)]
        public short VelocityZ;

        [SerializeAs(DataType.ByteArray)]
        public byte[] Metadata;
    }
}
