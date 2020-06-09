using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x57)]
    [GenerateSerializer]
    public sealed partial class EntityTeleport : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EID;

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

        [SerializeAs(DataType.Boolean)]
        public bool OnGround;
    }
}
