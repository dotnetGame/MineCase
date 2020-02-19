using System;
using System.IO;
using System.Threading.Tasks;
using MineCase.Protocol.Protocol;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Network
{
    internal class ClientboundPacketSinkGrain : Grain, IClientboundPacketSink
    {
        private GrainObserverManager<IClientboundPacketObserver> _subsManager;

        private volatile bool _useCompression = false;
        private uint _compressThreshold;

        private PacketInfo _packetInfo;
        private PacketEncoder _encoder;

        public override Task OnActivateAsync()
        {
            _packetInfo = new PacketInfo();
            _encoder = new PacketEncoder(PacketDirection.ClientBound, _packetInfo);

            _subsManager = new GrainObserverManager<IClientboundPacketObserver>();
            _subsManager.ExpirationDuration = new TimeSpan(0, 0, 60);
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

        public Task SendPacket(ISerializablePacket packet)
        {
            if (_subsManager.Count == 0)
            {
                DeactivateOnIdle();
            }
            else
            {
                RawPacket rawPacket = new RawPacket();
                using (MemoryStream ms = new MemoryStream())
                {
                    _encoder.Encode(packet, ms);
                    rawPacket.Length = (int)ms.Length;
                    rawPacket.RawData = ms.ToArray();
                }

                _subsManager.Notify(n => n.ReceivePacket(rawPacket));
            }

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
            // _subsManager.Notify(n => n.UseCompression(threshold));
            return Task.CompletedTask;
        }
    }
}