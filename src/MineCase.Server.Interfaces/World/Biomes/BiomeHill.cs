using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.World.EntitySpawner;
using MineCase.Server.World.Generation;
using MineCase.Server.World.Plants;
using Orleans;

namespace MineCase.Server.World.Biomes
{
    public enum BiomeHillType
    {
        Normal,
        ExtraTrees,
        Mutated
    }

    public class BiomeHill : Biome
    {
        protected BiomeHillType _type;

        public BiomeHill(BiomeHillType type, BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _type = type;
            if (type == BiomeHillType.Normal)
            {
                _name = "extreme_hills";
                _biomeId = BiomeId.ExtremeHills;

                _baseHeight = 1.0F;
                _heightVariation = 0.5F;
                _temperature = 0.2F;
                _rainfall = 0.3F;
                _enableRain = true;
            }
            else
            {
                _name = "extreme_hills";
                _biomeId = BiomeId.ExtremeHills;

                _baseHeight = 1.0F;
                _heightVariation = 0.5F;
                _temperature = 0.2F;
                _rainfall = 0.3F;
                _enableRain = true;
            }

            _passiveMobList.Add(Game.Entities.MobType.Pig);
            _passiveMobList.Add(Game.Entities.MobType.Sheep);
            _passiveMobList.Add(Game.Entities.MobType.Cow);
            _passiveMobList.Add(Game.Entities.MobType.Chicken);

            _monsterList.Add(Game.Entities.MobType.Creeper);
            _monsterList.Add(Game.Entities.MobType.Zombie);
            _monsterList.Add(Game.Entities.MobType.Skeleton);
            _monsterList.Add(Game.Entities.MobType.Spider);
        }

        // 添加其他东西
        public override async Task Decorate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random rand, BlockWorldPos pos)
        {
            await GenTrees(world, grainFactory, chunk, rand, pos);
        }

        // 添加生物群系特有的生物
        public override Task SpawnMob(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random rand, BlockWorldPos pos)
        {
            ChunkWorldPos chunkPos = pos.ToChunkWorldPos();
            int seed = chunkPos.Z * 16384 + chunkPos.X;
            Random r = new Random(seed);
            foreach (MobType eachType in _passiveMobList)
            {
                if (r.Next(32) == 0)
                {
                    PassiveMobSpawner spawner = new PassiveMobSpawner(eachType, 5);
                    spawner.Spawn(world, grainFactory, chunk, rand, new BlockWorldPos(pos.X, pos.Y, pos.Z));
                }
            }

            return Task.CompletedTask;
        }

        public override void GenerateBiomeTerrain(int seaLevel, Random rand, ChunkColumnStorage chunk, int chunk_x, int chunk_z, int x_in_chunk, int z_in_chunk, double noiseVal)
        {
            _topBlock = BlockStates.GrassBlock();
            _fillerBlock = BlockStates.Dirt();

            if ((noiseVal < -1.0D || noiseVal > 2.0D) && _type == BiomeHillType.Mutated)
            {
                _topBlock = BlockStates.Gravel();
                _fillerBlock = BlockStates.Gravel();
            }
            else if (noiseVal > 0.0D && _type != BiomeHillType.ExtraTrees)
            {
                _topBlock = BlockStates.Stone();
                _fillerBlock = BlockStates.Stone();
            }

            base.GenerateBiomeTerrain(seaLevel, rand, chunk, chunk_x, chunk_z, x_in_chunk, z_in_chunk, noiseVal);
        }

        public async Task GenTrees(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockWorldPos pos)
        {
            int treesPerChunk = _treesPerChunk;

            if (random.NextDouble() < _extraTreeChance)
            {
                ++treesPerChunk;
            }

            for (int num = 0; num < treesPerChunk; ++num)
            {
                int x = random.Next(12) + 2;
                int z = random.Next(12) + 2;

                TreeGenerator treeGenerator = new TreeGenerator(5, false, GetRandomTree(random));

                // 获得地表面高度
                int h = 0;
                for (int y = 255; y >= 0; --y)
                {
                    if (chunk[x, y, z] != BlockStates.Air())
                    {
                        h = y + 1;
                        break;
                    }
                }

                await treeGenerator.Generate(world, grainFactory, chunk, this, random, new BlockWorldPos(pos.X + x, h, pos.Z + z));
            }
        }
    }
}
