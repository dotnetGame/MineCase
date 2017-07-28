using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Protocol.Status;
using System.Threading.Tasks;

namespace MineCase.Server.Network.Status
{
    [StatelessWorker]
    [Reentrant]
    class PingGrain : Grain, IPing
    {
        public async Task DispatchPacket(Guid sessionId, Ping packet)
        {
            await GrainFactory.GetGrain<IClientboundPaketSink>(sessionId).SendPacket(new Pong { Payload = packet.Payload });
            GrainFactory.GetGrain<IPacketRouter>(sessionId).Close().Ignore();
        }
    }
}
