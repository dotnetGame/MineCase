using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Play.Client;
using MineCase.Server.Network;
using MineCase.Server.World;
using MineCase.Util.Math;
using MineCase.World.Chunk;
using Orleans;
using Orleans.Providers;

namespace MineCase.Game.Server.MultiPlayer
{
    public class UserState
    {
        public IMinecraftServer Server { get; set; }

        public IWorld World { get; set; }

        public string Name { get; set; }
}

    [StorageProvider(ProviderName = "MongoDBStore")]
    public class User : Grain<UserState>, IUser
    {
        private IPacketSink _packetSink;
        private IGameSession _gameSession;
        private ClientPlayPacketGenerator _packetGenerator;

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            _packetGenerator = new ClientPlayPacketGenerator(GrainFactory);
        }

        public override async Task OnDeactivateAsync()
        {
            await WriteStateAsync();
            await base.OnDeactivateAsync();
        }

        public Task SetServer(IMinecraftServer server)
        {
            State.Server = server;
            return Task.CompletedTask;
        }

        public Task<IMinecraftServer> GetServer()
        {
            return Task.FromResult(State.Server);
        }

        public Task SetWorld(IWorld world)
        {
            State.World = world;
            return Task.CompletedTask;
        }

        public Task<IWorld> GetWorld()
        {
            return Task.FromResult(State.World);
        }

        public Task SetSession(IGameSession session)
        {
            _gameSession = session;
            return Task.CompletedTask;
        }

        public Task<IGameSession> GetSession()
        {
            return Task.FromResult(_gameSession);
        }

        public Task SetName(string name)
        {
            State.Name = name;
            return Task.CompletedTask;
        }

        public Task<string> GetGame()
        {
            return Task.FromResult(State.Name);
        }

        public Task<Guid> GetEntityId()
        {
            return Task.FromResult(this.GetPrimaryKey());
        }

        public Task BindPacketSink(IPacketSink sink)
        {
            _packetSink = sink;
            return Task.CompletedTask;
        }

        public async Task SetPlayerPosition(EntityPos pos)
        {
            await _gameSession.SetPlayerPosition(this.GetPrimaryKey(), pos);
        }

        // Packets
        public async Task SendChunkData(int chunkX, int chunkZ, ChunkColumn chunk)
        {
            var packet = await _packetGenerator.ChunkData(chunkX, chunkZ, chunk, 0xFFFF);
            await _packetSink.SendPacket(packet);
        }
    }
}
