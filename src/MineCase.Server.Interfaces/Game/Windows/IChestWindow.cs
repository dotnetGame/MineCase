using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Game.BlockEntities;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Windows
{
    public interface IChestWindow : IWindow
    {
        Task SetEntities(Immutable<IDependencyObject[]> entities);
    }
}
