using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using MineCase.Block;
using MineCase.Protocol;
using MineCase.World.Chunk;

namespace MineCase.Serialization
{
    internal static class BinaryWriterExtensions
    {
        public static void WriteAsBoolean(this BinaryWriter bw, bool value) =>
            bw.Write(value);

        public static void WriteAsByte(this BinaryWriter bw, sbyte value) =>
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
            bw.WriteAsString(value.ToString());
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

        public static void WriteAsAngle(this BinaryWriter bw, Angle value)
        {
            bw.Write(value.Value);
        }

        public static void WriteAsArray<T>(this BinaryWriter bw, IReadOnlyList<T> array)
            where T : ISerializablePacket
        {
            foreach (var item in array)
                item.Serialize(bw);
        }

        public static void WriteAsIntArray<T>(this BinaryWriter bw, IReadOnlyList<int> array)
        {
            bw.WriteAsVarInt((uint)array.Count, out _);

            foreach (int i in array)
            {
                bw.WriteAsInt(i);
            }
        }

        // TODO write as long
        // write as var int array
        // write as var long array
        public static void WriteAsLongArray(this BinaryWriter bw, IReadOnlyList<long> array)
        {
            bw.WriteAsVarInt((uint)array.Count, out _);

            foreach (long i in array)
            {
                bw.WriteAsLong(i);
            }
        }

        public static void WriteAsPosition(this BinaryWriter bw, Position position)
        {
            Debug.Assert(IsValueInRangeInclusive(position.X, -33554432, 33554431), "position X value not in range.");
            Debug.Assert(IsValueInRangeInclusive(position.Y, -2048, 2047), "position Y value not in range.");
            Debug.Assert(IsValueInRangeInclusive(position.Z, -33554432, 33554431), "position Z value not in range.");

            var value = (ulong)((uint)position.X & 0x3FFFFFF) << 38;
            value |= (ulong)((uint)position.Y & 0xFFF) << 26;
            value |= (ulong)((uint)position.Z & 0x3FFFFFF);
            bw.WriteAsUnsignedLong(value);
        }

        public static void WriteAsChunkSection(this BinaryWriter bw, ChunkSection value)
        {
            bw.WriteAsShort((short)value.BlockCount);
            bw.WriteAsBlockStateContainer(value.Data);
        }

        public static void WriteAsBlockStateContainer(this BinaryWriter bw, BlockStateContainer<BlockState> value)
        {
            bw.WriteAsByte((sbyte)value.Bits);

            // TODO
            // this.palette.write(buf);
            bw.WriteAsLongArray(Array.ConvertAll(value.Storage.GetBackingData(), x => (long)x));
        }

        private static bool IsValueInRangeInclusive(long value, long min, long max)
        {
            return (value >= min) && (value <= max);
        }

        /*
        public static void WriteAsSlot(this BinaryWriter bw, Slot slot)
        {
            bw.WriteAsShort(slot.BlockId);
            if (!slot.IsEmpty)
            {
                bw.WriteAsByte(slot.ItemCount);
                bw.WriteAsShort(slot.ItemDamage);
                if (slot.NBT != null)
                    slot.NBT.WriteTo(bw.BaseStream);
                else
                    bw.WriteAsByte(0);
            }
        }
        */

        /*
        public static BinaryWriter WriteAsEntityMetadata(this BinaryWriter bw, byte index, EntityMetadataType type)
        {
            bw.WriteAsByte(index);
            bw.WriteAsByte((byte)type);
            return bw;
        }
        */

        public static void WriteAsCompoundTag(this BinaryWriter bw, Nbt.Tags.NbtCompound nbt)
        {
            if (nbt == null)
            {
                bw.WriteAsByte(0);
            }
            else
            {
                Nbt.Serialization.NbtTagSerializer.SerializeTag(nbt, bw);
            }
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
