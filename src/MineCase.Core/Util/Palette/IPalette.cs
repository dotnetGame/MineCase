using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Util.Palette
{
    public interface IPalette<T>
    {
        int IndexOf(T value);
        bool Contains(T value);
        T Get(int index);
    }
}
