using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Commands
{
    /// <summary>
    /// 命令参数接口
    /// </summary>
    public interface ICommandArgument
    {
        /// <summary>
        /// Gets 命令参数的原本内容
        /// </summary>
        string RawContent { get; }
    }

    /// <summary>
    /// 命令接口
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets 该命令的名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets 该命令的描述，可为 null
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets 要执行此命令需要的权限，可为 null
        /// </summary>
        Permission NeededPermission { get; }

        /// <summary>
        /// Gets 该命令的别名，可为 null
        /// </summary>
        IEnumerable<string> Aliases { get; }

        /// <summary>
        /// 执行该命令
        /// </summary>
        /// <param name="commandSender">发送命令者</param>
        /// <param name="args">命令的参数</param>
        /// <returns>执行是否成功，如果成功则返回 true</returns>
        /// <exception cref="CommandException">可能抛出派生自 <see cref="CommandException"/> 的异常</exception>
        Task<bool> Execute(ICommandSender commandSender, IReadOnlyList<ICommandArgument> args);
    }

    /// <inheritdoc />
    /// <summary>
    /// 命令执行过程中可能发生的异常的基类
    /// </summary>
    /// <remarks>派生自此类的异常在 <see cref="M:MineCase.Server.Game.Commands.CommandMap.Dispatch(MineCase.Server.Game.Commands.ICommandSender,System.String)" /> 中将会被吃掉，不会传播到外部</remarks>
    public class CommandException : Exception
    {
        public ICommand Command { get; }

        public CommandException(ICommand command = null, string content = null, Exception innerException = null)
            : base(content, innerException)
        {
            Command = command;
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// 表示命令的使用方式错误的异常
    /// </summary>
    public class CommandWrongUsageException : CommandException
    {
        public CommandWrongUsageException(ICommand command, string content = null, Exception innerException = null)
            : base(command, content, innerException)
        {
        }
    }

    /// <inheritdoc />
    /// <summary>
    /// 可发送命令者接口
    /// </summary>
    public interface ICommandSender : IPermissible
    {
        /// <summary>
        /// 向可发送命令者发送（一般为反馈）特定的信息
        /// </summary>
        /// <param name="msg">要发送的信息</param>
        Task SendMessage(string msg);
    }
}
