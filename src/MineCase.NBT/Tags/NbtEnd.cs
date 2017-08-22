using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.End"/>
    public sealed class NbtEnd : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.End;
        public override bool HasValue => false;
    }
}
