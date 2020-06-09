using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x0A)]
    [GenerateSerializer]
    public sealed partial class ServerboundCloseWindow : IPacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;
    }

    [Packet(0x14)]
    [GenerateSerializer]
    public sealed partial class ClientboundCloseWindow : IPacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;
    }
}
