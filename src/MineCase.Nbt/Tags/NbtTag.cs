using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Nbt.Tags
{
    /// <summary>
    /// NBT Tag 的抽象基类.
    /// </summary>
    public abstract class NbtTag
    {
        /// <summary>
        /// Gets 该 Tag 从属于的 Tag.
        /// </summary>
        public NbtTag Parent { get; internal set; }

        /// <summary>
        /// Gets 该 Tag 的类型.
        /// </summary>
        /// <remarks>不会在运行时改变，同一类 <see cref="NbtTag"/> 永远返回该类型关联的 <see cref="NbtTagType"/>.</remarks>
        public abstract NbtTagType TagType { get; }

        /// <summary>
        /// Gets a value indicating whether. <para />
        /// 指示该 Tag 是否具有值.
        /// </summary>
        /// <remarks>该属性指示本 Tag 是否具有 Value 属性.</remarks>
        public abstract bool HasValue { get; }

        /// <summary>
        /// Get name from parent if parent is a compound.
        /// </summary>
        public string Name
        {
            get
            {
                if (Parent is NbtCompound)
                    return ((NbtCompound)Parent).Name;
                else
                    return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NbtTag"/> class.<para />
        /// 默认构造方法.
        /// </summary>
        /// <param name="name">该 Tag 的名称.</param>
        /// <param name="parent">该 Tag 所从属于的 Tag.</param>
        /// <remarks>一般不需要手动指定 <paramref name="parent"/>，因为将 Tag 加入其它 Tag 时会自动设置，更加安全.</remarks>
        protected NbtTag(NbtTag parent = null)
        {
            Parent = parent;
        }

        /// <summary>
        /// 当子 Tag 被重命名之时通知该 Tag 从属于的 Tag 以完成相关的变动.
        /// </summary>
        /// <param name="tag">将被重命名的 Tag.</param>
        /// <param name="newName">将要作为该 Tag 的新名称的字符串.</param>
        protected virtual void OnChildTagRenamed(NbtTag tag, string newName)
        {
            throw new NotSupportedException("这个类型的 Tag 由于不具有子 Tag 因此无法响应此通知");
        }

        /// <summary>
        /// 用于接受访问者的接口.
        /// </summary>
        /// <param name="visitor">将用于访问的访问者.</param>
        public virtual void Accept(INbtTagVisitor visitor)
        {
            visitor.VisitTag(this);
        }
    }
}
