﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using Orleans;

namespace MineCase.Server.Game.Windows
{
    internal abstract class WindowGrain : Grain, IWindow
    {
        protected List<Slot> Slots { get; } = new List<Slot>();

        public Task<uint> GetSlotCount()
        {
            return Task.FromResult((uint)Slots.Count);
        }

        public Task<IReadOnlyList<Slot>> GetSlots()
        {
            return Task.FromResult<IReadOnlyList<Slot>>(Slots);
        }
    }
}
