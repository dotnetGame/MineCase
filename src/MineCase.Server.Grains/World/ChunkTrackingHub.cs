using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Server.Game;
using MineCase.Server.Network;
using MineCase.Server.User;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class ChunkTrackingHub : Grain, IChunkTrackingHub
    {
        private readonly IPacketPackager _packetPackager;
        private Dictionary<IUser, IClientboundPacketSink> _trackingUsers;
        private BroadcastPacketSink _broadcastPacketSink;
        private HashSet<ITickable> _tickables;
        private IGameSession _gameSession;

        public ChunkTrackingHub(IPacketPackager packetPackager)
        {
            _packetPackager = packetPackager;
        }

        public override Task OnActivateAsync()
        {
            _trackingUsers = new Dictionary<IUser, IClientboundPacketSink>();
            _tickables = new HashSet<ITickable>();
            _broadcastPacketSink = new BroadcastPacketSink(_trackingUsers.Values, _packetPackager);
            _gameSession = GrainFactory.GetGrain<IGameSession>(this.GetWorldAndChunkPosition().worldKey);
            return base.OnActivateAsync();
        }

        public Task OnGameTick(TimeSpan deltaTime)
        {
            if (_tickables.Count != 0)
            {
                return Task.WhenAll(from t in _tickables
                                    select t.OnGameTick(deltaTime));
            }

            return Task.CompletedTask;
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
            await _gameSession.Subscribe(this);
        }

        public Task Subscribe(ITickable tickableEntity)
        {
            _tickables.Add(tickableEntity);
            return Task.CompletedTask;
        }

        public async Task Unsubscribe(IUser user)
        {
            _trackingUsers.Remove(user);
            if (_tickables.Count == 0)
                await _gameSession.Unsubscribe(this);
        }

        public Task Unsubscribe(ITickable tickableEntity)
        {
            _tickables.Remove(tickableEntity);
            return Task.CompletedTask;
        }
    }
}
