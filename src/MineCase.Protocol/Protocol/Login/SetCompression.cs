using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login
{
    [Packet(0x03)]
    [GenerateSerializer]
    public sealed partial class SetCompression : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint Threshold;
    }
}
