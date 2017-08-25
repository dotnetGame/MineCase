using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.IntArray"/>
    public sealed class NbtIntArray : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.IntArray;

        public override bool HasValue => true;

        private int[] _value;

        public int[] Value
        {
            get => _value;
            set => _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtIntArray"/> class.<para />
        /// 默认构造函数
        /// </summary>
        /// <param name="value">要初始化的值</param>
        /// <param name="name">该 Tag 的名称</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> 为 null</exception>
        public NbtIntArray(int[] value, string name = null)
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

                var value = br.ReadTagIntArray(br.ReadInt32().ToggleEndian());
                return new NbtIntArray(value, name);
            }

            public void Serialize(NbtTag tag, BinaryWriter bw, bool requireName)
            {
                var nbtIntArray = (NbtIntArray)tag;

                if (requireName)
                {
                    bw.WriteTagValue(nbtIntArray.Name);
                }

                bw.WriteTagValue(nbtIntArray.Value);
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.IntArray, new Serializer());
        }
    }
}
