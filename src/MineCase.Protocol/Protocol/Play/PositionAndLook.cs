using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x36)]
    [GenerateSerializer]
    public sealed partial class ClientboundPositionAndLook : IPacket
    {
        [SerializeAs(DataType.Double)]
        public double X;

        [SerializeAs(DataType.Double)]
        public double Y;

        [SerializeAs(DataType.Double)]
        public double Z;

        [SerializeAs(DataType.Float)]
        public float Yaw;

        [SerializeAs(DataType.Float)]
        public float Pitch;

        [SerializeAs(DataType.Byte)]
        public byte Flags;

        [SerializeAs(DataType.VarInt)]
        public uint TeleportId;
    }

    [Packet(0x12)]
    [GenerateSerializer]
    public sealed partial class ServerboundPositionAndLook : IPacket
    {
        [SerializeAs(DataType.Double)]
        public double X;

        [SerializeAs(DataType.Double)]
        public double FeetY;

        [SerializeAs(DataType.Double)]
        public double Z;

        [SerializeAs(DataType.Float)]
        public float Yaw;

        [SerializeAs(DataType.Float)]
        public float Pitch;

        [SerializeAs(DataType.Boolean)]
        public bool OnGround;
    }
}
