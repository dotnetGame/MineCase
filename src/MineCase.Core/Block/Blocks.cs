using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Util.Collections;

namespace MineCase.Block
{
    public class Blocks
    {
        public static ObjectIntDictionary<Block> BlocksToInt = new ObjectIntDictionary<Block>();
        public static Dictionary<int, Block> IntToBlock = new Dictionary<int, Block>();

        public static Block Air = Register("air", new BlockAir());
        public static Block Stone = Register("stone", new BlockStone());

        protected static Block Register(string name, Block block)
        {
            IntToBlock.Add(IntToBlock.Count, block);
            return block;
        }
    }
}
