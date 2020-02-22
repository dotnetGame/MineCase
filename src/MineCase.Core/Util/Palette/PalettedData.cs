using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Block;
using MineCase.Util.Collections;

namespace MineCase.Util.Palette
{
    public class PalettedData<T>
    {
        private readonly T _defaultState;

        private readonly TinyIntArray _storage;

        private readonly IPalette<T> _palette;

        private int _bits;

        public PalettedData(int size, T value)
        {
            _bits = 4;
            _storage = new TinyIntArray(_bits, size);
            _defaultState = value;
            _palette = new PaletteArray<T>(_bits);
        }

        public T this[int offset]
        {
            get
            {
                T t = _palette.Get((int)_storage[offset]);
                return t ?? _defaultState;
            }
            set
            {
                int paletteIndex = _palette.IndexOf(value);
                _storage[offset] = (uint)paletteIndex;
            }
        }
    }
}
