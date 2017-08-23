using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Nbt
{
    /// <summary>
    /// 表示 <see cref="NbtTagType"/> 所关联的 <see cref="Tags.NbtTag"/> 类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal class TagClassAttribute : Attribute
    {
        internal Type TagClassType { get; }

        internal TagClassAttribute(Type type)
        {
            TagClassType = type;
        }
    }

    /// <summary>
    /// NBT Tag 的类型
    /// </summary>
    /// <remarks>注释内容来自<see href="http://wiki.vg/NBT#Specification"/></remarks>
    public enum NbtTagType : byte
    {
        /// <summary>
        /// Signifies the end of a Compound (<see cref="Compound"/>). It is only ever used inside a Compound, and is not named despite being in a Compound
        /// </summary>
        [TagClass(typeof(Tags.NbtEnd))]
        End = 0x00,

        /// <summary>
        /// A single signed byte (<see cref="sbyte"/>)
        /// </summary>
        [TagClass(typeof(Tags.NbtByte))]
        Byte = 0x01,

        /// <summary>
        /// A single signed, big endian 16 bit integer (<see cref="short"/>)
        /// </summary>
        [TagClass(typeof(Tags.NbtShort))]
        Short = 0x02,

        /// <summary>
        /// A single signed, big endian 32 bit integer (<see cref="int"/>)
        /// </summary>
        [TagClass(typeof(Tags.NbtInt))]
        Int = 0x03,

        /// <summary>
        /// A single signed, big endian 64 bit integer (<see cref="long"/>)
        /// </summary>
        [TagClass(typeof(Tags.NbtLong))]
        Long = 0x04,

        /// <summary>
        /// A single, big endian IEEE-754 single-precision floating point number (<see cref="float"/>)
        /// </summary>
        [TagClass(typeof(Tags.NbtFloat))]
        Float = 0x05,

        /// <summary>
        /// A single, big endian IEEE-754 double-precision floating point number (<see cref="double"/>)
        /// </summary>
        [TagClass(typeof(Tags.NbtDouble))]
        Double = 0x06,

        /// <summary>
        /// A length-prefixed array of signed bytes (<see cref="T:sbyte[]"/>). The prefix is a signed integer (thus 4 bytes, <see cref="int"/>)
        /// </summary>
        [TagClass(typeof(Tags.NbtByteArray))]
        ByteArray = 0x07,

        /// <summary>
        /// A length-prefixed UTF-8 string (<see cref="string"/>).
        /// The prefix is an unsigned short (thus 2 bytes, <see cref="ushort"/>) signifying the length of the string in bytes
        /// </summary>
        [TagClass(typeof(Tags.NbtString))]
        String = 0x08,

        /// <summary>
        /// A list of nameless tags, all of the same type.
        /// The list is prefixed with the Type ID of the items it contains (thus 1 byte), and the length of the list as a signed integer (a further 4 bytes).
        /// If the length of the list is 0 or negative, the type may be 0 (<see cref="End"/>) but otherwise it must be any other type.
        /// </summary>
        /// <remarks>
        /// The notchian implementation uses End (<see cref="End"/>) in that situation, but another reference implementation by Mojang uses 1 instead;
        /// parsers should accept any type if the length is &lt;= 0
        /// </remarks>
        [TagClass(typeof(Tags.NbtList))]
        List = 0x09,

        /// <summary>
        /// Effectively a list of a named tags. Order is not guaranteed.
        /// </summary>
        [TagClass(typeof(Tags.NbtCompound))]
        Compound = 0x0a,

        /// <summary>
        /// A length-prefixed array of signed integers (<see cref="T:int[]"/>).
        /// The prefix is a signed integer (thus 4 bytes, <see cref="int"/>) and indicates the number of 4 byte integers.
        /// </summary>
        [TagClass(typeof(Tags.NbtIntArray))]
        IntArray = 0x0b
    }
}
