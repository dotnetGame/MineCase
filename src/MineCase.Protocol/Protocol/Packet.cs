using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MineCase.Serialization;

namespace MineCase.Protocol.Protocol
{
    public class UncompressedPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint Length;

        [SerializeAs(DataType.VarInt)]
        public uint PacketId;

        [SerializeAs(DataType.ByteArray)]
        public byte[] Data;

        public async Task SerializeAsync(Stream stream)
        {
            
        }

        public static async Task<UncompressedPacket> DeserializeAsync(Stream stream)
        {
            
        }
    }

    public class CompressedPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint PacketLength;

        [SerializeAs(DataType.VarInt)]
        public uint DataLength;

        [SerializeAs(DataType.VarInt)]
        public byte[] CompressedData;

        public async Task SerializeAsync(Stream stream)
        {
            
        }

        public static async Task<CompressedPacket> DeserializeAsync(Stream stream)
        {
            
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class PacketAttribute : Attribute
    {
        public uint PacketId { get; }

        public PacketAttribute(uint packetId)
        {
            PacketId = packetId;
        }
    }

    public interface ISerializablePacket
    {
        void Serialize(BinaryWriter bw);

        void Deserialize(BinaryReader br);
    }
}
