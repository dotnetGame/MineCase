using MineCase.Engine;
using MineCase.Server.Game.Windows;
using MineCase.Server.Network.Play;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entities.Components
{
    internal class InventoryComponent : Component<PlayerGrain>, IHandle<PlayerLoggedIn>
    {
        public InventoryComponent(string name = "inventory")
            : base(name)
        {
        }

        public IInventoryWindow GetInventoryWindow() =>
            GrainFactory.GetGrain<IInventoryWindow>(Guid.Empty);

        async Task IHandle<PlayerLoggedIn>.Handle(PlayerLoggedIn message)
        {
            var slots = await GetInventoryWindow().GetSlots(AttachedObject);
            await AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator()
                .WindowItems(0, slots);
        }
    }
}
