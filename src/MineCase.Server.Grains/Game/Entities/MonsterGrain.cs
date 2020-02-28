using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.World;
using MineCase.Server.World.EntitySpawner;
using MineCase.World;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Entities
{
    [Reentrant]
    internal class MonsterGrain : EntityGrain, IMonster
    {
        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            SetComponent(new BlockPlacementComponent());  // 末影人
            SetComponent(new DiggingComponent()); // 末影人
            SetComponent(new EntityLifeTimeComponent());
            SetComponent(new EntityOnGroundComponent());
            SetComponent(new HealthComponent());
            SetComponent(new StandaloneHeldItemComponent());
            SetComponent(new NameComponent());
            SetComponent(new DiscoveryRegisterComponent());
        }

        public async override Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            this.SetLocalValue(HealthComponent.MaxHealthProperty, 20);
            this.SetLocalValue(FoodComponent.MaxFoodProperty, 20);
            this.SetLocalValue(HealthComponent.HealthProperty, GetValue(HealthComponent.MaxHealthProperty));
            this.SetLocalValue(FoodComponent.FoodProperty, GetValue(FoodComponent.MaxFoodProperty));
        }
    }
}
