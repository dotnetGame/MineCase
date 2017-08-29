using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Nbt.Tags;

namespace MineCase.Server.Game.Commands
{
    /// <summary>
    /// 数据标签参数
    /// </summary>
    /// <remarks>表示一个 <see cref="NbtTag"/></remarks>
    public class DataTagArgument : UnresolvedArgument
    {
        internal const char PrefixToken = '{';

        public NbtCompound Tag { get; }

        public DataTagArgument(string rawContent)
            : base(rawContent)
        {
            throw new NotImplementedException();
        }
    }
}
