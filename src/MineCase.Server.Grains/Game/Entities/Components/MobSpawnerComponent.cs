using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm;
using MineCase.Algorithm.Game.Entity.Ai.MobAi;
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
using Orleans;

namespace MineCase.Server.Game.Entities.Components
{
    internal class MobSpawnerComponent : Component<PlayerGrain>
    {
        public MobSpawnerComponent(string name = "mobSpawner")
            : base(name)
        {
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

        private async Task OnGameTick(object sender, (TimeSpan deltaTime, long worldAge) e)
        {
            if (e.worldAge % 512 == 0 && e.worldAge > 9000 && e.worldAge < 18000)
            {
                var chunkAccessor = AttachedObject.GetComponent<ChunkAccessorComponent>();
                await chunkAccessor.GetBlockState(BlockWorldPos.Add(blockPos, 0, i, 0));
            }
        }
    }
}
