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
using MineCase.World;
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
        private Slot _draggedSlot;
        private short _heldSlot;
        private IInventoryWindow _inventory;
        private Dictionary<byte, WindowContext> _windows;

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

        private bool _isOnGround;

        public override Task OnActivateAsync()
        {
            _inventory = GrainFactory.GetGrain<IInventoryWindow>(Guid.NewGuid());
            _currentExp = 0;
            _totalExp = 0;
            _level = 0;
            _teleportId = 0;
            _levelMaxExp = 7;
            _isOnGround = true;

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
            _windows = new Dictionary<byte, WindowContext>
            {
                { 0, new WindowContext { Window = _inventory } }
            };
            _draggedSlot = Slot.Empty;
            _heldSlot = 0;
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

                // 产生 Pickup
                var finder = GrainFactory.GetGrain<IEntityFinder>(World.MakeEntityFinderKey(chunk.chunkX, chunk.chunkZ));
                await finder.SpawnPickup(location, new[] { new Slot { BlockId = (short)oldState.Id, ItemCount = 1 } }.AsImmutable());
            }
        }

        public Task<IUser> GetUser() => Task.FromResult(_user);

        public async Task SetPosition(double x, double feetY, double z, bool onGround)
        {
            await SetPosition(new Vector3((float)x, (float)feetY, (float)z));
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
            var collectables = await GrainFactory.GetGrain<IEntityFinder>(World.MakeEntityFinderKey(chunkPos.x, chunkPos.z)).CollisionCollectable(this);
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

        public async Task PlaceBlock(Position location, EntityInteractHand hand, PlayerDiggingFace face, Vector3 cursorPosition)
        {
            if (face != PlayerDiggingFace.Special)
            {
                var blockState = await World.GetBlockState(GrainFactory, location);
                var blockHandler = BlockHandler.Create((BlockId)blockState.Id);
                if (blockHandler.IsUsable)
                {
                    await blockHandler.UseBy(this, GrainFactory, World, location, cursorPosition);
                }
                else
                {
                    var slotIndex = await _inventory.GetHotbarGlobalIndex(this, _heldSlot);
                    var slot = await _inventory.GetSlot(this, slotIndex);
                    if (!slot.IsEmpty)
                    {
                        var itemHandler = ItemHandler.Create((uint)slot.BlockId);
                        if (itemHandler.IsPlaceable)
                            await itemHandler.PlaceBy(this, GrainFactory, World, location, _inventory, slotIndex, face, cursorPosition);
                    }
                }
            }
        }

        public Task SetHeldItem(short slot)
        {
            _heldSlot = slot;
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

        public Task<byte> GetWindowId(IWindow window)
        {
            return Task.FromResult(_windows.First(o => o.Value.Window.GetPrimaryKey() == window.GetPrimaryKey()).Key);
        }

        private WindowContext GetWindow(byte windowId)
        {
            return _windows[windowId];
        }

        public async Task SetDraggedSlot(Slot item)
        {
            _draggedSlot = item;
            await _generator.SetSlot(0xFF, -1, item);
        }

        public Task<Slot> GetDraggedSlot() => Task.FromResult(_draggedSlot);

        public async Task ClickWindow(byte windowId, short slot, ClickAction clickAction, short actionNumber, Slot clickedItem)
        {
            var window = GetWindow(windowId);
            await window.Window.Click(this, slot, clickAction, clickedItem);
            await _generator.ConfirmTransaction(windowId, window.ActionNumber++, true);
        }

        public async Task CloseWindow(byte windowId)
        {
            await GetWindow(windowId).Window.Close(this);
            if (windowId != 0)
                _windows.Remove(windowId);
        }

        public Task OpenWindow(IWindow window)
        {
            var id = (from w in _windows
                      where w.Value.Window.GetPrimaryKey() == window.GetPrimaryKey()
                      select (byte?)w.Key).FirstOrDefault();
            if (id == null)
            {
                for (byte i = 1; i <= byte.MaxValue; i++)
                {
                    if (!_windows.ContainsKey(i))
                    {
                        id = i;
                        _windows.Add(i, new WindowContext { Window = window });
                        break;
                    }
                }
            }

            if (id != null)
                window.OpenWindow(this).Ignore();
            return Task.CompletedTask;
        }

        public Task SetOnGround(bool state)
        {
            _isOnGround = state;
            return Task.CompletedTask;
        }

        public Task<(float pitch, float yaw)> GetLook()
        {
            return Task.FromResult((_pitch, _yaw));
        }

        private class WindowContext
        {
            public IWindow Window;
            public short ActionNumber;
        }
    }
}
