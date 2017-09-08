using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Windows.SlotAreas;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Windows
{
    internal class ChestWindowGrain : WindowGrain, IChestWindow
    {
        protected override string WindowType => "minecraft:chest";

        protected override Chat Title { get; } = new Chat("Chest");

        public Task SetEntities(Immutable<IChestBlockEntity[]> entities)
        {
            SlotAreas.Clear();

            foreach (var entity in entities.Value)
                SlotAreas.Add(new ChestSlotArea(entity, this, GrainFactory));
            return Task.CompletedTask;
        }
    }
}
