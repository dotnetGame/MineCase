using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Game.Windows;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Game.Entities.Components
{
    internal class InventoryComponent : Component<PlayerGrain>, IHandle<AskInventoryWindow, IInventoryWindow>
    {
        public InventoryComponent(string name = "inventory")
            : base(name)
        {
        }

        public IInventoryWindow GetInventoryWindow() =>
            GrainFactory.GetGrain<IInventoryWindow>(Guid.Empty);

        Task<IInventoryWindow> IHandle<AskInventoryWindow, IInventoryWindow>.Handle(AskInventoryWindow message) =>
            Task.FromResult(GetInventoryWindow());
    }
}
