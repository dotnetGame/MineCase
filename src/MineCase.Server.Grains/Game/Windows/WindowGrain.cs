using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Windows.SlotAreas;
using MineCase.Server.Network.Play;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Game.Windows
{
    internal abstract class WindowGrain : Grain, IWindow
    {
        protected List<Slot> Slots { get; } = new List<Slot>();

        protected List<SlotArea> SlotAreas { get; } = new List<SlotArea>();

        protected IWorld World { get; private set; }

        protected abstract string WindowType { get; }

        protected abstract Chat Title { get; }

        protected virtual byte? EntityId => null;

        public Task<uint> GetSlotCount()
        {
            return Task.FromResult((uint)Slots.Count);
        }

        public async Task<IReadOnlyList<Slot>> GetSlots(IPlayer player)
        {
            var slots = new List<Slot>();
            foreach (var slotArea in SlotAreas)
            {
                for (int i = 0; i < slotArea.SlotsCount; i++)
                    slots.Add(await slotArea.GetSlot(player, i));
            }

            return slots;
        }

        internal async Task NotifySlotChanged(SlotArea slotArea, IPlayer player, int slotIndex, Slot item)
        {
            new ClientPlayPacketGenerator(await (await player.GetUser()).GetClientPacketSink())
                .SetSlot(await player.GetWindowId(this), (short)LocalSlotIndexToGlobal(slotArea, slotIndex), item).Ignore();
        }

        protected int LocalSlotIndexToGlobal(SlotArea slotArea, int slotIndex)
        {
            for (int i = 0; i < SlotAreas.Count; i++)
            {
                if (SlotAreas[i] == slotArea)
                    break;
                slotIndex += SlotAreas[i].SlotsCount;
            }

            return slotIndex;
        }

        protected (SlotArea slotArea, int slotIndex) GlobalSlotIndexToLocal(int slotIndex)
        {
            for (int i = 0; i < SlotAreas.Count; i++)
            {
                if (slotIndex < SlotAreas[i].SlotsCount)
                    return (SlotAreas[i], slotIndex);
                slotIndex -= SlotAreas[i].SlotsCount;
            }

            throw new ArgumentOutOfRangeException(nameof(slotIndex));
        }

        public virtual Task<Slot> DistributeStack(IPlayer player, Slot item)
        {
            return DistributeStack(player, SlotAreas, item, false);
        }

        protected async Task<Slot> DistributeStack(IPlayer player, IReadOnlyList<SlotArea> slotAreas, Slot item, bool fillFromBack)
        {
            // 先使用已有的 Slot，再使用空 Slot
            for (int pass = 0; pass < 2; pass++)
            {
                foreach (var slotArea in slotAreas)
                {
                    item = await slotArea.DistributeStack(player, item, pass == 1, fillFromBack);
                    if (item.IsEmpty) break;
                }
            }

            return item;
        }

        public async Task Click(IPlayer player, int slotIndex, ClickAction clickAction, Slot clickedItem)
        {
            switch (clickAction)
            {
                case ClickAction.LeftMouseClick:
                case ClickAction.RightMouseClick:
                    var slot = GlobalSlotIndexToLocal(slotIndex);
                    await slot.slotArea.Click(player, slot.slotIndex, clickAction, clickedItem);
                    break;
                default:
                    break;
            }
        }

        public async Task Close(IPlayer player)
        {
            foreach (var slotArea in SlotAreas)
                await slotArea.Close(player);
        }

        private byte GetNonInventorySlotsCount()
        {
            byte num = 0;
            foreach (var slotArea in SlotAreas)
            {
                if (slotArea is TemporarySlotArea || slotArea is InventorySlotAreaBase) continue;
                num += (byte)slotArea.SlotsCount;
            }

            return num;
        }

        public async Task OpenWindow(IPlayer player)
        {
            var slots = await GetSlots(player);
            var generator = new ClientPlayPacketGenerator(await (await player.GetUser()).GetClientPacketSink());

            var id = await player.GetWindowId(this);
            await generator.OpenWindow(id, WindowType, Title, GetNonInventorySlotsCount(), EntityId);
            await generator.WindowItems(id, slots);
        }

        public Task<Slot> GetSlot(IPlayer player, int slotIndex)
        {
            var area = GlobalSlotIndexToLocal(slotIndex);
            return area.slotArea.GetSlot(player, area.slotIndex);
        }

        public Task SetSlot(IPlayer player, int slotIndex, Slot item)
        {
            var area = GlobalSlotIndexToLocal(slotIndex);
            return area.slotArea.SetSlot(player, area.slotIndex, item);
        }
    }
}
