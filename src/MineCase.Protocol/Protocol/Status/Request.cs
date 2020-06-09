using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Status
{
    [Packet(0x00)]
    [GenerateSerializer]
    public sealed partial class Request : IPacket
    {
        public static readonly Request Empty = new Request();
    }

    [Packet(0x00)]
    [GenerateSerializer]
    public sealed partial class Response : IPacket
    {
        [SerializeAs(DataType.String)]
        public string JsonResponse;
    }
}
