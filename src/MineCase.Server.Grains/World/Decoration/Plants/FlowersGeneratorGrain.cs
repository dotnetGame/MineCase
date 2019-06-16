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
    public class FlowersGeneratorGrain : PlantsGeneratorGrain, IFlowersGenerator
    {
        protected PlantsType _flowerType;

        public FlowersGeneratorGrain(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FlowersGeneratorGrain>();
        }

        public async override Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            _flowerType = _generatorSettings.PlantType;
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
