using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MineCase.Serialization;

namespace MineCase.Protocol.Protocol
{
    public class RawPacket
    {
        [SerializeAs(DataType.VarInt)]
        public int Length;

        [SerializeAs(DataType.ByteArray)]
        public byte[] RawData;

        public async Task DeserializeAsync(Stream stream)
        {
            using (BinaryReader br = new BinaryReader(stream, Encoding.UTF8, true))
            {
                Length = br.ReadAsVarInt(out _);
                Protocol.ValidatePacketLength(Length);
            }

            RawData = new byte[Length];
            await stream.ReadExactAsync(RawData, 0, Length);
        }

        public async Task SerializeAsync(Stream stream)
        {
            using (BinaryWriter bw = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                Protocol.ValidatePacketLength(Length);
                bw.WriteAsVarInt(Length, out _);
                bw.Flush();
            }

            if (RawData != null)
            {
                await stream.WriteAsync(RawData, 0, Length);
                // System.Console.WriteLine($"Write packet length to stream: {Length}");
            }

            // System.Console.WriteLine($"Write packet length: {Length}");
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class PacketAttribute : Attribute
    {
        public uint PacketId { get; }

        public ProtocolType PacketType { get; }

        public PacketDirection Direction { get; }

        public PacketAttribute(uint packetId, ProtocolType packetType, PacketDirection direction)
        {
            PacketId = packetId;
            PacketType = packetType;
            Direction = direction;
        }
    }

    public interface ISerializablePacket
    {
        void Serialize(BinaryWriter bw);

        void Deserialize(BinaryReader br);
    }
}
