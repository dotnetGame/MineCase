using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Network;
using Orleans;

namespace MineCase.Server.Game
{
    public interface IChunkSender : IGrainWithStringKey
    {
        Task PostChunk(int x, int z, IReadOnlyCollection<IClientboundPacketSink> clients);
    }
}
