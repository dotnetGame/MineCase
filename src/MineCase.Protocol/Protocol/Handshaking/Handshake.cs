using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Handshaking
{
    [Packet(0x00)]
    [GenerateSerializer]
    public sealed partial class Handshake : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint ProtocolVersion;

        [SerializeAs(DataType.String)]
        public string ServerAddress;

        [SerializeAs(DataType.UnsignedShort)]
        public ushort ServerPort;

        [SerializeAs(DataType.VarInt)]
        public uint NextState;
    }
}
