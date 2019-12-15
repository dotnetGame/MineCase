using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Item;

namespace MineCase.Block
{
    public abstract class Block
    {
        /** Is it a full block */
        public bool FullBlock { get; set; }

        /** How much light is subtracted for going through this block */
        public int LightOpacity { get; set; }

        public bool Translucent { get; set; }

        /** Amount of light emitted */
        public int LightValue { get; set; }

        /** Flag if block should use the brightest neighbor light value as its own */
        public bool UseNeighborBrightness { get; set; }

        /** Indicates how many hits it takes to break a block. */
        public float BlockHardness { get; set; }

        /** Indicates how much this block can resist explosions */
        public float BlockResistance { get; set; }

        public bool EnableStats { get; set; }

        /**
         * Flags whether or not this block is of a type that needs random ticking. Ref-counted by ExtendedBlockStorage in
         * order to broadly cull a chunk from the random chunk update list for efficiency's sake.
         */
        public bool NeedsRandomTick { get; set; }

        /** true if the Block contains a Tile Entity */
        public bool IsBlockContainer { get; set; }

        /** Sound of stepping on the block */
        public SoundType BlockSoundType { get; set; }

        public float BlockParticleGravity { get; set; }

        public BlockState BlockState { get; set; }

        public String UnlocalizedName { get; set; }

        public abstract ItemState BlockBrokenItem(ItemState hand, bool silktouch);

        public static Block FromBlockState(BlockState blockState)
        {
            if (blockState == BlockStates.Air())
            {
                return new BlockAir();
            }
            else if (blockState.IsId(BlockId.Stone))
            {
                var stone = new BlockStone();
                stone.BlockState = blockState;
                return stone;
            }
            else if (blockState == BlockStates.GrassBlock())
            {
                return new BlockGrassBlock();
            }
            else if (blockState == BlockStates.Dirt())
            {
                return new BlockDirt();
            }
            else if (blockState == BlockStates.Cobblestone())
            {
                return new BlockCobblestone();
            }
            else if (blockState.IsId(BlockId.WoodPlanks))
            {
                var planks = new BlockWoodPlanks();
                planks.BlockState = blockState;
                return planks;
            }
            else if (blockState.IsId(BlockId.Sapling))
            {
                var planks = new BlockSapling();
                planks.BlockState = blockState;
                return planks;
            }
            else if (blockState == BlockStates.Bedrock())
            {
                return new BlockBedrock();
            }
            else if (blockState == BlockStates.Water())
            {
                return new BlockWater();
            }
            else
            {
                return new BlockAir();
            }
        }
    }
}
