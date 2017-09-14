using System;
using MineCase.Algorithm.World.Biomes;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Algorithm.World.Plants
{
    public class TreeGenerator : PlantsGenerator
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
                _wood = BlockStates.Wood(WoodType.Oak);
                _leaves = BlockStates.Leaves(LeaveType.Oak);
            }
            else if (treeType == PlantsType.Spruce)
            {
                _wood = BlockStates.Wood(WoodType.Spruce);
                _leaves = BlockStates.Leaves(LeaveType.Spruce);
            }
            else if (treeType == PlantsType.Birch)
            {
                _wood = BlockStates.Wood(WoodType.Birch);
                _leaves = BlockStates.Leaves(LeaveType.Birch);
            }
        }

        public bool CanTreeGrow(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Biome biome, Random random, BlockWorldPos pos, int height)
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
                            if (state != BlockStates.Air() &&
                                state.IsSameId(BlockStates.Leaves()) &&
                                state.IsSameId(BlockStates.Leaves2()))
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

                                        if (block == BlockStates.Air()
                                            || block.IsSameId(BlockStates.Leaves())
                                            || block.IsSameId(BlockStates.Vines()))
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

                            if (upBlock == BlockStates.Air()
                                            || upBlock.IsSameId(BlockStates.Leaves())
                                            || upBlock.IsSameId(BlockStates.Vines()))
                            {
                                chunk[chunkUpPos.X, chunkUpPos.Y, chunkUpPos.Z] = _wood;
                            }

                            ++upPos.Y;
                        }
                    }
                }
            }
        }
    }
}
