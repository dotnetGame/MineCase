using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.Short"/>
    public sealed class NbtShort : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Short;
        public override bool HasValue => true;
        public short Value { get; set; }

    }
}
