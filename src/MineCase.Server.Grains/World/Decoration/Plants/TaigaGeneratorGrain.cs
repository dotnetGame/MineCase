using System;
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
    public class TaigaGeneratorGrain : AbstractTreeGeneratorGrain, ITaigaGenerator
    {
        private readonly ILogger _logger;

        private int _minTreeHeight;

        private bool _vines;

        private PlantsType _treeType;

        private BlockState _wood;

        private BlockState _leaves;

        public TaigaGeneratorGrain(ILoggerFactory loggerFactory)
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
                _treeType = plantsInfo.PlantType;
                if (plantsInfo.PlantType == PlantsType.Spruce)
                {
                    _wood = BlockStates.Wood(WoodType.Spruce);
                    _leaves = BlockStates.Leaves(LeaveType.Spruce);
                }
            }
            catch (Exception e)
            {
                this._logger.LogError(default(EventId), e, e.Message);
            }

            return base.OnActivateAsync();
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
            int height = random.Next(5) + 7;
            int heightLeaves = height - random.Next(2) - 3;
            int k = height - heightLeaves;
            int l = 1 + random.Next(k + 1);

            if (pos.Y >= 1 && pos.Y + height + 1 <= 256)
            {
                bool canSustainTreeFlag = true;

                for (int y = pos.Y; y <= pos.Y + 1 + height && canSustainTreeFlag; ++y)
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

                    for (int x = pos.X - xzWidth; x <= pos.X + xzWidth && canSustainTreeFlag; ++x)
                    {
                        for (int z = pos.Z - xzWidth; z <= pos.Z + xzWidth && canSustainTreeFlag; ++z)
                        {
                            if (y >= 0 && y < 256)
                            {
                                BlockState block = await GetBlock(world, chunkWorldPos, new BlockWorldPos(x, y, z));
                                if (!(block.IsAir()
                                            || block.IsLeaves()
                                            || block.IsSameId(BlockStates.Vines())))
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
                                        state = await GetBlock(world, chunkWorldPos, new BlockWorldPos(x, y, z));

                                        if (state.IsAir()
                                            || state.IsLeaves()
                                            || state.IsSameId(BlockStates.Vines()))
                                        {
                                            await SetBlock(world, chunkWorldPos, new BlockWorldPos(x, y, z), _leaves);
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
