using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.Double"/>
    public sealed class NbtDouble : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Double;

        public override bool HasValue => true;

        public double Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtDouble"/> class.<para />
        /// 默认构造函数
        /// </summary>
        /// <param name="value">要初始化的值</param>
        /// <param name="name">该 Tag 的名称</param>
        public NbtDouble(double value, string name = null)
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

                var value = br.ReadTagDouble();
                return new NbtDouble(value, name);
            }

            public void Serialize(NbtTag tag, BinaryWriter bw, bool requireName)
            {
                var nbtDouble = (NbtDouble)tag;

                if (requireName)
                {
                    bw.WriteTagValue(nbtDouble.Name);
                }

                bw.WriteTagValue(nbtDouble.Value);
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.Double, new Serializer());
        }
    }
}
