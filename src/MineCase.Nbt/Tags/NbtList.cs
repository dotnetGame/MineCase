using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using MineCase.Nbt.Serialization;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.List"/>
    public sealed class NbtList : NbtTag, IEnumerable<NbtTag>
    {
        public override NbtTagType TagType => NbtTagType.List;

        public override bool HasValue => false;

        public NbtTagType ElementType { get; private set; }

        private readonly List<NbtTag> _childTags;

        public int Count => _childTags.Count;

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtList"/> class.<para />
        /// 默认构造方法.
        /// </summary>
        /// <param name="elementType">指定该 <see cref="NbtList"/> 的元素类型.</param>
        /// <param name="name">该 Tag 的名称.</param>
        public NbtList(NbtTagType elementType)
        {
            ElementType = elementType;
            _childTags = new List<NbtTag>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtList"/> class.<para />
        /// 从 <paramref name="tags"/> 初始化子 Tag 的构造方法.
        /// </summary>
        /// <param name="tags">要用于提供子 Tag 的范围.</param>
        /// <param name="name">该 Tag 的名称.</param>
        /// <exception cref="ArgumentNullException"><paramref name="tags"/> 为 null.</exception>
        /// <exception cref="ArgumentException"><paramref name="tags"/> 中包含了不合法的 Tag.</exception>
        public NbtList(IEnumerable<NbtTag> tags)
        {
            if (tags == null)
            {
                throw new ArgumentNullException(nameof(tags));
            }

            // TODO: 此处隐藏了重复元素的问题，是否需要检查？
            var tmpTags = new List<NbtTag>(tags.Distinct());

            if (tmpTags.Count == 0)
            {
                throw new ArgumentException($"{nameof(tags)} 是空集合", nameof(tags));
            }

            ElementType = tmpTags[0]?.TagType ?? throw new ArgumentException($"{nameof(tags)} 中包含了 null", nameof(tags));

            if (tmpTags.FindIndex(tag => tag == null || tag.TagType != ElementType) != -1)
            {
                throw new ArgumentException($"{nameof(tags)} 中包含了 null 或者具有名称的 Tag 或者具有不同类型的 Tag", nameof(tags));
            }

            _childTags = tmpTags;
        }

        /// <see cref="Get(int)"/>
        public NbtTag this[int index] => Get(index);

        /// <summary>以指定的索引值获取 Tag.</summary>
        /// <param name="index">指定的索引值.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/>超出边界.</exception>
        public NbtTag Get(int index)
        {
            if (index < 0 || index >= _childTags.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            Contract.EndContractBlock();

            return _childTags[index];
        }

        /// <summary>以指定的索引值及期望的 Tag 类型获取 Tag.</summary>
        /// <typeparam name="T">期望获取的 Tag 类型.</typeparam>
        /// <param name="index">指定的索引值.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> 超出边界.</exception>
        /// <exception cref="InvalidCastException">未能将获得的 Tag 转换为 <typeparamref name="T"/>.</exception>
        public T Get<T>(int index)
            where T : NbtTag
        {
            return (T)Get(index);
        }

        /// <summary>判断指定的 Tag 是否为本 NbtList 的子 Tag.</summary>
        /// <param name="tag">指定的 Tag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> 为 null.</exception>
        public bool ContainsTag(NbtTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            Contract.EndContractBlock();

            return tag.TagType != ElementType && _childTags.Contains(tag);
        }

        /// <summary>在本 NbtList 的子 Tag 中寻找指定的 Tag.</summary>
        /// <param name="tag">指定的 Tag.</param>
        /// <returns>若找到，返回指定的 Tag 在本 NbtList 中的索引；若未找到，则返回 -1.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> 为 null.</exception>
        public int FindTag(NbtTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            Contract.EndContractBlock();

            return _childTags.FindIndex(curTag => curTag == tag);
        }

        /// <summary>添加子 Tag.</summary>
        /// <param name="tag">要添加的 Tag.</param>
        /// <exception cref="ArgumentException"><paramref name="tag"/> 不符合要求.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> 为 null.</exception>
        public void Add(NbtTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            Contract.EndContractBlock();

            if (tag.TagType != ElementType)
            {
                if (ElementType == NbtTagType.End && tag.TagType != NbtTagType.End)
                {
                    ElementType = tag.TagType;
                }
                else
                {
                    throw new ArgumentException(
                        $"{nameof(tag)} 具有不同的类型（需要 {ElementType}，但获得的是{tag.TagType}）或者具有类型 {NbtTagType.End}",
                        nameof(tag));
                }
            }

            // TODO: 这个检查是否必要？
            Contract.Assert(!_childTags.Contains(tag));

            // TODO: 是否需要检查 tag 是否已经关联到其他 tag ？
            _childTags.Add(tag);
            tag.Parent = this;
        }

        /// <summary>于指定的索引处添加子 Tag.</summary>
        /// <param name="index">指定的索引.</param>
        /// <param name="tag">要添加的 Tag.</param>
        /// <exception cref="ArgumentException"><paramref name="tag"/> 不符合要求.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> 不在合法范围内.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> 为 null.</exception>
        public void Add(int index, NbtTag tag)
        {
            if (index < 0 || index > _childTags.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            Contract.EndContractBlock();

            if (tag.TagType != ElementType)
            {
                if (ElementType == NbtTagType.End && tag.TagType != NbtTagType.End)
                {
                    ElementType = tag.TagType;
                }
                else
                {
                    throw new ArgumentException(
                        $"{nameof(tag)} 具有不同的类型（需要 {ElementType}，但获得的是{tag.TagType}）或者具有类型 {NbtTagType.End}",
                        nameof(tag));
                }
            }

            // TODO: 这个检查是否必要？
            Contract.Assert(!_childTags.Contains(tag));

            _childTags.Insert(index, tag);
            tag.Parent = this;
        }

        /// <summary>移除子 Tag.</summary>
        /// <param name="tag">要移除的 Tag.</param>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> 为 null.</exception>
        /// <exception cref="ArgumentException"><paramref name="tag"/> 不是该 NbtList 的子 Tag.</exception>
        public void Remove(NbtTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            if (tag.Parent != this)
            {
                throw new ArgumentException($"{nameof(tag)} 不是该 NbtList 的子 Tag");
            }

            Contract.EndContractBlock();

            // TODO: 是否需要对这个方法判断是否成功？
            _childTags.Remove(tag);
            tag.Parent = null;
        }

        /// <summary>于指定的索引处移除子 Tag.</summary>
        /// <param name="index">指定的索引.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> 不在合法范围内.</exception>
        public void Remove(int index)
        {
            if (index < 0 || index >= _childTags.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            Contract.EndContractBlock();

            var tag = _childTags[index];
            Contract.Assert(tag.Parent == this);
            _childTags.RemoveAt(index);
            tag.Parent = null;
        }

        public override void Accept(INbtTagVisitor visitor)
        {
            base.Accept(visitor);

            visitor.StartChild();

            foreach (var tag in _childTags)
            {
                tag.Accept(visitor);
            }

            visitor.EndChild();
        }

        public IEnumerator<NbtTag> GetEnumerator()
        {
            return _childTags.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Serializer : ITagSerializer
        {
            public NbtTag Deserialize(BinaryReader br)
            {
                var elementType = br.ReadTagType();
                var count = br.ReadInt32().ToggleEndian();

                if (count <= 0)
                {
                    return new NbtList(elementType);
                }

                var elements = new NbtTag[count];
                for (var i = 0; i < count; ++i)
                {
                    elements[i] = NbtTagSerializer.DeserializeTag(br, elementType);
                }

                return new NbtList(elements);
            }

            public void Serialize(NbtTag tag, BinaryWriter bw)
            {
                var nbtList = (NbtList)tag;

                bw.WriteTagValue(nbtList.ElementType);
                bw.Write(nbtList.Count.ToggleEndian());

                foreach (var elem in nbtList._childTags)
                {
                    NbtTagSerializer.SerializeTag(elem, bw, false);
                }
            }
        }

        internal static void RegisterSerializer()
        {
            NbtTagSerializer.RegisterTag(NbtTagType.List, new Serializer());
        }
    }
}
