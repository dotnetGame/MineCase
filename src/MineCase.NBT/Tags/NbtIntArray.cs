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
        public int[] Value { get; set; }


    }
}
