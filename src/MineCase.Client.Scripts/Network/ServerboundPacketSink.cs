using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;

namespace MineCase.Client.Network
{
    public interface IServerboundPacketSink
    {
        void Subscribe(IServerboundPacketObserver observer);

        void Unsubscribe(IServerboundPacketObserver observer);
    }

    internal class ServerboundPacketSink : IServerboundPacketSink, IPacketSink
    {
        private readonly IPacketPackager _packetPackager;
        private ImmutableList<IServerboundPacketObserver> _observers;

        public ServerboundPacketSink(IPacketPackager packetPackager)
        {
            _packetPackager = packetPackager;
            _observers = ImmutableList.Create<IServerboundPacketObserver>();
        }

        public async Task SendPacket(ISerializablePacket packet)
        {
            var prepared = _packetPackager.PreparePacket(packet);
            await SendPacket(prepared.packetId, prepared.data);
        }

        public Task SendPacket(uint packetId, byte[] data)
        {
            var packet = new UncompressedPacket
            {
                PacketId = packetId,
                Data = new ArraySegment<byte>(data)
            };
            _observers.ForEach(o => o.ReceivePacket(packet));
            return Task.CompletedTask;
        }

        public void Subscribe(IServerboundPacketObserver observer)
        {
            _observers = _observers.Add(observer);
        }

        public void Unsubscribe(IServerboundPacketObserver observer)
        {
            _observers = _observers.Remove(observer);
        }
    }
}
