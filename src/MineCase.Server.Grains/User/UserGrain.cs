using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.User
{
    [Reentrant]
    internal class UserGrain : Grain, IUser
    {
        private string _name;
        private uint _protocolVersion;
        private string _worldId;
        private IWorld _world;
        private IClientboundPacketSink _sink;
        private IPacketRouter _packetRouter;
        private ClientPlayPacketGenerator _generator;
        private IUserChunkLoader _chunkLoader;
        private IDisposable _sendKeepAliveTimer;
        private IDisposable _worldTimeSyncTimer;
        public HashSet<uint> _keepAliveWaiters;

        private readonly Random _keepAliveIdRand = new Random();
        private const int ClientKeepInterval = 6;
        private bool _isOnline = false;
        private DateTime _keepAliveRequestTime;
        private DateTime _keepAliveResponseTime;
        private UserState _state;

        private IPlayer _player;

        public override async Task OnActivateAsync()
        {
            if (string.IsNullOrEmpty(_worldId))
            {
                var world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetDefaultWorld();
                _worldId = world.GetPrimaryKeyString();
                _world = world;
            }

            _world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetWorld(_worldId);
            _chunkLoader = GrainFactory.GetGrain<IUserChunkLoader>(this.GetPrimaryKey());
        }

        public Task<IClientboundPacketSink> GetClientPacketSink()
        {
            return Task.FromResult(_sink);
        }

        public async Task<IGameSession> GetGameSession()
        {
            var world = await GetWorld();
            return GrainFactory.GetGrain<IGameSession>(world.GetPrimaryKeyString());
        }

        public Task<IWorld> GetWorld() => Task.FromResult(_world);

        public Task SetClientPacketSink(IClientboundPacketSink sink)
        {
            _sink = sink;
            _generator = new ClientPlayPacketGenerator(sink);
            return _chunkLoader.SetClientPacketSink(sink);
        }

        public async Task JoinGame()
        {
            var playerEid = await _world.NewEntityId();
            _player = GrainFactory.GetGrain<IPlayer>(_world.MakeEntityKey(playerEid));
            await _player.SetName(_name);
            await _player.BindToUser(this);
            await _world.AttachEntity(_player);

            await _chunkLoader.JoinGame(_world, _player);
            _state = UserState.JoinedGame;
            _keepAliveWaiters = new HashSet<uint>();
            _sendKeepAliveTimer = RegisterTimer(OnSendKeepAliveRequests, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            // _worldTimeSyncTimer = RegisterTimer(OnSyncWorldTime, null, TimeSpan.Zero, )
        }

        private async Task SendTimeUpdate()
        {
            var time = await (await GetWorld()).GetTime();
            await _generator.TimeUpdate(time.age, time.timeOfDay);
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
                KickPlayer().Ignore();
            }
            else
            {
                var id = (uint)_keepAliveIdRand.Next();
                _keepAliveWaiters.Add(id);
                _keepAliveRequestTime = DateTime.UtcNow;
                await _generator.KeepAlive(id);
            }
        }

        public Task KeepAlive(uint keepAliveId)
        {
            _keepAliveWaiters.Remove(keepAliveId);
            if (_keepAliveWaiters.Count == 0)
                _keepAliveResponseTime = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        private async Task KickPlayer()
        {
            _sendKeepAliveTimer?.Dispose();
            _sendKeepAliveTimer = null;
            _worldTimeSyncTimer?.Dispose();
            _worldTimeSyncTimer = null;

            var game = await GetGameSession();
            await game.LeaveGame(this);
            await _sink.Close();
            DeactivateOnIdle();
        }

        public Task<IPlayer> GetPlayer() => Task.FromResult(_player);

        public async Task NotifyLoggedIn()
        {
            _isOnline = true;
            _keepAliveWaiters = new HashSet<uint>();

            await SendTimeUpdate();
            await _player.NotifyLoggedIn();
            _state = UserState.DownloadingWorld;
        }

        public Task SendChatMessage(Chat jsonData, byte position)
        {
            _generator.SendChatMessage(jsonData, position);
            return Task.CompletedTask;
        }

        public Task<String> GetName()
        {
            return Task.FromResult(_name);
        }

        public Task SetName(string name)
        {
            _name = name;
            return Task.CompletedTask;
        }

        public Task<uint> GetProtocolVersion()
        {
            return Task.FromResult(_protocolVersion);
        }

        public Task SetProtocolVersion(uint version)
        {
            _protocolVersion = version;
            return Task.CompletedTask;
        }

        public Task<uint> GetPing()
        {
            uint ping;
            var diff = DateTime.UtcNow - _keepAliveRequestTime;
            if (diff.Ticks < 0)
                ping = int.MaxValue;
            else
                ping = (uint)diff.TotalMilliseconds;
            return Task.FromResult(ping);
        }

        public async Task OnGameTick(TimeSpan deltaTime)
        {
            _packetRouter?.OnGameTick();
            if (_state == UserState.DownloadingWorld)
            {
                await _player.SendPositionAndLook();
                _state = UserState.Playing;
            }

            if (_state >= UserState.JoinedGame && _state < UserState.Destroying)
                await _chunkLoader.OnGameTick();
        }

        public Task SetPacketRouter(IPacketRouter packetRouter)
        {
            _packetRouter = packetRouter;
            return Task.CompletedTask;
        }

        public Task SetViewDistance(int viewDistance)
        {
            return _chunkLoader.SetViewDistance(viewDistance);
        }

        private enum UserState : uint
        {
            None,
            JoinedGame,
            DownloadingWorld,
            Playing,
            Destroying
        }
    }
}
