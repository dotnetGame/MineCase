using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Status
{
    [Immutable]
    [Packet(0x00)]
    public sealed class Request
    {
        public static Request Deserialize(BinaryReader br)
        {
            return new Request();
        }
    }

    [Immutable]
    [Packet(0x00)]
    public sealed class Response : ISerializablePacket
    {
        [SerializeAs(DataType.String)]
        public string JsonResponse;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(JsonResponse);
        }
    }
}
