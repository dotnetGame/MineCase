using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

using MineCase.Nbt;
using MineCase.Nbt.Serialization;
using MineCase.Nbt.Tags;

namespace MineCase.Serialization
{
    public ref struct SpanReader
    {
        private ReadOnlySpan<byte> _span;

        public bool IsCosumed => _span.IsEmpty;

        public SpanReader ReadAsSubReader(int length)
        {
            var reader = new SpanReader(_span.Slice(0, length));
            Advance(length);
            return reader;
        }

        public SpanReader(ReadOnlySpan<byte> span)
        {
            _span = span;
        }

        public uint ReadAsVarInt(out int bytesRead)
        {
            int numRead = 0;
            uint result = 0;
            byte read;
            do
            {
                read = _span[numRead];
                uint value = (uint)(read & 0b01111111);
                result |= value << (7 * numRead);

                numRead++;
                if (numRead > 5)
                    throw new InvalidDataException("VarInt is too big");
            }
            while ((read & 0b10000000) != 0);

            bytesRead = numRead;
            Advance(numRead);
            return result;
        }

        public unsafe string ReadAsString()
        {
            var len = ReadAsVarInt(out _);
            var bytes = ReadBytes((int)len);
            return Encoding.UTF8.GetString((byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(bytes)), bytes.Length);
        }

        public ushort ReadAsUnsignedShort()
        {
            var value = _span.ReadBigEndian<ushort>();
            Advance(sizeof(ushort));
            return value;
        }

        public uint ReadAsUnsignedInt()
        {
            var value = _span.ReadBigEndian<uint>();
            Advance(sizeof(uint));
            return value;
        }

        public ulong ReadAsUnsignedLong()
        {
            var value = _span.ReadBigEndian<ulong>();
            Advance(sizeof(ulong));
            return value;
        }

        public int ReadAsInt()
        {
            var value = _span.ReadBigEndian<int>();
            Advance(sizeof(int));
            return value;
        }

        public long ReadAsLong()
        {
            var value = _span.ReadBigEndian<long>();
            Advance(sizeof(long));
            return value;
        }

        public byte PeekAsByte()
        {
            var value = _span.ReadBigEndian<byte>();
            return value;
        }

        public byte ReadAsByte()
        {
            var value = _span.ReadBigEndian<byte>();
            Advance(sizeof(byte));
            return value;
        }

        public bool ReadAsBoolean()
        {
            var value = _span.ReadBigEndian<bool>();
            Advance(sizeof(bool));
            return value;
        }

        public short ReadAsShort()
        {
            var value = _span.ReadBigEndian<short>();
            Advance(sizeof(short));
            return value;
        }

        public float ReadAsFloat()
        {
            var value = _span.ReadBigEndian<float>();
            Advance(sizeof(float));
            return value;
        }

        public double ReadAsDouble()
        {
            var value = _span.ReadBigEndian<double>();
            Advance(sizeof(double));
            return value;
        }

        public byte[] ReadAsByteArray(int length)
        {
            var value = ReadBytes(length);
            return value.ToArray();
        }

        public byte[] ReadAsByteArray()
        {
            var bytes = _span.ToArray();
            _span = ReadOnlySpan<byte>.Empty;
            return bytes;
        }

        public int[] ReadAsIntArray(int length)
        {
            int[] ret = new int[length];
            for (int i = 0; i < length; ++i)
            {
                ret[i] = ReadAsInt();
            }

            return ret;
        }

        public NbtCompound ReadAsNbtTag()
        {
            NbtCompound nbt;
            using (MemoryStream ms = new MemoryStream(_span.ToArray()))
            {
                using (var br = new BinaryReader(ms, Encoding.UTF8, false))
                {
                    nbt = NbtTagSerializer.DeserializeTag<NbtCompound>(br);
                }

                Advance((int)ms.Position);
            }

            return nbt;
        }

        public NbtCompound[] ReadAsNbtTagArray(int length)
        {
            NbtCompound[] ret = new NbtCompound[length];
            for (int i = 0; i < length; ++i)
            {
                ret[i] = ReadAsNbtTag();
            }

            return ret;
        }

        public Position ReadAsPosition()
        {
            var value = _span.ReadBigEndian<ulong>();
            Advance(sizeof(ulong));
            return new Position
            {
                X = SignBy26(value >> 38),
                Y = SignBy12(value & 0xFFF),
                Z = SignBy26(value << 26 >> 38)
            };
        }

        private const ulong _26mask = (1u << 26) - 1;
        private const ulong _12mask = (1u << 12) - 1;

        private static int SignBy26(ulong value)
        {
            if ((value & 0b10_0000_0000_0000_0000_0000_0000) != 0)
                return -(int)((~value & _26mask) + 1);
            return (int)value;
        }

        private static int SignBy12(ulong value)
        {
            if ((value & 0b1000_0000_0000) != 0)
                return -(int)((~value & _12mask) + 1);
            return (int)value;
        }

        public Slot ReadAsSlot()
        {
            bool present = ReadAsBoolean();
            var slot = new Slot { BlockId = -1 };
            if (present)
            {
                slot.BlockId = (short)ReadAsVarInt(out _);
                slot.ItemCount = ReadAsByte();
                if (PeekAsByte() == 0)
                    Advance(1);
                else
                    slot.NBT = new NbtFile(new MemoryStream(ReadAsByteArray()), false);
            }

            return slot;
        }

        private ReadOnlySpan<byte> ReadBytes(int length)
        {
            var bytes = _span.Slice(0, length);
            Advance(length);
            return bytes;
        }

        private void Advance(int count)
        {
            _span = _span.Slice(count);
        }
    }
}
