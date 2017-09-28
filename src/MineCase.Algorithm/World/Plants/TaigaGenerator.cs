using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Algorithm.World.Biomes;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Algorithm.World.Plants
{
    public class TaigaGenerator : PlantsGenerator
    {
        private int _minTreeHeight;

        private bool _vines;

        private BlockState _wood;

        private BlockState _leaves;

        public TaigaGenerator(int treeHeight, bool vines, PlantsType treeType)
        {
            _minTreeHeight = treeHeight;
            _vines = vines;
            _wood = BlockStates.Wood(WoodType.Spruce);
            _leaves = BlockStates.Leaves(LeaveType.Spruce);
        }

        public static bool CanSustainTree(PlantsType type, BlockState state)
        {
            if (state == BlockStates.Dirt() ||
                state == BlockStates.GrassBlock())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            int i = random.Next(5) + 7;
            int j = i - random.Next(2) - 3;
            int k = i - j;
            int l = 1 + random.Next(k + 1);

            if (pos.Y >= 1 && pos.Y + i + 1 <= 256)
            {
                bool flag = true;

                for (int i1 = pos.Y; i1 <= pos.Y + 1 + i && flag; ++i1)
                {
                    int j1 = 1;

                    if (i1 - pos.Y < j)
                    {
                        j1 = 0;
                    }
                    else
                    {
                        j1 = l;
                    }

                    BlockChunkPos blockpos = new BlockChunkPos { };

                    for (int k1 = pos.X - j1; k1 <= pos.X + j1 && flag; ++k1)
                    {
                        for (int l1 = pos.Z - j1; l1 <= pos.Z + j1 && flag; ++l1)
                        {
                            if (i1 >= 0 && i1 < 256)
                            {
                                BlockChunkPos chunkpos = new BlockWorldPos(k1, i1, l1).ToBlockChunkPos();
                                BlockState block = chunk[chunkpos.X, chunkpos.Y, chunkpos.Z];
                                if (!(block.IsAir()
                                            || block.IsSameId(BlockStates.Leaves())
                                            || block.IsSameId(BlockStates.Vines())))
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
                    bool isSoil = CanSustainTree(PlantsType.Spruce, state);

                    if (isSoil && pos.Y < 256 - i - 1)
                    {
                        // state.getBlock().onPlantGrow(state, world, down, position);
                        int k2 = 0;

                        for (int l2 = pos.Y + i; l2 >= pos.Y + j; --l2)
                        {
                            for (int j3 = pos.X - k2; j3 <= pos.X + k2; ++j3)
                            {
                                int k3 = j3 - pos.X;

                                for (int i2 = pos.Z - k2; i2 <= pos.Z + k2; ++i2)
                                {
                                    int j2 = i2 - pos.Z;

                                    if (Math.Abs(k3) != k2 || Math.Abs(j2) != k2 || k2 <= 0)
                                    {
                                        BlockChunkPos blockpos = new BlockWorldPos(j3, l2, i2).ToBlockChunkPos();
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

                            if (k2 >= 1 && l2 == pos.Y + j + 1)
                            {
                                --k2;
                            }
                            else if (k2 < l)
                            {
                                ++k2;
                            }
                        }

                        for (int i3 = 0; i3 < i - 1; ++i3)
                        {
                            BlockChunkPos upN = new BlockWorldPos(pos.X, pos.Y + i3, pos.Z).ToBlockChunkPos();
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
