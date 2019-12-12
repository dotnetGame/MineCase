using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.NbtLongArray"/>
    public sealed class NbtLongArray : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.LongArray;

        public override bool HasValue => true;

        private long[] _value;

        public long[] Value
        {
            get => _value;
            set => _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtLongArray"/> class.<para />
        /// 默认构造函数.
        /// </summary>
        /// <param name="value">要初始化的值.</param>
        /// <param name="name">该 Tag 的名称.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> 为 null.</exception>
        public NbtLongArray(long[] value)
        {
            Value = value;
        }

        private class Serializer : ITagSerializer
        {
            public NbtTag Deserialize(BinaryReader br)
            {
                var value = br.ReadTagLongArray(br.ReadInt32().ToggleEndian());
                return new NbtLongArray(value);
            }

            public void Serialize(NbtTag tag, BinaryWriter bw)
            {
                var nbtLongArray = (NbtLongArray)tag;
                bw.WriteTagValue(nbtLongArray.Value);
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.LongArray, new Serializer());
        }
    }
}
