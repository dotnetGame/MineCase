using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.Double"/>
    public sealed class NbtDouble : NbtNumber
    {
        public override NbtTagType TagType => NbtTagType.Double;

        public override bool HasValue => true;

        public double Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtDouble"/> class.<para />
        /// 默认构造函数.
        /// </summary>
        /// <param name="value">要初始化的值.</param>
        /// <param name="name">该 Tag 的名称.</param>
        public NbtDouble(double value)
        {
            Value = value;
        }

        private class Serializer : ITagSerializer
        {
            public NbtTag Deserialize(BinaryReader br)
            {
                var value = br.ReadTagDouble();
                return new NbtDouble(value);
            }

            public void Serialize(NbtTag tag, BinaryWriter bw)
            {
                var nbtDouble = (NbtDouble)tag;
                bw.WriteTagValue(nbtDouble.Value);
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.Double, new Serializer());
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
            throw new NotImplementedException();
        }

        public override sbyte GetByte()
        {
            throw new NotImplementedException();
        }

        public override double GetDouble()
        {
            return Value;
        }

        public override float GetFloat()
        {
            throw new NotImplementedException();
        }
    }
}
