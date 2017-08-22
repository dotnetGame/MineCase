using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.Byte"/>
    public sealed class NbtByte : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Byte;
        public override bool HasValue => true;
        public sbyte Value { get; set; }


    }
}
