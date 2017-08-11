using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Windows
{
    internal abstract class WindowGrain : Grain, IWindow
    {
        private List<Slot> _slots = new List<Slot>();

        public Task<uint> GetSlotCount()
        {
            return Task.FromResult((uint)_slots.Count);
        }

        public Task<IReadOnlyList<Slot>> GetSlots()
        {
            return Task.FromResult<IReadOnlyList<Slot>>(_slots);
        }
    }
}
