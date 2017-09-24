using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities.Components;

namespace MineCase.Server.Game.Entities
{
    internal class MobGrain : EntityGrain, IMob
    {
        protected override async Task InitializeComponents()
        {
            await base.InitializeComponents();

            // await SetComponent(new ActiveWorldPartitionComponent());
            await SetComponent(new EntityLifeTimeComponent());
            await SetComponent(new EntityOnGroundComponent());
            await SetComponent(new HealthComponent());
            await SetComponent(new StandaloneHeldItemComponent());
            await SetComponent(new NameComponent());
            await SetComponent(new EntityAiComponent());
            await SetComponent(new DiscoveryRegisterComponent());
            await SetComponent(new MobDiscoveryComponent());
            await SetComponent(new MobTypeComponent());
            /*
            if (MobType == MobType.Enderman)
            {
                await SetComponent(new BlockPlacementComponent());
                await SetComponent(new DiggingComponent());
            }
            */
        }

        public async override Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            await this.SetLocalValue(HealthComponent.MaxHealthProperty, 20u);
            await this.SetLocalValue(HealthComponent.HealthProperty, GetValue(HealthComponent.MaxHealthProperty));
            await this.SetLocalValue(EntityOnGroundComponent.IsOnGroundProperty, true);
        }
    }
}
