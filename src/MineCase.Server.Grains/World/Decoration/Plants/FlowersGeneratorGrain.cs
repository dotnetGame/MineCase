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
    public class FlowersGeneratorGrain : PlantsGeneratorGrain, IFlowersGenerator
    {
        private readonly ILogger _logger;

        protected PlantsType _flowerType;

        public FlowersGeneratorGrain(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TreeGeneratorGrain>();
        }

        public override Task OnActivateAsync()
        {
            try
            {
                var settings = this.GetPrimaryKeyString();
                PlantsInfo plantsInfo = JsonConvert.DeserializeObject<PlantsInfo>(settings);

                _flowerType = plantsInfo.PlantType;
            }
            catch (Exception e)
            {
                this._logger.LogError(default(EventId), e, e.Message);
            }

            return base.OnActivateAsync();
        }

        public override async Task GenerateSingle(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            var curBlock = await GetBlock(world, chunkWorldPos, pos);
            var downBlock = await GetBlock(world, chunkWorldPos, new BlockWorldPos { X = pos.X, Y = pos.Y - 1, Z = pos.Z });

            if (curBlock.IsAir() &&
                downBlock == BlockStates.GrassBlock())
            {
                switch (_flowerType)
                {
                    case PlantsType.Poppy:
                        await SetBlock(world, chunkWorldPos, pos, BlockStates.Poppy());
                        break;
                    case PlantsType.Dandelion:
                        await SetBlock(world, chunkWorldPos, pos, BlockStates.Dandelion());
                        break;
                }
            }
        }
    }
}
