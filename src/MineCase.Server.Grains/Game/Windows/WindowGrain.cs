using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
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

        public byte WindowId { get; private set; } = 0;

        protected IWorld World { get; private set; }

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

        public async Task SetWorld(IWorld world)
        {
            World = world;
            if (WindowId == 0 && !(this is InventoryWindowGrain))
                WindowId = await World.NewWindowId();
        }

        internal async void NotifySlotChanged(SlotArea slotArea, IPlayer player, int slotIndex, Slot item)
        {
            new ClientPlayPacketGenerator(await (await player.GetUser()).GetClientPacketSink())
                .SetSlot(WindowId, (short)LocalSlotIndexToGlobal(slotArea, slotIndex), item).Ignore();
        }

        private int LocalSlotIndexToGlobal(SlotArea slotArea, int slotIndex)
        {
            for (int i = 0; i < SlotAreas.Count; i++)
            {
                if (SlotAreas[i] == slotArea)
                    break;
                slotIndex += SlotAreas[i].SlotsCount;
            }

            return slotIndex;
        }

        public virtual Task<Slot> DistributeStack(IPlayer player, Slot item)
        {
            return DistributeStack(player, SlotAreas, item, false);
        }

        protected async Task<Slot> DistributeStack(IPlayer player, IReadOnlyList<SlotArea> slotAreas, Slot item, bool fillFromBack)
        {
            foreach (var slotArea in slotAreas)
            {
                item = await slotArea.DistributeStack(player, item, fillFromBack);
                if (item.IsEmpty) break;
            }

            return item;
        }
    }
}
