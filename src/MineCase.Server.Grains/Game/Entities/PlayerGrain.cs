using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MineCase.Formats;
using MineCase.Protocol.Play;
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
        private IUser _user;
        private ClientPlayPacketGenerator _generator;

        private string _name;
        private Slot[] _inventorySlots;
        private IInventoryWindow _inventory;

        public Task<IInventoryWindow> GetInventory() => Task.FromResult(_inventory);

        private uint _health;
        private const uint MaxHealth = 20;
        public const uint MaxFood = 20;
        private uint _currentExp;
        private uint _levelMaxExp;
        private uint _totalExp;
        private uint _level;
        private uint _teleportId;

        private float _pitch;
        private float _yaw;

        private (Position, BlockState)? _diggingBlock;

        public override Task OnActivateAsync()
        {
            _inventory = GrainFactory.GetGrain<IInventoryWindow>(Guid.NewGuid());
            _currentExp = 0;
            _totalExp = 0;
            _level = 0;
            _teleportId = 0;
            _levelMaxExp = 7;

            return base.OnActivateAsync();
        }

        public async Task SendWholeInventory()
        {
            var slots = await _inventory.GetSlots(this);
            await _generator.WindowItems(0, slots);
        }

        public async Task BindToUser(IUser user)
        {
            _generator = new ClientPlayPacketGenerator(await user.GetClientPacketSink());
            _user = user;
            _health = MaxHealth;
        }

        public async Task SendHealth()
        {
            await _generator.UpdateHealth(_health, MaxHealth, 20, MaxFood, 5.0f);
        }

        public async Task SendExperience()
        {
            await _generator.SetExperience((float)_currentExp / _levelMaxExp, _level, _totalExp);
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
            _inventorySlots = await _user.GetInventorySlots();
            await SendWholeInventory();
            await SendExperience();
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

        public Task SetLook(float yaw, float pitch, bool onGround)
        {
            _pitch = pitch;
            _yaw = yaw;
            return Task.CompletedTask;
        }

        public Task<SwingHandState> OnSwingHand(SwingHandState handState)
        {
            // TODO:update player state here.
            return Task.FromResult(
                handState == SwingHandState.MainHand ?
                SwingHandState.MainHand : SwingHandState.OffHand);
        }

        public async Task SendClientAnimation(uint entityID, ClientboundAnimationId animationID)
        {
            await _generator.SendClientAnimation(entityID, animationID);
        }

        private const float _maxDiggingRadius = 6;

        public async Task StartDigging(Position location, PlayerDiggingFace face)
        {
            // A Notchian server only accepts digging packets with coordinates within a 6-unit radius between the center of the block and 1.5 units from the player's feet (not their eyes).
            var distance = (new Vector3(location.X + 0.5f, location.Y + 0.5f, location.Z + 0.5f)
                - new Vector3(Position.X, Position.Y + 1.5f, Position.Z)).Length();
            if (distance <= _maxDiggingRadius)
                _diggingBlock = (location, await World.GetBlockState(GrainFactory, location.X, location.Y, location.Z));
        }

        public Task CancelDigging(Position location, PlayerDiggingFace face)
        {
            _diggingBlock = null;
            return Task.CompletedTask;
        }

        public async Task FinishDigging(Position location, PlayerDiggingFace face)
        {
            if (_diggingBlock != null)
            {
                var oldState = _diggingBlock.Value.Item2;
                var newState = BlockStates.Air();
                await World.SetBlockState(GrainFactory, location.X, location.Y, location.Z, newState);

                var chunk = location.GetChunk();
                await GetBroadcastGenerator(chunk.chunkX, chunk.chunkZ).BlockChange(location, newState);

                // 产生 Pickup
                var finder = GrainFactory.GetGrain<ICollectableFinder>(World.MakeCollectableFinderKey(chunk.chunkX, chunk.chunkZ));
                await finder.SpawnPickup(location, new[] { new Slot { BlockId = (short)oldState.Id, ItemDamage = (short)oldState.MetaValue, ItemCount = 1 } }.AsImmutable());
            }
        }

        public Task<IUser> GetUser() => Task.FromResult(_user);

        public Task SetPosition(double x, double feetY, double z, bool onGround)
        {
            return SetPosition(new Vector3((float)x, (float)feetY, (float)z));
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
            await GetBroadcastGenerator().CollectItem(collectedEntityId, EntityId, item.ItemCount);
            return await _inventory.DistributeStack(this, item);
        }

        public Task PlaceBlock(Position location, EntityInteractHand hand, PlayerDiggingFace face, Vector3 cursorPosition)
        {
            return Task.CompletedTask;
        }

        public Task SetHeldItem(short slot)
        {
            return Task.CompletedTask;
        }

        public Task<Slot> GetInventorySlot(int index)
        {
            return Task.FromResult(_inventorySlots[index]);
        }

        public Task SetInventorySlot(int index, Slot slot)
        {
            _inventorySlots[index] = slot;
            return Task.CompletedTask;
        }
    }
}
