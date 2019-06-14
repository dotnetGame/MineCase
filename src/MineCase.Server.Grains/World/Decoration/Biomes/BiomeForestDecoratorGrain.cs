using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.World.Decoration.Plants;
using MineCase.World;
using MineCase.World.Biomes;
using MineCase.World.Plants;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Biomes
{
    [StatelessWorker]
    public class BiomeForestDecoratorGrain : BiomeDecoratorGrain, IBiomeForestDecorator
    {
        public override Task OnActivateAsync()
        {
            if (this.GetPrimaryKeyLong() == (long)BiomeId.Forest)
            {
                BiomeProperties.BaseHeight = 0.1f;
                BiomeProperties.HeightVariation = 0.2f;
                BiomeProperties.Temperature = 0.7f;
                BiomeProperties.Rainfall = 0.8f;
                BiomeProperties.EnableSnow = false;
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

        public async override Task Decorate(IWorld world, ChunkWorldPos chunkWorldPos)
        {
            PlantsInfo info = new PlantsInfo();
            String infoString = JsonConvert.SerializeObject(info);

            var grassGenerator = GrainFactory.GetGrain<IGrassGenerator>(infoString);
            await grassGenerator.Generate(world, chunkWorldPos, 10, 0);

            var treeGenerator = GrainFactory.GetGrain<ITreeGenerator>(infoString);
            await treeGenerator.Generate(world, chunkWorldPos, 1, 1);
        }

        public override Task SpawnMob(IWorld world, ChunkWorldPos chunkWorldPos)
        {
            throw new NotImplementedException();
        }

        public override Task SpawnMonster(IWorld world, ChunkWorldPos chunkWorldPos)
        {
            throw new NotImplementedException();
        }
    }
}
