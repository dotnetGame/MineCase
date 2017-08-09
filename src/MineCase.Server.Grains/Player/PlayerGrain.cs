using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Game;
using MineCase.Server.World;
using System.Threading.Tasks;
using MineCase.Server.Network;
using System.Numerics;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Player
{
    class PlayerGrain : Grain, IPlayer
    {
        private string _worldId;
        private uint _eid;
        private IClientboundPacketSink _sink;
        private ClientPlayPacketGenerator _generator;
        private IDisposable _sendKeepAliveTimer;
        public HashSet<uint> _keepAliveWaiters;

        private readonly Random _keepAliveIdRand = new Random();
        private const int ClientKeepInterval = 6;
        private bool _isOnline = false;

        public Task<IClientboundPacketSink> GetClientPacketSink()
        {
            return Task.FromResult(_sink);
        }

        public async Task<IGameSession> GetGameSession()
        {
            var world = await GetWorld();
            return GrainFactory.GetGrain<IGameSession>(world.GetPrimaryKeyString());
        }

        public async Task<IWorld> GetWorld()
        {
            if (string.IsNullOrEmpty(_worldId))
            {
                var world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetDefaultWorld();
                _worldId = world.GetPrimaryKeyString();
                return world;
            }
            return await GrainFactory.GetGrain<IWorldAccessor>(0).GetWorld(_worldId);
        }

        public Task SetClientPacketSink(IClientboundPacketSink sink)
        {
            _sink = sink;
            _generator = new ClientPlayPacketGenerator(sink);

            _isOnline = true;
            _keepAliveWaiters = new HashSet<uint>();
            _sendKeepAliveTimer = RegisterTimer(OnSendKeepAliveRequests, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task SetEntityId(uint eid)
        {
            _eid = eid;
            return Task.CompletedTask;
        }

        public Task UseEntity(uint targetEid, EntityUsage type, Vector3? targetPosition, EntityInteractHand? hand)
        {
            return Task.CompletedTask;
        }

        private async Task OnSendKeepAliveRequests(object state)
        {
            if (_isOnline && _keepAliveWaiters.Count >= ClientKeepInterval)
            {
                _isOnline = false;
                _sendKeepAliveTimer.Dispose();
                _sendKeepAliveTimer = null;

                KickPlayer().Ignore();
            }
            else
            {
                var id = (uint)_keepAliveIdRand.Next();
                _keepAliveWaiters.Add(id);
                await _generator.KeepAlive(id);
            }
        }

        public Task KeepAlive(uint keepAliveId)
        {
            _keepAliveWaiters.Remove(keepAliveId);
            return Task.CompletedTask;
        }

        private async Task KickPlayer()
        {
            var game = await GetGameSession();
            await game.LeaveGame(this);
            await _sink.Close();
            DeactivateOnIdle();
        }
    }
}
