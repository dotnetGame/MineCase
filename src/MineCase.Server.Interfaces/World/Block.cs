using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World
{
    public enum BlockId : uint
    {
        Air = 0,
        Stone = 1,
        Grass = 2,
        Dirt = 3,
        Cobblestone = 4,
        Water = 9
    }

    public struct BlockState
    {
        public uint Id { get; set; }

        public uint MetaValue { get; set; }
    }
}
