using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.World;
using Orleans;
using Orleans.Streams;

namespace MineCase.Server.Game
{
    public sealed class SendChunkJob
    {
        public IWorld World { get; set; }

        public int ChunkX { get; set; }

        public int ChunkZ { get; set; }

        public IReadOnlyCollection<IClientboundPacketSink> Clients { get; set; }
    }

    internal interface IChunkSenderJobWorker : IGrainWithGuidKey
    {
    }

    [ImplicitStreamSubscription(StreamProviders.Namespaces.ChunkSender)]
    internal class ChunkSenderJobWorker : Grain, IChunkSenderJobWorker
    {
        public override async Task OnActivateAsync()
        {
            var stream = GetStreamProvider(StreamProviders.JobsProvider).GetStream<SendChunkJob>(this.GetPrimaryKey(), StreamProviders.Namespaces.ChunkSender);
            await stream.SubscribeAsync(OnNextAsync);
        }

        private async Task OnNextAsync(SendChunkJob job, StreamSequenceToken token)
        {
            var chunkColumn = GrainFactory.GetGrain<IChunkColumn>(job.World.MakeChunkColumnKey(job.ChunkX, job.ChunkZ));

            var generator = new ClientPlayPacketGenerator(new BroadcastPacketSink(job.Clients));
            await generator.ChunkData(Dimension.Overworld, job.ChunkX, job.ChunkZ, await chunkColumn.GetState());
        }

        private class BroadcastPacketSink : IPacketSink
        {
            private IReadOnlyCollection<IPacketSink> _sinks;

            public BroadcastPacketSink(IReadOnlyCollection<IPacketSink> sinks)
            {
                _sinks = sinks ?? Array.Empty<IPacketSink>();
            }

            public Task<(uint packetId, byte[] data)> PreparePacket(ISerializablePacket packet)
            {
                if (_sinks.Any())
                    return _sinks.First().PreparePacket(packet);
                throw new InvalidOperationException("No sinks avaliable.");
            }

            public async Task SendPacket(ISerializablePacket packet)
            {
                if (_sinks.Any())
                {
                    var preparedPacket = await PreparePacket(packet);
                    await SendPacket(preparedPacket.packetId, preparedPacket.data);
                }
            }

            public Task SendPacket(uint packetId, byte[] data)
            {
                return Task.WhenAll(from sink in _sinks
                                    select sink.SendPacket(packetId, data));
            }
        }
    }
}
