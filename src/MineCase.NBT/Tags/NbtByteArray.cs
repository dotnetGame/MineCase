using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.ByteArray"/>
    public sealed class NbtByteArray : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.ByteArray;
        public override bool HasValue => true;
        public sbyte[] Value { get; set; }


    }
}
