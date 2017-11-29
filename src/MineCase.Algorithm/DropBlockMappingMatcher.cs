using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Algorithm
{
    public class DropBlockMappingMatcher
    {
        private Dictionary<BlockState, DropBlockEntry> _mapping;

        public DropBlockMappingMatcher(Dictionary<BlockState, DropBlockEntry> mapping)
        {
            _mapping = mapping;
        }

        public uint DropBlock(uint item, BlockState block)
        {
            if (_mapping.ContainsKey(block))
            {
                DropBlockEntry entry = _mapping[block];

                // TODO drop test
                return block.Id;
            }
            else
            {
                return uint.MaxValue;
            }
        }
    }
}
