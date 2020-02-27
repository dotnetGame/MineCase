using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.Compound"/>
    public sealed class NbtCompound : NbtTag, IEnumerable<NbtTag>
    {
        public override NbtTagType TagType => NbtTagType.Compound;

        public override bool HasValue => false;

        private readonly Dictionary<string, NbtTag> _childTags;

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtCompound"/> class.<para />
        /// 默认构造方法.
        /// </summary>
        /// <param name="name">该 Tag 的名称.</param>
        public NbtCompound(string name = null)
            : this(null, name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtCompound"/> class.<para />
        /// 从 <paramref name="tags"/> 初始化子 Tag 的构造方法.
        /// </summary>
        /// <param name="tags">要用于提供子 Tag 的范围.</param>
        /// <param name="name">该 Tag 的名称.</param>
        /// <remarks><paramref name="tags"/> 为 null 时将子 Tag 初始化为空集合.</remarks>
        /// <exception cref="ArgumentException"><paramref name="tags"/> 中包含了不具有名称的 Tag.</exception>
        /// <exception cref="ArgumentException"><paramref name="tags"/> 中包含了 null.</exception>
        public NbtCompound(IEnumerable<NbtTag> tags, string name = null)
            : base(name)
        {
            _childTags =
                tags?.ToDictionary(
                    tag => tag?.Name ?? throw new ArgumentException($"{nameof(tags)} 中包含了不具有名称的 Tag", nameof(tags)),
                    tag => tag ?? throw new ArgumentException($"{nameof(tags)} 中包含了 null", nameof(tags))) ??
                new Dictionary<string, NbtTag>();
        }

        /// <see cref="Get(string)"/>
        public NbtTag this[string tagName] => Get(tagName);

        /// <summary>以指定的 Tag 名称获取 Tag</summary>
        /// <param name="tagName">Tag 的名称</param>
        /// <exception cref="ArgumentNullException"><paramref name="tagName"/> 为 null</exception>
        /// <exception cref="KeyNotFoundException">未能自子 Tag 中寻找到具有指定名称的 Tag</exception>
        public NbtTag Get(string tagName)
        {
            if (tagName == null)
            {
                throw new ArgumentNullException(nameof(tagName));
            }

            Contract.EndContractBlock();

            return _childTags[tagName];
        }

        /// <summary>以指定的 Tag 名称及期望的 Tag 类型获取 Tag</summary>
        /// <typeparam name="T">期望获取的 Tag 类型</typeparam>
        /// <param name="tagName">Tag 的名称</param>
        /// <exception cref="ArgumentNullException"><paramref name="tagName"/> 为 null</exception>
        /// <exception cref="KeyNotFoundException">未能自子 Tag 中寻找到具有指定名称的 Tag</exception>
        /// <exception cref="InvalidCastException">未能将获得的 Tag 转换为 <typeparamref name="T"/></exception>
        public T Get<T>(string tagName)
            where T : NbtTag
        {
            return (T)Get(tagName);
        }

        /// <summary>是否包含指定名称的子 Tag</summary>
        /// <param name="tagName">指定的名称</param>
        public bool ContainsTagName(string tagName)
        {
            return _childTags.ContainsKey(tagName);
        }

        /// <summary>判断指定的 Tag 是否是该 NbtCompound 的子 Tag</summary>
        /// <param name="tag">指定的 Tag</param>
        public bool ContainsTag(NbtTag tag)
        {
            return tag.Parent == this && _childTags.ContainsValue(tag);
        }

        /// <summary>添加子 Tag</summary>
        /// <param name="tag">需要添加的 tag</param>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> 为 null</exception>
        /// <exception cref="ArgumentException"><paramref name="tag"/> 不具有名称</exception>
        /// <exception cref="ArgumentException"><paramref name="tag"/> 与已存在的子 Tag 重名</exception>
        public void Add(NbtTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            if (tag.Name == null)
            {
                throw new ArgumentException("NbtCompound 的子 Tag 必须具有名称", nameof(tag));
            }

            if (_childTags.ContainsKey(tag.Name))
            {
                throw new ArgumentException($"试图加入具有名称{tag.Name}的 Tag，但因已有重名的子 Tag 而失败", nameof(tag));
            }

            Contract.EndContractBlock();

            _childTags.Add(tag.Name, tag);
            tag.Parent = this;
        }

        /// <summary>移除名称为 <paramref name="tagName"/> 的子 Tag</summary>
        /// <param name="tagName">要移除的子 Tag 的名称</param>
        /// <exception cref="ArgumentNullException"><paramref name="tagName"/> 为 null</exception>
        /// <exception cref="ArgumentException">无法找到具有指定名称的子 Tag</exception>
        public void Remove(string tagName)
        {
            if (tagName == null)
            {
                throw new ArgumentNullException(nameof(tagName));
            }

            if (!_childTags.TryGetValue(tagName, out var tag))
            {
                throw new ArgumentException($"此 NbtCompound 没有具有名称 \"{tagName}\" 的子 Tag", nameof(tagName));
            }

            Contract.EndContractBlock();

            _childTags.Remove(tagName);
            tag.Parent = null;
        }

        /// <summary>移除子 Tag</summary>
        /// <param name="tag">要移除的子 Tag</param>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> 为 null</exception>
        /// <exception cref="ArgumentException"><paramref name="tag"/>并非本 Tag 的子 Tag</exception>
        public void Remove(NbtTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            if (tag.Parent != this || !_childTags.ContainsValue(tag))
            {
                throw new ArgumentException($"{nameof(tag)} 不属于此 NbtCompound", nameof(tag));
            }

            Contract.EndContractBlock();

            _childTags.Remove(tag.Name);
            tag.Parent = null;
        }

        protected override void OnChildTagRenamed(NbtTag tag, string newName)
        {
            Debug.Assert(tag != null, $"{nameof(tag)} 不得为空");
            Debug.Assert(tag.Name != null, $"{nameof(tag)} 必须具有名称");
            Debug.Assert(tag.Parent == this && _childTags.ContainsKey(tag.Name) && _childTags.ContainsValue(tag), $"{nameof(tag)} 不属于此 NbtCompound");

            _childTags.Remove(tag.Name);
            _childTags.Add(newName, tag);
        }

        public override void Accept(INbtTagVisitor visitor)
        {
            base.Accept(visitor);

            visitor.StartChild();

            foreach (var tag in _childTags)
            {
                tag.Value.Accept(visitor);
            }

            visitor.EndChild();
        }

        public IEnumerator<NbtTag> GetEnumerator()
        {
            return _childTags.Select(pair => pair.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Serializer : ITagSerializer
        {
            public NbtTag Deserialize(BinaryReader br, bool requireName)
            {
                string name = null;
                if (requireName)
                {
                    name = br.ReadTagString();
                }

                var elements = new List<NbtTag>();
                while (true)
                {
                    var curElement = NbtTagSerializer.DeserializeTag(br);
                    if (curElement.TagType == NbtTagType.End)
                    {
                        break;
                    }

                    elements.Add(curElement);
                }

                return new NbtCompound(elements, name);
            }

            public void Serialize(NbtTag tag, BinaryWriter bw, bool requireName)
            {
                var nbtCompound = (NbtCompound)tag;

                if (requireName)
                {
                    bw.WriteTagValue(nbtCompound.Name);
                }

                foreach (var elem in nbtCompound._childTags)
                {
                    NbtTagSerializer.SerializeTag(elem.Value, bw);
                }

                NbtTagSerializer.SerializeTag(new NbtEnd(), bw);
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.Compound, new Serializer());
        }
    }
}
