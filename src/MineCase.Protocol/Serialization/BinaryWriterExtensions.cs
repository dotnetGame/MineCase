using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Serialization
{
    internal static class BinaryWriterExtensions
    {
        public static void WriteAsBoolean(this BinaryWriter bw, bool value) =>
            bw.Write(value);

        public static void WriteAsByte(this BinaryWriter bw, byte value) =>
            bw.Write(value);

        /// <see cref="http://wiki.vg/Protocol#VarInt_and_VarLong"/>
        public static void WriteAsVarInt(this BinaryWriter bw, uint value)
        {
            do
            {
                byte temp = (byte)(value & 0b01111111);
                value >>= 7;
                if (value != 0)
                    temp |= 0b10000000;
                bw.Write(temp);
            } while (value != 0);
        }

        public static void WriteAsByteArray(this BinaryWriter bw, byte[] value) =>
            bw.Write(value);
    }

    internal static class DataTypeSizeExtensions
    {
        public static uint SizeOfVarInt(this uint value)
        {
            return 0;
        }
    }
}
