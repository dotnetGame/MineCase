﻿using System;
using System.Binary;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace MineCase.Serialization
{
    public struct SpanReader
    {
        private ReadOnlySpan<byte> _span;

        public bool IsCosumed => _span.IsEmpty;

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
            return Encoding.UTF8.GetString((byte*)Unsafe.AsPointer(ref bytes.DangerousGetPinnableReference()), bytes.Length);
        }

        public ushort ReadAsUnsignedShort()
        {
            var value = _span.ReadBigEndian<ushort>();
            Advance(sizeof(ushort));
            return value;
        }

        public long ReadAsLong()
        {
            var value = _span.ReadBigEndian<long>();
            Advance(sizeof(long));
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
