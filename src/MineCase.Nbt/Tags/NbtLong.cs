using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.Long"/>
    public sealed class NbtLong : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Long;

        public override bool HasValue => true;

        public long Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtLong"/> class.<para />
        /// 默认构造函数.
        /// </summary>
        /// <param name="value">要初始化的值.</param>
        /// <param name="name">该 Tag 的名称.</param>
        public NbtLong(long value)
        {
            Value = value;
        }

        private class Serializer : ITagSerializer
        {
            public NbtTag Deserialize(BinaryReader br)
            {
                var value = br.ReadInt64().ToggleEndian();
                return new NbtLong(value);
            }

            public void Serialize(NbtTag tag, BinaryWriter bw)
            {
                var nbtLong = (NbtLong)tag;
                bw.Write(nbtLong.Value.ToggleEndian());
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.Long, new Serializer());
        }
    }
}
