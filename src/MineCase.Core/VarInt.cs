using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase
{
    public class VarInt
    {
        public static uint SizeOf(uint value)
        {
            uint numWrite = 0;
            do
            {
                value >>= 7;
                numWrite++;
            }
            while (value != 0);
            return numWrite;
        }
    }
}
