using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World
{
    public class Blocks
    {
        public Block GetBlockStone()
        {
            return new Block{ Id = (uint)BlockId.Stone };
        }
    }

}