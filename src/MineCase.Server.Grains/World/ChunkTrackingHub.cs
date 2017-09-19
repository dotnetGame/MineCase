using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.User;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class ChunkTrackingHub : Grain, IChunkTrackingHub
    {
        private readonly IPacketPackager _packetPackager;
        private Dictionary<IPlayer, IPacketSink> _trackingPlayers;
        private BroadcastPacketSink _broadcastPacketSink;

        public ChunkTrackingHub(IPacketPackager packetPackager)
        {
            _packetPackager = packetPackager;
        }

        public override Task OnActivateAsync()
        {
            _trackingPlayers = new Dictionary<IPlayer, IPacketSink>();
            _broadcastPacketSink = new BroadcastPacketSink(_trackingPlayers.Values, _packetPackager);
            return base.OnActivateAsync();
        }

        public Task SendPacket(ISerializablePacket packet)
        {
            return _broadcastPacketSink.SendPacket(packet);
        }

        public Task SendPacket(uint packetId, Immutable<byte[]> data)
        {
            return _broadcastPacketSink.SendPacket(packetId, data);
        }

        public Task Subscribe(IPlayer player)
        {
            if (!_trackingPlayers.ContainsKey(player))
                _trackingPlayers.Add(player, new ForwardToPlayerPacketSink(player, _packetPackager));
            return Task.CompletedTask;
        }

        public Task Unsubscribe(IPlayer player)
        {
            _trackingPlayers.Remove(player);
            return Task.CompletedTask;
        }
    }
}
