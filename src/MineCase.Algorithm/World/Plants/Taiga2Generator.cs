using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Algorithm.World.Biomes;
using MineCase.Block;
using MineCase.Server.World;
using MineCase.World;
using MineCase.World.Plants;
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
            _wood = BlockStates.SpruceLog();

            // TODO distance should use the distance to the nearest log
            _leaves = BlockStates.SpruceLeaves(SpruceLeavesDistanceType.Distance1, SpruceLeavesPersistentType.False);
        }

        public override void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            int height = random.Next(4) + 6;
            int j = 1 + random.Next(2);
            int k = height - j;
            int l = 2 + random.Next(2);
            bool flag = true;

            if (pos.Y >= 1 && pos.Y + height + 1 <= 255)
            {
                for (int y = pos.Y; y <= pos.Y + 1 + height && flag; ++y)
                {
                    int xzWidth;

                    if (y - pos.Y < j)
                    {
                        xzWidth = 0;
                    }
                    else
                    {
                        xzWidth = l;
                    }

                    for (int x = pos.X - xzWidth; x <= pos.X + xzWidth && flag; ++x)
                    {
                        for (int z = pos.Z - xzWidth; z <= pos.Z + xzWidth && flag; ++z)
                        {
                            if (y >= 0 && y < 256)
                            {
                                BlockChunkPos chunkPos = new BlockWorldPos(x, y, z).ToBlockChunkPos();
                                BlockState state = chunk[chunkPos.X, chunkPos.Y, chunkPos.Z];

                                if (!(state.IsAir()
                                            || state.IsLeaves()))
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

                    if (CanSustainTree(PlantsType.Spruce, state) && pos.Y < 256 - height - 1)
                    {
                        int xzWidth = random.Next(2);
                        int j3 = 1;
                        int k3 = 0;

                        for (int l3 = 0; l3 <= k; ++l3)
                        {
                            int y = pos.Y + height - l3;

                            for (int x = pos.X - xzWidth; x <= pos.X + xzWidth; ++x)
                            {
                                int deltaX = x - pos.X;

                                for (int z = pos.Z - xzWidth; z <= pos.Z + xzWidth; ++z)
                                {
                                    int deltaZ = z - pos.Z;

                                    if (Math.Abs(deltaX) != xzWidth || Math.Abs(deltaZ) != xzWidth || xzWidth <= 0)
                                    {
                                        BlockChunkPos blockpos = new BlockWorldPos(x, y, z).ToBlockChunkPos();
                                        state = chunk[blockpos.X, blockpos.Y, blockpos.Z];

                                        if (state.IsAir()
                                            || state.IsLeaves()
                                            || state.IsId(BlockId.Vine))
                                        {
                                            chunk[blockpos.X, blockpos.Y, blockpos.Z] = _leaves;
                                        }
                                    }
                                }
                            }

                            if (xzWidth >= j3)
                            {
                                xzWidth = k3;
                                k3 = 1;
                                ++j3;

                                if (j3 > l)
                                {
                                    j3 = l;
                                }
                            }
                            else
                            {
                                ++xzWidth;
                            }
                        }

                        int heightLeft = random.Next(3);

                        for (int y = 0; y < height - heightLeft; ++y)
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
