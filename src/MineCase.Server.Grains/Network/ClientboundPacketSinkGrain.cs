using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Protocol.Handshaking;
using Orleans;

namespace MineCase.Server.Network
{
    internal class ClientboundPacketSinkGrain : Grain, IClientboundPacketSink
    {
        private ObserverSubscriptionManager<IClientboundPacketObserver> _subsManager;

        public override Task OnActivateAsync()
        {
            _subsManager = new ObserverSubscriptionManager<IClientboundPacketObserver>();
            return base.OnActivateAsync();
        }

        // Clients call this to subscribe.
        public Task Subscribe(IClientboundPacketObserver observer)
        {
            _subsManager.Subscribe(observer);
            return Task.CompletedTask;
        }

        // Also clients use this to unsubscribe themselves to no longer receive the messages.
        public Task UnSubscribe(IClientboundPacketObserver observer)
        {
            _subsManager.Unsubscribe(observer);
            return Task.CompletedTask;
        }

        public async Task SendPacket(ISerializablePacket packet)
        {
            var prepared = await PreparePacket(packet);
            await SendPacket(prepared.packetId, prepared.data);
        }

        private uint GetPacketId(ISerializablePacket packet)
        {
            var typeInfo = packet.GetType().GetTypeInfo();
            var attr = typeInfo.GetCustomAttribute<PacketAttribute>();
            return attr.PacketId;
        }

        public Task SendPacket(uint packetId, byte[] data)
        {
            var packet = new UncompressedPacket
            {
                PacketId = packetId,
                Data = data
            };
            _subsManager.Notify(n => n.ReceivePacket(packet));
            return Task.CompletedTask;
        }

        public Task Close()
        {
            _subsManager.Notify(n => n.OnClosed());
            DeactivateOnIdle();
            return Task.CompletedTask;
        }

        public Task<(uint packetId, byte[] data)> PreparePacket(ISerializablePacket packet)
        {
            using (var stream = new MemoryStream())
            using (var bw = new BinaryWriter(stream))
            {
                packet.Serialize(bw);
                bw.Flush();
                return Task.FromResult((GetPacketId(packet), stream.ToArray()));
            }
        }
    }
}