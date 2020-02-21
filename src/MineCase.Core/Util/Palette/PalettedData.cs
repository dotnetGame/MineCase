using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Block;
using MineCase.Util.Collections;

namespace MineCase.Util.Palette
{
    public class PalettedData<T>
    {
        protected TinyIntArray _storage;

        public BlockState this[int offset]
        {
            get => null;
            set { }
        }
    }
}
