using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login
{
    [Packet(0x01)]
    [GenerateSerializer]
    public sealed partial class EncryptionRequest : IPacket
    {
        [SerializeAs(DataType.String)]
        public string ServerID;

        [SerializeAs(DataType.VarInt)]
        public uint PublicKeyLength;

        [SerializeAs(DataType.ByteArray, ArrayLengthMember = nameof(PublicKeyLength))]
        public byte[] PublicKey;

        [SerializeAs(DataType.VarInt)]
        public uint VerifyTokenLength;

        [SerializeAs(DataType.ByteArray, ArrayLengthMember = nameof(VerifyTokenLength))]
        public byte[] VerifyToken;
    }

    [Packet(0x01)]
    [GenerateSerializer]
    public sealed partial class EncryptionResponse : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint SharedSecretLength;

        [SerializeAs(DataType.ByteArray, ArrayLengthMember = nameof(SharedSecretLength))]
        public byte[] SharedSecret;

        [SerializeAs(DataType.VarInt)]
        public uint VerifyTokenLength;

        [SerializeAs(DataType.ByteArray, ArrayLengthMember = nameof(VerifyTokenLength))]
        public byte[] VerifyToken;
    }
}
