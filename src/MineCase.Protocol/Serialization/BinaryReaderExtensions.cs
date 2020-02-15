using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Protocol.Serialization
{
    internal static class BinaryReaderExtensions
    {
        public static sbyte ReadAsByte(this BinaryReader br) =>
            br.ReadSByte();

        public static bool ReadAsBoolean(this BinaryReader br) =>
            br.ReadBoolean();

        public static byte ReadAsUnsignedByte(this BinaryReader br) =>
            br.ReadByte();

        public static short ReadAsShort(this BinaryReader br) =>
            br.ReadInt16();

        public static ushort ReadAsUnsignedShort(this BinaryReader br) =>
            br.ReadUInt16();

        public static int ReadAsInt(this BinaryReader br) =>
            br.ReadInt32();

        public static long ReadAsLong(this BinaryReader br) =>
            br.ReadInt64();

        // http://wiki.vg/Protocol#VarInt_and_VarLong"
        public static uint ReadAsVarInt(this BinaryReader br, out int bytesRead)
        {
            int numRead = 0;
            uint result = 0;
            byte read;
            do
            {
                read = br.ReadByte();
                uint value = (uint)(read & 0b01111111);
                result |= value << (7 * numRead);

                numRead++;
                if (numRead > 5)
                    throw new InvalidDataException("VarInt is too big");
            }
            while ((read & 0b10000000) != 0);

            bytesRead = numRead;
            return result;
        }

        public static string ReadAsString(this BinaryReader br)
        {
            var len = br.ReadAsVarInt(out _);
            var bytes = br.ReadBytes((int)len);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
