using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login.Client
{
    [Packet(0x02, ProtocolType.Login, PacketDirection.ClientBound)]
    public sealed class LoginSuccess : ISerializablePacket
    {
        [SerializeAs(DataType.String)]
        public string UUID;

        [SerializeAs(DataType.String)]
        public string Username;

        public void Deserialize(BinaryReader br)
        {
            UUID = br.ReadAsString();
            Username = br.ReadAsString();
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(UUID);
            bw.WriteAsString(Username);
        }
    }
}
