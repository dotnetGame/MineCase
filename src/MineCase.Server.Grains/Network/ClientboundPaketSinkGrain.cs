using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Protocol.Handshaking;
using System.Threading.Tasks;
using System.IO;
using MineCase.Protocol;
using System.Reflection;

namespace MineCase.Server.Network
{
    class ClientboundPaketSinkGrain : Grain, IClientboundPaketSink
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

        //Also clients use this to unsubscribe themselves to no longer receive the messages.
        public Task UnSubscribe(IClientboundPacketObserver observer)
        {
            _subsManager.Unsubscribe(observer);
            return Task.CompletedTask;
        }

        public Task SendPacket(ISerializablePacket packet)
        {
            using (var stream = new MemoryStream())
            using (var bw = new BinaryWriter(stream))
            {
                packet.Serialize(bw);
                bw.Flush();
                return SendPacket(GetPacketId(packet), stream.ToArray());
            }
        }

        private uint GetPacketId(ISerializablePacket packet)
        {
            var typeInfo = packet.GetType().GetTypeInfo();
            var attr = typeInfo.GetCustomAttribute<PacketAttribute>();
            return attr.PacketId;
        }

        private Task SendPacket(uint packetId, byte[] data)
        {
            var packet = new UncompressedPacket
            {
                PacketId = packetId,
                Data = data
            };
            _subsManager.Notify(n => n.ReceivePacket(packet));
            return Task.CompletedTask;
        }
    }
}
