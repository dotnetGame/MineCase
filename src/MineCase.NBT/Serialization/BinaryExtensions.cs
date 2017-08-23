using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Nbt.Serialization
{
    internal static class BinaryExtensions
    {
        internal static NbtTagType ReadTagType(this BinaryReader br)
        {
            return (NbtTagType) br.ReadByte();
        }
        
        internal static string ReadTagString(this BinaryReader br)
        {
            // TODO: 名称长度未说明是否有符号，假设为无符号，若为有符号则与此方法不兼容，需要另外实现
            var strLen = br.ReadUInt16();
            return strLen == 0 ? null : new string(Encoding.UTF8.GetChars(br.ReadBytes(strLen)));
        }

        internal static void Write(this BinaryWriter bw, NbtTagType tagType)
        {
            bw.Write((byte) tagType);
        }

        internal static void WriteTagString(this BinaryWriter bw, string value)
        {
            if (value == null)
            {
                // TODO: 这个行为是否正确？
                bw.Write((ushort) 0);
                return;
            }

            bw.Write((ushort) value.Length);
            bw.Write(Encoding.UTF8.GetBytes(value));
        }
    }
}
