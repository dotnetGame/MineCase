using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.Short"/>
    public sealed class NbtShort : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Short;

        public override bool HasValue => true;

        public short Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtShort"/> class.<para />
        /// 默认构造函数
        /// </summary>
        /// <param name="value">要初始化的值</param>
        /// <param name="name">该 Tag 的名称</param>
        public NbtShort(short value, string name = null)
            : base(name)
        {
            Value = value;
        }

        private class Serializer : ITagSerializer
        {
            public NbtTag Deserialize(BinaryReader br, bool requireName)
            {
                string name = null;
                if (requireName)
                {
                    name = br.ReadTagString();
                }

                var value = br.ReadInt16().ToggleEndian();
                return new NbtShort(value, name);
            }

            public void Serialize(NbtTag tag, BinaryWriter bw, bool requireName)
            {
                var nbtShort = (NbtShort)tag;

                if (requireName)
                {
                    bw.WriteTagValue(nbtShort.Name);
                }

                bw.Write(nbtShort.Value.ToggleEndian());
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.Short, new Serializer());
        }
    }
}
