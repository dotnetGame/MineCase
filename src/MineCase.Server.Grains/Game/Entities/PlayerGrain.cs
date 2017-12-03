using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MineCase.Engine;
using MineCase.Protocol.Play;
using MineCase.Server.Components;
using MineCase.Server.Game.Blocks;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.Game.Items;
using MineCase.Server.Game.Windows;
using MineCase.Server.Game.Windows.SlotAreas;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.User;
using MineCase.Server.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Entities
{
    [Reentrant]
    internal class PlayerGrain : EntityGrain, IPlayer
    {
        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            SetComponent(new EntityLifeTimeComponent());
            SetComponent(new ActiveWorldPartitionComponent());
            SetComponent(new BlockPlacementComponent());
            SetComponent(new ClientboundPacketComponent());
            SetComponent(new ChunkLoaderComponent());
            SetComponent(new DiggingComponent());
            SetComponent(new DiscoveryRegisterComponent());
            SetComponent(new DraggedSlotComponent());
            SetComponent(new ExperienceComponent());
            SetComponent(new EntityOnGroundComponent());
            SetComponent(new FoodComponent());
            SetComponent(new HealthComponent());
            SetComponent(new HeldItemComponent());
            SetComponent(new InventoryComponent());
            SetComponent(new KeepAliveComponent());
            SetComponent(new NameComponent());
            SetComponent(new GameModeComponent());
            SetComponent(new PlayerListComponent());
            SetComponent(new PlayerDiscoveryComponent());
            SetComponent(new ServerboundPacketComponent());
            SetComponent(new SlotContainerComponent(SlotArea.UserSlotsCount));
            SetComponent(new SyncPlayerStateComponent());
            SetComponent(new TeleportComponent());
            SetComponent(new TossPickupComponent());
            SetComponent(new ViewDistanceComponent());
            SetComponent(new WindowManagerComponent());
            SetComponent(new CollectorComponent());
            SetComponent(new ColliderComponent());
            SetComponent(new MobSpawnerComponent());
        }

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            this.SetLocalValue(HealthComponent.MaxHealthProperty, 20u);
            this.SetLocalValue(FoodComponent.MaxFoodProperty, 20u);
            this.SetLocalValue(HealthComponent.HealthProperty, GetValue(HealthComponent.MaxHealthProperty));
            this.SetLocalValue(FoodComponent.FoodProperty, GetValue(FoodComponent.MaxFoodProperty));
        }

        public Task<SwingHandState> OnSwingHand(SwingHandState handState)
        {
            // TODO:update player state here.
            return Task.FromResult(
                handState == SwingHandState.MainHand ?
                SwingHandState.MainHand : SwingHandState.OffHand);
        }

        public Task SendClientAnimation(uint entityID, ClientboundAnimationId animationID)
        {
            // await _generator.SendClientAnimation(entityID, animationID);
            throw new NotImplementedException();
        }
    }
}
