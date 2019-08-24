using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Nbt
{
    /// <summary>
    /// 用于访问 Tag 的访问者.
    /// </summary>
    public interface INbtTagVisitor
    {
        /// <summary>
        /// 指示已开始对上次访问的 Tag 的子 Tag 的访问.
        /// </summary>
        void StartChild();

        /// <summary>
        /// 指示已结束对上次 StartChild 所指示的 Tag 的子 Tag 访问.
        /// </summary>
        void EndChild();

        /// <summary>
        /// 访问 Tag.
        /// </summary>
        /// <param name="tag">本次访问的 Tag.</param>
        void VisitTag(Tags.NbtTag tag);
    }
}
