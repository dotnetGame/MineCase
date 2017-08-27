using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game
{
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

        public abstract bool Execute(ICommandSender commandSender, IEnumerable<string> args);
    }
}
