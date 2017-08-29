using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace MineCase.Server.Game.Commands
{
    /// <summary>
    /// 未解析参数
    /// </summary>
    /// <remarks>用于参数无法解析或当前不需要已解析的形式的情形</remarks>
    public class UnresolvedArgument : ICommandArgument
    {
        public string RawContent { get; }

        public UnresolvedArgument(string rawContent)
        {
            if (rawContent == null)
            {
                throw new ArgumentNullException(nameof(rawContent));
            }

            if (rawContent.Length <= 0)
            {
                throw new ArgumentException($"{nameof(rawContent)} 不得为空", nameof(rawContent));
            }

            RawContent = rawContent;
        }
    }

    /// <summary>
    /// 命令分析器
    /// </summary>
    public static class CommandParser
    {
        /// <summary>
        /// 分析命令
        /// </summary>
        /// <param name="input">输入，即作为命令被分析的文本，应当不为 null、经过 <see cref="string.Trim()"/> 处理且以 '/' 开头</param>
        /// <returns>命令名及命令的参数</returns>
        /// <exception cref="ArgumentException"><paramref name="input"/> 不合法</exception>
        public static (string, IList<ICommandArgument>) ParseCommand(string input)
        {
            if (input == null || input.Length < 2 || input[0] != '/')
            {
                throw new ArgumentException("输入不合法", nameof(input));
            }

            var splitResult = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (splitResult.Length == 0)
            {
                throw new ArgumentException($"输入 ({input}) 不合法", nameof(input));
            }

            return (splitResult[0].Substring(1), ParseCommandArgument(splitResult.Skip(1)));
        }

        // 参数必须保持原来的顺序，因此返回值使用 IList 而不是 IEnumerable
        private static IList<ICommandArgument> ParseCommandArgument(IEnumerable<string> input)
        {
            var result = new List<ICommandArgument>();

            foreach (var arg in input)
            {
                Contract.Assert(!string.IsNullOrWhiteSpace(arg));

                // TODO: 使用更加具有可扩展性的方法
                switch (arg[0])
                {
                    case TargetSelectorArgument.PrefixToken:
                        result.Add(new TargetSelectorArgument(arg));
                        break;
                    case DataTagArgument.PrefixToken:
                        result.Add(new DataTagArgument(arg));
                        break;
                    case TildeNotationArgument.PrefixToken:
                        result.Add(new TildeNotationArgument(arg));
                        break;
                    default:
                        result.Add(new UnresolvedArgument(arg));
                        break;
                }
            }

            return result;
        }
    }
}
