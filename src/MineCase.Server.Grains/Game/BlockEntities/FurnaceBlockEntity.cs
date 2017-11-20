using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm;
using MineCase.Game.Windows;
using MineCase.Server.Game.BlockEntities.Components;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.Game.Windows;
using MineCase.Server.Game.Windows.SlotAreas;
using MineCase.Server.World;
using Orleans.Concurrency;

namespace MineCase.Server.Game.BlockEntities
{
    [Reentrant]
    internal class FurnaceBlockEntity : BlockEntityGrain, IFurnaceBlockEntity
    {
        protected override async Task InitializeComponents()
        {
            await base.InitializeComponents();
            await SetComponent(new SlotContainerComponent(FurnaceSlotArea.FurnaceSlotsCount));
            await SetComponent(new FurnaceComponent());
        }
    }
}
