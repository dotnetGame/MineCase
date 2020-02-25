using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Status.Client
{
    [Packet(0x00, ProtocolType.Status, PacketDirection.ClientBound)]
    public sealed class Response : ISerializablePacket
    {
        [SerializeAs(DataType.String)]
        public string JsonResponse;

        public void Deserialize(BinaryReader br)
        {
            JsonResponse = br.ReadAsString();
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(JsonResponse);
        }
    }
}
