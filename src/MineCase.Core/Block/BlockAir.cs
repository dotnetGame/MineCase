using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Block.State;
using MineCase.Item;

namespace MineCase.Block
{
    public class BlockAir : Block
    {
        public BlockAir()
        {
            Material = MineCase.Block.Material.Materials.Air;
            FullBlock = true;
            LightOpacity = 0;
            Translucent = false;
            LightValue = 0;
            UseNeighborBrightness = false;
            BlockHardness = 1.0f;
            BlockResistance = 0.0f;
            EnableStats = false;
            NeedsRandomTick = false;
            IsBlockContainer = false;
            BlockSoundType = null;
            BlockParticleGravity = 1.0f;
            Name = "air";
            BaseBlockState = new BlockState { Id = (uint)BlockId.Air, MetaValue = 0 };
        }
    }
}
