using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Gateway.Network
{
    public class ClientboundPacketSink
    {
        public ClientboundPacketSink(IPacketPackager packetPackager)
        {
            _packetPackager = packetPackager;
        }

        public override Task OnActivateAsync()
        {
            _subsManager = new Grains.GrainObserverManager<IClientboundPacketObserver>();
            _subsManager.ExpirationDuration = new TimeSpan(0, 0, 20);
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
            var prepared = await _packetPackager.PreparePacket(packet);
            await SendPacket(prepared.packetId, prepared.data.AsImmutable());
        }

        public Task SendPacket(uint packetId, Immutable<byte[]> data)
        {
            var packet = new UncompressedPacket
            {
                PacketId = packetId,
                Data = new ArraySegment<byte>(data.Value)
            };
            if (_subsManager.Count == 0)
                DeactivateOnIdle();
            else
                _subsManager.Notify(n => n.ReceivePacket(packet));
            return Task.CompletedTask;
        }

        public Task Close()
        {
            _subsManager.Notify(n => n.OnClosed());
            _subsManager.Clear();
            DeactivateOnIdle();
            return Task.CompletedTask;
        }

        public Task NotifyUseCompression(uint threshold)
        {
            _subsManager.Notify(n => n.UseCompression(threshold));
            return Task.CompletedTask;
        }
    }
}
