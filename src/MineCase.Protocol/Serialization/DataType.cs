using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Protocol.Serialization
{
    internal static class DataTypeSizeExtensions
    {
        public static uint SizeOfVarInt(this uint value)
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

        public static ushort ToBigEndian(this ushort value)
        {
            return (ushort)((value >> 8) | (((byte)value) << 8));
        }

        public static uint ToBigEndian(this uint value)
        {
            return (value >> 24) | ((value & 0x00FF_0000) >> 8) |
                ((value & 0x0000_FF00) << 8) | ((value & 0x0000_00FF) << 24);
        }

        public static ulong ToBigEndian(this ulong value)
        {
            return (value >> 56) | ((value & 0x00FF_0000_0000_0000) >> 40) | ((value & 0x0000_FF00_0000_0000) >> 24) |
                ((value & 0x0000_00FF_0000_0000) >> 8) | ((value & 0x0000_0000_FF00_0000) << 8) | ((value & 0x0000_0000_00FF_0000) << 24) |
                ((value & 0x0000_0000_0000_FF00) << 40) | ((value & 0x0000_0000_0000_00FF) << 56);
        }
    }
}
