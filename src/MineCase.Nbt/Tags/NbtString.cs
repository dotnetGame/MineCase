using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.String"/>
    public sealed class NbtString : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.String;

        public override bool HasValue => true;

        private string _value;

        public string Value
        {
            get => _value;
            set => _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtString"/> class.<para />
        /// 默认构造函数
        /// </summary>
        /// <param name="value">要初始化的值</param>
        /// <param name="name">该 Tag 的名称</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> 为 null</exception>
        public NbtString(string value, string name = null)
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

                var value = br.ReadTagString();
                return new NbtString(value, name);
            }

            public void Serialize(NbtTag tag, BinaryWriter bw, bool requireName)
            {
                var nbtString = (NbtString)tag;

                if (requireName)
                {
                    bw.WriteTagValue(nbtString.Name);
                }

                bw.WriteTagValue(nbtString.Value);
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.String, new Serializer());
        }
    }
}
