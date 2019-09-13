using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Status;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Network.Status
{
    [StatelessWorker]
    [Reentrant]
    internal class PingGrain : Grain, IPing
    {
        public async Task DispatchPacket(Guid sessionId, Ping packet)
        {
            await GrainFactory.GetGrain<IClientboundPacketSink>(sessionId).SendPacket(new Pong { Payload = packet.Payload });
            GrainFactory.GetGrain<IPacketRouter>(sessionId).Close().Ignore();
        }
    }
}
