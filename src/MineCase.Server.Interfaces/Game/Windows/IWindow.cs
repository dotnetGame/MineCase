﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.Game.Entities;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Game.Windows
{
    public interface IWindow : IGrainWithGuidKey
    {
        Task<uint> GetSlotCount();

        Task<IReadOnlyList<Slot>> GetSlots(IPlayer player);

        Task<Slot> DistributeStack(IPlayer player, Slot item);

        Task Click(IPlayer player, int slotIndex, ClickAction clickAction, Slot clickedItem);

        Task Close(IPlayer player);
    }
}
