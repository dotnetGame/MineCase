using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.End"/>
    public sealed class NbtEnd : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.End;

        public override bool HasValue => false;

        private class Serializer : ITagSerializer
        {
            public NbtTag Deserialize(BinaryReader br, bool requireName)
            {
                return new NbtEnd();
            }

            public void Serialize(NbtTag tag, BinaryWriter bw, bool requireName)
            {
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.End, new Serializer());
        }
    }
}
