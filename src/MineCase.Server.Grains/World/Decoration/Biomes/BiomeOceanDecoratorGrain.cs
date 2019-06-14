using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
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
    public class BiomeOceanDecoratorGrain : BiomeDecoratorGrain, IBiomeOceanDecorator
    {
        public override Task OnActivateAsync()
        {
            if (this.GetPrimaryKeyLong() == (long)BiomeId.Ocean)
            {
                BiomeProperties.BaseHeight = -1.0f;
                BiomeProperties.HeightVariation = 0.1f;
                BiomeProperties.Temperature = 0.5f;
                BiomeProperties.Rainfall = 0.5f;
                BiomeProperties.EnableSnow = false;
            }
            else if (this.GetPrimaryKeyLong() == (long)BiomeId.FrozenOcean)
            {
                BiomeProperties.BaseHeight = -1.0f;
                BiomeProperties.HeightVariation = 0.1f;
                BiomeProperties.Temperature = 0.0f;
                BiomeProperties.Rainfall = 0.5f;
                BiomeProperties.EnableSnow = true;
            }
            else if (this.GetPrimaryKeyLong() == (long)BiomeId.DeepOcean)
            {
                BiomeProperties.BaseHeight = -1.8f;
                BiomeProperties.HeightVariation = 0.1f;
                BiomeProperties.Temperature = 0.5f;
                BiomeProperties.Rainfall = 0.5f;
                BiomeProperties.EnableSnow = false;
            }

            TopBlock = BlockStates.Dirt();
            FillerBlock = BlockStates.Dirt();

            MonsterList.Add(MobType.Creeper);
            MonsterList.Add(MobType.Skeleton);
            MonsterList.Add(MobType.Zombie);
            MonsterList.Add(MobType.Spider);

            return Task.CompletedTask;
        }

        public override Task Decorate(IWorld world, ChunkWorldPos chunkWorldPos)
        {
            return Task.CompletedTask;
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
