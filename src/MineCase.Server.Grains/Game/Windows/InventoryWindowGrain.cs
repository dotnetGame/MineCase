using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.Network.Play;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Game.Windows
{
    internal class InventoryWindowGrain : WindowGrain, IInventoryWindow
    {
        private IUser _user;
        private ClientPlayPacketGenerator _generator;

        public async Task<bool> AddItem(Slot item)
        {
            int index = -1;
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].BlockId == item.BlockId)
                {
                    index = i;
                    Slots[i].ItemCount += item.ItemCount;
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
    }
}
