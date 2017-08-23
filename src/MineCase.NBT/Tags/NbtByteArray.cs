using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.ByteArray"/>
    public sealed class NbtByteArray : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.ByteArray;
        public override bool HasValue => true;

        private sbyte[] _value;
        public sbyte[] Value
        {
            get => _value;
            set => _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>默认构造函数</summary>
        /// <param name="value">要初始化的值</param>
        /// <param name="name">该 Tag 的名称</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> 为 null</exception>
        public NbtByteArray(sbyte[] value, string name = null) : base(name)
        {
            Value = value;
        }
    }
}
