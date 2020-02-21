using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Util.Math
{
    public class MathHelper
    {
        /// <summary>
        /// Find the smallest multiple of interval greater than number.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static int RoundUp(int number, int interval)
        {
            if (interval == 0)
            {
                return 0;
            }
            else if (number == 0)
            {
                return interval;
            }
            else
            {
                if (number < 0)
                {
                    interval *= -1;
                }

                int i = number % interval;
                return i == 0 ? number : number + interval - i;
            }
        }
    }
}
