using MineCase.Network;
using MineCase.Util.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Util.Palette
{
    public class PaletteHashMap<T> : IPalette<T>
    {
        private readonly BiDictionary<int, T> _stateRegistry;

        // private readonly T[] _values;
        public int Length { get; set; }
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

        public int GetSerializedSize()
        {
            /*
            uint size = Length.SizeOfVarInt();

            for (int i = 0; i < Length; ++i)
            {
                size += _stateRegistry.GetSecond(_values[i]).SizeOfVarInt();
            }

            return (int)size;
            */
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
