using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Commands
{
    /// <summary>
    /// 命令 Map
    /// </summary>
    public class CommandMap
    {
        private readonly Dictionary<string, ICommand> _commandMap = new Dictionary<string, ICommand>();

        /// <summary>
        /// 注册一个命令
        /// </summary>
        /// <param name="command">要注册的命令</param>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> 为 null</exception>
        /// <exception cref="ArgumentException">已有重名的 command 被注册</exception>
        public void RegisterCommand(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            Contract.EndContractBlock();

            _commandMap.Add(command.Name, command);
        }

        /// <summary>
        /// 分派命令
        /// </summary>
        /// <param name="sender">命令的发送者</param>
        /// <param name="commandContent">命令的内容</param>
        public Task<bool> Dispatch(ICommandSender sender, string commandContent)
        {
            var (commandName, args) = CommandParser.ParseCommand(commandContent);

            try
            {
                if (_commandMap.TryGetValue(commandName, out var command) &&
                    (command.NeededPermission == null || sender.HasPermission(command.NeededPermission).Result))
                {
                    return command.Execute(sender, args);
                }

                return Task.FromResult(false);
            }
            catch (CommandException e)
            {
                sender.SendMessage($"在执行命令 {commandName} 之时发生命令相关的异常 {e}");
                return Task.FromResult(false);
            }
        }
    }
}
