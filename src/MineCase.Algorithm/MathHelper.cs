using System;
using System.Collections.Generic;


namespace MineCase.Algorithm
{
    public class MathHelper
    {
        public static float denormalizeClamp(float min,float max,float value)
        {
            if(value < 0)
            {
                return min;
            }
            else if(value > 1)
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
