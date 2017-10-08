using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Algorithm.World.Biomes;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Algorithm.World.Plants
{
    public class TaigaGenerator : AbstractTreeGenerator
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

        public override void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            int i = random.Next(5) + 7;
            int j = i - random.Next(2) - 3;
            int k = i - j;
            int l = 1 + random.Next(k + 1);

            if (pos.Y >= 1 && pos.Y + i + 1 <= 256)
            {
                bool flag = true;

                for (int y = pos.Y; y <= pos.Y + 1 + i && flag; ++y)
                {
                    int j1 = 1;

                    if (y - pos.Y < j)
                    {
                        j1 = 0;
                    }
                    else
                    {
                        j1 = l;
                    }

                    for (int x = pos.X - j1; x <= pos.X + j1 && flag; ++x)
                    {
                        for (int z = pos.Z - j1; z <= pos.Z + j1 && flag; ++z)
                        {
                            if (y >= 0 && y < 256)
                            {
                                BlockChunkPos chunkpos = new BlockWorldPos(x, y, z).ToBlockChunkPos();
                                BlockState block = chunk[chunkpos.X, chunkpos.Y, chunkpos.Z];
                                if (!(block.IsAir()
                                            || block.IsLeaves()
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
                        int k2 = 0;

                        for (int y = pos.Y + i; y >= pos.Y + j; --y)
                        {
                            for (int x = pos.X - k2; x <= pos.X + k2; ++x)
                            {
                                int k3 = x - pos.X;

                                for (int z = pos.Z - k2; z <= pos.Z + k2; ++z)
                                {
                                    int j2 = z - pos.Z;

                                    if (Math.Abs(k3) != k2 || Math.Abs(j2) != k2 || k2 <= 0)
                                    {
                                        BlockChunkPos blockpos = new BlockWorldPos(x, y, z).ToBlockChunkPos();
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

                            if (k2 >= 1 && y == pos.Y + j + 1)
                            {
                                --k2;
                            }
                            else if (k2 < l)
                            {
                                ++k2;
                            }
                        }

                        for (int y = 0; y < i - 1; ++y)
                        {
                            BlockChunkPos upN = new BlockWorldPos(pos.X, pos.Y + y, pos.Z).ToBlockChunkPos();
                            state = chunk[upN.X, upN.Y, upN.Z];

                            if (state.IsAir() || state.IsLeaves())
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
