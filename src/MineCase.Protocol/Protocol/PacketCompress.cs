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
        public static UncompressedPacket Decompress(CompressedPacket packet, uint threshold)
        {
            if (packet.DataLength != 0 && packet.DataLength < threshold)
                throw new InvalidDataException("Uncompressed data length is lower than threshold.");
            bool useCompression = packet.DataLength != 0;
            var dataLength = useCompression ? packet.DataLength : (uint)packet.CompressedData.Length;

            var targetPacket = new UncompressedPacket();
            using (var stream = new MemoryStream(packet.CompressedData))
            using (var br = new BinaryReader(useCompression ? (Stream)new ZlibStream(stream, CompressionMode.Decompress, CompressionLevel.BestSpeed) : stream))
            {
                targetPacket.PacketId = br.ReadAsVarInt(out var packetIdLen);

                targetPacket.Data = new byte[(int)(dataLength - packetIdLen)];
                br.Read(targetPacket.Data, 0, targetPacket.Data.Length);
            }

            targetPacket.Length = packet.DataLength;
            return targetPacket;
        }

        public static CompressedPacket Compress(UncompressedPacket packet, uint threshold)
        {
            var targetPacket = new CompressedPacket();
            using (var stream = new MemoryStream())
            {
                var dataLength = packet.PacketId.SizeOfVarInt() + (uint)packet.Data.Length;
                bool useCompression = dataLength >= threshold;
                targetPacket.DataLength = useCompression ? dataLength : 0;

                using (var bw = new BinaryWriter(useCompression ? (Stream)new ZlibStream(stream, CompressionMode.Compress, CompressionLevel.BestSpeed) : stream))
                {
                    bw.WriteAsVarInt(packet.PacketId, out _);
                    bw.Write(packet.Data, 0, packet.Data.Length);
                    bw.Flush();
                }

                targetPacket.CompressedData = stream.ToArray();
            }

            return targetPacket;
        }
    }
}
