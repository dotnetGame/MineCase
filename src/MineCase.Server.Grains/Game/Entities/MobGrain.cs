using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.Network.Play;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Entities
{
    [Reentrant]
    internal class MobGrain : EntityGrain, IMob
    {
        protected override void InitializeComponents()
        {
            base.InitializeComponents();

            // await SetComponent(new ActiveWorldPartitionComponent());
            SetComponent(new EntityLifeTimeComponent());
            SetComponent(new EntityOnGroundComponent());
            SetComponent(new HealthComponent());
            SetComponent(new StandaloneHeldItemComponent());
            SetComponent(new NameComponent());
            SetComponent(new EntityAiComponent());
            SetComponent(new DiscoveryRegisterComponent());
            SetComponent(new MobDiscoveryComponent());
            SetComponent(new MobTypeComponent());
            SetComponent(new SyncMobStateComponent());
        }

        public async override Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            this.SetLocalValue(HealthComponent.MaxHealthProperty, 20u);
            this.SetLocalValue(HealthComponent.HealthProperty, GetValue(HealthComponent.MaxHealthProperty));
            this.SetLocalValue(EntityOnGroundComponent.IsOnGroundProperty, true);
        }
    }
}
