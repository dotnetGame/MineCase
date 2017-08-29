using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MineCase.Server.Game.Commands
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class TargetSelectorAliasAsAttribute : Attribute
    {
        public char Alias { get; }

        public TargetSelectorAliasAsAttribute(char alias) => Alias = alias;
    }

    /// <summary>
    /// 目标选择器类型
    /// </summary>
    public enum TargetSelectorType
    {
        /// <summary>
        /// 选择最近的玩家为目标
        /// </summary>
        [TargetSelectorAliasAs('p')]
        NearestPlayer,

        /// <summary>
        /// 选择随机的玩家为目标
        /// </summary>
        [TargetSelectorAliasAs('r')]
        RandomPlayer,

        /// <summary>
        /// 选择所有玩家为目标
        /// </summary>
        [TargetSelectorAliasAs('a')]
        AllPlayers,

        /// <summary>
        /// 选择所有实体为目标
        /// </summary>
        [TargetSelectorAliasAs('e')]
        AllEntites,

        /// <summary>
        /// 选择命令的执行者为目标
        /// </summary>
        [TargetSelectorAliasAs('s')]
        Executor
    }

    /// <summary>
    /// 用于选择目标的 <see cref="ICommandArgument"/>
    /// </summary>
    public class TargetSelectorArgument : UnresolvedArgument, IEnumerable<KeyValuePair<string, string>>
    {
        private enum ParseStatus
        {
            Prefix,
            VariableTag,
            OptionalArgumentListStart,
            ArgumentElementName,
            ArgumentElementValue,
            ArgumentListEnd,

            Accepted,
            Rejected
        }

        internal const char PrefixToken = '@';
        private const char ArgumentListStartToken = '[';
        private const char ArgumentListEndToken = ']';
        private const char ArgumentAssignmentToken = '=';
        private const char ArgumentSeparatorToken = ',';

        private static readonly Dictionary<char, TargetSelectorType> TargetSelectorMap =
            typeof(TargetSelectorType).GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static)
                .ToDictionary(
                    v => v.GetCustomAttribute<TargetSelectorAliasAsAttribute>().Alias,
                    v => (TargetSelectorType)v.GetRawConstantValue());

        /// <summary>
        /// Gets 指示选择了哪一类型的目标
        /// </summary>
        public TargetSelectorType Type { get; }

        private readonly Dictionary<string, string> _arguments = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetSelectorArgument"/> class.<para />
        /// 构造并分析一个目标选择器
        /// </summary>
        /// <param name="rawContent">作为目标选择器的内容</param>
        /// <exception cref="ArgumentNullException"><paramref name="rawContent"/> 为 null</exception>
        /// <exception cref="ArgumentException"><paramref name="rawContent"/> 无法作为目标选择器解析</exception>
        public TargetSelectorArgument(string rawContent)
            : base(rawContent)
        {
            var status = ParseStatus.Prefix;
            string argName = null;
            var tmpString = new StringBuilder();

            foreach (var cur in rawContent)
            {
                switch (status)
                {
                    case ParseStatus.Prefix:
                        if (cur == PrefixToken)
                        {
                            status = ParseStatus.VariableTag;
                        }
                        else
                        {
                            goto case ParseStatus.Rejected;
                        }

                        break;
                    case ParseStatus.VariableTag:
                        if (!TargetSelectorMap.TryGetValue(cur, out var type))
                        {
                            goto case ParseStatus.Rejected;
                        }

                        Type = type;
                        status = ParseStatus.OptionalArgumentListStart;
                        break;
                    case ParseStatus.OptionalArgumentListStart:
                        if (cur != ArgumentListStartToken)
                        {
                            goto case ParseStatus.Rejected;
                        }

                        status = ParseStatus.ArgumentElementName;
                        break;
                    case ParseStatus.ArgumentElementName:
                        if (char.IsWhiteSpace(cur) && tmpString.Length == 0)
                        {
                            // 略过开头的空白字符，但是这不可能发生。。。
                            continue;
                        }

                        if (cur == ArgumentAssignmentToken)
                        {
                            argName = tmpString.ToString();
                            tmpString = new StringBuilder();
                            status = ParseStatus.ArgumentElementValue;
                            break;
                        }

                        tmpString.Append(cur);
                        break;
                    case ParseStatus.ArgumentElementValue:
                        if (cur == ArgumentSeparatorToken || cur == ArgumentListEndToken)
                        {
                            Contract.Assert(argName != null);
                            _arguments.Add(argName, tmpString.ToString());
                            tmpString = new StringBuilder();
                            if (cur == ArgumentSeparatorToken)
                            {
                                status = ParseStatus.ArgumentElementName;
                            }
                            else
                            {
                                goto case ParseStatus.ArgumentListEnd;
                            }

                            break;
                        }

                        tmpString.Append(cur);
                        break;
                    case ParseStatus.ArgumentListEnd:
                        status = ParseStatus.Accepted;
                        break;
                    case ParseStatus.Accepted:
                        // 尾部有多余的字符
                        status = ParseStatus.Rejected;
                        break;
                    case ParseStatus.Rejected:
                        throw new ArgumentException($"\"{rawContent}\" 不能被解析为合法的 TargetSelector", nameof(rawContent));
                    default:
                        // 任何情况下都不应当发生
                        throw new ArgumentOutOfRangeException(nameof(status));
                }
            }

            if (status != ParseStatus.Accepted && status != ParseStatus.OptionalArgumentListStart)
            {
                throw new ArgumentException($"在解析 \"{rawContent}\" 的过程中，解析被过早地中止");
            }
        }

        /// <see cref="GetArgumentValue(string)"/>
        public string this[string name] => GetArgumentValue(name);

        /// <summary>
        /// 获得具有指定名称的参数值
        /// </summary>
        /// <param name="name">要查找的名称</param>
        /// <exception cref="KeyNotFoundException">无法找到具有指定名称的参数</exception>
        public string GetArgumentValue(string name) => _arguments[name];

        /// <summary>
        /// 判断是否存在具有指定名称的参数
        /// </summary>
        /// <param name="name">要判断的名称</param>
        public bool ContainsArgument(string name) => _arguments.ContainsKey(name);

        /// <summary>
        /// Gets 具有的参数数量
        /// </summary>
        public int ArgumentCount => _arguments.Count;

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => _arguments.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
