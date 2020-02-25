using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login.Server
{
    [Packet(0x01, ProtocolType.Login, PacketDirection.ServerBound)]
    public sealed class LoginPluginResponse : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public int MessageID;

        [SerializeAs(DataType.Boolean)]
        public bool Successful;

        [SerializeAs(DataType.ByteArray)]
        public byte[] Data;

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
