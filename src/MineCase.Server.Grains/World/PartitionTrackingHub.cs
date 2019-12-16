using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Server.Game;
using MineCase.Server.Game.Entity;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.User;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class PartitionTrackingHub : Grain, IPartitionTrackingHub
    {
        private readonly HashSet<IUser> _trackingUsers = new HashSet<IUser>();

        public PartitionTrackingHub()
        {
        }

        public Task Subscribe(IUser user)
        {
            if (!_trackingUsers.Contains(user))
            {
                _trackingUsers.Add(user);
            }

            return Task.CompletedTask;
        }

        public Task Unsubscribe(IUser user)
        {
            _trackingUsers.Remove(user);
            return Task.CompletedTask;
        }

        public Task<List<IUser>> GetTrackedUsers()
        {
            return Task.FromResult(_trackingUsers.ToList());
        }
    }
}
