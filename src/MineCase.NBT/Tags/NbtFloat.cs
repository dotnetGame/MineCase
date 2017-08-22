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


    }
}
