using System;
using System.Collections.Generic;

namespace MineCase.Server.Game
{
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
        bool Execute(ICommandSender commandSender, IEnumerable<string> args);
    }

    /// <summary>
    /// 可发送命令者接口
    /// </summary>
    public interface ICommandSender : IPermissible
    {
    }
}
