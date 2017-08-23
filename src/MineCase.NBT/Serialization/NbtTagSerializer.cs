using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using MineCase.Nbt.Tags;

namespace MineCase.Nbt.Serialization
{
    /// <summary>
    /// Tag 的自定义序列化及反序列化接口
    /// </summary>
    internal interface ITagSerializer
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="br">已打开的 <see cref="BinaryReader"/></param>
        /// <param name="requireName">当前上下文是否需要 Tag 具有名称</param>
        /// <remarks>实现保证此方法被调用之时一定已经读取到了当前注册到的 <see cref="NbtTagType"/>，且 <paramref name="br"/> 一定不为 null</remarks>
        NbtTag Deserialize(BinaryReader br, bool requireName);

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="tag">要序列化的 Tag</param>
        /// <param name="bw">已打开的 <see cref="BinaryWriter"/></param>
        /// <remarks>
        /// 实现保证此方法被调用之时一定已经写入了当前注册到的 <see cref="NbtTagType"/>，
        /// 且 <paramref name="tag"/> 及 <paramref name="bw"/> 一定不为 null，
        /// 并且 <paramref name="tag"/> 一定为当前注册到的 <see cref="NbtTagType"/> 关联的 <see cref="NbtTag"/> 类型
        /// </remarks>
        void Serialize(NbtTag tag, BinaryWriter bw);
    }

    /// <summary>
    /// 用于将 NbtTag 进行序列化和反序列化的类
    /// </summary>
    public static class NbtTagSerializer
    {
        private static readonly Dictionary<NbtTagType, ITagSerializer> TagDictionary = new Dictionary<NbtTagType, ITagSerializer>();

        internal static void RegisterTag(NbtTagType tagType, ITagSerializer tagSerializer)
        {
            TagDictionary.Add(tagType, tagSerializer);
        }

        /// <summary>
        /// 反序列化 Tag
        /// </summary>
        /// <param name="br">已打开的 <see cref="BinaryReader"/></param>
        /// <param name="requireName">当前上下文是否需要 Tag 具有名称</param>
        /// <exception cref="ArgumentNullException"><paramref name="br"/> 为 null</exception>
        public static NbtTag DeserializeTag(BinaryReader br, bool requireName)
        {
            if (br == null)
            {
                throw new ArgumentNullException(nameof(br));
            }

            var tagType = br.ReadTagType();
            return TagDictionary[tagType].Deserialize(br, requireName);
        }

        /// <summary>
        /// 以指定的类型反序列化 Tag
        /// </summary>
        /// <typeparam name="T">指定的类型</typeparam>
        /// <param name="br">已打开的 <see cref="BinaryReader"/></param>
        /// <param name="requireName">当前上下文是否需要 Tag 具有名称</param>
        /// <exception cref="ArgumentNullException"><paramref name="br"/> 为 null</exception>
        /// <exception cref="InvalidCastException">无法将获得的 Tag 转换到类型 <typeparamref name="T"/></exception>
        public static T DeserializeTag<T>(BinaryReader br, bool requireName) where T : NbtTag
        {
            return (T) DeserializeTag(br, requireName);
        }

        /// <summary>
        /// 序列化 Tag
        /// </summary>
        /// <param name="tag">要序列化的 Tag</param>
        /// <param name="bw">已打开的 <see cref="BinaryWriter"/></param>
        /// <exception cref="ArgumentNullException"><paramref name="bw"/> 为 null</exception>
        public static void SerializeTag(NbtTag tag, BinaryWriter bw)
        {
            var tagType = tag.TagType;
            TagDictionary[tagType].Serialize(tag, bw);
        }
    }
}
