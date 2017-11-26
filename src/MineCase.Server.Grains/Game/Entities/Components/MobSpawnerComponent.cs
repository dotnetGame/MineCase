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
            long timeOfDay = e.worldAge % 24000;
            if (e.worldAge % 16 == 0 && timeOfDay > 12000 && timeOfDay < 24000)
            {
                EntityWorldPos playerPosition = AttachedObject.GetValue(EntityWorldPositionComponent.EntityWorldPositionProperty);
                int distance = random.Next(16) + 16;
                double angle = random.NextDouble() * 2 * Math.PI;
                double deltaX = distance * Math.Cos(angle) + playerPosition.X;
                double deltaZ = distance * Math.Sin(angle) + playerPosition.Z;
                BlockWorldPos monsterBlockPos = new BlockWorldPos((int)deltaX, 0, (int)deltaZ);
                ChunkWorldPos monsterChunkPos = monsterBlockPos.ToChunkWorldPos();
                var chunkAccessor = AttachedObject.GetComponent<ChunkAccessorComponent>();
                BiomeId biomeId = await chunkAccessor.GetBlockBiome(monsterBlockPos);
                IWorld world = AttachedObject.GetValue(WorldComponent.WorldProperty);
                GeneratorSettings setting = await world.GetGeneratorSettings();
                Biome biome = Biome.GetBiome((int)biomeId, setting);
                IChunkColumn chunk = await chunkAccessor.GetChunk(monsterChunkPos);

                // TODO
                biome.SpawnMonster(world, GrainFactory, await chunk.GetState(), random, monsterBlockPos);
            }
        }
    }
}
