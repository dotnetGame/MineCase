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
            BlockState = BlockStates.OakSapling();
            UnlocalizedName = "sapling";
        }

        // FIXME the broken item is not correct
        public override ItemState BlockBrokenItem(ItemState hand, bool silktouch)
        {
            return new ItemState { Id = (uint)BlockId.OakSapling, MetaValue = BlockState.MetaValue };
        }
    }
}
