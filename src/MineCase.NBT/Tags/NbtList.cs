using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MineCase.Nbt.Tags
{
    /// <see cref="NbtTagType.List"/>
    public sealed class NbtList : NbtTag, IEnumerable<NbtTag>
    {
        public override NbtTagType TagType => NbtTagType.List;
        public override bool HasValue => false;

        private readonly List<NbtTag> _childTags = new List<NbtTag>();

        public int Count => _childTags.Count;

        /// <see cref="Get(int)"/>
        public NbtTag this[int index] => Get(index);

        /// <summary>以指定的索引值获取 Tag</summary>
        /// <param name="index">指定的索引值</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/>超出边界</exception>
        public NbtTag Get(int index)
        {
            if (index < 0 || index >= _childTags.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return _childTags[index];
        }

        /// <summary>以指定的索引值及期望的 Tag 类型获取 Tag</summary>
        /// <typeparam name="T">期望获取的 Tag 类型</typeparam>
        /// <param name="index">指定的索引值</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> 超出边界</exception>
        /// <exception cref="InvalidCastException">未能将获得的 Tag 转换为 <typeparamref name="T"/></exception>
        public T Get<T>(int index) where T : NbtTag
        {
            return (T) Get(index);
        }

        /// <summary>判断指定的 Tag 是否为本 NbtList 的子 Tag</summary>
        /// <param name="tag">指定的 Tag</param>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> 为 null</exception>
        public bool ContainsTag(NbtTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            return _childTags.Contains(tag);
        }

        /// <summary>在本 NbtList 的子 Tag 中寻找指定的 Tag</summary>
        /// <param name="tag">指定的 Tag</param>
        /// <returns>若找到，返回指定的 Tag 在本 NbtList 中的索引；若未找到，则返回 -1</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> 为 null</exception>
        public int FindTag(NbtTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            return _childTags.FindIndex(curTag => curTag == tag);
        }

        /// <summary>添加子 Tag</summary>
        /// <param name="tag">要添加的 Tag</param>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> 为 null</exception>
        public void Add(NbtTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }
            
            // TODO: 这个检查是否必要？
            Debug.Assert(!_childTags.Contains(tag));

            // TODO: 是否需要检查 tag 是否已经关联到其他 tag ？

            _childTags.Add(tag);
            tag.Parent = this;
        }

        /// <summary>于指定的索引处添加子 Tag</summary>
        /// <param name="index">指定的索引</param>
        /// <param name="tag">要添加的 Tag</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> 不在合法范围内</exception>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> 为 null</exception>
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

            // TODO: 这个检查是否必要？
            Debug.Assert(!_childTags.Contains(tag));

            _childTags.Insert(index, tag);
            tag.Parent = this;
        }

        /// <summary>移除子 Tag</summary>
        /// <param name="tag">要移除的 Tag</param>
        /// <exception cref="ArgumentNullException"><paramref name="tag"/> 为 null</exception>
        /// <exception cref="ArgumentException"><paramref name="tag"/> 不是该 NbtList 的子 Tag</exception>
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

            // TODO: 是否需要对这个方法判断是否成功？
            _childTags.Remove(tag);
            tag.Parent = null;
        }

        /// <summary>于指定的索引处移除子 Tag</summary>
        /// <param name="index">指定的索引</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> 不在合法范围内</exception>
        public void Remove(int index)
        {
            if (index < 0 || index >= _childTags.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var tag = _childTags[index];
            Debug.Assert(tag.Parent == this);
            _childTags.RemoveAt(index);
            tag.Parent = null;
        }

        public IEnumerator<NbtTag> GetEnumerator()
        {
            return _childTags.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
