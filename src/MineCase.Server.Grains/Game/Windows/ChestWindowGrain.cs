using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Windows
{
    internal class ChestWindowGrain : WindowGrain, IChestWindow
    {
        protected override string WindowType => "minecraft:chest";

        protected override Chat Title { get; } = new Chat("Chest");
    }
}
