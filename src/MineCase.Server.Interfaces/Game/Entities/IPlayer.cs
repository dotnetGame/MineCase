using MineCase.Server.Game.Windows;
using MineCase.Server.Network;
using MineCase.Server.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entities
{
    public interface IPlayer : IEntity
    {
        Task SetClientSink(IClientboundPacketSink sink);

        Task<IInventoryWindow> GetInventory();
        Task SendWholeInventory();
        Task SendExperience();
    }
}
