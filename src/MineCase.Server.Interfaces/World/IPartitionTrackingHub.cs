using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Entity;
using MineCase.Server.Network;
using MineCase.Server.User;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World
{
    public interface IPartitionTrackingHub : IAddressByPartition
    {
        Task Subscribe(IUser user);

        Task Unsubscribe(IUser user);

        Task<List<IUser>> GetTrackedUsers();
    }
}
