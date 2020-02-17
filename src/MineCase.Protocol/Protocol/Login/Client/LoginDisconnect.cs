using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Protocol.Login.Client
{
    [Packet(0x00, ProtocolType.Login, PacketDirection.ClientBound)]
    public sealed class LoginDisconnect : ISerializablePacket
    {
        [SerializeAs(DataType.Chat)]
        public string Reason;

        public void Deserialize(BinaryReader br)
        {
            Reason = br.ReadAsString();
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.Write(Reason);
        }
    }
}
