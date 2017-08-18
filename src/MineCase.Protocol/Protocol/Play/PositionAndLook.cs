using MineCase.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MineCase.Protocol.Play
{
    [Packet(0x2E)]
    public sealed class ClientboundPositionAndLook : ISerializablePacket
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

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsDouble(X);
            bw.WriteAsDouble(Y);
            bw.WriteAsDouble(Z);
            bw.WriteAsFloat(Yaw);
            bw.WriteAsFloat(Pitch);
            bw.WriteAsByte(Flags);
            bw.WriteAsVarInt(TeleportId, out _);
        }
    }

    [Packet(0x0F)]
    public sealed class ServerboundPositionAndLook
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

        public static ServerboundPositionAndLook Deserialize(BinaryReader br)
        {
            return new ServerboundPositionAndLook
            {
                X = br.ReadAsDouble(),
                FeetY = br.ReadAsDouble(),
                Z = br.ReadAsDouble(),
                Yaw = br.ReadAsFloat(),
                Pitch = br.ReadAsFloat(),
                OnGround = br.ReadAsBoolean()
            };
        }
    }
}
