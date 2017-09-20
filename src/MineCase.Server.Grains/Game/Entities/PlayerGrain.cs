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
        protected override async Task InitializeComponents()
        {
            await base.InitializeComponents();
            await SetComponent(new ActiveWorldPartitionComponent());
            await SetComponent(new BlockPlacementComponent());
            await SetComponent(new ClientboundPacketComponent());
            await SetComponent(new ChunkLoaderComponent());
            await SetComponent(new DiggingComponent());
            await SetComponent(new DiscoveryRegisterComponent());
            await SetComponent(new DraggedSlotComponent());
            await SetComponent(new ExperienceComponent());
            await SetComponent(new EntityLifeTimeComponent());
            await SetComponent(new EntityOnGroundComponent());
            await SetComponent(new FoodComponent());
            await SetComponent(new HealthComponent());
            await SetComponent(new HeldItemComponent());
            await SetComponent(new InventoryComponent());
            await SetComponent(new KeepAliveComponent());
            await SetComponent(new NameComponent());
            await SetComponent(new PlayerListComponent());
            await SetComponent(new PlayerDiscoveryComponent());
            await SetComponent(new ServerboundPacketComponent());
            await SetComponent(new SlotContainerComponent(SlotArea.UserSlotsCount));
            await SetComponent(new SyncPlayerStateComponent());
            await SetComponent(new TeleportComponent());
            await SetComponent(new TossPickupComponent());
            await SetComponent(new ViewDistanceComponent());
            await SetComponent(new WindowManagerComponent());
        }

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            await this.SetLocalValue(HealthComponent.MaxHealthProperty, 20u);
            await this.SetLocalValue(FoodComponent.MaxFoodProperty, 20u);
            await this.SetLocalValue(HealthComponent.HealthProperty, GetValue(HealthComponent.MaxHealthProperty));
            await this.SetLocalValue(FoodComponent.FoodProperty, GetValue(FoodComponent.MaxFoodProperty));
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

        /*
        protected override Task OnPositionChanged()
        {
            return CollectCollectables();
        }

        private async Task CollectCollectables()
        {
            var chunkPos = GetChunkPosition();
            var collectables = await GrainFactory.GetGrain<ICollectableFinder>(World.MakeCollectableFinderKey(chunkPos.x, chunkPos.z)).Collision(this);
            await Task.WhenAll(from c in collectables
                               select c.CollectBy(this));
        }

        public async Task<Slot> Collect(uint collectedEntityId, Slot item)
        {
            var after = await _inventory.DistributeStack(this, item);
            if (item.ItemCount != after.ItemCount)
                await GetBroadcastGenerator().CollectItem(collectedEntityId, EntityId, (byte)(item.ItemCount - after.ItemCount));
            return after;
        }

        public async Task TossPickup(Slot slot)
        {
            var chunk = GetChunkPosition();

            // 产生 Pickup
            var finder = GrainFactory.GetGrain<ICollectableFinder>(World.MakeCollectableFinderKey(chunk.x, chunk.z));
            await finder.SpawnPickup(Position, new[] { slot }.AsImmutable());
        }*/
    }
}
