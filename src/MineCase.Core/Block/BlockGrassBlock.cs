using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Item;

namespace MineCase.Block
{
    public class BlockGrassBlock : Block
    {
        public BlockGrassBlock()
        {
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
            BlockState = BlockStates.GrassBlock();
            UnlocalizedName = "grass";
        }

        public override ItemState BlockBrokenItem(ItemState hand, bool silktouch)
        {
            if (silktouch)
                return new ItemState { Id = (uint)BlockId.GrassBlock, MetaValue = 0 };
            else
                return new ItemState { Id = (uint)BlockId.Dirt, MetaValue = 0 };
        }
    }
}
