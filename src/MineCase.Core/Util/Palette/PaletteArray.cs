using MineCase.Block;
using MineCase.Network;
using MineCase.Util.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Util.Palette
{
    public class PaletteArray<T> : IPalette<T>
    {
        private readonly BiDictionary<int, T> _stateRegistry;

        private readonly T[] _values;

        private readonly int _bits;

        public int Length { get; set; }

        public PaletteArray(BiDictionary<int, T> registry, int bits)
        {
            _stateRegistry = registry;
            _values = new T[1 << bits];
            _bits = bits;
        }

        // FIXME for array length
        public bool Contains(T value)
        {
            return Array.IndexOf(_values, value) > -1;
        }

        // FIXME for array length
        public int IndexOf(T value)
        {
            return Array.IndexOf(_values, value);
        }

        // FIXME for array length
        public T Get(int index)
        {
            return _values[index];
        }

        public void SetRaw(int index, T value)
        {
            _values[index] = value;
        }

        public T GetRaw(int index)
        {
            return _values[index];
        }

        public void Read(BinaryReader br)
        {
            Length = br.ReadAsVarInt(out _);

            for (int i = 0; i < Length; ++i)
            {
                _values[i] = _stateRegistry.GetFirst(br.ReadAsVarInt(out _));
            }
        }

        public void Write(BinaryWriter bw)
        {
            throw new NotImplementedException();
        }
    }
}
