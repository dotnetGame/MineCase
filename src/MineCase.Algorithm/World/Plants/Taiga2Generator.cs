using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Algorithm.World.Biomes;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Algorithm.World.Plants
{
    public class Taiga2Generator : AbstractTreeGenerator
    {
        private int _minTreeHeight;

        private bool _vines;

        private BlockState _wood;

        private BlockState _leaves;

        public Taiga2Generator(int treeHeight, bool vines, PlantsType treeType)
        {
            _minTreeHeight = treeHeight;
            _vines = vines;
            _wood = BlockStates.Wood(WoodType.Spruce);
            _leaves = BlockStates.Leaves(LeaveType.Spruce);
        }

        public override void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            int i = random.Next(4) + 6;
            int j = 1 + random.Next(2);
            int k = i - j;
            int l = 2 + random.Next(2);
            bool flag = true;

            if (pos.Y >= 1 && pos.Y + i + 1 <= 255)
            {
                for (int i1 = pos.Y; i1 <= pos.Y + 1 + i && flag; ++i1)
                {
                    int j1;

                    if (i1 - pos.Y < j)
                    {
                        j1 = 0;
                    }
                    else
                    {
                        j1 = l;
                    }

                    for (int k1 = pos.X - j1; k1 <= pos.X + j1 && flag; ++k1)
                    {
                        for (int l1 = pos.Z - j1; l1 <= pos.Z + j1 && flag; ++l1)
                        {
                            if (i1 >= 0 && i1 < 256)
                            {
                                BlockChunkPos chunkPos = new BlockWorldPos(k1, i1, l1).ToBlockChunkPos();
                                BlockState state = chunk[chunkPos.X, chunkPos.Y, chunkPos.Z];

                                if (!(state.IsAir()
                                            || state.IsSameId(BlockStates.Leaves())))
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
                    // return false;
                }
                else
                {
                    BlockChunkPos down = new BlockWorldPos(pos.X, pos.Y - 1, pos.Z).ToBlockChunkPos();
                    BlockState state = chunk[down.X, down.Y, down.Z];

                    if (CanSustainTree(PlantsType.Spruce, state) && pos.Y < 256 - i - 1)
                    {
                        // state.getBlock().onPlantGrow(state, worldIn, down, position);
                        int i3 = random.Next(2);
                        int j3 = 1;
                        int k3 = 0;

                        for (int l3 = 0; l3 <= k; ++l3)
                        {
                            int j4 = pos.Y + i - l3;

                            for (int i2 = pos.X - i3; i2 <= pos.X + i3; ++i2)
                            {
                                int j2 = i2 - pos.X;

                                for (int k2 = pos.Z - i3; k2 <= pos.Z + i3; ++k2)
                                {
                                    int l2 = k2 - pos.Z;

                                    if (Math.Abs(j2) != i3 || Math.Abs(l2) != i3 || i3 <= 0)
                                    {
                                        BlockChunkPos blockpos = new BlockWorldPos(i2, j4, k2).ToBlockChunkPos();
                                        state = chunk[blockpos.X, blockpos.Y, blockpos.Z];

                                        if (state.IsAir()
                                            || state.IsSameId(BlockStates.Leaves())
                                            || state.IsSameId(BlockStates.Vines()))
                                        {
                                            chunk[blockpos.X, blockpos.Y, blockpos.Z] = _leaves;
                                        }
                                    }
                                }
                            }

                            if (i3 >= j3)
                            {
                                i3 = k3;
                                k3 = 1;
                                ++j3;

                                if (j3 > l)
                                {
                                    j3 = l;
                                }
                            }
                            else
                            {
                                ++i3;
                            }
                        }

                        int i4 = random.Next(3);

                        for (int k4 = 0; k4 < i - i4; ++k4)
                        {
                            BlockChunkPos upN = new BlockWorldPos(pos.X, pos.Y + k4, pos.Z).ToBlockChunkPos();
                            state = chunk[upN.X, upN.Y, upN.Z];

                            if (state.IsAir() || state.IsSameId(BlockStates.Leaves()) || state.IsSameId(BlockStates.Leaves2()))
                            {
                                chunk[upN.X, upN.Y, upN.Z] = _wood;
                            }
                        }

                        // return true;
                    }
                    else
                    {
                        // return false;
                    }
                }
            }
            else
            {
                // return false;
            }
        }
    }
}
