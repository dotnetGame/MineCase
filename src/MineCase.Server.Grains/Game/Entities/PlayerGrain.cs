using MineCase.Server.Game.Windows;
using MineCase.Server.Network.Play;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.User;
using MineCase.Server.Network;

namespace MineCase.Server.Game.Entities
{
    class PlayerGrain : EntityGrain, IPlayer
    {
        private ClientPlayPacketGenerator _generator;

        private IInventoryWindow _inventory;

        public Task<IInventoryWindow> GetInventory() => Task.FromResult(_inventory);

        private uint _health;
        private const uint MaxHealth = 20;
        public const uint MaxFood = 20;
        private uint _currentExp, _levelMaxExp, _totalExp;
        private uint _level;

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

        public Task SetClientSink(IClientboundPacketSink sink)
        {
            _generator = new ClientPlayPacketGenerator(sink);
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
    }
}
