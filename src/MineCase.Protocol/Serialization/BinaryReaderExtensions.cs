using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Serialization
{
    internal static class BinaryReaderExtensions
    {
        public static byte ReadAsByte(this BinaryReader br) =>
            br.ReadByte();

        public static bool ReadAsBoolean(this BinaryReader br) =>
            br.ReadBoolean();

        // http://wiki.vg/Protocol#VarInt_and_VarLong"
        public static uint ReadAsVarInt(this BinaryReader br, out int bytesRead)
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
            return result;
        }

        public static string ReadAsString(this BinaryReader br)
        {
            var len = br.ReadAsVarInt(out _);
            var bytes = br.ReadBytes((int)len);
            return Encoding.UTF8.GetString(bytes);
        }

        public static Chat ReadAsChat(this BinaryReader br)
        {
            string str = br.ReadAsString();
            return Chat.Parse(str);
        }

        public static string ReadAsIdentifier(this BinaryReader br)
        {
            string str = br.ReadAsString();
            return str;
        }

        public static short ReadAsShort(this BinaryReader br) =>
            (short)br.ReadAsUnsignedShort();

        public static ushort ReadAsUnsignedShort(this BinaryReader br)
        {
            var value = br.ReadUInt16();
            return value.ToBigEndian();
        }

        public static uint ReadAsUnsignedInt(this BinaryReader br)
        {
            var value = br.ReadUInt32();
            return value.ToBigEndian();
        }

        public static int ReadAsInt(this BinaryReader br) =>
            (int)br.ReadAsUnsignedInt();

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

        public static float ReadAsFloat(this BinaryReader br)
        {
            var value = br.ReadAsUnsignedInt();
            return Unsafe.As<uint, float>(ref value);
        }

        public static double ReadAsDouble(this BinaryReader br)
        {
            var value = br.ReadAsUnsignedLong();
            return Unsafe.As<ulong, double>(ref value);
        }

        public static Position ReadAsPosition(this BinaryReader br)
        {
            var value = br.ReadAsUnsignedLong();
            return new Position
            {
                // note: binary cast, do not check value range.
                X = (int)(value >> 38),
                Y = (int)((value >> 26) & 0xFFF),
                Z = (int)(value & 0x3FFFFFF)
            };
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
