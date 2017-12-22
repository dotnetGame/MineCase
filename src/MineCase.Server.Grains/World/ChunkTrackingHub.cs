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
using MineCase.Server.Persistence;
using MineCase.Server.Persistence.Components;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class ChunkTrackingHub : Grain, IChunkTrackingHub
    {
        private readonly IPacketPackager _packetPackager;
        private readonly Dictionary<IPlayer, IPacketSink> _trackingPlayers = new Dictionary<IPlayer, IPacketSink>();
        private BroadcastPacketSink _broadcastPacketSink;

        public ChunkTrackingHub(IPacketPackager packetPackager)
        {
            _packetPackager = packetPackager;
            _broadcastPacketSink = new BroadcastPacketSink(_trackingPlayers.Values, packetPackager);
        }

        public Task SendPacket(ISerializablePacket packet, IPlayer except)
        {
            IPacketSink exceptSink = null;
            if (except != null)
                _trackingPlayers.TryGetValue(except, out exceptSink);
            return _broadcastPacketSink.SendPacket(packet, exceptSink);
        }

        public Task SendPacket(uint packetId, Immutable<byte[]> data, IPlayer except)
        {
            IPacketSink exceptSink = null;
            if (except != null)
                _trackingPlayers.TryGetValue(except, out exceptSink);
            return _broadcastPacketSink.SendPacket(packetId, data, exceptSink);
        }

        public Task Subscribe(IPlayer player)
        {
            if (!_trackingPlayers.ContainsKey(player))
            {
                _trackingPlayers.Add(player, new ForwardToPlayerPacketSink(player, _packetPackager));
            }

            return Task.CompletedTask;
        }

        public Task Unsubscribe(IPlayer player)
        {
            _trackingPlayers.Remove(player);
            return Task.CompletedTask;
        }

        public Task<List<IPlayer>> GetTrackedPlayers()
        {
            return Task.FromResult(_trackingPlayers.Keys.ToList());
        }
    }
}
