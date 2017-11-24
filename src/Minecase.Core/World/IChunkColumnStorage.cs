using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.World
{
    public interface IChunkColumnStorage
    {
        BlockState this[int x, int y, int z] { get; set; }
    }
}
