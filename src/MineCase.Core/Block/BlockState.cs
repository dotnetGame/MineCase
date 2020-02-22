using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block
{
    public class BlockState
    {
        private readonly bool _solid;
        private readonly bool _opaque;
        private readonly Block _block;

        public BlockState(Block block)
        {
            _block = block;
        }

        public Block GetBlock()
        {
            return _block;
        }
    }
}
