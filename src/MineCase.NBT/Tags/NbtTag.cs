﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Nbt.Tags
{
    /// <summary>
    /// NBT Tag 的抽象基类
    /// </summary>
    public abstract class NbtTag
    {
        /// <summary>
        /// 该 Tag 从属于的 Tag
        /// </summary>
        public NbtTag Parent { get; internal set; }

        /// <summary>
        /// 该 Tag 的类型
        /// </summary>
        /// <remarks>不会在运行时改变，同一类 <see cref="NbtTag"/> 永远返回该类型关联的 <see cref="NbtTagType"/></remarks>
        public abstract NbtTagType TagType { get; }

        /// <summary>
        /// 指示该 Tag 是否具有值
        /// </summary>
        /// <remarks>该属性指示本 Tag 是否具有 Value 属性</remarks>
        public abstract bool HasValue { get; }

        /// <summary>
        /// 该 Tag 的名称
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                {
                    return;
                }

                Parent?.OnChildTagRenamed(this, value);
                _name = value;
            }
        }
        private string _name;

        /// <summary>
        /// 当子 Tag 被重命名之时通知该 Tag 从属于的 Tag 以完成相关的变动
        /// </summary>
        /// <param name="tag">将被重命名的 Tag</param>
        /// <param name="newName">将要作为该 Tag 的新名称的字符串</param>
        protected virtual void OnChildTagRenamed(NbtTag tag, string newName)
        {
            throw new NotSupportedException("这个类型的 Tag 由于不具有子 Tag 因此无法响应此通知");
        }

        /// <summary>
        /// 用于接受访问者的接口
        /// </summary>
        /// <param name="visitor">将用于访问的访问者</param>
        public virtual void Accept(INbtTagVisitor visitor)
        {
            visitor.VisitTag(this);
        }
    }
}
