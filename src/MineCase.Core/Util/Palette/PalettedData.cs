using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Block;
using MineCase.Util.Collections;

namespace MineCase.Util.Palette
{
    public class PalettedData<T>
    {
        private T _defaultState;

        private TinyIntArray _storage;

        private IPalette<T> _palette;

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
