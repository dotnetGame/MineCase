using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.Game.Windows.SlotAreas;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.World;
using MineCase.World;
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
        private UserState _state;

        private IPlayer _player;

        private Slot[] _slots;

        public override async Task OnActivateAsync()
        {
            if (string.IsNullOrEmpty(_worldId))
            {
                var world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetDefaultWorld();
                _worldId = world.GetPrimaryKeyString();
                _world = world;
            }

            _world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetWorld(_worldId);
            _slots = new[]
            {
                new Slot { BlockId = (short)BlockId.Furnace, ItemCount = 1 },
                new Slot { BlockId = (short)BlockId.Wood, ItemCount = 8 }
            }.Concat(Enumerable.Repeat(Slot.Empty, SlotArea.UserSlotsCount - 2)).ToArray();
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
            return Task.CompletedTask;
        }

        public async Task JoinGame()
        {
            _player = GrainFactory.GetGrain<IPlayer>(this.GetPrimaryKey());
            await _player.Tell(new BindToUser { User = this.AsReference<IUser>() });

            _state = UserState.JoinedGame;

            // 设置出生点
            await _player.Tell(new SpawnEntity
            {
                EntityId = await _world.NewEntityId(),
                Position = new EntityWorldPos(1000, 200, 1000)
            });
        }

        private async Task KickPlayer()
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

        public Task OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            if (_state == UserState.DownloadingWorld)
            {
                // await _player.SendPositionAndLook();
                _state = UserState.Playing;
            }

            /*
            if (_state >= UserState.JoinedGame && _state < UserState.Destroying)
                await _chunkLoader.OnGameTick(worldAge);*/
            return Task.CompletedTask;
        }

        public Task SetPacketRouter(IPacketRouter packetRouter)
        {
            _packetRouter = packetRouter;
            return Task.CompletedTask;
        }

        public Task<Slot[]> GetInventorySlots() => Task.FromResult(_slots);

        public Task ForwardPacket(UncompressedPacket packet)
        {
            return _player.Tell(new ServerboundPacketMessage
            {
                Packet = packet
            });
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
