using System;
using System.Buffers.Binary;
using System.IO;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using MineCase.Nbt;
using MineCase.Nbt.Serialization;
using MineCase.Nbt.Tags;
using MineCase.Protocol;

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

        public Chat ReadAsChat()
        {
            string str = ReadAsString();
            return Chat.Parse(str);
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

        public Angle ReadAsAngle()
        {
            return new Angle(ReadAsByte());
        }

        public Guid ReadAsUUID()
        {
            var bytes = ReadBytes(16);
            return new Guid(bytes);
        }

        public unsafe string ReadAsString()
        {
            var len = ReadAsVarInt(out _);
            var bytes = ReadBytes((int)len);
            return Encoding.UTF8.GetString(bytes);
        }

        public ushort ReadAsUnsignedShort()
        {
            var value = BinaryPrimitives.ReadUInt16BigEndian(_span);
            Advance(sizeof(ushort));
            return value;
        }

        public uint ReadAsUnsignedInt()
        {
            var value = BinaryPrimitives.ReadUInt32BigEndian(_span);
            Advance(sizeof(uint));
            return value;
        }

        public ulong ReadAsUnsignedLong()
        {
            var value = BinaryPrimitives.ReadUInt64BigEndian(_span);
            Advance(sizeof(ulong));
            return value;
        }

        public int ReadAsInt()
        {
            var value = BinaryPrimitives.ReadInt32BigEndian(_span);
            Advance(sizeof(int));
            return value;
        }

        public long ReadAsLong()
        {
            var value = BinaryPrimitives.ReadInt64BigEndian(_span);
            Advance(sizeof(long));
            return value;
        }

        public byte PeekAsByte()
        {
            return _span[0];
        }

        public byte ReadAsByte()
        {
            var value = _span[0];
            Advance(sizeof(byte));
            return value;
        }

        public bool ReadAsBoolean()
        {
            var value = Convert.ToBoolean(_span[0]);
            return value;
        }

        public short ReadAsShort()
        {
            var value = BinaryPrimitives.ReadInt16BigEndian(_span);
            Advance(sizeof(short));
            return value;
        }

        public float ReadAsFloat()
        {
            var value = BinaryPrimitives.ReadSingleBigEndian(_span);
            Advance(sizeof(float));
            return value;
        }

        public double ReadAsDouble()
        {
            var value = BinaryPrimitives.ReadDoubleBigEndian(_span);
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
            var expectedLen = length * sizeof(int);
            if (expectedLen > _span.Length)
                throw new IndexOutOfRangeException();

            int[] ret = new int[length];
            for (int i = 0; i < length; ++i)
            {
                ret[i] = BinaryPrimitives.ReadInt32BigEndian(_span);
                Advance(sizeof(int));
            }

            return ret;
        }

        public uint[] ReadAsVarIntArray(int length)
        {
            var array = new uint[length];
            var subReader = new SpanReader(_span);
            for (int i = 0; i < array.Length; i++)
                array[i] = subReader.ReadAsVarInt(out _);

            _span = subReader._span;
            return array;
        }

        public Slot[] ReadAsSlotArray(int length)
        {
            var array = new Slot[length];
            var subReader = new SpanReader(_span);
            for (int i = 0; i < array.Length; i++)
                array[i] = subReader.ReadAsSlot();

            _span = subReader._span;
            return array;
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
            var value = ReadAsUnsignedLong();
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

        public T[] ReadAsArray<T>(int count)
            where T : IPacket, new()
        {
            var array = new T[count];
            var subReader = new SpanReader(_span);
            for (int i = 0; i < array.Length; i++)
            {
                var item = new T();
                item.Deserialize(ref subReader);
                array[i] = item;
            }

            _span = subReader._span;
            return array;
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
