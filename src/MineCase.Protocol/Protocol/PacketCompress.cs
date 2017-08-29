using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol
{
    public static class PacketCompress
    {
        public static UncompressedPacket Decompress(ref CompressedPacket packet)
        {
            throw new NotImplementedException();
            /*
            var targetPacket = default(UncompressedPacket);
            using (var br = new BinaryReader(new DeflateStream(new MemoryStream(packet.CompressedData), CompressionMode.Decompress)))
            {
                targetPacket.PacketId = br.ReadAsVarInt(out var packetIdLen);
                targetPacket.Data = br.ReadBytes((int)packet.DataLength - packetIdLen);
            }

            targetPacket.Length = packet.DataLength;
            return targetPacket;*/
        }

        public static CompressedPacket Compress(ref UncompressedPacket packet)
        {
            throw new NotImplementedException();
            /*
            var targetPacket = default(CompressedPacket);
            using (var stream = new MemoryStream())
            using (var bw = new BinaryWriter(new DeflateStream(stream, CompressionMode.Compress)))
            {
                bw.WriteAsVarInt(packet.PacketId, out var packetIdLen);
                bw.Write(packet.Data);
                bw.Flush();

                targetPacket.DataLength = packetIdLen + (uint)packet.Data.Length;
                targetPacket.CompressedData = stream.ToArray();
            }

            return targetPacket;*/
        }
    }
}
