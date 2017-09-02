using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.Game.Entities;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal abstract class TemporarySlotArea : SlotArea
    {
        private readonly Dictionary<IPlayer, Slot[]> _tempSlotsMap = new Dictionary<IPlayer, Slot[]>();

        public TemporarySlotArea(int slotsCount, WindowGrain window)
            : base(slotsCount, window)
        {
        }

        public override Task<Slot> GetSlot(IPlayer player, int slotIndex)
        {
            return Task.FromResult(GetSlots(player)[slotIndex]);
        }

        public override Task SetSlot(IPlayer player, int slotIndex, Slot slot)
        {
            GetSlots(player)[slotIndex] = slot;
            NotifySlotChanged(player, slotIndex, slot);
            return Task.CompletedTask;
        }

        protected Slot[] GetSlots(IPlayer player)
        {
            if (!_tempSlotsMap.TryGetValue(player, out var slots))
            {
                slots = Enumerable.Repeat(Slot.Empty, SlotsCount).ToArray();
                _tempSlotsMap.Add(player, slots);
            }

            return slots;
        }
    }
}
