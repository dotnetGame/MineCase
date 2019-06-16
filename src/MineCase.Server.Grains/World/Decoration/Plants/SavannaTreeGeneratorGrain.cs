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
            _wood = BlockStates.Wood2(Wood2Type.Acacia);
            _leaves = BlockStates.Leaves2(Leave2Type.Acacia);
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
                        int k2 = height - rand.Next(4) - 1;
                        int l2 = 3 - rand.Next(3);
                        int i3 = pos.X;
                        int j1 = pos.Z;
                        int k1 = 0;

                        for (int l1 = 0; l1 < height; ++l1)
                        {
                            int i2 = pos.Y + l1;

                            if (l1 >= k2 && l2 > 0)
                            {
                                i3 += enumfacing.ToBlockVector().X;
                                j1 += enumfacing.ToBlockVector().Z;
                                --l2;
                            }

                            BlockWorldPos blockpos = new BlockWorldPos(i3, i2, j1);
                            state = await world.GetBlockStateUnsafe(this.GrainFactory, blockpos);

                            if (state.IsAir() || state.IsLeaves())
                            {
                                await world.SetBlockStateUnsafe(this.GrainFactory, blockpos, _wood);
                                k1 = i2;
                            }
                        }

                        BlockWorldPos blockpos2 = new BlockWorldPos(i3, k1, j1);

                        for (int j3 = -3; j3 <= 3; ++j3)
                        {
                            for (int i4 = -3; i4 <= 3; ++i4)
                            {
                                if (Math.Abs(j3) != 3 || Math.Abs(i4) != 3)
                                {
                                    await world.SetBlockStateUnsafe(this.GrainFactory, BlockWorldPos.Add(blockpos2, j3, 0, i4), _leaves);
                                }
                            }
                        }

                        blockpos2 = blockpos2.Up();

                        for (int k3 = -1; k3 <= 1; ++k3)
                        {
                            for (int j4 = -1; j4 <= 1; ++j4)
                            {
                                await world.SetBlockStateUnsafe(this.GrainFactory, BlockWorldPos.Add(blockpos2, k3, 0, j4), _leaves);
                            }
                        }

                        await world.SetBlockStateUnsafe(this.GrainFactory, blockpos2.East(2), _leaves);
                        await world.SetBlockStateUnsafe(this.GrainFactory, blockpos2.West(2), _leaves);
                        await world.SetBlockStateUnsafe(this.GrainFactory, blockpos2.South(2), _leaves);
                        await world.SetBlockStateUnsafe(this.GrainFactory, blockpos2.North(2), _leaves);
                        i3 = pos.X;
                        j1 = pos.Z;
                        Facing enumfacing1 = Facing.RadomFacing(rand, Plane.XZ);

                        if (enumfacing1 != enumfacing)
                        {
                            int l3 = k2 - rand.Next(2) - 1;
                            int k4 = 1 + rand.Next(3);
                            k1 = 0;

                            for (int l4 = l3; l4 < height && k4 > 0; --k4)
                            {
                                if (l4 >= 1)
                                {
                                    int j2 = pos.Y + l4;
                                    i3 += enumfacing1.ToBlockVector().X;
                                    j1 += enumfacing1.ToBlockVector().Z;
                                    BlockWorldPos blockpos1 = new BlockWorldPos(i3, j2, j1);
                                    state = await world.GetBlockStateUnsafe(this.GrainFactory, blockpos1);

                                    if (state.IsAir() || state.IsLeaves())
                                    {
                                        await world.SetBlockStateUnsafe(this.GrainFactory, blockpos1, _wood);
                                        k1 = j2;
                                    }
                                }

                                ++l4;
                            }

                            if (k1 > 0)
                            {
                                BlockWorldPos blockpos3 = new BlockWorldPos(i3, k1, j1);

                                for (int i5 = -2; i5 <= 2; ++i5)
                                {
                                    for (int k5 = -2; k5 <= 2; ++k5)
                                    {
                                        if (Math.Abs(i5) != 2 || Math.Abs(k5) != 2)
                                        {
                                            await world.SetBlockStateUnsafe(this.GrainFactory, BlockWorldPos.Add(blockpos3, i5, 0, k5), _leaves);
                                        }
                                    }
                                }

                                blockpos3 = blockpos3.Up();

                                for (int j5 = -1; j5 <= 1; ++j5)
                                {
                                    for (int l5 = -1; l5 <= 1; ++l5)
                                    {
                                        await world.SetBlockStateUnsafe(this.GrainFactory, BlockWorldPos.Add(blockpos3, j5, 0, l5), _leaves);
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
