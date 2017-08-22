using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.String"/>
    public sealed class NbtString : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.String;
        public override bool HasValue => true;
        public string Value { get; set; }


    }
}
