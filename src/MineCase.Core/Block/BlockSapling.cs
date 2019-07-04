using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Item;

namespace MineCase.Block
{
    public class BlockSapling : Block
    {
        public BlockSapling()
        {
            FullBlock = true;
            LightOpacity = 3;
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
            BlockState = BlockStates.Sapling();
            UnlocalizedName = "sapling";
        }

        public override ItemState BlockBrokenItem(ItemState hand, bool silktouch)
        {
            if (silktouch)
                return new ItemState { Id = (uint)BlockId.WoodPlanks, MetaValue = BlockState.MetaValue };
            else
                return new ItemState { Id = (uint)BlockId.Air, MetaValue = 0 };
        }
    }
}
