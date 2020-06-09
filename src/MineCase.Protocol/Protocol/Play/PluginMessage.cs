using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x0A)]
    [GenerateSerializer]
    public sealed partial class ServerboundPluginMessage : IPacket
    {
        [SerializeAs(DataType.String)]
        public string Channel;

        [SerializeAs(DataType.ByteArray)]
        public byte[] Data;
    }
}
