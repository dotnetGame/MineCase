using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Protocol.Protocol;
using MineCase.Serialization;

namespace MineCase.Protocol.Protocol.Handshaking.Server
{
    [Packet(0x00, ProtocolType.Handshake, PacketDirection.ServerBound)]
    public sealed class Handshake : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public int ProtocolVersion;

        [SerializeAs(DataType.String)]
        public string ServerAddress;

        [SerializeAs(DataType.UnsignedShort)]
        public ushort ServerPort;

        [SerializeAs(DataType.VarInt)]
        public int NextState;

        public void Deserialize(BinaryReader br)
        {
            ProtocolVersion = br.ReadAsVarInt(out _);
            ServerAddress = br.ReadAsString();
            ServerPort = br.ReadAsUnsignedShort();
            NextState = br.ReadAsVarInt(out _);
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(ProtocolVersion, out _);
            bw.WriteAsString(ServerAddress);
            bw.WriteAsUnsignedShort(ServerPort);
            bw.WriteAsVarInt(NextState, out _);
        }
    }
}
