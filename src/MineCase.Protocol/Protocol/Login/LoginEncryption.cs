using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login
{
    [Packet(0x01)]
    [GenerateSerializer]
    public sealed class EncryptionRequest : ISerializablePacket
    {
        [SerializeAs(DataType.String)]
        public string ServerID;

        [SerializeAs(DataType.VarInt)]
        public uint PublicKeyLength;

        [SerializeAs(DataType.ByteArray)]
        public byte[] PublicKey;

        [SerializeAs(DataType.VarInt)]
        public uint VerifyTokenLength;

        [SerializeAs(DataType.ByteArray)]
        public byte[] VerifyToken;

        public static EncryptionRequest Deserialize(ref SpanReader br)
        {
            string serverID = br.ReadAsString();
            uint publicKeyLength = br.ReadAsVarInt(out _);
            byte[] publicKey = br.ReadAsByteArray((int)publicKeyLength);
            uint verifyTokenLength = br.ReadAsVarInt(out _);
            byte[] verifyToken = br.ReadAsByteArray((int)verifyTokenLength);

            return new EncryptionRequest
            {
                ServerID = serverID,
                PublicKeyLength = publicKeyLength,
                PublicKey = publicKey,
                VerifyTokenLength = verifyTokenLength,
                VerifyToken = verifyToken
            };
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

    [Packet(0x01)]
    public sealed class EncryptionResponse : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint SharedSecretLength;

        [SerializeAs(DataType.ByteArray)]
        public byte[] SharedSecret;

        [SerializeAs(DataType.VarInt)]
        public uint VerifyTokenLength;

        [SerializeAs(DataType.ByteArray)]
        public byte[] VerifyToken;

        public static EncryptionResponse Deserialize(ref SpanReader br)
        {
            uint sharedSecretLength = br.ReadAsVarInt(out _);
            byte[] sharedSecret = br.ReadAsByteArray((int)sharedSecretLength);
            uint verifyTokenLength = br.ReadAsVarInt(out _);
            byte[] verifyToken = br.ReadAsByteArray((int)verifyTokenLength);

            return new EncryptionResponse
            {
                SharedSecretLength = sharedSecretLength,
                SharedSecret = sharedSecret,
                VerifyTokenLength = verifyTokenLength,
                VerifyToken = verifyToken
            };
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(SharedSecretLength, out _);
            bw.WriteAsByteArray(SharedSecret);
            bw.WriteAsVarInt(VerifyTokenLength, out _);
            bw.WriteAsByteArray(VerifyToken);
        }
    }
}
