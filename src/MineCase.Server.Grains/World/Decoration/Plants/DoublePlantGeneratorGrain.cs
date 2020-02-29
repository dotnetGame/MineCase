using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineCase.Block;
using MineCase.World;
using MineCase.World.Plants;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Plants
{
    [StatelessWorker]
    public class DoublePlantGeneratorGrain : PlantsGeneratorGrain, IDoublePlantGenerator
    {
        private PlantsType _plantType;

        public DoublePlantGeneratorGrain(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DoublePlantGeneratorGrain>();
        }

        public async override Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            _plantType = _generatorSettings.PlantType;
        }

        public override Task GenerateSingle(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> GenerateImpl(IWorld world, BlockWorldPos pos, Random random)
        {
            if (await CanPlaceAt(world, pos, _plantType))
            {
                await PlaceAt(world, pos, _plantType);
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task PlaceAt(IWorld world, BlockWorldPos pos, PlantsType type)
        {
            if (type == PlantsType.DoubleTallgrass)
            {
                await world.SetBlockStateUnsafe(this.GrainFactory, pos, BlockStates.TallGrass(TallGrassHalfType.Lower));
                await world.SetBlockStateUnsafe(this.GrainFactory, pos.Up(), BlockStates.TallGrass(TallGrassHalfType.Upper));
            }
        }

        private async Task<bool> CanPlaceAt(IWorld world, BlockWorldPos pos, PlantsType type)
        {
            var block = await world.GetBlockStateUnsafe(this.GrainFactory, pos);
            var upBlock = await world.GetBlockStateUnsafe(this.GrainFactory, pos.Up());

            return block.IsAir() && upBlock.IsAir();
        }
    }
}
