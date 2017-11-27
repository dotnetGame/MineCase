using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm;
using MineCase.Algorithm.Game.Entity.Ai.MobAi;
using MineCase.Algorithm.World.Biomes;
using MineCase.Engine;
using MineCase.Graphics;
using MineCase.Server.Components;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.World;
using MineCase.Server.World.EntitySpawner;
using MineCase.Server.World.EntitySpawner.Ai;
using MineCase.Server.World.EntitySpawner.Ai.Action;
using MineCase.Server.World.EntitySpawner.Ai.MobAi;
using MineCase.World;
using MineCase.World.Biomes;
using MineCase.World.Generation;
using Orleans;

namespace MineCase.Server.Game.Entities.Components
{
    internal class MobSpawnerComponent : Component<PlayerGrain>
    {
        private Random random;

        public MobSpawnerComponent(string name = "mobSpawner")
            : base(name)
        {
            random = new Random();
        }

        protected override Task OnAttached()
        {
            Register();
            return base.OnAttached();
        }

        protected override Task OnDetached()
        {
            Unregister();
            return base.OnDetached();
        }

        private void Register()
        {
            AttachedObject.GetComponent<GameTickComponent>()
                .Tick += OnGameTick;
        }

        private void Unregister()
        {
            AttachedObject.GetComponent<GameTickComponent>()
                .Tick -= OnGameTick;
        }

        private async Task OnGameTick(object sender, GameTickArgs e)
        {
            if (e.WorldAge % 512 == 0 && e.TimeOfDay > 9000 && e.TimeOfDay < 18000)
            {
                EntityWorldPos playerPosition = AttachedObject.GetValue(EntityWorldPositionComponent.EntityWorldPositionProperty);
                int x = random.Next(9) - 4 + (int)playerPosition.X;
                int z = random.Next(9) - 4 + (int)playerPosition.Z;
                BlockWorldPos monsterBlockPos = new BlockWorldPos(x, 0, z);
                ChunkWorldPos monsterChunkPos = monsterBlockPos.ToChunkWorldPos();
                var chunkAccessor = AttachedObject.GetComponent<ChunkAccessorComponent>();
                BiomeId biomeId = await chunkAccessor.GetBlockBiome(monsterBlockPos);
                IWorld world = AttachedObject.GetValue(WorldComponent.WorldProperty);
                GeneratorSettings setting = await world.GetGeneratorSettings();
                Biome biome = Biome.GetBiome((int)biomeId, setting);
                IChunkColumn chunk = await chunkAccessor.GetChunk(monsterChunkPos);

                // TODO
                // biome.SpawnMonster(world, GrainFactory, await chunk.GetState(), random, monsterBlockPos);
            }
        }
    }
}
