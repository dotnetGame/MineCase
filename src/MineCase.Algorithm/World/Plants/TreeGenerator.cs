using System;
using MineCase.Algorithm.World.Biomes;
using MineCase.Block;
using MineCase.Server.World;
using MineCase.World;
using MineCase.World.Plants;
using Orleans;

namespace MineCase.Algorithm.World.Plants
{
    public class TreeGenerator : AbstractTreeGenerator
    {
        private int _minTreeHeight;

        private bool _vines;

        private BlockState _wood;

        private BlockState _leaves;

        private PlantsType _treeType;

        public TreeGenerator(int treeHeight, bool vines, PlantsType treeType)
        {
            _minTreeHeight = treeHeight;
            _vines = vines;
            _treeType = treeType;
            if (treeType == PlantsType.Oak)
            {
                _wood = BlockStates.OakLog();
                _leaves = BlockStates.OakLeaves(OakLeavesDistanceType.Distance1, OakLeavesPersistentType.False);
            }
            else if (treeType == PlantsType.Spruce)
            {
                _wood = BlockStates.SpruceLog();
                _leaves = BlockStates.SpruceLeaves(SpruceLeavesDistanceType.Distance1, SpruceLeavesPersistentType.False);
            }
            else if (treeType == PlantsType.Birch)
            {
                _wood = BlockStates.BirchLog();
                _leaves = BlockStates.BirchLeaves(BirchLeavesDistanceType.Distance1, BirchLeavesPersistentType.False);
            }
        }

        public bool CanTreeGrow(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Biome biome, Random random, BlockWorldPos pos, int height)
        {
            bool result = true;

            // 检查所有方块可替换
            for (int y = pos.Y; y <= pos.Y + 1 + height; ++y)
            {
                int xzSize = 1;

                // 底端
                if (y == pos.Y)
                {
                    xzSize = 0;
                }

                // 顶端
                if (y >= pos.Y + height - 1)
                {
                    xzSize = 2;
                }

                // 检查这个平面所有方块可替换
                for (int x = pos.X - xzSize; x <= pos.X + xzSize && result; ++x)
                {
                    for (int z = pos.Z - xzSize; z <= pos.Z + xzSize && result; ++z)
                    {
                        if (y >= 0 && y < 256)
                        {
                            BlockChunkPos chunkPos = pos.ToBlockChunkPos();
                            BlockState state = chunk[chunkPos.X, chunkPos.Y, chunkPos.Z];
                            if (!state.IsAir() &&
                                state.IsLeaves())
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }
            }

            return result;
        }

        public override void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            int height = random.Next(3) + _minTreeHeight;

            // 不超出世界边界
            if (pos.Y >= 1 && pos.Y + height + 1 <= 256)
            {
                bool canTreeGrow = CanTreeGrow(world, grainFactory, chunk, biome, random, pos, height);
                if (canTreeGrow)
                {
                    BlockWorldPos downPos = new BlockWorldPos(pos.X, pos.Y - 1, pos.Z);
                    BlockChunkPos chunkDownPos = downPos.ToBlockChunkPos();
                    BlockState downBlock = chunk[chunkDownPos.X, chunkDownPos.Y, chunkDownPos.Z];

                    // 是可生成树的土壤
                    bool isSoil = CanSustainTree(_treeType, downBlock);

                    if (isSoil && pos.Y < 256 - height - 1)
                    {
                        // 生成叶子
                        for (int y = pos.Y + height - 3; y <= pos.Y + height; ++y)
                        {
                            int restHeight = y - (pos.Y + height);
                            int xzSize = 1 - restHeight / 2;

                            for (int x = pos.X - xzSize; x <= pos.X + xzSize; ++x)
                            {
                                int xOffset = x - pos.X;

                                for (int z = pos.Z - xzSize; z <= pos.Z + xzSize; ++z)
                                {
                                    int zOffset = z - pos.Z;

                                    if (Math.Abs(xOffset) != xzSize
                                        || Math.Abs(zOffset) != xzSize // 不在边缘4个点
                                        || (random.Next(2) != 0
                                        && restHeight != 0))
                                    {
                                        BlockWorldPos blockpos = new BlockWorldPos(x, y, z);
                                        BlockChunkPos chunkBlockPos = blockpos.ToBlockChunkPos();
                                        BlockState block = chunk[chunkBlockPos.X, chunkBlockPos.Y, chunkBlockPos.Z];

                                        if (block.IsAir()
                                            || block.IsLeaves()
                                            || block.IsId(BlockId.Vine))
                                        {
                                            chunk[chunkBlockPos.X, chunkBlockPos.Y, chunkBlockPos.Z] = _leaves;
                                        }
                                    }
                                }
                            }
                        }

                        // 生成木头
                        BlockWorldPos upPos = pos;
                        for (int y = 0; y < height; ++y)
                        {
                            BlockChunkPos chunkUpPos = upPos.ToBlockChunkPos();
                            BlockState upBlock = chunk[chunkUpPos.X, chunkUpPos.Y, chunkUpPos.Z];

                            if (upBlock.IsAir()
                                            || upBlock.IsLeaves()
                                            || upBlock.IsId(BlockId.Vine))
                            {
                                chunk[chunkUpPos.X, chunkUpPos.Y, chunkUpPos.Z] = _wood;
                            }

                            // 生成藤蔓
                            if (_vines && y > 0)
                            {
                                if (random.Next(3) > 0 && chunk[chunkUpPos.X - 1, chunkUpPos.Y, chunkUpPos.Z].IsAir())
                                {
                                    chunk[chunkUpPos.X - 1, chunkUpPos.Y, chunkUpPos.Z] = BlockStates.Vine(new VineType { East = VineEastType.True });
                                }

                                if (random.Next(3) > 0 && chunk[chunkUpPos.X + 1, chunkUpPos.Y, chunkUpPos.Z].IsAir())
                                {
                                    chunk[chunkUpPos.X + 1, chunkUpPos.Y, chunkUpPos.Z] = BlockStates.Vine(new VineType { West = VineWestType.True });
                                }

                                if (random.Next(3) > 0 && chunk[chunkUpPos.X, chunkUpPos.Y, chunkUpPos.Z - 1].IsAir())
                                {
                                    chunk[chunkUpPos.X, chunkUpPos.Y, chunkUpPos.Z - 1] = BlockStates.Vine(new VineType { South = VineSouthType.True });
                                }

                                if (random.Next(3) > 0 && chunk[chunkUpPos.X, chunkUpPos.Y, chunkUpPos.Z + 1].IsAir())
                                {
                                    chunk[chunkUpPos.X, chunkUpPos.Y, chunkUpPos.Z + 1] = BlockStates.Vine(new VineType { North = VineNorthType.True });
                                }
                            }

                            ++upPos.Y;
                        }

                        // 生成藤蔓
                        BlockChunkPos chunkPos = pos.ToBlockChunkPos();
                        if (_vines)
                        {
                            for (int y = chunkPos.Y + height - 3; y <= chunkPos.Y + height; ++y)
                            {
                                int restHeight = y - (chunkPos.Y + height);
                                int xzSize = 2 - restHeight / 2;

                                for (int x = chunkPos.X - xzSize; x <= chunkPos.X + xzSize; ++x)
                                {
                                    for (int z = chunkPos.Z - xzSize; z <= chunkPos.Z + xzSize; ++z)
                                    {
                                        if (chunk[x, y, z].IsLeaves())
                                        {
                                            if (random.Next(4) == 0 && chunk[x - 1, y, z].IsAir())
                                            {
                                                chunk[x - 1, y, z] = BlockStates.Vine(new VineType { East = VineEastType.True });
                                            }

                                            if (random.Next(4) == 0 && chunk[x + 1, y, z].IsAir())
                                            {
                                                chunk[x + 1, y, z] = BlockStates.Vine(new VineType { West = VineWestType.True });
                                            }

                                            if (random.Next(4) == 0 && chunk[x, y, z - 1].IsAir())
                                            {
                                                chunk[x, y, z - 1] = BlockStates.Vine(new VineType { South = VineSouthType.True });
                                            }

                                            if (random.Next(4) == 0 && chunk[x, y, z + 1].IsAir())
                                            {
                                                chunk[x, y, z + 1] = BlockStates.Vine(new VineType { North = VineNorthType.True });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
