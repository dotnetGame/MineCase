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
    public class BiomeSavannaDecoratorGrain : BiomeDecoratorGrain, IBiomeSavannaDecorator
    {
        public override Task OnActivateAsync()
        {
            if (this.GetPrimaryKeyLong() == (long)BiomeId.Savanna)
            {
                BiomeProperties.BaseHeight = 0.125f;
                BiomeProperties.HeightVariation = 0.05f;
                BiomeProperties.Temperature = 1.2f;
                BiomeProperties.Rainfall = 0.0f;
                BiomeProperties.EnableSnow = false;
                BiomeProperties.WaterColor = 16777215;
                BiomeProperties.EnableRain = false;
            }

            TopBlock = BlockStates.GrassBlock();
            FillerBlock = BlockStates.Dirt();

            PlantsList.Add(PlantsType.TallGrass);
            PlantsList.Add(PlantsType.AcaciaTree);

            PassiveMobList.Add(MobType.Cow);
            PassiveMobList.Add(MobType.Sheep);
            PassiveMobList.Add(MobType.Horse);

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

            /*
            var jungletreeGenerator = GrainFactory.GetGrain<IJungleGenerator>(
                JsonConvert.SerializeObject(new PlantsInfo
                {
                    PlantType = PlantsType.Jungle,
                    TreeHeight = 10,
                    ExtraHeight = 20
                }));
            await jungletreeGenerator.Generate(world, chunkWorldPos, 1);
            */
            var savannatreeGenerator = GrainFactory.GetGrain<ISavannaTreeGenerator>(
                JsonConvert.SerializeObject(new PlantsInfo
                {
                    PlantType = PlantsType.AcaciaTree,
                }));
            await savannatreeGenerator.Generate(world, chunkWorldPos, 1);
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
