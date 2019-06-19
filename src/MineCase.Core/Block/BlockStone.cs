using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Item;

namespace MineCase.Block
{
    public class BlockStone : Block
    {
        public BlockStone()
        {
            FullBlock = true;
            LightOpacity = 0;
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
            BlockState = BlockStates.Stone();
            UnlocalizedName = "stone";
        }

        public override ItemState BlockBrokenItem(ItemState hand, bool silktouch)
        {
            if (silktouch)
                return new ItemState { Id = (uint)BlockId.Stone, MetaValue = 0 };
            else if (hand.IsPickaxe())
                return new ItemState { Id = (uint)BlockId.Cobblestone, MetaValue = 0 };
            else
                return new ItemState { Id = (uint)BlockId.Air, MetaValue = 0 };
        }
    }
}
