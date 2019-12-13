using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MineCase.Nbt.Tags;

namespace MineCase.Nbt.Serialization
{
    /// <summary>
    /// Tag 的自定义序列化及反序列化接口.
    /// </summary>
    internal interface ITagSerializer
    {
        /// <summary>
        /// 反序列化.
        /// </summary>
        /// <param name="br">已打开的 <see cref="BinaryReader"/>.</param>
        /// <remarks>实现保证此方法被调用之时一定已经读取到了当前注册到的 <see cref="NbtTagType"/>，且 <paramref name="br"/> 一定不为 null.</remarks>
        NbtTag Deserialize(BinaryReader br);

        /// <summary>
        /// 序列化.
        /// </summary>
        /// <param name="tag">要序列化的 Tag.</param>
        /// <param name="bw">已打开的 <see cref="BinaryWriter"/>.</param>
        /// <remarks>
        /// 实现保证此方法被调用之时一定已经写入了当前注册到的 <see cref="NbtTagType"/>，
        /// 且 <paramref name="tag"/> 及 <paramref name="bw"/> 一定不为 null，
        /// 并且 <paramref name="tag"/> 一定为当前注册到的 <see cref="NbtTagType"/> 关联的 <see cref="NbtTag"/> 类型.
        /// </remarks>
        void Serialize(NbtTag tag, BinaryWriter bw);
    }

    /// <summary>
    /// 用于将 NbtTag 进行序列化和反序列化的类.
    /// </summary>
    public static class NbtTagSerializer
    {
        private static readonly Dictionary<NbtTagType, ITagSerializer> TagDictionary = new Dictionary<NbtTagType, ITagSerializer>();

        internal static void RegisterTag(NbtTagType tagType, ITagSerializer tagSerializer)
        {
            TagDictionary.Add(tagType, tagSerializer);
        }

        /// <summary>
        /// 反序列化 Tag.
        /// </summary>
        /// <param name="br">已打开的 <see cref="BinaryReader"/>.</param>
        /// <param name="requireName">当前上下文是否需要 Tag 具有名称，这个参数将会直接被转发到 <see cref="ITagSerializer.Deserialize(BinaryReader, bool)"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="br"/> 为 null.</exception>
        public static NbtTag DeserializeTag(BinaryReader br)
        {
            if (br == null)
            {
                throw new ArgumentNullException(nameof(br));
            }

            Contract.EndContractBlock();

            var tagType = br.ReadTagType();

            return TagDictionary[tagType].Deserialize(br);
        }

        public static NbtTag DeserializeTag(BinaryReader br, out string name)
        {
            if (br == null)
            {
                throw new ArgumentNullException(nameof(br));
            }

            Contract.EndContractBlock();

            var tagType = br.ReadTagType();
            name = br.ReadTagString();

            return TagDictionary[tagType].Deserialize(br);
        }

        /// <summary>
        /// 以指定的类型反序列化 Tag.
        /// </summary>
        /// <typeparam name="T">指定的类型.</typeparam>
        /// <param name="br">已打开的 <see cref="BinaryReader"/>.</param>
        /// <param name="requireName">当前上下文是否需要 Tag 具有名称，这个参数将会直接被转发到 <see cref="ITagSerializer.Deserialize(BinaryReader, bool)"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="br"/> 为 null.</exception>
        /// <exception cref="InvalidCastException">无法将获得的 Tag 转换到类型 <typeparamref name="T"/>.</exception>
        public static T DeserializeTag<T>(BinaryReader br)
            where T : NbtTag
        {
            return (T)DeserializeTag(br);
        }

        public static T DeserializeTag<T>(BinaryReader br, out string name)
            where T : NbtTag
        {
            return (T)DeserializeTag(br, out name);
        }

        /// <summary>
        /// 使用已知的 <see cref="NbtTagType"/> 反序列化 Tag.
        /// </summary>
        /// <remarks>将不会试图读取 <see cref="NbtTagType"/>.</remarks>
        /// <param name="br">已打开的 <see cref="BinaryReader"/>.</param>
        /// <param name="tagType">已知的 <see cref="NbtTagType"/>.</param>
        /// <param name="requireName">当前上下文是否需要 Tag 具有名称，这个参数将会直接被转发到 <see cref="ITagSerializer.Deserialize(BinaryReader, bool)"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="br"/> 为 null.</exception>
        public static NbtTag DeserializeTag(BinaryReader br, NbtTagType tagType)
        {
            if (br == null)
            {
                throw new ArgumentNullException(nameof(br));
            }

            Contract.EndContractBlock();

            return TagDictionary[tagType].Deserialize(br);
        }

        public static NbtTag DeserializeTag(BinaryReader br, NbtTagType tagType, out string name)
        {
            if (br == null)
            {
                throw new ArgumentNullException(nameof(br));
            }

            Contract.EndContractBlock();

            name = br.ReadTagString();

            return TagDictionary[tagType].Deserialize(br);
        }

        /// <summary>
        /// 使用已知的 <see cref="NbtTagType"/>，以指定的类型反序列化 Tag.
        /// </summary>
        /// <remarks>将不会试图读取 <see cref="NbtTagType"/>.</remarks>
        /// <typeparam name="T">指定的类型.</typeparam>
        /// <param name="br">已打开的 <see cref="BinaryReader"/>.</param>
        /// <param name="tagType">已知的 <see cref="NbtTagType"/>.</param>
        /// <param name="requireName">当前上下文是否需要 Tag 具有名称，这个参数将会直接被转发到 <see cref="ITagSerializer.Deserialize(BinaryReader, bool)"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="br"/> 为 null.</exception>
        /// <exception cref="InvalidCastException">无法将获得的 Tag 转换到类型 <typeparamref name="T"/>.</exception>
        public static T DeserializeTag<T>(BinaryReader br, NbtTagType tagType)
            where T : NbtTag
        {
            return (T)DeserializeTag(br, tagType);
        }

        /// <summary>
        /// 序列化 Tag.
        /// </summary>
        /// <param name="tag">要序列化的 Tag.</param>
        /// <param name="bw">已打开的 <see cref="BinaryWriter"/>.</param>
        /// <param name="writeTagType">指示是否需要写入 <see cref="NbtTagType"/>.</param>
        /// <param name="requireName">指示是否需要写入名称，这个参数将会直接被转发到 <see cref="ITagSerializer.Serialize(NbtTag, BinaryWriter, bool)"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> 或 <paramref name="bw"/> 为 null.</exception>
        public static void SerializeTag(NbtTag tag, BinaryWriter bw, bool writeTagType = true, string name = null)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            if (bw == null)
            {
                throw new ArgumentNullException(nameof(bw));
            }

            Contract.EndContractBlock();

            var tagType = tag.TagType;
            var tagSerializer = TagDictionary[tagType];

            if (writeTagType)
            {
                bw.WriteTagValue(tagType);
            }

            if (name != null)
            {
                bw.WriteTagValue(name);
            }

            tagSerializer.Serialize(tag, bw);
        }

        static NbtTagSerializer()
        {
            foreach (var type in from t in typeof(NbtTag).GetTypeInfo().Assembly.DefinedTypes where t.IsSubclassOf(typeof(NbtTag)) select t)
            {
                type.GetDeclaredMethod("RegisterSerializer").Invoke(null, null);
            }
        }
    }
}
