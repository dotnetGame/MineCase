using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network;
using MineCase.Server.User;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World
{
    public interface IChunkTrackingHub : IAddressByPartition, IPacketSink
    {
        Task Subscribe(IPlayer player);

        Task Unsubscribe(IPlayer player);

        Task<List<IPlayer>> GetTrackedPlayers();
    }
}
