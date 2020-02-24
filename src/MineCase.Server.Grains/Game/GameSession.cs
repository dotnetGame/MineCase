using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Game.Server.MultiPlayer;
using MineCase.Server.Entity;
using MineCase.Server.Entity.Player;
using MineCase.Server.World;
using MineCase.Server.World.Chunk;
using MineCase.Util.Event;
using MineCase.Util.Math;
using MineCase.World;
using Orleans;

namespace MineCase.Game.Server
{
    public enum GameSessionState
    {
        Closed,
        Closing,
        Open,
    }

    public class GameSession : Grain, IGameSession
    {
        private GameSessionState _gameSessionState;

        // Tick info
        private IDisposable _tickTimer;
        private long _worldAge;
        private static readonly long _updateMs = 50;

        public event AsyncEventHandler<GameTickArgs> Tick;

        // World info
        private IWorld _world = null;

        private ChunkManager _chunkManager;

        private Dictionary<Guid, IEntity> _globalEnities;

        private Dictionary<IUser, UserContext> _users;

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            _gameSessionState = GameSessionState.Closed;
            var ownerUser = GrainFactory.GetGrain<IUser>(this.GetPrimaryKey());
            _world = await ownerUser.GetWorld();

            _globalEnities = new Dictionary<Guid, IEntity>();
            _chunkManager = new ChunkManager(GrainFactory, _world, this, _globalEnities);
            _users = new Dictionary<IUser, UserContext>();
        }

        public override async Task OnDeactivateAsync()
        {
            await base.OnDeactivateAsync();
        }

        public async Task Enable()
        {
            _tickTimer?.Dispose();
            _worldAge = await _world.GetAge();

            _tickTimer = RegisterTimer(OnTick, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(_updateMs));
            Tick += _chunkManager.OnGameTick;
            _gameSessionState = GameSessionState.Open;
        }

        protected async Task OnTick(object arg)
        {
            var e = new GameTickArgs { WorldAge = _worldAge, TimeOfDay = _worldAge % 24000 };
            await Tick.InvokeSerial(this, e);
            _worldAge++;
        }

        public Task Disable()
        {
            _gameSessionState = GameSessionState.Closed;
            Tick -= _chunkManager.OnGameTick;
            _tickTimer?.Dispose();
            _tickTimer = null;
            return Task.CompletedTask;
        }

        public async Task UserEnter(IUser user)
        {
            await user.SetSession(this);
            await AddEntity(await user.GetEntityId());
        }

        public async Task UserLeave(IUser user)
        {
            await user.SetSession(null);
        }

        public async Task SetPlayerPosition(Guid playerId, EntityPos pos)
        {
            if (_globalEnities.ContainsKey(playerId) && _globalEnities[playerId] is PlayerEntity)
            {
                var player = (PlayerEntity)_globalEnities[playerId];
                var oldPos = player.Position;
                player.Position = pos;
            }

            // FIXME : else check
        }

        private async Task AddEntity(Guid entityKey)
        {
            var entity = GrainFactory.GetGrain<IEntityHolder>(entityKey);

            // TODO Check return
            await entity.Lock(this);

            // Load data to session
            var e = await entity.Load();

            // Add to entities list
            _globalEnities.Add(entityKey, e);

            // Register to tick
            Tick += e.OnGameTick;
        }

        private async Task RemoveEntity(IEntity entityObject)
        {
            var entity = GrainFactory.GetGrain<IEntityHolder>(await entityObject.GetID());

            // Remove from tick
            Tick -= entityObject.OnGameTick;

            // Save data to holder
            await entity.Save(entityObject);

            // TODO Check return
            await entity.UnLock(this);
        }

        private class UserContext
        {
            public List<ChunkPos> ViewRegion { get; set; } = new List<ChunkPos>();
        }
    }
}
