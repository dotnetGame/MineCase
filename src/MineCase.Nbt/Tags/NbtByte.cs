using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.Byte"/>
    public sealed class NbtByte : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Byte;

        public override bool HasValue => true;

        public sbyte Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtByte"/> class. <para />
        /// 默认构造函数.
        /// </summary>
        /// <param name="value">要初始化的值.</param>
        public NbtByte(sbyte value)
        {
            Value = value;
        }

        private class Serializer : ITagSerializer
        {
            public NbtTag Deserialize(BinaryReader br)
            {
                var value = br.ReadSByte();
                return new NbtByte(value);
            }

            public void Serialize(NbtTag tag, BinaryWriter bw)
            {
                var nbtByte = (NbtByte)tag;
                bw.Write(nbtByte.Value);
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.Byte, new Serializer());
        }
    }
}
