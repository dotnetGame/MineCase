using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Server.Network;
using MineCase.Server.User;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    internal class ChunkTrackingHub : Grain, IChunkTrackingHub
    {
        private readonly IPacketPackager _packetPackager;
        private Dictionary<IUser, IClientboundPacketSink> _trackingUsers;
        private BroadcastPacketSink _broadcastPacketSink;

        public ChunkTrackingHub(IPacketPackager packetPackager)
        {
            _packetPackager = packetPackager;
        }

        public override Task OnActivateAsync()
        {
            _trackingUsers = new Dictionary<IUser, IClientboundPacketSink>();
            _broadcastPacketSink = new BroadcastPacketSink(_trackingUsers.Values, _packetPackager);
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

        public async Task Subscribe(IUser user)
        {
            if (!_trackingUsers.ContainsKey(user))
                _trackingUsers.Add(user, await user.GetClientPacketSink());
        }

        public Task Unsubscribe(IUser user)
        {
            _trackingUsers.Remove(user);
            return Task.CompletedTask;
        }
    }
}
