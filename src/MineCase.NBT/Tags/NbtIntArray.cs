using System;
using System.Collections.Generic;
using System.Text;

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

        /// <summary>默认构造函数</summary>
        /// <param name="value">要初始化的值</param>
        /// <param name="name">该 Tag 的名称</param>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> 为 null</exception>
        public NbtIntArray(int[] value, string name = null) : base(name)
        {
            Value = value;
        }
    }
}
