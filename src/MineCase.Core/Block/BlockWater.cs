using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Block.State;
using MineCase.Item;

namespace MineCase.Block
{
    public class BlockWater : Block
    {
        public static IntegerProperty LevelProperty = new IntegerProperty("level", 16);

        public BlockWater()
        {
            FullBlock = true;
            LightOpacity = 1;
            Translucent = false;
            LightValue = 0;
            UseNeighborBrightness = false;
            BlockHardness = 1.0f;
            BlockResistance = 0.0f;
            EnableStats = false;
            NeedsRandomTick = true;
            IsBlockContainer = false;
            BlockSoundType = null;
            BlockParticleGravity = 1.0f;
            BaseBlockState = new BlockState { Id = (uint)BlockId.Water, MetaValue = 0 };
            Name = "water";
        }
    }
}
