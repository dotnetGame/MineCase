using MineCase.Util.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Util.Collections
{
    public class TinyIntArray
    {
        private ulong[] _data;
        private readonly int _bitsPerEntry;
        private readonly ulong _maxEntryValue;
        private readonly int _arraySize;

        public int Count { get => _arraySize; }

        public TinyIntArray(int bitsPerEntry, int arraySize)
            :this(bitsPerEntry, arraySize, new ulong[MathHelper.RoundUp(arraySize * bitsPerEntry, 64) / 64])
        {
        }
        public TinyIntArray(int bitsPerEntry, int arraySize, ulong[] data)
        {
            if (!(bitsPerEntry >= 1 && bitsPerEntry <= 32))
                throw new ArgumentException("TinyIntArray.TinyIntArray: invalid bits per entry number");
            _arraySize = arraySize;
            _bitsPerEntry = bitsPerEntry;
            _data = data;
            _maxEntryValue = (1UL << bitsPerEntry) - 1UL;
            int dataLength = MathHelper.RoundUp(arraySize * bitsPerEntry, 64) / 64;
            if (data.Length != dataLength)
            {
                throw new ArgumentException($"Invalid length given for storage, got: {data.Length} but expected: {dataLength}.");
            }
        }

        public uint this[int index]
        {
            get
            {
                if (!(index >= 0 && index <= _arraySize - 1))
                    throw new IndexOutOfRangeException("TinyIntArray.operator[]: index out of range.");
                int bitOffset = index * _bitsPerEntry;
                int ulongOfsset = bitOffset >> 6;
                int ulongOfssetNext = (index + 1) * _bitsPerEntry - 1 >> 6;
                int bitsLow = bitOffset ^ ulongOfsset << 6;
                if (ulongOfsset == ulongOfssetNext)
                {
                    return (uint)(_data[ulongOfsset] >> bitsLow & _maxEntryValue);
                }
                else
                {
                    int bitsHigh = 64 - bitsLow;
                    return (uint)((_data[ulongOfsset] >> bitsLow | _data[ulongOfssetNext] << bitsHigh) & _maxEntryValue);
                }
            }
            set
            {
                if (!(index >= 0 && index <= _arraySize - 1))
                    throw new IndexOutOfRangeException("TinyIntArray.operator[]: index out of range.");
                if (!(value >= 0 && value <= _maxEntryValue))
                    throw new ArgumentException("TinyIntArray.operator[]: invalid entry value.");
                int bitOffset = index * _bitsPerEntry;
                int ulongOfsset = bitOffset >> 6;
                int ulongOfssetNext = (index + 1) * _bitsPerEntry - 1 >> 6;
                int bitsLow = bitOffset ^ ulongOfsset << 6;
                _data[ulongOfsset] = _data[ulongOfsset] & ~(_maxEntryValue << bitsLow) | (value & _maxEntryValue) << bitsLow;
                if (ulongOfsset != ulongOfssetNext)
                {
                    int bitsHigh = 64 - bitsLow;
                    int entryOffset = _bitsPerEntry - bitsHigh;
                    _data[ulongOfssetNext] = _data[ulongOfssetNext] >> entryOffset << entryOffset | (value & _maxEntryValue) >> bitsHigh;
                }
            }
        }

        public ulong[] GetRawArray()
        {
            return _data;
        }

        public void SetRawArray(ulong[] data)
        {
            _data = data;
        }
    }
}
