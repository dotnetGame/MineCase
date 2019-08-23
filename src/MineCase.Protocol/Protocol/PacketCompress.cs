using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Buffers;
using MineCase.Serialization;
using SharpCompress.Compressors;
using SharpCompress.Compressors.Deflate;

namespace MineCase.Protocol
{
    public static class PacketCompress
    {
        public static UncompressedPacket Decompress(CompressedPacket packet, IBufferPoolScope<byte> bufferPool, uint threshold, UncompressedPacket targetPacket = null)
        {
            if (packet.DataLength != 0 && packet.DataLength < threshold)
                throw new InvalidDataException("Uncompressed data length is lower than threshold.");
            bool useCompression = packet.DataLength != 0;
            var dataLength = useCompression ? packet.DataLength : (uint)packet.CompressedData.Length;

            targetPacket = targetPacket ?? new UncompressedPacket();
            using (var stream = new MemoryStream(packet.CompressedData))
            using (var br = new BinaryReader(useCompression ? (Stream)new ZlibStream(stream, CompressionMode.Decompress, CompressionLevel.BestSpeed) : stream))
            {
                targetPacket.PacketId = br.ReadAsVarInt(out var packetIdLen);

                targetPacket.Data = bufferPool.Rent((int)(dataLength - packetIdLen));
                br.Read(targetPacket.Data.Array, targetPacket.Data.Offset, targetPacket.Data.Count);
            }

            targetPacket.Length = packet.DataLength;
            return targetPacket;
        }

        public static CompressedPacket Compress(UncompressedPacket packet, IBufferPoolScope<byte> bufferPool, uint threshold)
        {
            var targetPacket = new CompressedPacket();
            using (var stream = new MemoryStream())
            {
                var dataLength = packet.PacketId.SizeOfVarInt() + (uint)packet.Data.Count;
                bool useCompression = dataLength >= threshold;
                targetPacket.DataLength = useCompression ? dataLength : 0;

                using (var bw = new BinaryWriter(useCompression ? (Stream)new ZlibStream(stream, CompressionMode.Compress, CompressionLevel.BestSpeed) : stream))
                {
                    bw.WriteAsVarInt(packet.PacketId, out _);
                    bw.Write(packet.Data.Array, packet.Data.Offset, packet.Data.Count);
                    bw.Flush();
                }

                targetPacket.CompressedData = stream.ToArray();
            }

            return targetPacket;
        }
    }
}
