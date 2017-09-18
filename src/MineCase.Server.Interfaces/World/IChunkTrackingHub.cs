using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Network;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.World
{
    public interface IChunkTrackingHub : IGrainWithStringKey, IPacketSink
    {
        Task Subscribe(IUser user);

        Task Unsubscribe(IUser user);
    }
}
