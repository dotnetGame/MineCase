using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal abstract class TemporarySlotArea : SlotArea
    {
        private readonly Dictionary<IPlayer, Slot[]> _tempSlotsMap = new Dictionary<IPlayer, Slot[]>();

        public TemporarySlotArea(int slotsCount, WindowGrain window, IGrainFactory grainFactory)
            : base(slotsCount, window, grainFactory)
        {
        }

        public override Task<Slot> GetSlot(IPlayer player, int slotIndex)
        {
            return Task.FromResult(GetSlots(player)[slotIndex]);
        }

        public override Task SetSlot(IPlayer player, int slotIndex, Slot slot)
        {
            GetSlots(player)[slotIndex] = slot;
            return NotifySlotChanged(player, slotIndex, slot);
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

        public override async Task Close(IPlayer player)
        {
            var slots = GetSlots(player);
            var items = (from s in slots.Skip(1)
                         where !s.IsEmpty
                         select s).ToArray();
            if (items.Length != 0)
            {
                await player.Tell(new TossPickup { Slots = items });
                _tempSlotsMap.Remove(player);

                for (int i = 0; i < slots.Length; i++)
                    await NotifySlotChanged(player, i, Slot.Empty);
            }
        }
    }
}
