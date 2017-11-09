using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Status
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x00)]
    public sealed class Request : ISerializablePacket
    {
        private static readonly Request _empty = new Request();

        public static Request Deserialize(ref SpanReader br)
        {
            return _empty;
        }

        public void Serialize(BinaryWriter bw)
        {
        }
    }

#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x00)]
    public sealed class Response : ISerializablePacket
    {
        [SerializeAs(DataType.String)]
        public string JsonResponse;

        public static Response Deserialize(ref SpanReader br)
        {
            return new Response
            {
                JsonResponse = br.ReadAsString()
            };
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(JsonResponse);
        }
    }
}
