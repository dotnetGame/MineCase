using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Game.Windows
{
    public interface IInventoryWindow : IWindow
    {
        Task SetUser(IUser user);

        Task AddItem(Slot item);
    }
}
