using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.Double"/>
    public sealed class NbtDouble : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Double;
        public override bool HasValue => true;
        public double Value { get; set; }

        /// <summary>默认构造函数</summary>
        /// <param name="value">要初始化的值</param>
        /// <param name="name">该 Tag 的名称</param>
        public NbtDouble(double value, string name = null) : base(name)
        {
            Value = value;
        }
    }
}
