using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MineCase.Server.Game.Entities;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Game.Windows
{
    public interface IInventoryWindow : IWindow
    {
        Task<Slot> GetHotbarItem(IPlayer player, int slotIndex);

        Task UseHotbarItem(IPlayer player, int slotIndex);
    }
}
