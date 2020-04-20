using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineCase.Block;
using MineCase.World;
using MineCase.World.Plants;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Plants
{
    [StatelessWorker]
    public class Taiga2GeneratorGrain : AbstractTreeGeneratorGrain, ITaiga2Generator
    {
        private int _minTreeHeight;

        private bool _vines;

        private PlantsType _treeType;

        public Taiga2GeneratorGrain(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Taiga2GeneratorGrain>();
        }

        public async override Task OnActivateAsync()
        {
            await base.OnActivateAsync();

            _minTreeHeight = _generatorSettings.TreeHeight;
            _vines = _generatorSettings.TreeVine;
            _treeType = _generatorSettings.PlantType;
            if (_generatorSettings.PlantType == PlantsType.Spruce)
            {
                _wood = BlockStates.SpruceLog();
                _leaves = BlockStates.SpruceLeaves(SpruceLeavesDistanceType.Distance1, SpruceLeavesPersistentType.False);
            }
        }

        public async override Task GenerateSingle(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            int seed = await world.GetSeed();
            int treeSeed = pos.X ^ pos.Z ^ seed;
            Random rand = new Random(treeSeed);
            await GenerateImpl(world, chunkWorldPos, pos, rand);
        }

        private async Task<bool> GenerateImpl(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos, Random random)
        {
            int height = random.Next(4) + 6;
            int j = 1 + random.Next(2);
            int k = height - j;
            int l = 2 + random.Next(2);
            bool canSustainTreeFlag = true;

            if (pos.Y >= 1 && pos.Y + height + 1 <= 255)
            {
                for (int y = pos.Y; y <= pos.Y + 1 + height && canSustainTreeFlag; ++y)
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

                    for (int x = pos.X - xzWidth; x <= pos.X + xzWidth && canSustainTreeFlag; ++x)
                    {
                        for (int z = pos.Z - xzWidth; z <= pos.Z + xzWidth && canSustainTreeFlag; ++z)
                        {
                            if (y >= 0 && y < 256)
                            {
                                BlockState state = await GetBlock(world, chunkWorldPos, new BlockWorldPos(x, y, z));

                                if (!(state.IsAir()
                                            || state.IsLeaves()))
                                {
                                    canSustainTreeFlag = false;
                                }
                            }
                            else
                            {
                                canSustainTreeFlag = false;
                            }
                        }
                    }
                }

                if (!canSustainTreeFlag)
                {
                    return false;
                }
                else
                {
                    BlockState state = await GetBlock(world, chunkWorldPos, new BlockWorldPos(pos.X, pos.Y - 1, pos.Z));

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
                                        state = await GetBlock(world, chunkWorldPos, new BlockWorldPos(x, y, z));

                                        if (state.IsAir()
                                            || state.IsLeaves()
                                            || state.IsId(BlockId.Vine))
                                        {
                                            await SetBlock(world, chunkWorldPos, new BlockWorldPos(x, y, z), _leaves);
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
                            BlockWorldPos upN = new BlockWorldPos(pos.X, pos.Y + y, pos.Z);
                            state = await GetBlock(world, chunkWorldPos, upN);

                            if (state.IsAir() || state.IsLeaves())
                            {
                                await SetBlock(world, chunkWorldPos, upN, _wood);
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
