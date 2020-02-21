using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.Short"/>
    public sealed class NbtShort : NbtNumber
    {
        public override NbtTagType TagType => NbtTagType.Short;

        public override bool HasValue => true;

        public short Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtShort"/> class.<para />
        /// 默认构造函数.
        /// </summary>
        /// <param name="value">要初始化的值.</param>
        /// <param name="name">该 Tag 的名称.</param>
        public NbtShort(short value)
        {
            Value = value;
        }

        private class Serializer : ITagSerializer
        {
            public NbtTag Deserialize(BinaryReader br)
            {
                var value = br.ReadInt16().ToggleEndian();
                return new NbtShort(value);
            }

            public void Serialize(NbtTag tag, BinaryWriter bw)
            {
                var nbtShort = (NbtShort)tag;
                bw.Write(nbtShort.Value.ToggleEndian());
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.Short, new Serializer());
        }

        public override long GetLong()
        {
            throw new NotImplementedException();
        }

        public override int GetInt()
        {
            throw new NotImplementedException();
        }

        public override short GetShort()
        {
            return Value;
        }

        public override sbyte GetByte()
        {
            throw new NotImplementedException();
        }

        public override double GetDouble()
        {
            throw new NotImplementedException();
        }

        public override float GetFloat()
        {
            throw new NotImplementedException();
        }
    }
}
