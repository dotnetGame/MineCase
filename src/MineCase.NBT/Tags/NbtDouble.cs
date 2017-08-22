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


    }
}
