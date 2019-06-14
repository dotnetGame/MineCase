using System;
using MineCase.Algorithm.World.Plants;
using MineCase.Block;
using MineCase.Server.Game.Entities;
using MineCase.Server.World;
using MineCase.Server.World.EntitySpawner;
using MineCase.World;
using MineCase.World.Biomes;
using MineCase.World.Generation;
using Orleans;

namespace MineCase.Algorithm.World.Biomes
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

            _passiveMobList.Add(MobType.Pig);
            _passiveMobList.Add(MobType.Sheep);
            _passiveMobList.Add(MobType.Cow);
            _passiveMobList.Add(MobType.Chicken);

            _monsterList.Add(MobType.Creeper);
            _monsterList.Add(MobType.Zombie);
            _monsterList.Add(MobType.Skeleton);
            _monsterList.Add(MobType.Spider);
        }

        // 添加其他东西
        public override void Decorate(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Random rand, BlockWorldPos pos)
        {
            GenTrees(world, grainFactory, chunk, rand, pos);
        }

        // 添加生物群系特有的生物
        public override void SpawnMob(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Random rand, BlockWorldPos pos)
        {
            ChunkWorldPos chunkPos = pos.ToChunkWorldPos();
            int seed = chunkPos.Z * 16384 + chunkPos.X;
            Random r = new Random(seed);
            foreach (MobType eachType in _passiveMobList)
            {
                if (r.Next(32) == 0)
                {
                    PassiveMobSpawner spawner = new PassiveMobSpawner(eachType, 15);
                    spawner.Spawn(world, grainFactory, chunk, rand, new BlockWorldPos(pos.X, pos.Y, pos.Z));
                }
            }
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

        public void GenTrees(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Random random, BlockWorldPos pos)
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
                    if (!chunk[x, y, z].IsAir())
                    {
                        h = y + 1;
                        break;
                    }
                }

                treeGenerator.Generate(world, grainFactory, chunk, this, random, new BlockWorldPos(pos.X + x, h, pos.Z + z));
            }
        }
    }
}
