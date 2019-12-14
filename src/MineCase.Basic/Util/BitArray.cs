using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Util.Math;

namespace MineCase.Util
{

    public class BitArray
    {
        private ulong[] _longArray;
        private int _bitsPerEntry;
        private ulong _maxEntryValue;
        private int _arraySize;

        public BitArray(int bitsPerEntryIn, int arraySizeIn)
        {
            // this(bitsPerEntryIn, arraySizeIn, new long[MathHelper.roundUp(arraySizeIn * bitsPerEntryIn, 64) / 64]);
        }

        /*
        public BitArray(int p_i47901_1_, int p_i47901_2_, ulong[] data)
        {
            Validate.inclusiveBetween(1L, 32L, (long)p_i47901_1_);
            _arraySize = p_i47901_2_;
            _bitsPerEntry = p_i47901_1_;
            _longArray = data;
            _maxEntryValue = (1UL << p_i47901_1_) - 1UL;
            int i = MathHelper.IntervalFloor(p_i47901_2_ * p_i47901_1_, 64) / 64;
            if (data.Length != i)
            {
                throw new ArgumentException("Invalid length given for storage, got: " + data.Length + " but expected: " + i);
            }
        }

        public int func_219789_a(int p_219789_1_, int p_219789_2_)
        {
            Validate.inclusiveBetween(0L, (long)(_arraySize - 1), (long)p_219789_1_);
            Validate.inclusiveBetween(0L, _maxEntryValue, (long)p_219789_2_);
            int i = p_219789_1_ * _bitsPerEntry;
            int j = i >> 6;
            int k = (p_219789_1_ + 1) * _bitsPerEntry - 1 >> 6;
            int l = i ^ j << 6;
            int i1 = 0;
            i1 = i1 | (int)(_longArray[j] >> l & _maxEntryValue);
            _longArray[j] = _longArray[j] & ~(_maxEntryValue << l) | ((ulong)p_219789_2_ & _maxEntryValue) << l;
            if (j != k)
            {
                int j1 = 64 - l;
                int k1 = _bitsPerEntry - j1;
                i1 |= (int)(_longArray[k] << j1 & _maxEntryValue);
                _longArray[k] = _longArray[k] >> k1 << k1 | ((ulong)p_219789_2_ & _maxEntryValue) >> j1;
            }

            return i1;
        }

        // Sets the entry at the given location to the given value
        public void SetAt(int index, int value)
        {
            Validate.inclusiveBetween(0L, (long)(_arraySize - 1), (long)index);
            Validate.inclusiveBetween(0L, _maxEntryValue, (long)value);
            int i = index * _bitsPerEntry;
            int j = i >> 6;
            int k = (index + 1) * _bitsPerEntry - 1 >> 6;
            int l = i ^ j << 6;
            _longArray[j] = _longArray[j] & ~(_maxEntryValue << l) | ((ulong)value & _maxEntryValue) << l;
            if (j != k)
            {
                int i1 = 64 - l;
                int j1 = _bitsPerEntry - i1;
                _longArray[k] = _longArray[k] >> j1 << j1 | ((ulong)value & _maxEntryValue) >> i1;
            }

        }

        // Gets the entry at the given index
        public int GetAt(int index)
        {
            Validate.inclusiveBetween(0L, (long)(_arraySize - 1), (long)index);
            int i = index * _bitsPerEntry;
            int j = i >> 6;
            int k = (index + 1) * _bitsPerEntry - 1 >> 6;
            int l = i ^ j << 6;
            if (j == k)
            {
                return (int)(_longArray[j] >> l & _maxEntryValue);
            }
            else
            {
                int i1 = 64 - l;
                return (int)((_longArray[j] >> l | _longArray[k] << i1) & _maxEntryValue);
            }
        }
        */

        // Gets the long array that is used to store the data in this BitArray. This is useful for sending packet data.
        public ulong[] GetBackingData()
        {
            return _longArray;
        }

        public int Size()
        {
            return _arraySize;
        }

        public int BitsPerEntry()
        {
            return _bitsPerEntry;
        }

        /*
        public void func_225421_a(IntConsumer p_225421_1_)
        {
            int i = _longArray.Length;
            if (i != 0)
            {
                int j = 0;
                ulong k = _longArray[0];
                ulong l = i > 1 ? _longArray[1] : 0L;

                for (int i1 = 0; i1 < _arraySize; ++i1)
                {
                    int j1 = i1 * _bitsPerEntry;
                    int k1 = j1 >> 6;
                    int l1 = (i1 + 1) * _bitsPerEntry - 1 >> 6;
                    int i2 = j1 ^ k1 << 6;
                    if (k1 != j)
                    {
                        k = l;
                        l = k1 + 1 < i ? _longArray[k1 + 1] : 0L;
                        j = k1;
                    }

                    if (k1 == l1)
                    {
                        p_225421_1_.accept((int)(k >> i2 & _maxEntryValue));
                    }
                    else
                    {
                        int j2 = 64 - i2;
                        p_225421_1_.accept((int)((k >> i2 | l << j2) & _maxEntryValue));
                    }
                }

            }
        }
        */
    }
}
