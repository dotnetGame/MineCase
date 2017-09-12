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
    public class BiomeOcean : Biome
    {
        public BiomeOcean(BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _name = "ocean";
            _biomeId = BiomeId.Ocean;
            _baseHeight = -1.0F;
            _heightVariation = 0.1F;

            _passiveMobList.Add(Game.Entities.MobType.Squid);
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
                    PassiveMobSpawner spawner = new PassiveMobSpawner(eachType, 15);
                    spawner.Spawn(world, grainFactory, chunk, rand, new BlockWorldPos(pos.X, pos.Y, pos.Z));
                }
            }

            return Task.CompletedTask;
        }
    }
}