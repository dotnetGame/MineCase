using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Status
{
    [Packet(0x00)]
    public sealed class Request : ISerializablePacket
    {
        public static readonly Request Empty = new Request();

        public static Request Deserialize(ref SpanReader br)
        {
            return Empty;
        }

        public void Serialize(BinaryWriter bw)
        {
        }
    }

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
