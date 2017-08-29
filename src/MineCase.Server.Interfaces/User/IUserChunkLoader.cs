using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network;
using MineCase.Server.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.User
{
    public interface IUserChunkLoader : IGrainWithGuidKey
    {
        Task SetClientPacketSink(IClientboundPacketSink sink);

        Task JoinGame(IWorld world, IPlayer player);

        Task OnGameTick();

        [OneWay]
        Task OnChunkSent(int chunkX, int chunkZ);

        Task SetViewDistance(int viewDistance);
    }
}
