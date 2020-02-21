using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Util.Palette
{
    public class PaletteArray<T> : IPalette<T>
    {
        private readonly T[] _values;

        private readonly int _bits;

        public PaletteArray(int bits)
        {
            _values = new T[1 << bits];
            _bits = bits;
        }
        public bool Contains(T value)
        {
            return Array.IndexOf(_values, value) > -1;
        }

        public int IndexOf(T value)
        {
            return Array.IndexOf(_values, value);
        }

        public T Get(int index)
        {
            return _values[index];
        }
    }
}
