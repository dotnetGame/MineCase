using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.Float"/>
    public sealed class NbtFloat : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Float;

        public override bool HasValue => true;

        public float Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtFloat"/> class.<para />
        /// 默认构造函数
        /// </summary>
        /// <param name="value">要初始化的值</param>
        /// <param name="name">该 Tag 的名称</param>
        public NbtFloat(float value, string name = null)
            : base(name)
        {
            Value = value;
        }
    }
}
