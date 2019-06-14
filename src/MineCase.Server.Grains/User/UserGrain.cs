using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Protocol;
using MineCase.Server.Components;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.Game.Windows.SlotAreas;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.Persistence;
using MineCase.Server.Persistence.Components;
using MineCase.Server.Settings;
using MineCase.Server.World;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.User
{
    [PersistTableName("user")]
    [Reentrant]
    internal class UserGrain : PersistableDependencyObject, IUser
    {
        private uint _protocolVersion;
        private IClientboundPacketSink _sink;
        private IPacketRouter _packetRouter;
        private ClientPlayPacketGenerator _generator;
        private IPlayer _player;
        private UserState _userState;

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override void InitializePreLoadComponent()
        {
            var stateComponent = new StateComponent<StateHolder>();
            SetComponent(stateComponent);

            stateComponent.AfterReadState += StateComponent_AfterReadState;
            stateComponent.SetDefaultState += StateComponent_SetDefaultState;

            SetComponent(new PeriodicSaveStateComponent(TimeSpan.FromMinutes(1)));
        }

        private async Task StateComponent_SetDefaultState(object sender, EventArgs e)
        {
            var settings = await GrainFactory.GetGrain<IServerSettings>(0).GetSettings();
            State.GameMode = new GameMode { ModeClass = (GameMode.Class)settings.Gamemode };
        }

        private void StateComponent_AfterReadState(object sender, EventArgs e)
        {
            if (State.World == null)
            {
                QueueOperation(async () =>
                {
                    var world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetDefaultWorld();
                    State.World = world;
                    MarkDirty();
                });
            }
        }

        protected override void InitializeComponents()
        {
            SetComponent(new ChunkLoaderComponent());
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

        public Task<IWorld> GetWorld() => Task.FromResult(State.World);

        public Task<GameMode> GetGameMode() => Task.FromResult(State.GameMode);

        public Task SetClientPacketSink(IClientboundPacketSink sink)
        {
            _sink = sink;
            _generator = new ClientPlayPacketGenerator(sink);
            return Task.CompletedTask;
        }

        public async Task JoinGame()
        {
            _player = GrainFactory.GetGrain<IPlayer>(this.GetPrimaryKey());
            await _player.Tell(DestroyEntity.Default);
            await Tell(BeginLogin.Default);
            await _player.Tell(BeginLogin.Default);
            await Tell(new BindToUser { User = this.AsReference<IUser>() });
            await _player.Tell(new BindToUser { User = this.AsReference<IUser>() });

            _userState = UserState.JoinedGame;

            // 设置出生点
            var world = State.World;
            await _player.Tell(new SpawnEntity
            {
                World = world,
                EntityId = await world.NewEntityId(),
                Position = State.SpawnPosition ?? await world.GetSpawnPosition()
            });
        }

        public async Task Kick()
        {
            State.SpawnPosition = await _player.GetPosition();
            MarkDirty();
            await _player.Tell(DestroyEntity.Default);
            var game = await GetGameSession();
            await game.LeaveGame(this);
            await _sink.Close();
            _sink = null;
            DeactivateOnIdle();
        }

        public Task<IPlayer> GetPlayer() => Task.FromResult(_player);

        public async Task NotifyLoggedIn()
        {
            await Tell(PlayerLoggedIn.Default);
            await _player.Tell(PlayerLoggedIn.Default);
            _userState = UserState.DownloadingWorld;
        }

        public Task UpdatePlayerList(IReadOnlyList<IPlayer> players)
        {
            return _player.Tell(new PlayerListAdd { Players = players });
        }

        public Task SendChatMessage(Chat jsonData, byte position)
        {
            _generator.SendChatMessage(jsonData, position);
            return Task.CompletedTask;
        }

        public Task<String> GetName()
        {
            return Task.FromResult(State.Name);
        }

        public Task SetName(string name)
        {
            State.Name = name;
            MarkDirty();
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

        public Task SetGameMode(GameMode gameMode)
        {
            State.GameMode = gameMode;
            MarkDirty();
            return Task.CompletedTask;
        }

        public async Task OnGameTick(GameTickArgs e)
        {
            if (_userState == UserState.DownloadingWorld)
            {
                _userState = UserState.Playing;
                await _generator.TimeUpdate(e.WorldAge, e.TimeOfDay);
            }

            if (e.WorldAge % 20 == 0)
                await _generator.TimeUpdate(e.WorldAge, e.TimeOfDay);

            await Tell(new GameTick { Args = e });
        }

        public Task SetPacketRouter(IPacketRouter packetRouter)
        {
            _packetRouter = packetRouter;
            return Task.CompletedTask;
        }

        public Task<Slot[]> GetInventorySlots() => Task.FromResult(State.Slots);

        public Task ForwardPacket(UncompressedPacket packet)
        {
            _player.InvokeOneWay(p => p.Tell(new ServerboundPacketMessage
            {
                Packet = packet
            }));
            return Task.CompletedTask;
        }

        public Task SetInventorySlot(int index, Slot slot)
        {
            State.Slots[index] = slot;
            MarkDirty();
            return Task.CompletedTask;
        }

        private enum UserState : uint
        {
            None,
            JoinedGame,
            DownloadingWorld,
            Playing,
            Destroying
        }

        private void MarkDirty()
        {
            ValueStorage.IsDirty = true;
        }

        public Task RemovePlayerList(List<IPlayer> players)
        {
            return _player.Tell(new PlayerListRemove { Players = players });
        }

        internal class StateHolder
        {
            public string Name { get; set; }

            public IWorld World { get; set; }

            public Slot[] Slots { get; set; }

            public GameMode GameMode { get; set; }

            public EntityWorldPos? SpawnPosition { get; set; }

            public StateHolder()
            {
            }

            public StateHolder(InitializeStateMark mark)
            {
                Slots = new[]
                {
                    new Slot { BlockId = (short)BlockId.Furnace, ItemCount = 1 },
                    new Slot { BlockId = (short)BlockId.Wood, ItemCount = 8 }
                }.Concat(Enumerable.Repeat(Slot.Empty, SlotArea.UserSlotsCount - 2)).ToArray();
            }
        }
    }
}
