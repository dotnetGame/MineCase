using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Commands
{
    public class CommandMap
    {
        private readonly Dictionary<string, ICommand> _commandMap = new Dictionary<string, ICommand>();

        public void RegisterCommand(ICommand command)
        {
            _commandMap.Add(command.Name, command);
        }

        public bool Dispatch(ICommandSender sender, string commandContent)
        {
            var (commandName, args) = CommandParser.ParseCommand(commandContent);

            try
            {
                return _commandMap.TryGetValue(commandName, out var command) &&
                       (command.NeededPermission == null || sender.HasPermission(command.NeededPermission).Result) &&
                       command.Execute(sender, args);
            }
            catch (CommandException e)
            {
                sender.SendMessage($"在执行指令 {commandName} 之时发生指令相关的异常 {e}");
                return false;
            }
        }
    }
}
