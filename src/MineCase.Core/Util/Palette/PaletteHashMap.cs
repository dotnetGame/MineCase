using MineCase.Util.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Util.Palette
{
    public class PaletteHashMap<T> : IPalette<T>
    {
        public PaletteHashMap(BiDictionary<int, T> registry, int bits)
        {
        }
        public bool Contains(T value)
        {
            throw new NotImplementedException();
        }

        public T Get(int index)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(T value)
        {
            throw new NotImplementedException();
        }

        public void Read(BinaryReader br)
        {
            throw new NotImplementedException();
        }

        public void Write(BinaryWriter bw)
        {
            throw new NotImplementedException();
        }
    }
}
