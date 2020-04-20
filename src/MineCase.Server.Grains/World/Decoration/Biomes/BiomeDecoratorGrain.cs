using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm.Noise;
using MineCase.Block;
using MineCase.Server.Game.Entities;
using MineCase.Server.World.Decoration.Mine;
using MineCase.Server.World.Decoration.Plants;
using MineCase.World;
using MineCase.World.Biomes;
using MineCase.World.Generation;
using MineCase.World.Plants;
using Orleans;

namespace MineCase.Server.World.Decoration.Biomes
{
    public abstract class BiomeDecoratorGrain : Grain, IBiomeDecorator
    {
        public GeneratorSettings GenSettings { get; set; } = new GeneratorSettings();

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

        protected async Task GenerateOre(IWorld world, ChunkWorldPos chunkWorldPos, GeneratorSettings settings)
        {
            var oreGenerator = GrainFactory.GetGrain<IMinableGenerator>(0);
            await oreGenerator.Generate(world, chunkWorldPos, BlockStates.Dirt(), settings.DirtCount, settings.DirtSize, settings.DirtMinHeight, settings.DirtMaxHeight);
            await oreGenerator.Generate(world, chunkWorldPos, BlockStates.Gravel(), settings.GravelCount, settings.GravelSize, settings.GravelMinHeight, settings.GravelMaxHeight);
            await oreGenerator.Generate(world, chunkWorldPos, BlockStates.Granite(), settings.GraniteCount, settings.GraniteSize, settings.GraniteMinHeight, settings.GraniteMaxHeight);
            await oreGenerator.Generate(world, chunkWorldPos, BlockStates.Diorite(), settings.DioriteCount, settings.DioriteSize, settings.DioriteMinHeight, settings.DioriteMaxHeight);
            await oreGenerator.Generate(world, chunkWorldPos, BlockStates.Andesite(), settings.AndesiteCount, settings.AndesiteSize, settings.AndesiteMinHeight, settings.AndesiteMaxHeight);

            await oreGenerator.Generate(world, chunkWorldPos, BlockStates.CoalOre(), settings.CoalCount, settings.CoalSize, settings.CoalMinHeight, settings.CoalMaxHeight);
            await oreGenerator.Generate(world, chunkWorldPos, BlockStates.IronOre(), settings.IronCount, settings.IronSize, settings.IronMinHeight, settings.IronMaxHeight);
            await oreGenerator.Generate(world, chunkWorldPos, BlockStates.GoldOre(), settings.GoldCount, settings.GoldSize, settings.GoldMinHeight, settings.GoldMaxHeight);
            await oreGenerator.Generate(world, chunkWorldPos, BlockStates.DiamondOre(), settings.DiamondCount, settings.DiamondSize, settings.DiamondMinHeight, settings.DiamondMaxHeight);
            await oreGenerator.Generate(world, chunkWorldPos, BlockStates.RedstoneOre(), settings.RedstoneCount, settings.RedstoneSize, settings.RedstoneMinHeight, settings.RedstoneMaxHeight);
            await oreGenerator.Generate(world, chunkWorldPos, BlockStates.LapisOre(), settings.LapisCount, settings.LapisSize, settings.LapisCenterHeight - settings.LapisSpread, settings.LapisCenterHeight + settings.LapisSpread);
        }

        public virtual Task Decorate(IWorld world, ChunkWorldPos chunkWorldPos, GeneratorSettings settings)
        {
            throw new NotImplementedException();
        }

        public virtual Task SpawnMob(IWorld world, ChunkWorldPos chunkWorldPos, GeneratorSettings settings)
        {
            throw new NotImplementedException();
        }

        public virtual Task SpawnMonster(IWorld world, ChunkWorldPos chunkWorldPos, GeneratorSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}
