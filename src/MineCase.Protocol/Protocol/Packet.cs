using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using MineCase.Buffers;
using MineCase.Serialization;

namespace MineCase.Protocol
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    public class UncompressedPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint Length;

        [SerializeAs(DataType.VarInt)]
        public uint PacketId;

        [SerializeAs(DataType.ByteArray)]
        public ArraySegment<byte> Data;

        public async Task SerializeAsync(Stream stream)
        {
            Length = (uint)Data.Count + PacketId.SizeOfVarInt();

            using (var bw = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                bw.WriteAsVarInt(Length, out _);
                bw.WriteAsVarInt(PacketId, out _);
                bw.Flush();
            }

            await stream.WriteAsync(Data.Array, Data.Offset, Data.Count);
        }

        public static async Task<UncompressedPacket> DeserializeAsync(Stream stream, IBufferPoolScope<byte> bufferPool, UncompressedPacket packet = null)
        {
            packet = packet ?? new UncompressedPacket();
            int packetIdLen;
            using (var br = new BinaryReader(stream, Encoding.UTF8, true))
            {
                packet.Length = br.ReadAsVarInt(out _);
                packet.PacketId = br.ReadAsVarInt(out packetIdLen);
            }

            packet.Data = bufferPool.Rent((int)(packet.Length - packetIdLen));
            await stream.ReadExactAsync(packet.Data.Array, packet.Data.Offset, packet.Data.Count);
            return packet;
        }
    }

#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
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
            PacketLength = (uint)CompressedData.Length + DataLength.SizeOfVarInt();

            using (var bw = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                bw.WriteAsVarInt(PacketLength, out _);
                bw.WriteAsVarInt(DataLength, out _);
                bw.Flush();
            }

            await stream.WriteAsync(CompressedData, 0, CompressedData.Length);
        }

        public static async Task<CompressedPacket> DeserializeAsync(Stream stream, CompressedPacket packet = null)
        {
            packet = packet ?? new CompressedPacket();
            int dataLengthLen;
            using (var br = new BinaryReader(stream, Encoding.UTF8, true))
            {
                packet.PacketLength = br.ReadAsVarInt(out _);
                packet.DataLength = br.ReadAsVarInt(out dataLengthLen);
            }

            packet.CompressedData = new byte[packet.PacketLength - dataLengthLen];
            await stream.ReadExactAsync(packet.CompressedData, 0, packet.CompressedData.Length);
            return packet;
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
    }
}
