using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using MineCase.Formats;
using MineCase.Protocol;

namespace MineCase.Serialization
{
    internal static class BinaryWriterExtensions
    {
        public static void WriteAsBoolean(this BinaryWriter bw, bool value) =>
            bw.Write(value);

        public static void WriteAsByte(this BinaryWriter bw, byte value) =>
            bw.Write(value);

        // http://wiki.vg/Protocol#VarInt_and_VarLong
        public static void WriteAsVarInt(this BinaryWriter bw, uint value, out uint bytesWrite)
        {
            uint numWrite = 0;
            do
            {
                byte temp = (byte)(value & 0b01111111);
                value >>= 7;
                if (value != 0)
                    temp |= 0b10000000;
                bw.Write(temp);
                numWrite++;
            }
            while (value != 0);
            bytesWrite = numWrite;
        }

        public static void WriteAsByteArray(this BinaryWriter bw, byte[] value) =>
            bw.Write(value);

        public static void WriteAsString(this BinaryWriter bw, string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            bw.WriteAsVarInt((uint)bytes.Length, out _);
            bw.Write(bytes);
        }

        public static void WriteAsChat(this BinaryWriter bw, Chat value)
        {
            bw.Write(value.ToString());
        }

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

        public static void WriteAsFloat(this BinaryWriter bw, float value)
        {
            var uintValue = Unsafe.As<float, uint>(ref value);
            bw.Write(uintValue.ToBigEndian());
        }

        public static void WriteAsDouble(this BinaryWriter bw, double value)
        {
            var ulongValue = Unsafe.As<double, ulong>(ref value);
            bw.Write(ulongValue.ToBigEndian());
        }

        public static void WriteAsUUID(this BinaryWriter bw, Guid value)
        {
            bw.Write(value.ToByteArray());
        }

        public static void WriteAsArray<T>(this BinaryWriter bw, IReadOnlyList<T> array)
            where T : ISerializablePacket
        {
            foreach (var item in array)
                item.Serialize(bw);
        }
    }

    internal static class DataTypeSizeExtensions
    {
        public static uint SizeOfVarInt(this uint value)
        {
            uint numWrite = 0;
            do
            {
                value >>= 7;
                numWrite++;
            }
            while (value != 0);
            return numWrite;
        }

        public static ushort ToBigEndian(this ushort value)
        {
            return (ushort)((value >> 8) | (((byte)value) << 8));
        }

        public static uint ToBigEndian(this uint value)
        {
            return (value >> 24) | ((value & 0x00FF_0000) >> 8) |
                ((value & 0x0000_FF00) << 8) | ((value & 0x0000_00FF) << 24);
        }

        public static ulong ToBigEndian(this ulong value)
        {
            return (value >> 56) | ((value & 0x00FF_0000_0000_0000) >> 40) | ((value & 0x0000_FF00_0000_0000) >> 24) |
                ((value & 0x0000_00FF_0000_0000) >> 8) | ((value & 0x0000_0000_FF00_0000) << 8) | ((value & 0x0000_0000_00FF_0000) << 24) |
                ((value & 0x0000_0000_0000_FF00) << 40) | ((value & 0x0000_0000_0000_00FF) << 56);
        }
    }
}
