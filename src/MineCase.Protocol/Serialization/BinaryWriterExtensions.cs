using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Serialization
{
    internal static class BinaryWriterExtensions
    {
        public static void WriteAsBoolean(this BinaryWriter bw, bool value) =>
            bw.Write(value);

        public static void WriteAsByte(this BinaryWriter bw, sbyte value) =>
            bw.Write(value);

        public static void WriteAsShort(this BinaryWriter bw, short value)
        {
            bw.Write(((ushort)value).ToBigEndian());
        }

        public static void WriteAsInt(this BinaryWriter bw, int value)
        {
            bw.Write(((uint)value).ToBigEndian());
        }

        public static void WriteAsLong(this BinaryWriter bw, long value)
        {
            bw.Write(((ulong)value).ToBigEndian());
        }

        public static void WriteAsUnsignedByte(this BinaryWriter bw, byte value)
        {
            bw.Write(value);
        }

        public static void WriteAsUnsignedShort(this BinaryWriter bw, ushort value)
        {
            bw.Write(value.ToBigEndian());
        }

        public static void WriteAsUnsignedLong(this BinaryWriter bw, ulong value)
        {
            bw.Write(value.ToBigEndian());
        }

        // http://wiki.vg/Protocol#VarInt_and_VarLong
        public static void WriteAsVarInt(this BinaryWriter bw, int value, out uint bytesWrite)
        {
            uint uvalue = (uint)value;
            uint numWrite = 0;
            do
            {
                byte temp = (byte)(uvalue & 0b01111111);
                uvalue >>= 7;
                if (uvalue != 0)
                    temp |= 0b10000000;
                bw.Write(temp);
                numWrite++;
            }
            while (uvalue != 0);
            bytesWrite = numWrite;
        }

        public static void WriteAsByteArray(this BinaryWriter bw, byte[] value) =>
            bw.Write(value);

        public static void WriteAsString(this BinaryWriter bw, string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            bw.WriteAsVarInt(bytes.Length, out _);
            bw.Write(bytes);
        }
    }
}
