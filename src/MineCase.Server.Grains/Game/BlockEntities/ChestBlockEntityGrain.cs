using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities.Components;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.Game.Windows;
using MineCase.Server.Game.Windows.SlotAreas;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game.BlockEntities
{
    [Reentrant]
    internal class ChestBlockEntityGrain : BlockEntityGrain, IChestBlockEntity
    {
        protected override void InitializeComponents()
        {
            base.InitializeComponents();
            SetComponent(new SlotContainerComponent(ChestSlotArea.ChestSlotsCount));
            SetComponent(new ChestComponent());
        }
    }
}
