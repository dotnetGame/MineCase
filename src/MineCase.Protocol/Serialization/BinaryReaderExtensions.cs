using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Protocol.Serialization
{
    internal static class BinaryReaderExtensions
    {
        public static sbyte ReadAsByte(this BinaryReader br) =>
            br.ReadSByte();

        public static bool ReadAsBoolean(this BinaryReader br) =>
            br.ReadBoolean();

        public static byte ReadAsUnsignedByte(this BinaryReader br) =>
            br.ReadByte();

        public static short ReadAsShort(this BinaryReader br) =>
            (short)br.ReadAsUnsignedShort();

        public static ushort ReadAsUnsignedShort(this BinaryReader br) =>
            br.ReadUInt16().ToBigEndian();

        public static int ReadAsInt(this BinaryReader br) =>
            (int)br.ReadAsUnsignedInt();

        public static uint ReadAsUnsignedInt(this BinaryReader br) =>
            br.ReadUInt32().ToBigEndian();

        public static long ReadAsLong(this BinaryReader br)
        {
            var value = br.ReadUInt64();
            return (long)value.ToBigEndian();
        }

        public static ulong ReadAsUnsignedLong(this BinaryReader br)
        {
            var value = br.ReadUInt64();
            return value.ToBigEndian();
        }

        // http://wiki.vg/Protocol#VarInt_and_VarLong"
        public static int ReadAsVarInt(this BinaryReader br, out int bytesRead)
        {
            int numRead = 0;
            uint result = 0;
            byte read;
            do
            {
                read = br.ReadByte();
                uint value = (uint)(read & 0b01111111);
                result |= value << (7 * numRead);

                numRead++;
                if (numRead > 5)
                    throw new InvalidDataException("VarInt is too big");
            }
            while ((read & 0b10000000) != 0);

            bytesRead = numRead;
            return (int)result;
        }

        public static string ReadAsString(this BinaryReader br)
        {
            var len = br.ReadAsVarInt(out _);
            var bytes = br.ReadBytes((int)len);
            return Encoding.UTF8.GetString(bytes);
        }
    }

    internal static class StreamExtensions
    {
        public static async Task ReadExactAsync(this Stream stream, byte[] buffer, int offset, int count)
        {
            while (count != 0)
            {
                var numRead = await stream.ReadAsync(buffer, offset, count);
                if (numRead == 0)
                    throw new EndOfStreamException();
                offset += numRead;
                count -= numRead;
            }
        }
    }
}
