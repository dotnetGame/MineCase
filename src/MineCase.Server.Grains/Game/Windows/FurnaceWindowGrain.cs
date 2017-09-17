using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Game.Windows;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Windows.SlotAreas;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Windows
{
    internal class FurnaceWindowGrain : WindowGrain, IFurnaceWindow
    {
        protected override string WindowType => "minecraft:furnace";

        protected override Chat Title { get; } = new Chat("Furnace");

        private IFurnaceBlockEntity _furnaceEntity;
        private Dictionary<FurnaceWindowProperty, short> _properties = new Dictionary<FurnaceWindowProperty, short>();

        public Task SetEntity(IFurnaceBlockEntity furnaceEntity)
        {
            SlotAreas.Clear();
            _properties.Clear();

            SlotAreas.Add(new FurnaceSlotArea(furnaceEntity, this, GrainFactory));
            SlotAreas.Add(new InventorySlotArea(this, GrainFactory));
            SlotAreas.Add(new HotbarSlotArea(this, GrainFactory));
            _furnaceEntity = furnaceEntity;
            return Task.CompletedTask;
        }

        public Task SetProperty(FurnaceWindowProperty property, short value)
        {
            _properties[property] = value;
            return BroadcastWindowProperty(property, value);
        }

        public override async Task OpenWindow(IPlayer player)
        {
            await base.OpenWindow(player);
            foreach (var property in _properties)
                await NotifyWindowProperty(player, property.Key, property.Value);
        }
    }
}
