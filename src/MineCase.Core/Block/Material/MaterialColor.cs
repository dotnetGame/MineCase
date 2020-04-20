using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block.Material
{
    public struct MaterialColor : IEquatable<MaterialColor>
    {
        public int ColorValue { get; set; }

        public int ColorIndex { get; set; }

        public MaterialColor(int index, int value)
        {
            ColorIndex = index;
            ColorValue = value;
        }

        public override bool Equals(object obj)
        {
            return obj is MaterialColor && Equals((MaterialColor)obj);
        }

        public bool Equals(MaterialColor other)
        {
            return ColorValue == other.ColorValue &&
                   ColorIndex == other.ColorIndex;
        }

        public override int GetHashCode()
        {
            var hashCode = -81208087;
            hashCode = hashCode * -1521134295 + ColorValue.GetHashCode();
            hashCode = hashCode * -1521134295 + ColorIndex.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(MaterialColor state1, MaterialColor state2)
        {
            return state1.Equals(state2);
        }

        public static bool operator !=(MaterialColor state1, MaterialColor state2)
        {
            return !(state1 == state2);
        }
    }
}
