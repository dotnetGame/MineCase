using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm.Noise;
using MineCase.Block;
using MineCase.Server.Game.Entities;
using MineCase.Server.World.Decoration.Plants;
using MineCase.World;
using MineCase.World.Biomes;
using MineCase.World.Plants;
using Orleans;

namespace MineCase.Server.World.Decoration.Biomes
{
    public abstract class BiomeDecoratorGrain : Grain, IBiomeDecorator
    {
        public BiomeProperties BiomeProperties { get; set; } = new BiomeProperties();

        /** The block expected to be on the top of this biome */
        public BlockState TopBlock { get; set; } = BlockStates.GrassBlock();

        /** The block to fill spots in when not on the top */
        public BlockState FillerBlock { get; set; } = BlockStates.Dirt();

        // define some noise
        protected static readonly OctavedNoise<Algorithm.Noise.PerlinNoise> _temperatureNoise =
            new OctavedNoise<PerlinNoise>(new PerlinNoise(1234), 4, 0.5F);

        protected static readonly OctavedNoise<PerlinNoise> _grassColorNoise =
            new OctavedNoise<PerlinNoise>(new PerlinNoise(2345), 4, 0.5F);

        // plants
        public List<PlantsType> PlantsList { get; set; } = new List<PlantsType>();

        // creatures
        public List<MobType> PassiveMobList { get; set; } = new List<MobType>();

        public List<MobType> MonsterList { get; set; } = new List<MobType>();

        public virtual Task Decorate(IWorld world, ChunkWorldPos chunkWorldPos)
        {
            throw new NotImplementedException();
        }

        public virtual Task SpawnMob(IWorld world, ChunkWorldPos chunkWorldPos)
        {
            throw new NotImplementedException();
        }

        public virtual Task SpawnMonster(IWorld world, ChunkWorldPos chunkWorldPos)
        {
            throw new NotImplementedException();
        }
    }
}
