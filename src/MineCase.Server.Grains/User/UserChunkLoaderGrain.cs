using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.User
{
    internal class UserChunkLoaderGrain : Grain, IUserChunkLoader
    {
        private IUser _user;
        private IClientboundPacketSink _sink;
        private IWorld _world;
        private ClientPlayPacketGenerator _generator;
        private IPlayer _player;

        private (int x, int z)? _lastStreamedChunk;
        private HashSet<(int x, int z)> _sendingChunks;
        private HashSet<(int x, int z)> _sentChunks;

        private int _viewDistance = 10;

        public override Task OnActivateAsync()
        {
            _user = GrainFactory.GetGrain<IUser>(this.GetPrimaryKey());
            return Task.CompletedTask;
        }

        public Task OnChunkSent(int chunkX, int chunkZ)
        {
            _sendingChunks.Remove((chunkX, chunkZ));
            _sentChunks.Add((chunkX, chunkZ));
            return Task.CompletedTask;
        }

        public async Task OnGameTick()
        {
            for (int i = 0; i < 4; i++)
            {
                if (!await StreamNextChunk())
                    break;
            }

            // unload per 5 ticks
            if (await _world.GetAge() % 100 == 0)
                await UnloadOutOfRangeChunks();
        }

        private async Task<bool> StreamNextChunk()
        {
            var currentChunk = await _player.GetChunkPosition();
            if (_lastStreamedChunk.HasValue && _lastStreamedChunk.Value.Equals((currentChunk.x, currentChunk.z))) return true;

            for (int d = 0; d <= _viewDistance; d++)
            {
                for (int x = -d; x <= d; x++)
                {
                    var z = d - Math.Abs(x);

                    if (await StreamChunk(currentChunk.x + x, currentChunk.z + z))
                        return false;
                    if (await StreamChunk(currentChunk.x + x, currentChunk.z - z))
                        return false;
                }
            }

            _lastStreamedChunk = (currentChunk.x, currentChunk.z);
            return true;
        }

        private async Task<bool> StreamChunk(int chunkX, int chunkZ)
        {
            var trunkSender = GrainFactory.GetGrain<IChunkSender>(_world.GetPrimaryKeyString());
            if (!_sentChunks.Contains((chunkX, chunkZ)) && _sendingChunks.Add((chunkX, chunkZ)))
            {
                await trunkSender.PostChunk(chunkX, chunkZ, new[] { _sink }, new[] { this.AsReference<IUserChunkLoader>() });
                await GrainFactory.GetGrain<IChunkTrackingHub>(_world.MakeChunkTrackingHubKey(chunkX, chunkZ)).Subscribe(_user);
                return true;
            }

            return false;
        }

        private readonly List<(int x, int y)> _clonedSentChunks = new List<(int x, int y)>();

        private List<(int x, int z)> CloneSentChunks()
        {
            _clonedSentChunks.Clear();
            _clonedSentChunks.AddRange(_sentChunks);
            return _clonedSentChunks;
        }

        private async Task UnloadOutOfRangeChunks()
        {
            var currentChunk = await _player.GetChunkPosition();
            foreach (var chunk in CloneSentChunks())
            {
                var distance = Math.Abs(chunk.x - currentChunk.x) + Math.Abs(chunk.z - currentChunk.z);
                if (distance > _viewDistance)
                {
                    await GrainFactory.GetGrain<IChunkTrackingHub>(_world.MakeChunkTrackingHubKey(chunk.x, chunk.z)).Unsubscribe(_user);
                    await _generator.UnloadChunk(chunk.x, chunk.z);
                    _sentChunks.Remove(chunk);
                }
            }
        }

        public Task SetClientPacketSink(IClientboundPacketSink sink)
        {
            _sink = sink;
            _generator = new ClientPlayPacketGenerator(sink);
            return Task.CompletedTask;
        }

        public Task JoinGame(IWorld world, IPlayer player)
        {
            _world = world;
            _player = player;
            _lastStreamedChunk = null;
            _sendingChunks = new HashSet<(int x, int z)>();
            _sentChunks = new HashSet<(int x, int z)>();
            return Task.CompletedTask;
        }

        public Task SetViewDistance(int viewDistance)
        {
            _viewDistance = viewDistance;
            return Task.CompletedTask;
        }
    }
}
