using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login.Client
{
    [Packet(0x01, ProtocolType.Login, PacketDirection.ClientBound)]
    public sealed class EncryptionRequest : ISerializablePacket
    {
        [SerializeAs(DataType.String)]
        public string ServerID;

        [SerializeAs(DataType.VarInt)]
        public int PublicKeyLength;

        [SerializeAs(DataType.ByteArray)]
        public byte[] PublicKey;

        [SerializeAs(DataType.VarInt)]
        public int VerifyTokenLength;

        [SerializeAs(DataType.ByteArray)]
        public byte[] VerifyToken;

        public void Deserialize(BinaryReader br)
        {
            ServerID = br.ReadAsString();
            PublicKeyLength = br.ReadAsVarInt(out _);
            PublicKey = br.ReadAsByteArray(PublicKeyLength);
            VerifyTokenLength = br.ReadAsVarInt(out _);
            VerifyToken = br.ReadAsByteArray(VerifyTokenLength);
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(ServerID);
            bw.WriteAsVarInt(PublicKeyLength, out _);
            bw.WriteAsByteArray(PublicKey);
            bw.WriteAsVarInt(VerifyTokenLength, out _);
            bw.WriteAsByteArray(VerifyToken);
        }
    }
}
