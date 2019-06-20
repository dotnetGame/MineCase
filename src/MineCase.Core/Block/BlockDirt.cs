﻿using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Item;

namespace MineCase.Block
{
    public class BlockDirt : Block
    {
        public BlockDirt()
        {
            FullBlock = true;
            LightOpacity = 255;
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
            BlockState = BlockStates.Dirt();
            UnlocalizedName = "dirt";
        }

        public override ItemState BlockBrokenItem(ItemState hand, bool silktouch)
        {
            return new ItemState { Id = (uint)BlockId.Dirt, MetaValue = 0 };
        }
    }
}
