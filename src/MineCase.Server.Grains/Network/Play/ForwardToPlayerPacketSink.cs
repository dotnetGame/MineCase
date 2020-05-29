using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Server.Game.Entities;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Network.Play
{
    internal class ForwardToPlayerPacketSink : IPacketSink
    {
        private readonly IPlayer _player;
        private readonly IPacketPackager _packetPackager;

        public ForwardToPlayerPacketSink(IPlayer player, IPacketPackager packetPackager)
        {
            _player = player;
            _packetPackager = packetPackager;
        }

        public async Task SendPacket(ISerializablePacket packet)
        {
            var package = await _packetPackager.PreparePacket(packet);
            await SendPacket(package.PacketId, package.Data.AsImmutable());
        }

        public Task SendPacket(uint packetId, Immutable<byte[]> data)
        {
            _player.InvokeOneWay(e => e.Tell(new PacketForwardToPlayer
            {
                PacketId = packetId,
                Data = data.Value
            }));
            return Task.CompletedTask;
        }
    }
}
