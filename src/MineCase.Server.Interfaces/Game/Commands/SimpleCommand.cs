using System;
using System.Collections.Generic;

namespace MineCase.Server.Game.Commands
{
    /// <summary>
    /// 简单指令
    /// </summary>
    /// <remarks>用于无复杂的名称、描述、权限及别名机制的指令</remarks>
    public abstract class SimpleCommand : ICommand
    {
        public string Name { get; }

        public string Description { get; }

        public Permission NeededPermission { get; }

        private readonly HashSet<string> _aliases;

        public IEnumerable<string> Aliases => _aliases;

        protected SimpleCommand(string name, string description = null, Permission neededPermission = null, IEnumerable<string> aliases = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            NeededPermission = neededPermission;
            if (aliases != null)
            {
                _aliases = new HashSet<string>(aliases);
            }
        }

        public abstract bool Execute(ICommandSender commandSender, IList<ICommandArgument> args);
    }
}
