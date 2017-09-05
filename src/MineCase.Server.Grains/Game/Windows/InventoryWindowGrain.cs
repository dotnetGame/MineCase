using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Windows.SlotAreas;
using MineCase.Server.Network.Play;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Game.Windows
{
    internal class InventoryWindowGrain : WindowGrain, IInventoryWindow
    {
        private IUser _user;
        private ClientPlayPacketGenerator _generator;

        public override Task OnActivateAsync()
        {
            SlotAreas.Add(new CraftingSlotArea(2, this, GrainFactory));
            SlotAreas.Add(new ArmorSlotArea(this, GrainFactory));
            SlotAreas.Add(new InventorySlotArea(this, GrainFactory));
            SlotAreas.Add(new HotbarSlotArea(this, GrainFactory));
            SlotAreas.Add(new OffhandSlotArea(this, GrainFactory));

            return base.OnActivateAsync();
        }

        public async Task<bool> AddItem(Slot item)
        {
            int index = -1;
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].BlockId == item.BlockId)
                {
                    index = i;
                    Slots[i] = new Slot
                    {
                        BlockId = item.BlockId,
                        ItemCount = (byte)(Slots[i].ItemCount + item.ItemCount)
                    };
                    break;
                }
            }

            if (index == -1)
            {
                Slots.Add(item);
                index = Slots.Count - 1;
            }

            await _generator.SetSlot(0, (short)(index + 36), Slots[index]);
            return true;
        }

        public async Task SetUser(IUser user)
        {
            _user = user;
            _generator = new ClientPlayPacketGenerator(await user.GetClientPacketSink());
        }

        public override Task<Slot> DistributeStack(IPlayer player, Slot item)
        {
            return DistributeStack(player, new[] { SlotAreas[3], SlotAreas[2] }, item, false);
        }
    }
}
