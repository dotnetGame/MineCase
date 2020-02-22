using MineCase.Block.Material;
using MineCase.Util.Collections;
using MineCase.Util.Palette;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block
{
    public class Blocks
    {
        public static readonly Block Air = new AirBlock(
            new BlockProperties { Material = Materials.Air, MapColor = MaterialColors.Air, BlocksMovement = false, }
        );
        public static readonly Block VoidAir = new AirBlock(
            new BlockProperties { Material = Materials.Air, MapColor = MaterialColors.Air, BlocksMovement = false, }
        );


        public static Dictionary<BlockState, int> BlockStateToId { get; } = new Dictionary<BlockState, int>()
        {
            { Air.GetDefaultState(), 0 },
            { VoidAir.GetDefaultState(), 1 },
        };

        public static Dictionary<int, BlockState> IdToBlockState { get; } = new Dictionary<int, BlockState>()
        {
            { 0, Air.GetDefaultState() },
            { 1, VoidAir.GetDefaultState() },
        };

        public static BiDictionary<int, BlockState> BlockStateRegistry { get; } = new BiDictionary<int, BlockState>(IdToBlockState, BlockStateToId);

        public static IPalette<BlockState> GlobalPalette { get; } = new PaletteIdentity<BlockState>(BlockStateRegistry, Blocks.Air.GetDefaultState()); 
    }
}
