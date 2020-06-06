using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Command
{
    public class CommandGraph
    {
        public List<CommandNode> Nodes { get; set; }

        public int RootNodeIndex { get; set; }
    }
}
