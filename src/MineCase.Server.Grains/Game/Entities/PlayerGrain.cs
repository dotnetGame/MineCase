using MineCase.Server.Game.Windows;
using MineCase.Server.Network.Play;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.User;
using MineCase.Server.Network;
using System.Linq;
using Orleans;
using System.Numerics;

namespace MineCase.Server.Game.Entities
{
    class PlayerGrain : EntityGrain, IPlayer
    {
        private IUser _user;
        private ClientPlayPacketGenerator _generator;
        private uint _ping;

        private string _name;
        private IInventoryWindow _inventory;

        public Task<IInventoryWindow> GetInventory() => Task.FromResult(_inventory);

        private uint _health;
        private const uint MaxHealth = 20;
        public const uint MaxFood = 20;
        private uint _currentExp, _levelMaxExp, _totalExp;
        private uint _level;
        private uint _teleportId;
        private bool _teleportConfirmed;

        private Vector3 _position;
        private float _pitch, _yaw;

        public override Task OnActivateAsync()
        {
            _inventory = GrainFactory.GetGrain<IInventoryWindow>(Guid.NewGuid());
            _levelMaxExp = 7;
            return base.OnActivateAsync();
        }

        public async Task SendWholeInventory()
        {
            var slots = await _inventory.GetSlots();
            await _generator.WindowItems(0, slots);
        }

        public Task BindToUser(IUser user, IClientboundPacketSink sink)
        {
            _generator = new ClientPlayPacketGenerator(sink);
            _user = user;
            _health = MaxHealth;
            return Task.CompletedTask;
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

        public Task<PlayerDescription> GetDescription()
        {
            return Task.FromResult(new PlayerDescription
            {
                UUID = _user.GetPrimaryKey(),
                Name = _name,
                GameMode = new GameMode { ModeClass = GameMode.Class.Survival },
                Ping = _ping
            });
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
            _teleportConfirmed = false;
        }

        public Task SetPing(uint ping)
        {
            _pitch = ping;
            return Task.CompletedTask;
        }

        public Task OnTeleportConfirm(uint teleportId)
        {
            _teleportConfirmed = true;
            return Task.CompletedTask;
        }
    }
}
