using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Command
{
    public enum CommandNodeType : byte
    {
        Root = 0,
        Literal = 1,
        Argument = 2
    }

    public class CommandNode
    {
        // Flags
        public byte Flags { get; set; }

        public CommandNodeType Type { get => (CommandNodeType)(Flags & 0x03); }

        public bool IsExecutable { get => (Flags & 0x04) == 1; }

        public bool HasRedirect { get => (Flags & 0x08) == 1; }

        public bool HasSuggestionsType { get => (Flags & 0x10) == 1; }

        public int ChildrenCount { get => Children.Count; }

        public List<int> Children { get; set; }

        public int RedirectNode { get; set; }

        // max 32767
        public string Name { get; set; }

        public string Parser { get; set; }

        public string SuggestionsType { get; set; }
    }
}
