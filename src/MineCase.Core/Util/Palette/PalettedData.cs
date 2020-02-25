using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Block;
using MineCase.Network;
using MineCase.Util.Collections;

namespace MineCase.Util.Palette
{
    public class PalettedData<T>
    {
        private IPalette<T> _globalPalette;

        private readonly T _defaultState;

        private TinyIntArray _storage;

        private IPalette<T> _palette;

        private int _bits;

        private BiDictionary<int, T> _registry;

        public PalettedData(IPalette<T> globalPalette, BiDictionary<int, T> registry, int size, T value)
        {
            _globalPalette = globalPalette;
            _bits = 4;
            _storage = new TinyIntArray(_bits, size);
            _defaultState = value;
            _palette = new PaletteArray<T>(registry, _bits);
            _registry = registry;
        }

        public void SetBits(int bits, bool forceBits = false)
        {
            if (bits != _bits)
            {
                _bits = bits;
                if (_bits <= 4)
                {
                    _bits = 4;
                    _palette = new PaletteArray<T>(_registry, _bits);
                }
                else if (_bits < 9)
                {
                    _palette = new PaletteHashMap<T>(_registry, _bits);
                }
                else
                {
                    _palette = _globalPalette;

                    // FIXME
                    // _bits = MathHelper.log2DeBruijn(_registry.size());
                    if (forceBits)
                        _bits = bits;
                }

                // FIXME
                // _palette.IndexOf(_defaultState);
                _storage = new TinyIntArray(_bits, 4096);
            }
        }
        public void Resize(int newBits)
        {
            // Save old array before 'SetBits' replaces it.
            TinyIntArray oldarray = _storage;
            IPalette<T> ipalette = _palette;
            SetBits(newBits);

            for (int i = 0; i < oldarray.Count; ++i)
            {
                T t = ipalette.Get((int)oldarray[i]);
                if (t != null)
                {
                    this[i] = t;
                }
            }
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

        public ulong[] GetStorage()
        {
            return _storage.GetRawArray();
        }

        public int GetSerializedSize()
        {
            return 1 + _palette.GetSerializedSize() + (int)_storage.Count.SizeOfVarInt() + _storage.GetRawArray().Length * 8;
        }

        public void Read(BinaryReader br)
        {
            int bits = br.ReadAsByte();
            if (_bits != bits)
                SetBits(bits, true);

            _palette.Read(br);
            _storage.SetRawArray(br.ReadAsUnsignedLongArray());

            // TODO
            int regSize = 0;
            // int regSize = MathHelper.log2DeBruijn(_registry.size());
            // When we are using global palette and bits is not 2^n, resize it to fit registry;
            if (_palette == _globalPalette && _bits != regSize)
                Resize(regSize);

        }
    }
}
