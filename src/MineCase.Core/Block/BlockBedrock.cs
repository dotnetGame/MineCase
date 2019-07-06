using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Item;

namespace MineCase.Block
{
    public class BlockBedrock : Block
    {
        public BlockBedrock()
        {
            FullBlock = true;
            LightOpacity = 255;
            Translucent = false;
            LightValue = 0;
            UseNeighborBrightness = false;
            BlockHardness = 60000.0f;
            BlockResistance = 60000.0f;
            EnableStats = false;
            NeedsRandomTick = true;
            IsBlockContainer = false;
            BlockSoundType = null;
            BlockParticleGravity = 1.0f;
            BlockState = BlockStates.Bedrock();
            UnlocalizedName = "bedrock";
        }

        public override ItemState BlockBrokenItem(ItemState hand, bool silktouch)
        {
            return new ItemState { Id = (uint)BlockId.Air, MetaValue = 0 };
        }
    }
}
