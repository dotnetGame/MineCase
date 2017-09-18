using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MineCase.Protocol.Play;
using MineCase.Server.Game.Blocks;
using MineCase.Server.Game.Items;
using MineCase.Server.Game.Windows;
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
        private string _name;
        private Slot _draggedSlot;

        private uint _health;
        private const uint MaxHealth = 20;
        public const uint MaxFood = 20;
        private uint _teleportId;

        public override Task OnActivateAsync()
        {
            _teleportId = 0;

            return base.OnActivateAsync();
        }

        public async Task SendHealth()
        {
            await _generator.UpdateHealth(_health, MaxHealth, 20, MaxFood, 5.0f);
        }

        public Task<string> GetName() => Task.FromResult(_name);

        public Task SetName(string name)
        {
            _name = name;
            return Task.CompletedTask;
        }

        public async Task SendPlayerListAddPlayer(IReadOnlyList<IPlayer> player)
        {
            var desc = await Task.WhenAll(from p in player
                                          select p.GetDescription());
            await _generator.PlayerListItemAddPlayer(desc);
        }

        public async Task<PlayerDescription> GetDescription()
        {
            return new PlayerDescription
            {
                UUID = _user.GetPrimaryKey(),
                Name = _name,
                GameMode = new GameMode { ModeClass = GameMode.Class.Survival },
                Ping = await _user.GetPing()
            };
        }

        public async Task NotifyLoggedIn()
        {
            _draggedSlot = Slot.Empty;
            await SendPlayerListAddPlayer(new[] { this });
        }

        public async Task SendPositionAndLook()
        {
            await _generator.PositionAndLook(Position.X, Position.Y, Position.Z, _yaw, _pitch, 0, _teleportId);
        }

        public Task OnTeleportConfirm(uint teleportId)
        {
            return Task.CompletedTask;
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

        public async Task Spawn(Guid uuid, Vector3 position, float pitch, float yaw)
        {
            UUID = uuid;
            await SetPosition(position);
            _pitch = pitch;
            _yaw = yaw;
        }

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

        public async Task SetDraggedSlot(Slot item)
        {
            _draggedSlot = item;
            await _generator.SetSlot(0xFF, -1, item);
        }

        public Task<Slot> GetDraggedSlot() => Task.FromResult(_draggedSlot);

        public async Task TossPickup(Slot slot)
        {
            var chunk = GetChunkPosition();

            // 产生 Pickup
            var finder = GrainFactory.GetGrain<ICollectableFinder>(World.MakeCollectableFinderKey(chunk.x, chunk.z));
            await finder.SpawnPickup(Position, new[] { slot }.AsImmutable());
        }
    }
}
