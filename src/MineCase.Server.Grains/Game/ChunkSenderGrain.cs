using MineCase.Server.Network;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Protocol;
using System.Threading.Tasks;
using System.Linq;
using Orleans.Streams;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Game
{
    public sealed class SendChunkJob
    {
       public IReadOnlyCollection<IClientboundPacketSink> Clients { get; set; }
    }

    [ImplicitStreamSubscription(StreamProviders.Namespaces.ChunkSender)]
    class ChunkSenderGrain : Grain, IChunkSender
    {
        public override async Task OnActivateAsync()
        {
            var stream = GetStreamProvider(StreamProviders.JobsProvider).GetStream<SendChunkJob>(this.GetPrimaryKey(), StreamProviders.Namespaces.ChunkSender);
            await stream.SubscribeAsync(OnNextAsync);
        }

        private async Task OnNextAsync(SendChunkJob job, StreamSequenceToken token)
        {
            var generator = new ClientPlayPacketGenerator(new BroadcastPacketSink(job.Clients));
            await generator.ChunkData();
        }

        class BroadcastPacketSink : IPacketSink
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
