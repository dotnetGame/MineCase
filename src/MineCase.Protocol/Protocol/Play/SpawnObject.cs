using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x00)]
    [GenerateSerializer]
    public sealed partial class SpawnObject : IPacket
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
        public Angle Pitch;

        [SerializeAs(DataType.Angle)]
        public Angle Yaw;

        [SerializeAs(DataType.Int)]
        public int Data;

        [SerializeAs(DataType.Short)]
        public short VelocityX;

        [SerializeAs(DataType.Short)]
        public short VelocityY;

        [SerializeAs(DataType.Short)]
        public short VelocityZ;
    }
}
