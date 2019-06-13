using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.World.Decoration.Plants;
using MineCase.World;
using MineCase.World.Biomes;
using MineCase.World.Plants;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Biomes
{
    [StatelessWorker]
    public class BiomePlainsDecoratorGrain : BiomeDecoratorGrain, IBiomePlainsDecorator
    {
        public override Task OnActivateAsync()
        {
            if (this.GetPrimaryKeyLong() == (long)BiomeId.Plains)
            {
                BiomeProperties.BaseHeight = 0.125f;
                BiomeProperties.HeightVariation = 0.05f;
                BiomeProperties.Temperature = 0.8f;
                BiomeProperties.Rainfall = 0.4f;
                BiomeProperties.EnableSnow = false;
            }
            else if (this.GetPrimaryKeyLong() == (long)BiomeId.SunflowerPlains)
            {
                BiomeProperties.BaseHeight = 0.125f;
                BiomeProperties.HeightVariation = 0.05f;
                BiomeProperties.Temperature = 0.8f;
                BiomeProperties.Rainfall = 0.4f;
                BiomeProperties.EnableSnow = false;
            }
            else if (this.GetPrimaryKeyLong() == (long)BiomeId.SnowyTundra)
            {
                BiomeProperties.BaseHeight = 0.125f;
                BiomeProperties.HeightVariation = 0.05f;
                BiomeProperties.Temperature = 0.0f;
                BiomeProperties.Rainfall = 0.5f;
                BiomeProperties.EnableSnow = true;
            }

            BiomeProperties.WaterColor = 16777215;
            BiomeProperties.EnableRain = true;
            TopBlock = BlockStates.GrassBlock();
            FillerBlock = BlockStates.Dirt();

            PlantsList.Add(PlantsType.TallGrass);
            PlantsList.Add(PlantsType.RedFlower);
            PlantsList.Add(PlantsType.YellowFlower);

            PassiveMobList.Add(MobType.Cow);
            PassiveMobList.Add(MobType.Sheep);
            PassiveMobList.Add(MobType.Horse);
            PassiveMobList.Add(MobType.Donkey);

            MonsterList.Add(MobType.Creeper);
            MonsterList.Add(MobType.Skeleton);
            MonsterList.Add(MobType.Zombie);
            MonsterList.Add(MobType.Spider);

            return Task.CompletedTask;
        }

        public override Task Decorate(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            var grassGenerator = GrainFactory.GetGrain<IGrassGenerator>(0);
            return grassGenerator.Generate(world, chunkWorldPos, 10, 0);
        }

        public override Task SpawnMob(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            throw new NotImplementedException();
        }

        public override Task SpawnMonster(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            throw new NotImplementedException();
        }
    }
}
