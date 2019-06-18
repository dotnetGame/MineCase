using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block
{
    public class BlockAir : Block
    {
        public BlockAir()
        {
            FullBlock = true;
            LightOpacity = 255;
            Translucent = true;
            LightValue = 0;
            UseNeighborBrightness = false;
            BlockHardness = 1.0f;
            BlockResistance = 0.0f;
            EnableStats = false;
            NeedsRandomTick = false;
            IsBlockContainer = false;

            BlockSoundType = null;

            BlockParticleGravity = 1.0f;

            BlockState = BlockStates.Air();

            UnlocalizedName = "air";
    }
    }
}
