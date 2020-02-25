using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Protocol.Login.Client
{
    [Packet(0x04, ProtocolType.Login, PacketDirection.ClientBound)]
    public sealed class LoginPluginRequest : ISerializablePacket
    {
        public void Deserialize(BinaryReader br)
        {
            throw new NotImplementedException();
        }

        public void Serialize(BinaryWriter bw)
        {
            throw new NotImplementedException();
        }
    }
}
