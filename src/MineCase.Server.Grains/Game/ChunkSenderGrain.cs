using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Network;
using MineCase.Server.User;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Game
{
    internal class ChunkSenderGrain : Grain, IChunkSender
    {
        private Guid _jobWorkerId;

        public override Task OnActivateAsync()
        {
            _jobWorkerId = Guid.NewGuid();
            return base.OnActivateAsync();
        }

        public Task PostChunk(int x, int z, IReadOnlyCollection<IClientboundPacketSink> clients, IReadOnlyCollection<IUser> users)
        {
            var stream = GetStreamProvider(StreamProviders.JobsProvider).GetStream<SendChunkJob>(_jobWorkerId, StreamProviders.Namespaces.ChunkSender);
            stream.OnNextAsync(new SendChunkJob
            {
                World = GrainFactory.GetGrain<IWorld>(this.GetPrimaryKeyString()),
                ChunkX = x,
                ChunkZ = z,
                Clients = clients,
                Users = users
            }).Ignore();
            return Task.CompletedTask;
        }
    }
}
