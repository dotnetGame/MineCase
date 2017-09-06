using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Windows.SlotAreas;

namespace MineCase.Server.Game.Windows
{
    internal class CraftingWindowGrain : WindowGrain, ICraftingWindow
    {
        protected override string WindowType => "minecraft:crafting_table";

        protected override Chat Title { get; } = new Chat("Crafting Table");

        public override Task OnActivateAsync()
        {
            SlotAreas.Add(new CraftingSlotArea(3, this, GrainFactory));
            SlotAreas.Add(new InventorySlotArea(this, GrainFactory));
            SlotAreas.Add(new HotbarSlotArea(this, GrainFactory));

            return base.OnActivateAsync();
        }
    }
}
