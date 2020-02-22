using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block.Material
{
    public class MaterialColor
    {
        public int Value { get; set; }
        public int Index { get; set; }
        public MaterialColor(int index, int color)
        {
            Index = index;
            Value = color;
        }
    }
}
