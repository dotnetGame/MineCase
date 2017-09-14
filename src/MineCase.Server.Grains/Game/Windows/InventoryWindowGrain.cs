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

        public Task UseItem(IPlayer player, int slotIndex)
        {
            var slotArea = GlobalSlotIndexToLocal(slotIndex);
            return slotArea.slotArea.TryUseItem(player, slotArea.slotIndex);
        }

        public Task<Slot> GetHotbarItem(IPlayer player, int slotIndex)
        {
            return SlotAreas[3].GetSlot(player, slotIndex);
        }

        public Task<int> GetHotbarGlobalIndex(IPlayer player, int slotIndex)
        {
            return Task.FromResult(LocalSlotIndexToGlobal(SlotAreas[3], slotIndex));
        }

        public override async Task Click(IPlayer player, int slotIndex, ClickAction clickAction, Slot clickedItem)
        {
            switch (clickAction)
            {
                case ClickAction.NumberKey1:
                case ClickAction.NumberKey2:
                case ClickAction.NumberKey3:
                case ClickAction.NumberKey4:
                case ClickAction.NumberKey5:
                case ClickAction.NumberKey6:
                case ClickAction.NumberKey7:
                case ClickAction.NumberKey8:
                case ClickAction.NumberKey9:
                    await player.SetHeldItem((short)(clickAction - ClickAction.NumberKey1));
                    break;
                default:
                    await base.Click(player, slotIndex, clickAction, clickedItem);
                    break;
            }
        }
    }
}
