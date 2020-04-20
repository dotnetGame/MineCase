using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Block.State;
using MineCase.Item;

namespace MineCase.Block
{
    public abstract class Block
    {
        public Material.Material Material { get; set; }

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

        public String Name { get; set; }

        public BlockState BaseBlockState { get; set; }

        public static Block FromBlockState(BlockState blockState)
        {
            if (!Blocks.IntToBlock.ContainsKey((int)blockState.Id))
                return Blocks.Air;
            else
                return Blocks.IntToBlock[(int)blockState.Id];
        }
    }
}
