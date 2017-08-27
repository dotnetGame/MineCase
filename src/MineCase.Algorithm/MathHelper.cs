using System;
using System.Collections.Generic;


namespace MineCase.Algorithm
{
    public class MathHelper
    {
        public static T denormalizeClamp<T>(T min,T max,T value)
            where T:IComparable
        {
            if(value.CompareTo(0) < 0)
            {
                return min;
            }
            else if(value.CompareTo(1) > 0)
            {
                return max;
            }
            else
            {
                return value;
            }
        }
    }
}
