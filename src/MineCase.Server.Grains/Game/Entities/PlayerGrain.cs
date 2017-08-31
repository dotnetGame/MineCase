using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.Game.Windows;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Game.Entities
{
    internal class PlayerGrain : EntityGrain, IPlayer
    {
        private IUser _user;
        private ClientPlayPacketGenerator _generator;

        private string _name;
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

        private Vector3 _position;
        private float _pitch;
        private float _yaw;

        public override Task OnActivateAsync()
        {
            _inventory = GrainFactory.GetGrain<IInventoryWindow>(Guid.NewGuid());
            _currentExp = 0;
            _totalExp = 0;
            _level = 0;
            _teleportId = 0;
            _levelMaxExp = 7;
            _position = new Vector3(0, 2, 0);
            _pitch = 0;
            _yaw = 0;
            return base.OnActivateAsync();
        }

        public async Task SendWholeInventory()
        {
            var slots = await _inventory.GetSlots();
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
            await SendWholeInventory();
            await SendExperience();
            await SendPlayerListAddPlayer(new[] { this });
        }

        public async Task SendPositionAndLook()
        {
            await _generator.PositionAndLook(_position.X, _position.Y, _position.Z, _yaw, _pitch, 0, _teleportId);
        }

        public Task OnTeleportConfirm(uint teleportId)
        {
            return Task.CompletedTask;
        }

        public Task<(int x, int y, int z)> GetChunkPosition()
        {
            return Task.FromResult(((int)(_position.X / 16), (int)(_position.Y / 16), (int)(_position.Z / 16)));
        }

        public Task SetPosition(double x, double feetY, double z, bool onGround)
        {
            _position = new Vector3((float)x, (float)feetY, (float)z);
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

        public async Task SendClientAnimation(uint entityID, ClientboundAnimationID animationID)
        {
            await _generator.SendClientAnimation(entityID, animationID);
        }

        public Task StartDigging(Position location, PlayerDiggingFace face)
        {
            return Task.CompletedTask;
        }

        public Task CancelDigging(Position location, PlayerDiggingFace face)
        {
            return Task.CompletedTask;
        }

        public Task FinishDigging(Position location, PlayerDiggingFace face)
        {
            return Task.CompletedTask;
        }
    }
}
