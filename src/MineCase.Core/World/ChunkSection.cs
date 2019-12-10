using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Block;

namespace MineCase.Core.World
{
    public class ChunkSection
    {
        public static ChunkSection EmptySection = null;
        private BlockState[] _data;


        public bool IsEmpty()
        {
            return _data.Length == 0;
        }
    }
}
