using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login.Server
{
    [Packet(0x00, ProtocolType.Login, PacketDirection.ServerBound)]
    public sealed class LoginStart : ISerializablePacket
    {
        [SerializeAs(DataType.String)]
        public string Name;

        public void Deserialize(BinaryReader br)
        {
            Name = br.ReadAsString();
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(Name);
        }
    }
}
