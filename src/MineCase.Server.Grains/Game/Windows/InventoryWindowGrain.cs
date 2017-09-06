using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Windows.SlotAreas;
using MineCase.Server.Network.Play;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Game.Windows
{
    internal class InventoryWindowGrain : WindowGrain, IInventoryWindow
    {
        protected override string WindowType => string.Empty;

        protected override Chat Title { get; } = new Chat("Inventory");

        public override Task OnActivateAsync()
        {
            SlotAreas.Add(new CraftingSlotArea(2, this, GrainFactory));
            SlotAreas.Add(new ArmorSlotArea(this, GrainFactory));
            SlotAreas.Add(new InventorySlotArea(this, GrainFactory));
            SlotAreas.Add(new HotbarSlotArea(this, GrainFactory));
            SlotAreas.Add(new OffhandSlotArea(this, GrainFactory));

            return base.OnActivateAsync();
        }

        public override Task<Slot> DistributeStack(IPlayer player, Slot item)
        {
            return DistributeStack(player, new[] { SlotAreas[3], SlotAreas[2] }, item, false);
        }

        public async Task UseHotbarItem(IPlayer player, int slotIndex)
        {
            await SlotAreas[3].TryUseItem(player, slotIndex);
        }

        public Task<Slot> GetHotbarItem(IPlayer player, int slotIndex)
        {
            return SlotAreas[3].GetSlot(player, slotIndex);
        }
    }
}
