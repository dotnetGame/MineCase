using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineCase.Block;
using MineCase.World;

namespace MineCase.Server.World.Decoration.Plants
{
    public abstract class HugeTreeGeneratorGrain : AbstractTreeGeneratorGrain, IHugeTreeGenerator
    {
        protected int _baseHeight;
        protected int _extraRandomHeight;
        protected BlockState _wood;
        protected BlockState _leaves;

        public HugeTreeGeneratorGrain(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HugeTreeGeneratorGrain>();
        }

        public async override Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            _baseHeight = _generatorSettings.TreeHeight;
            _extraRandomHeight = _generatorSettings.ExtraHeight;
        }

        protected int GetHeight(Random rand)
        {
            int height = rand.Next(3) + _baseHeight;
            if (_extraRandomHeight > 1)
            {
                height += rand.Next(_extraRandomHeight);
            }

            return height;
        }

        protected async Task<bool> IsAirLeaves(IWorld world, BlockWorldPos pos)
        {
            var block = await world.GetBlockStateUnsafe(this.GrainFactory, pos);
            return block.IsAir() || block.IsLeaves();
        }

        /**
         * returns whether or not a tree can grow at a specific position.
         * If it can, it generates surrounding dirt underneath.
         */
        protected async Task<bool> EnsureGrowable(IWorld world, Random rand, BlockWorldPos treePos, int height)
        {
            return await IsSpaceAt(world, treePos, height) && await CanSustainHugeTree(world, treePos);
        }

        /**
         * returns whether or not there is space for a tree to grow at a certain position
         */
        private async Task<bool> IsSpaceAt(IWorld world, BlockWorldPos treePos, int height)
        {
            bool flag = true;

            // in the world height limit
            if (treePos.Y >= 1 && treePos.Y + height + 1 <= 256)
            {
                // Traverse Y axis
                for (int yOffset = 0; yOffset <= 1 + height; ++yOffset)
                {
                    int xzRange = 2;

                    if (yOffset == 0)
                    {
                        xzRange = 1;
                    }
                    else if (yOffset >= 1 + height - 2)
                    {
                        xzRange = 2;
                    }

                    for (int xOffset = -xzRange; xOffset <= xzRange && flag; ++xOffset)
                    {
                        for (int zOffset = -xzRange; zOffset <= xzRange && flag; ++zOffset)
                        {
                            if (treePos.Y + yOffset < 0 ||
                                treePos.Y + yOffset >= 256)
                            {
                                var blockState = await world.GetBlockStateUnsafe(this.GrainFactory, BlockWorldPos.Add(treePos, xOffset, yOffset, zOffset));
                                if (IsReplaceable(blockState))
                                    flag = false;
                            }
                        }
                    }
                }

                return flag;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> CanSustainHugeTree(IWorld world, BlockWorldPos pos)
        {
            BlockWorldPos blockpos = pos.Down();
            BlockState state = await world.GetBlockStateUnsafe(this.GrainFactory, blockpos);
            bool isSoil = IsSoil(state);

            if (isSoil && pos.Y >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * grow leaves in a circle with the outsides being within the circle
         */
        protected async Task GrowLeavesLayerStrict(IWorld world, BlockWorldPos layerCenter, int width)
        {
            int i = width * width;

            for (int j = -width; j <= width + 1; ++j)
            {
                for (int k = -width; k <= width + 1; ++k)
                {
                    int l = j - 1;
                    int i1 = k - 1;

                    if (j * j + k * k <= i || l * l + i1 * i1 <= i || j * j + i1 * i1 <= i || l * l + k * k <= i)
                    {
                        BlockWorldPos blockpos = BlockWorldPos.Add(layerCenter, j, 0, k);
                        BlockState state = await world.GetBlockStateUnsafe(this.GrainFactory, blockpos);

                        if (state.IsAir() || state.IsLeaves())
                        {
                            await world.SetBlockStateUnsafe(this.GrainFactory, blockpos, _leaves);
                        }
                    }
                }
            }
        }

        /**
         * grow leaves in a circle
         */
        protected async Task GrowLeavesLayer(IWorld world, BlockWorldPos layerCenter, int width)
        {
            int i = width * width;

            for (int j = -width; j <= width; ++j)
            {
                for (int k = -width; k <= width; ++k)
                {
                    if (j * j + k * k <= i)
                    {
                        BlockWorldPos blockpos = BlockWorldPos.Add(layerCenter, j, 0, k);
                        BlockState state = await world.GetBlockStateUnsafe(this.GrainFactory, blockpos);

                        if (state.IsAir() || state.IsLeaves())
                        {
                            await world.SetBlockStateUnsafe(this.GrainFactory, blockpos, _leaves);
                        }
                    }
                }
            }
        }
    }
}
