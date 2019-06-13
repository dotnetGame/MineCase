using System;
using MineCase.Algorithm.World.Plants;
using MineCase.Server.Game.Entities;
using MineCase.Server.World;
using MineCase.Server.World.EntitySpawner;
using MineCase.World;
using MineCase.World.Biomes;
using MineCase.World.Generation;
using Orleans;

namespace MineCase.Algorithm.World.Biomes
{
    public class BiomeDesert : Biome
    {
        public BiomeDesert(BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _name = "desert";
            _biomeId = BiomeId.Desert;
            _baseHeight = 0.125F;
            _heightVariation = 0.05F;
            _temperature = 2.0F;
            _rainfall = 0.0F;
            _enableRain = false;

            _topBlock = BlockStates.Sand();
            _fillerBlock = BlockStates.Sand();
            _treesPerChunk = -999;
            _deadBushPerChunk = 2;
            _reedsPerChunk = 50;
            _cactiPerChunk = 10;

            _monsterList.Add(MobType.Creeper);
            _monsterList.Add(MobType.Zombie);
            _monsterList.Add(MobType.Skeleton);
            _monsterList.Add(MobType.Spider);
        }

        // 添加其他东西
        public override void Decorate(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Random rand, BlockWorldPos pos)
        {
            GenCacti(world, grainFactory, chunk, rand, pos);

            // TODO 生成仙人掌和枯木
            base.Decorate(world, grainFactory, chunk, rand, pos);
        }

        // 添加生物群系特有的生物
        public override void SpawnMob(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Random rand, BlockWorldPos pos)
        {
            ChunkWorldPos chunkPos = pos.ToChunkWorldPos();
            int seed = chunkPos.Z * 16384 + chunkPos.X;
            Random r = new Random(seed);
            foreach (MobType eachType in _passiveMobList)
            {
                if (r.Next(64) == 0)
                {
                    PassiveMobSpawner spawner = new PassiveMobSpawner(eachType, 10);
                    spawner.Spawn(world, grainFactory, chunk, rand, new BlockWorldPos(pos.X, pos.Y, pos.Z));
                }
            }
        }

        private void GenCacti(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Random random, BlockWorldPos pos)
        {
            int cactiMaxNum = random.Next(_cactiPerChunk);

            if (random.Next(64) == 0)
            {
                CactiGenerator generator = new CactiGenerator();
                for (int cactiNum = 0; cactiNum < cactiMaxNum; ++cactiNum)
                {
                    int x = random.Next(14) + 1;
                    int z = random.Next(14) + 1;
                    for (int y = 255; y >= 1; --y)
                    {
                        if (!chunk[x, y, z].IsAir())
                        {
                            generator.Generate(world, grainFactory, chunk, this, random, new BlockWorldPos(pos.X + x, y + 1, pos.Z + z));
                            break;
                        }
                    }
                }
            }
        }
    }
}