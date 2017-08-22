using System;
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


    }
}
