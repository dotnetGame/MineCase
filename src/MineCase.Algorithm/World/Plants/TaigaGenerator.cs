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
            _wood = BlockStates.SpruceLog();
            _leaves = BlockStates.SpruceLeaves(SpruceLeavesDistanceType.Distance1, SpruceLeavesPersistentType.False);
        }

        public override void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            int height = random.Next(5) + 7;
            int heightLeaves = height - random.Next(2) - 3;
            int k = height - heightLeaves;
            int l = 1 + random.Next(k + 1);

            if (pos.Y >= 1 && pos.Y + height + 1 <= 256)
            {
                bool flag = true;

                for (int y = pos.Y; y <= pos.Y + 1 + height && flag; ++y)
                {
                    int xzWidth = 1;

                    if (y - pos.Y < heightLeaves)
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
                                BlockChunkPos chunkpos = new BlockWorldPos(x, y, z).ToBlockChunkPos();
                                BlockState block = chunk[chunkpos.X, chunkpos.Y, chunkpos.Z];
                                if (!(block.IsAir()
                                            || block.IsLeaves()
                                            || block.IsId(BlockId.Vine)))
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

                    if (isSoil && pos.Y < 256 - height - 1)
                    {
                        int xzWidth = 0;

                        for (int y = pos.Y + height; y >= pos.Y + heightLeaves; --y)
                        {
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

                            if (xzWidth >= 1 && y == pos.Y + heightLeaves + 1)
                            {
                                --xzWidth;
                            }
                            else if (xzWidth < l)
                            {
                                ++xzWidth;
                            }
                        }

                        for (int y = 0; y < height - 1; ++y)
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
