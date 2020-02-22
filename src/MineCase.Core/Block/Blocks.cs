using MineCase.Block.Material;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block
{
    class Blocks
    {
        public static readonly Block Air = new AirBlock(
            new BlockProperties { Material = Materials.Air, MapColor = MaterialColors.Air, BlocksMovement = false, }
        );
        public static readonly Block VoidAir = new AirBlock(
            new BlockProperties { Material = Materials.Air, MapColor = MaterialColors.Air, BlocksMovement = false, }
        );

    }
}
