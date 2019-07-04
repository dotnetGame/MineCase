﻿using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Item;

namespace MineCase.Block
{
    public class BlockWoodPlanks : Block
    {
        public BlockWoodPlanks()
        {
            FullBlock = true;
            LightOpacity = 255;
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
            BlockState = BlockStates.WoodPlanks();
            UnlocalizedName = "planks";
        }

        public override ItemState BlockBrokenItem(ItemState hand, bool silktouch)
        {
            return new ItemState { Id = (uint)BlockId.WoodPlanks, MetaValue = BlockState.MetaValue };
        }
    }
}
