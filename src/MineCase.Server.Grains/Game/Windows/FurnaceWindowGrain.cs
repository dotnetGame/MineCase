using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Windows.SlotAreas;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Windows
{
    internal class FurnaceWindowGrain : WindowGrain, IFurnaceWindow
    {
        protected override string WindowType => "minecraft:furnace";

        protected override Chat Title { get; } = new Chat("Furnace");

        private IFurnaceBlockEntity _furnaceEntity;

        public async Task OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            if (worldAge % 100 == 0)
            {
                var properties = await _furnaceEntity.GetCookingState();
                await BroadcastWindowProperty(0, (short)properties.fuelLeft);
                await BroadcastWindowProperty(1, (short)properties.maxFuelTime);
                await BroadcastWindowProperty(2, (short)properties.cookProgress);
                await BroadcastWindowProperty(3, (short)properties.maxProgress);
            }
        }

        public Task SetEntity(IFurnaceBlockEntity furnaceEntity)
        {
            SlotAreas.Clear();

            SlotAreas.Add(new FurnaceSlotArea(furnaceEntity, this, GrainFactory));
            SlotAreas.Add(new InventorySlotArea(this, GrainFactory));
            SlotAreas.Add(new HotbarSlotArea(this, GrainFactory));
            _furnaceEntity = furnaceEntity;
            return Task.CompletedTask;
        }
    }
}
