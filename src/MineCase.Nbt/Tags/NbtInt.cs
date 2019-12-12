using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.Int"/>
    public sealed class NbtInt : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Int;

        public override bool HasValue => true;

        public int Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtInt"/> class.<para />
        /// 默认构造函数.
        /// </summary>
        /// <param name="value">要初始化的值.</param>
        /// <param name="name">该 Tag 的名称.</param>
        public NbtInt(int value, string name = null)
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

                var value = br.ReadInt32().ToggleEndian();
                return new NbtInt(value, name);
            }

            public void Serialize(NbtTag tag, BinaryWriter bw, bool requireName)
            {
                var nbtInt = (NbtInt)tag;

                if (requireName)
                {
                    bw.WriteTagValue(nbtInt.Name);
                }

                bw.Write(nbtInt.Value.ToggleEndian());
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.Int, new Serializer());
        }
    }
}
