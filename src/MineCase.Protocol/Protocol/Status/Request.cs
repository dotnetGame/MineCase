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
        private static readonly Request _empty = new Request();

        public static Request Deserialize(ref SpanReader br)
        {
            return _empty;
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
