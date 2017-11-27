using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
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

        private AutoSaveStateComponent _autoSave;

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override async Task InitializePreLoadComponent()
        {
            var stateComponent = new StateComponent<StateHolder>();
            await SetComponent(stateComponent);
            stateComponent.AfterReadState += StateComponent_AfterReadState;

            _autoSave = new AutoSaveStateComponent(AutoSaveStateComponent.PerMinute);
            await SetComponent(_autoSave);
        }

        private async Task StateComponent_AfterReadState(object sender, EventArgs e)
        {
            if (State.World == null)
            {
                var world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetDefaultWorld();
                State.World = world;
                MarkDirty();
            }
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

        public Task SetClientPacketSink(IClientboundPacketSink sink)
        {
            _sink = sink;
            _generator = new ClientPlayPacketGenerator(sink);
            return Task.CompletedTask;
        }

        public async Task JoinGame()
        {
            _player = GrainFactory.GetGrain<IPlayer>(this.GetPrimaryKey());
            await _player.Tell(Disable.Default);
            await _player.Tell(BeginLogin.Default);
            await _player.Tell(new BindToUser { User = this.AsReference<IUser>() });

            _userState = UserState.JoinedGame;

            // 设置出生点
            var world = State.World;
            await _player.Tell(new SpawnEntity
            {
                World = world,
                EntityId = await world.NewEntityId(),
                Position = new EntityWorldPos(0, 200, 0)
            });
        }

        public async Task Kick()
        {
            var game = await GetGameSession();
            await game.LeaveGame(this);
            await _sink.Close();
            DeactivateOnIdle();
        }

        public Task<IPlayer> GetPlayer() => Task.FromResult(_player);

        public async Task NotifyLoggedIn()
        {
            await _player.Tell(PlayerLoggedIn.Default);
            _userState = UserState.DownloadingWorld;
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

        public async Task OnGameTick(GameTickArgs e)
        {
            if (_userState == UserState.DownloadingWorld)
            {
                // await _player.SendPositionAndLook();
                _userState = UserState.Playing;
            }

            /*
            if (_state >= UserState.JoinedGame && _state < UserState.Destroying)
                await _chunkLoader.OnGameTick(worldAge);*/
            await _generator.TimeUpdate(e.WorldAge, e.TimeOfDay);
            await _autoSave.OnGameTick(this, e);
        }

        public Task SetPacketRouter(IPacketRouter packetRouter)
        {
            _packetRouter = packetRouter;
            return Task.CompletedTask;
        }

        public Task<Slot[]> GetInventorySlots() => Task.FromResult(State.Slots);

        public Task ForwardPacket(UncompressedPacket packet)
        {
            return _player.Tell(new ServerboundPacketMessage
            {
                Packet = packet
            });
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

        internal class StateHolder
        {
            public string Name { get; set; }

            public IWorld World { get; set; }

            public Slot[] Slots { get; set; }

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
