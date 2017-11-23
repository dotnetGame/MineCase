using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network;
using MineCase.Server.World;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.User
{
    public interface IUserChunkLoader : IGrainWithGuidKey
    {
        Task SetClientPacketSink(IClientboundPacketSink sink);

        Task JoinGame(IWorld world, IPlayer player);

        [OneWay]
        Task OnGameTick(long worldAge);

        [OneWay]
        Task OnChunkSent(ChunkWorldPos chunkPos);

        Task SetViewDistance(int viewDistance);
    }
}
