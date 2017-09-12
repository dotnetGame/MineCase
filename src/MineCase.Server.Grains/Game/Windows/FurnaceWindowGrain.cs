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

        public Task OnGameTick(TimeSpan deltaTime)
        {
            return Task.CompletedTask;
        }

        public Task SetEntity(IFurnaceBlockEntity furnaceEntity)
        {
            SlotAreas.Clear();

            SlotAreas.Add(new FurnaceSlotArea(furnaceEntity, this, GrainFactory));
            SlotAreas.Add(new InventorySlotArea(this, GrainFactory));
            SlotAreas.Add(new HotbarSlotArea(this, GrainFactory));
            return Task.CompletedTask;
        }
    }
}
