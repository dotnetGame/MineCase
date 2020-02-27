using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MineCase.Nbt.Serialization
{
    internal static class BinaryExtensions
    {
        internal static NbtTagType ReadTagType(this BinaryReader br)
        {
            return (NbtTagType)br.ReadByte();
        }

        internal static string ReadTagString(this BinaryReader br)
        {
            // TODO: 名称长度未说明是否有符号，假设为无符号，若为有符号则与此方法不兼容，需要另外实现
            var strLen = br.ReadUInt16().ToggleEndian();
            return strLen == 0 ? null : new string(Encoding.UTF8.GetChars(br.ReadBytes(strLen)));
        }

        internal static float ReadTagFloat(this BinaryReader br)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return br.ReadSingle();
            }

            var tmpConvertedValue = br.ReadUInt32().ToggleEndian();
            return Unsafe.As<uint, float>(ref tmpConvertedValue);
        }

        internal static double ReadTagDouble(this BinaryReader br)
        {
            if (!BitConverter.IsLittleEndian)
            {
                return br.ReadDouble();
            }

            var tmpConvertedValue = br.ReadUInt64().ToggleEndian();
            return Unsafe.As<ulong, double>(ref tmpConvertedValue);
        }

        internal static sbyte[] ReadTagBytes(this BinaryReader br, int count)
        {
            return Unsafe.As<sbyte[]>(br.ReadBytes(count));
        }

        internal static int[] ReadTagIntArray(this BinaryReader br, int count)
        {
            var retArr = new int[count];
            for (var i = 0; i < count; ++i)
            {
                retArr[i] = br.ReadInt32().ToggleEndian();
            }

            return retArr;
        }

        internal static long[] ReadTagLongArray(this BinaryReader br, int count)
        {
            var retArr = new long[count];
            for (var i = 0; i < count; ++i)
            {
                retArr[i] = br.ReadInt64().ToggleEndian();
            }

            return retArr;
        }

        internal static void WriteTagValue(this BinaryWriter bw, NbtTagType tagType)
        {
            bw.Write((byte)tagType);
        }

        internal static void WriteTagValue(this BinaryWriter bw, string value)
        {
            if (value == null)
            {
                // TODO: 这个行为是否正确？
                bw.Write((ushort)0);
                return;
            }

            bw.Write(((ushort)value.Length).ToggleEndian());
            bw.Write(Encoding.UTF8.GetBytes(value));
        }

        internal static void WriteTagValue(this BinaryWriter bw, float value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                bw.Write(value);
                return;
            }

            bw.Write(Unsafe.As<float, uint>(ref value).ToggleEndian());
        }

        internal static void WriteTagValue(this BinaryWriter bw, double value)
        {
            if (!BitConverter.IsLittleEndian)
            {
                bw.Write(value);
                return;
            }

            bw.Write(Unsafe.As<double, ulong>(ref value).ToggleEndian());
        }

        internal static void WriteTagValue(this BinaryWriter bw, sbyte[] value)
        {
            bw.Write(Unsafe.As<byte[]>(value));
        }

        internal static void WriteTagValue(this BinaryWriter bw, IEnumerable<int> value)
        {
            bw.Write(value.Count().ToggleEndian());
            foreach (var item in value)
            {
                bw.Write(item.ToggleEndian());
            }
        }

        internal static void WriteTagValue(this BinaryWriter bw, IEnumerable<long> value)
        {
            bw.Write(value.Count().ToggleEndian());
            foreach (var item in value)
            {
                bw.Write(item.ToggleEndian());
            }
        }
    }

    internal static class NbtEndianExtensions
    {
        internal static short ToggleEndian(this short value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (short)((value >> 8) | ((value & 0xFF) << 8));
            }

            return value;
        }

        internal static ushort ToggleEndian(this ushort value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (ushort)((value >> 8) | ((value & 0xFF) << 8));
            }

            return value;
        }

        internal static int ToggleEndian(this int value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (value >> 24) | ((value & 0x00FF0000) >> 8) | ((value & 0x0000FF00) << 8) | ((value & 0x000000FF) << 24);
            }

            return value;
        }

        internal static uint ToggleEndian(this uint value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (value >> 24) | ((value & 0x00FF0000) >> 8) | ((value & 0x0000FF00) << 8) | ((value & 0x000000FF) << 24);
            }

            return value;
        }

        internal static long ToggleEndian(this long value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (long)((ulong)value).ToggleEndian();
            }

            return value;
        }

        internal static ulong ToggleEndian(this ulong value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (value >> 56) | ((value & 0x00FF_0000_0000_0000) >> 40) | ((value & 0x0000_FF00_0000_0000) >> 24) |
                       ((value & 0x0000_00FF_0000_0000) >> 8) | ((value & 0x0000_0000_FF00_0000) << 8) | ((value & 0x0000_0000_00FF_0000) << 24) |
                       ((value & 0x0000_0000_0000_FF00) << 40) | ((value & 0x0000_0000_0000_00FF) << 56);
            }

            return value;
        }
    }
}
