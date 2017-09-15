using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Commands
{
    /// <inheritdoc />
    /// <summary>
    /// 波浪号记号参数
    /// </summary>
    /// <remarks>用于表示一个相对的值，具体含义由具体命令定义</remarks>
    public class TildeNotationArgument : UnresolvedArgument
    {
        internal const char PrefixToken = '~';

        /// <summary>
        /// Gets 波浪号记号参数表示的偏移量
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TildeNotationArgument"/> class.
        /// 构造并分析一个波浪号记号参数
        /// </summary>
        /// <param name="rawContent">作为波浪号记号的内容</param>
        public TildeNotationArgument(string rawContent)
            : base(rawContent)
        {
            if (rawContent.Length == 1)
            {
                Offset = 0;
                return;
            }

            var strToParse = rawContent.Substring(1);
            if (!int.TryParse(strToParse, out int offset))
            {
                throw new ArgumentException($"\"{strToParse}\" 不是合法的 offset 值", nameof(rawContent));
            }

            Offset = offset;
        }
    }
}
