using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x0F)]
    [GenerateSerializer]
    public sealed partial class ServerboundKeepAlive : IPacket
    {
        [SerializeAs(DataType.Long)]
        public long KeepAliveId;
    }

    [Packet(0x21)]
    [GenerateSerializer]
    public sealed partial class ClientboundKeepAlive : IPacket
    {
        [SerializeAs(DataType.Long)]
        public long KeepAliveId;
    }
}
