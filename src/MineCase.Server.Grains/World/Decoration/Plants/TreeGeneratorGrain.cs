﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineCase.World;
using MineCase.World.Plants;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Plants
{
    [StatelessWorker]
    public class TreeGeneratorGrain : AbstractTreeGeneratorGrain, ITreeGenerator
    {
        private readonly ILogger _logger;

        private int _minTreeHeight;

        private bool _vines;

        private BlockState _wood;

        private BlockState _leaves;

        private PlantsType _treeType;

        public TreeGeneratorGrain(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TreeGeneratorGrain>();
        }

        public override Task OnActivateAsync()
        {
            try
            {
                var settings = this.GetPrimaryKeyString();
                PlantsInfo plantsInfo = JsonConvert.DeserializeObject<PlantsInfo>(settings);

                _minTreeHeight = plantsInfo.TreeHeight;
                _vines = plantsInfo.TreeVine;
                _treeType = plantsInfo.TreeType;
                if (plantsInfo.TreeType == PlantsType.Oak)
                {
                    _wood = BlockStates.Wood(WoodType.Oak);
                    _leaves = BlockStates.Leaves(LeaveType.Oak);
                }
                else if (plantsInfo.TreeType == PlantsType.Spruce)
                {
                    _wood = BlockStates.Wood(WoodType.Spruce);
                    _leaves = BlockStates.Leaves(LeaveType.Spruce);
                }
                else if (plantsInfo.TreeType == PlantsType.Birch)
                {
                    _wood = BlockStates.Wood(WoodType.Birch);
                    _leaves = BlockStates.Leaves(LeaveType.Birch);
                }
            }
            catch (Exception e)
            {
                this._logger.LogError(default(EventId), e, e.Message);
            }

            return base.OnActivateAsync();
        }

        public override async Task GenerateSingle(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            int seed = await world.GetSeed();
            int treeSeed = pos.X ^ pos.Z ^ seed;
            Random rand = new Random(treeSeed);
            await GenerateImpl(world, chunkWorldPos, pos, rand);
        }

        public async Task<bool> CanTreeGrow(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos, int height)
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
                            var checkPos = new BlockWorldPos(x, y, z);
                            BlockState state = await GetBlock(world, chunkWorldPos, checkPos);
                            if (!state.IsAir() &&
                                !state.IsSameId(BlockStates.Leaves()) &&
                                !state.IsSameId(BlockStates.Leaves2()))
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

        protected async Task GenerateImpl(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos, Random random)
        {
            int height = random.Next(3) + _minTreeHeight;

            // 不超出世界边界
            if (pos.Y >= 1 && pos.Y + height + 1 <= 256)
            {
                bool canTreeGrow = await CanTreeGrow(world, chunkWorldPos, pos, height);
                if (canTreeGrow)
                {
                    BlockWorldPos downPos = new BlockWorldPos(pos.X, pos.Y - 1, pos.Z);
                    BlockState downBlock = await GetBlock(world, chunkWorldPos, downPos);

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
                                        BlockState block = await GetBlock(world, chunkWorldPos, blockpos);

                                        if (block.IsAir()
                                            || block.IsSameId(BlockStates.Leaves())
                                            || block.IsSameId(BlockStates.Vines()))
                                        {
                                            await SetBlock(world, chunkWorldPos, blockpos, _leaves);
                                        }
                                    }
                                }
                            }
                        }

                        // 生成木头
                        BlockWorldPos upPos = pos;
                        for (int y = 0; y < height; ++y)
                        {
                            BlockState upBlock = await GetBlock(world, chunkWorldPos, upPos);

                            if (upBlock.IsAir()
                                            || upBlock.IsSameId(BlockStates.Leaves())
                                            || upBlock.IsSameId(BlockStates.Vines()))
                            {
                                await SetBlock(world, chunkWorldPos, upPos, _wood);
                            }

                            // 生成藤蔓
                            if (_vines && y > 0)
                            {
                                await RandomSetIfAir(world, chunkWorldPos, upPos.X - 1, upPos.Y, upPos.Z, BlockStates.Vines(VineType.East), random, 0.666f);

                                await RandomSetIfAir(world, chunkWorldPos, upPos.X + 1, upPos.Y, upPos.Z, BlockStates.Vines(VineType.West), random, 0.666f);

                                await RandomSetIfAir(world, chunkWorldPos, upPos.X, upPos.Y, upPos.Z - 1, BlockStates.Vines(VineType.South), random, 0.666f);

                                await RandomSetIfAir(world, chunkWorldPos, upPos.X, upPos.Y, upPos.Z + 1, BlockStates.Vines(VineType.North), random, 0.666f);
                            }

                            ++upPos.Y;
                        }

                        // 生成藤蔓
                        if (_vines)
                        {
                            for (int y = pos.Y + height - 3; y <= pos.Y + height; ++y)
                            {
                                int restHeight = y - (pos.Y + height);
                                int xzSize = 2 - restHeight / 2;

                                for (int x = pos.X - xzSize; x <= pos.X + xzSize; ++x)
                                {
                                    for (int z = pos.Z - xzSize; z <= pos.Z + xzSize; ++z)
                                    {
                                        if ((await GetBlock(world, chunkWorldPos, new BlockWorldPos(x, y, z))).IsLeaves())
                                        {
                                            await RandomSetIfAir(world, chunkWorldPos, x - 1, y, z, BlockStates.Vines(VineType.East), random, 0.25f);

                                            await RandomSetIfAir(world, chunkWorldPos, x + 1, y, z, BlockStates.Vines(VineType.West), random, 0.25f);

                                            await RandomSetIfAir(world, chunkWorldPos, x, y, z - 1, BlockStates.Vines(VineType.South), random, 0.25f);

                                            await RandomSetIfAir(world, chunkWorldPos, x, y, z + 1, BlockStates.Vines(VineType.North), random, 0.25f);
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
