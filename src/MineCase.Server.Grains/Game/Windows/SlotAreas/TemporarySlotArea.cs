using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.Game.Entities;
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

        public override async Task Close(IPlayer player)
        {
            var slots = GetSlots(player);
            var items = (from s in slots
                         where !s.IsEmpty
                         select s).ToArray();
            if (items.Length != 0)
            {
                var position = await player.GetPosition();
                var chunk = await player.GetChunkPosition();
                var world = GrainFactory.GetGrain<IWorld>(player.GetWorldAndEntityId().worldKey);

                // 产生 Pickup
                var finder = GrainFactory.GetGrain<ICollectableFinder>(world.MakeCollectableFinderKey(chunk.x, chunk.z));
                await finder.SpawnPickup(
                    new Position { X = (int)position.X, Y = (int)position.Y, Z = (int)position.Z },
                    items.AsImmutable());
                _tempSlotsMap.Remove(player);

                for (int i = 0; i < slots.Length; i++)
                    NotifySlotChanged(player, i, Slot.Empty);
            }
        }
    }
}
