using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Serialization
{
    public enum DataType
    {
        Boolean,
        Byte,
        UnsignedByte,
        Short,
        UnsignedShort,
        Int,
        Long,
        Float,
        Double,
        String,
        Chat,
        VarInt,
        VarLong,
        EntityMetadata,
        Slot,
        NBTTag,
        Position,
        Angle,
        UUID,
        ByteArray,
        IntArray,
        NbtArray,
        Array
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class SerializeAsAttribute : Attribute
    {
        public DataType DataType { get; }

        public SerializeAsAttribute(DataType dataType)
        {
            DataType = dataType;
        }
    }
}
