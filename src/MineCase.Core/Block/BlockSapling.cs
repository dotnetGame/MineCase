using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Block.State;
using MineCase.Item;

namespace MineCase.Block
{
    public class BlockSapling : Block
    {
        public static IntegerProperty StageProperty = new IntegerProperty("stage", 2);

        public BlockSapling(string name = "sapling")
        {
            FullBlock = false;
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
            Name = name;
        }
    }
}
