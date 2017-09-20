using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Windows.SlotAreas;
using Orleans.Concurrency;
using MineCase.Engine;

namespace MineCase.Server.Game.Windows
{
    internal class ChestWindowGrain : WindowGrain, IChestWindow
    {
        protected override string WindowType => "minecraft:chest";

        private Chat _title;

        protected override Chat Title => _title;

        public Task SetEntities(Immutable<IDependencyObject[]> entities)
        {
            SlotAreas.Clear();

            foreach (var entity in entities.Value)
                SlotAreas.Add(new ChestSlotArea(entity, this, GrainFactory));
            SlotAreas.Add(new InventorySlotArea(this, GrainFactory));
            SlotAreas.Add(new HotbarSlotArea(this, GrainFactory));

            if (entities.Value.Length < 2)
                _title = new Chat("Chest");
            else
                _title = new Chat("Large chest");
            return Task.CompletedTask;
        }
    }
}
