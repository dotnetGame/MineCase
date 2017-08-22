using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.Long"/>
    public sealed class NbtLong : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Long;
        public override bool HasValue => true;
        public long Value { get; set; }


    }
}
