using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineCase.Block;
using MineCase.World;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Plants
{
    [StatelessWorker]
    public class SavannaTreeGeneratorGrain : AbstractTreeGeneratorGrain, ISavannaTreeGenerator
    {
        public SavannaTreeGeneratorGrain(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<JungleGeneratorGrain>();
        }

        public async override Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            _wood = BlockStates.AcaciaLog();
            _leaves = BlockStates.AcaciaLeaves(AcaciaLeavesDistanceType.Distance1, AcaciaLeavesPersistentType.False);
        }

        public async override Task GenerateSingle(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            int seed = await world.GetSeed();
            int treeSeed = pos.X ^ pos.Z ^ seed;
            Random rand = new Random(treeSeed);
            await GenerateImpl(world, chunkWorldPos, pos, rand);
        }

        private async Task<bool> GenerateImpl(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos, Random rand)
        {
            int height = rand.Next(3) + rand.Next(3) + 5;
            bool flag = true;

            if (pos.Y >= 1 && pos.Y + height + 1 <= 256)
            {
                // check if enough space
                for (int y = pos.Y; y <= pos.Y + 1 + height; ++y)
                {
                    int xzRange = 1;

                    if (y == pos.Y)
                    {
                        xzRange = 0;
                    }

                    if (y >= pos.Y + 1 + height - 2)
                    {
                        xzRange = 2;
                    }

                    for (int xOffset = pos.X - xzRange; xOffset <= pos.X + xzRange && flag; ++xOffset)
                    {
                        for (int zOffset = pos.Z - xzRange; zOffset <= pos.Z + xzRange && flag; ++zOffset)
                        {
                            if (y >= 0 && y < 256)
                            {
                                var blockState = await world.GetBlockStateUnsafe(this.GrainFactory, new BlockWorldPos(xOffset, y, zOffset));
                                if (!IsReplaceable(blockState))
                                {
                                    flag = false;
                                }
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                    }
                }

                // plant tree
                if (!flag)
                {
                    return false;
                }
                else
                {
                    BlockWorldPos down = pos.Down();
                    BlockState state = await world.GetBlockStateUnsafe(this.GrainFactory, down);
                    bool isSoil = IsSoil(state);

                    if (isSoil && pos.Y < ChunkConstants.ChunkHeight - height - 1)
                    {
                        // state.getBlock().onPlantGrow(state, world, down, pos);
                        Facing enumfacing = Facing.RadomFacing(rand, Plane.XZ);
                        int bendingPos = height - rand.Next(4) - 1;
                        int l2 = 3 - rand.Next(3);
                        int woodX = pos.X;
                        int woodZ = pos.Z;
                        int branchHeight = 0;

                        for (int yOffset = 0; yOffset < height; ++yOffset)
                        {
                            int curHeight = pos.Y + yOffset;

                            if (yOffset >= bendingPos && l2 > 0)
                            {
                                woodX += enumfacing.ToBlockVector().X;
                                woodZ += enumfacing.ToBlockVector().Z;
                                --l2;
                            }

                            BlockWorldPos blockpos = new BlockWorldPos(woodX, curHeight, woodZ);
                            state = await world.GetBlockStateUnsafe(this.GrainFactory, blockpos);

                            if (state.IsAir() || state.IsLeaves())
                            {
                                await world.SetBlockStateUnsafe(this.GrainFactory, blockpos, _wood);
                                branchHeight = curHeight;
                            }
                        }

                        BlockWorldPos blockpos2 = new BlockWorldPos(woodX, branchHeight, woodZ);

                        for (int xOffset = -3; xOffset <= 3; ++xOffset)
                        {
                            for (int zOffset = -3; zOffset <= 3; ++zOffset)
                            {
                                if (Math.Abs(xOffset) != 3 || Math.Abs(zOffset) != 3)
                                {
                                    await world.SetBlockStateUnsafe(this.GrainFactory, BlockWorldPos.Add(blockpos2, xOffset, 0, zOffset), _leaves);
                                }
                            }
                        }

                        blockpos2 = blockpos2.Up();

                        for (int xOffset = -1; xOffset <= 1; ++xOffset)
                        {
                            for (int zOffset = -1; zOffset <= 1; ++zOffset)
                            {
                                await world.SetBlockStateUnsafe(this.GrainFactory, BlockWorldPos.Add(blockpos2, xOffset, 0, zOffset), _leaves);
                            }
                        }

                        await world.SetBlockStateUnsafe(this.GrainFactory, blockpos2.East(2), _leaves);
                        await world.SetBlockStateUnsafe(this.GrainFactory, blockpos2.West(2), _leaves);
                        await world.SetBlockStateUnsafe(this.GrainFactory, blockpos2.South(2), _leaves);
                        await world.SetBlockStateUnsafe(this.GrainFactory, blockpos2.North(2), _leaves);
                        woodX = pos.X;
                        woodZ = pos.Z;
                        Facing enumfacing1 = Facing.RadomFacing(rand, Plane.XZ);

                        if (enumfacing1 != enumfacing)
                        {
                            int l3 = bendingPos - rand.Next(2) - 1;
                            int k4 = 1 + rand.Next(3);
                            branchHeight = 0;

                            for (int l4 = l3; l4 < height && k4 > 0; --k4)
                            {
                                if (l4 >= 1)
                                {
                                    int j2 = pos.Y + l4;
                                    woodX += enumfacing1.ToBlockVector().X;
                                    woodZ += enumfacing1.ToBlockVector().Z;
                                    BlockWorldPos blockpos1 = new BlockWorldPos(woodX, j2, woodZ);
                                    state = await world.GetBlockStateUnsafe(this.GrainFactory, blockpos1);

                                    if (state.IsAir() || state.IsLeaves())
                                    {
                                        await world.SetBlockStateUnsafe(this.GrainFactory, blockpos1, _wood);
                                        branchHeight = j2;
                                    }
                                }

                                ++l4;
                            }

                            if (branchHeight > 0)
                            {
                                BlockWorldPos blockpos3 = new BlockWorldPos(woodX, branchHeight, woodZ);

                                for (int xOffset = -2; xOffset <= 2; ++xOffset)
                                {
                                    for (int zOffset = -2; zOffset <= 2; ++zOffset)
                                    {
                                        if (Math.Abs(xOffset) != 2 || Math.Abs(zOffset) != 2)
                                        {
                                            await world.SetBlockStateUnsafe(this.GrainFactory, BlockWorldPos.Add(blockpos3, xOffset, 0, zOffset), _leaves);
                                        }
                                    }
                                }

                                blockpos3 = blockpos3.Up();

                                for (int xOffset = -1; xOffset <= 1; ++xOffset)
                                {
                                    for (int zOffset = -1; zOffset <= 1; ++zOffset)
                                    {
                                        await world.SetBlockStateUnsafe(this.GrainFactory, BlockWorldPos.Add(blockpos3, xOffset, 0, zOffset), _leaves);
                                    }
                                }
                            }
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}
