using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block
{
    public class Block
    {
        /** Is it a full block */
        protected bool FullBlock { get; set; }

        /** How much light is subtracted for going through this block */
        protected int LightOpacity { get; set; }

        protected bool Translucent { get; set; }

        /** Amount of light emitted */
        protected int LightValue { get; set; }

        /** Flag if block should use the brightest neighbor light value as its own */
        protected bool UseNeighborBrightness { get; set; }

        /** Indicates how many hits it takes to break a block. */
        protected float BlockHardness { get; set; }

        /** Indicates how much this block can resist explosions */
        protected float BlockResistance { get; set; }

        protected bool EnableStats { get; set; }

        /**
         * Flags whether or not this block is of a type that needs random ticking. Ref-counted by ExtendedBlockStorage in
         * order to broadly cull a chunk from the random chunk update list for efficiency's sake.
         */
        protected bool NeedsRandomTick { get; set; }

        /** true if the Block contains a Tile Entity */
        protected bool IsBlockContainer { get; set; }
        /** Sound of stepping on the block */
        protected SoundType BlockSoundType { get; set; }

        public float BlockParticleGravity { get; set; }

        protected BlockState BlockState { get; set; }

        private String UnlocalizedName { get; set; }
    }
}
