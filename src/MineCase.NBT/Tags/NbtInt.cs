﻿using System;
using System.Collections.Generic;
using System.Text;

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
        /// 默认构造函数
        /// </summary>
        /// <param name="value">要初始化的值</param>
        /// <param name="name">该 Tag 的名称</param>
        public NbtInt(int value, string name = null)
            : base(name)
        {
            Value = value;
        }
    }
}
