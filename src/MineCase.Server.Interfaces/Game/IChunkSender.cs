using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Network;
using MineCase.Server.User;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Game
{
    public interface IChunkSender : IGrainWithStringKey
    {
        Task PostChunk(ChunkWorldPos chunkPos, IReadOnlyCollection<IClientboundPacketSink> clients, IReadOnlyCollection<IUserChunkLoader> loaders);
    }
}
