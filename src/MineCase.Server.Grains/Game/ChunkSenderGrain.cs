using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Network;
using MineCase.Server.User;
using MineCase.Server.World;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game
{
    [Reentrant]
    internal class ChunkSenderGrain : Grain, IChunkSender
    {
        private Guid _jobWorkerId;

        public override Task OnActivateAsync()
        {
            _jobWorkerId = Guid.NewGuid();
            return base.OnActivateAsync();
        }

        public Task PostChunk(ChunkWorldPos chunkPos, IReadOnlyCollection<IClientboundPacketSink> clients, IReadOnlyCollection<IUserChunkLoader> loaders)
        {
            var stream = GetStreamProvider(StreamProviders.JobsProvider).GetStream<SendChunkJob>(_jobWorkerId, StreamProviders.Namespaces.ChunkSender);
            stream.OnNextAsync(new SendChunkJob
            {
                World = GrainFactory.GetGrain<IWorld>(this.GetPrimaryKeyString()),
                ChunkPosition = chunkPos,
                Clients = clients,
                Loaders = loaders
            }).Ignore();
            return Task.CompletedTask;
        }
    }
}
