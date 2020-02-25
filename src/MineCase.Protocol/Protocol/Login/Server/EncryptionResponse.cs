using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login.Server
{
    [Packet(0x02, ProtocolType.Login, PacketDirection.ServerBound)]
    public sealed class EncryptionResponse : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public int SharedSecretLength;

        [SerializeAs(DataType.ByteArray)]
        public byte[] SharedSecret;

        [SerializeAs(DataType.VarInt)]
        public int VerifyTokenLength;

        [SerializeAs(DataType.ByteArray)]
        public byte[] VerifyToken;

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
