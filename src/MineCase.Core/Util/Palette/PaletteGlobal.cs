using MineCase.Util.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Util.Palette
{
    public class PaletteIdentity<T>: IPalette<T>
    {
        private readonly BiDictionary<int, T> _registry;
        private readonly T _defaultState;

       public PaletteIdentity(BiDictionary<int, T> regitry, T defaultState)
        {
            _registry = regitry;
            _defaultState = defaultState;
        }

        public int IndexOf(T state)
        {
            int i = _registry.GetSecond(state);
            return i == -1 ? 0 : i;
        }

        public bool Contains(T value)
        {
            return true;
        }

        public T Get(int index)
        {
            T t = _registry.GetFirst(index);
            return (T)(t == null ? _defaultState : t);
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
