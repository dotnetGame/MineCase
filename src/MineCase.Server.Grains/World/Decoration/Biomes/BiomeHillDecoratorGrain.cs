using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Server.Game.Entities;
using MineCase.Server.World.Decoration.Plants;
using MineCase.World;
using MineCase.World.Biomes;
using MineCase.World.Generation;
using MineCase.World.Plants;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Biomes
{
    [StatelessWorker]
    public class BiomeHillDecoratorGrain : BiomeDecoratorGrain, IBiomeHillDecorator
    {
        public override Task OnActivateAsync()
        {
            if (this.GetPrimaryKeyLong() == (long)BiomeId.Mountains)
            {
                BiomeProperties.BaseHeight = 1.0f;
                BiomeProperties.HeightVariation = 0.5f;
                BiomeProperties.Temperature = 0.2f;
                BiomeProperties.Rainfall = 0.3f;
                BiomeProperties.EnableSnow = false;
            }
            else if (this.GetPrimaryKeyLong() == (long)BiomeId.DesertHills)
            {
                BiomeProperties.BaseHeight = 0.45f;
                BiomeProperties.HeightVariation = 0.3f;
                BiomeProperties.Temperature = 2.0f;
                BiomeProperties.Rainfall = 0.0f;
                BiomeProperties.EnableSnow = false;
            }
            else if (this.GetPrimaryKeyLong() == (long)BiomeId.TaigaHills)
            {
                BiomeProperties.BaseHeight = 0.45f;
                BiomeProperties.HeightVariation = 0.3f;
                BiomeProperties.Temperature = 0.25f;
                BiomeProperties.Rainfall = 0.8f;
                BiomeProperties.EnableSnow = true;
            }

            BiomeProperties.WaterColor = 16777215;
            BiomeProperties.EnableRain = true;
            TopBlock = BlockStates.GrassBlock();
            FillerBlock = BlockStates.Dirt();

            PlantsList.Add(PlantsType.TallGrass);
            PlantsList.Add(PlantsType.Poppy);
            PlantsList.Add(PlantsType.Dandelion);

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

        public async override Task Decorate(IWorld world, ChunkWorldPos chunkWorldPos, GeneratorSettings settings)
        {
            await GenerateOre(world, chunkWorldPos, settings);

            var grassGenerator = GrainFactory.GetGrain<IGrassGenerator>(JsonConvert.SerializeObject(new PlantsInfo { }));
            await grassGenerator.Generate(world, chunkWorldPos, 10);

            var poppyGenerator = GrainFactory.GetGrain<IFlowersGenerator>(
                JsonConvert.SerializeObject(new PlantsInfo
                {
                    PlantType = PlantsType.Poppy
                }));
            await poppyGenerator.Generate(world, chunkWorldPos, 3);

            var dandelionGenerator = GrainFactory.GetGrain<IFlowersGenerator>(
                JsonConvert.SerializeObject(new PlantsInfo
                {
                    PlantType = PlantsType.Dandelion
                }));
            await dandelionGenerator.Generate(world, chunkWorldPos, 3);

            var oaktreeGenerator = GrainFactory.GetGrain<ITreeGenerator>(
                JsonConvert.SerializeObject(new PlantsInfo
                {
                    PlantType = PlantsType.Oak
                }));
            await oaktreeGenerator.Generate(world, chunkWorldPos, 1);
        }

        public override Task SpawnMob(IWorld world, ChunkWorldPos chunkWorldPos, GeneratorSettings settings)
        {
            throw new NotImplementedException();
        }

        public override Task SpawnMonster(IWorld world, ChunkWorldPos chunkWorldPos, GeneratorSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}
