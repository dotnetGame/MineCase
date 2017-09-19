using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.User
{
    internal class UserChunkLoaderGrain : Grain, IUserChunkLoader
    {
        private IPlayer _player;
        private IClientboundPacketSink _sink;
        private IWorld _world;
        private ClientPlayPacketGenerator _generator;

        private ChunkWorldPos? _lastStreamedChunk;
        private HashSet<ChunkWorldPos> _sendingChunks;
        private HashSet<ChunkWorldPos> _sentChunks;

        private int _viewDistance = 10;

        public override Task OnActivateAsync()
        {
            _player = GrainFactory.GetGrain<IPlayer>(this.GetPrimaryKey());
            return Task.CompletedTask;
        }

        public Task OnChunkSent(ChunkWorldPos chunkPos)
        {
            _sendingChunks.Remove(chunkPos);
            _sentChunks.Add(chunkPos);
            return Task.CompletedTask;
        }

        public async Task OnGameTick(long worldAge)
        {
            for (int i = 0; i < 4; i++)
            {
                if (!await StreamNextChunk())
                    break;
            }

            // unload per 5 seconds
            if (worldAge % 100 == 0)
                await UnloadOutOfRangeChunks();
        }

        private async Task<bool> StreamNextChunk()
        {
            var currentChunk = (await _player.GetPosition()).ToChunkWorldPos();
            if (_lastStreamedChunk.HasValue && _lastStreamedChunk.Value == currentChunk) return true;

            for (int d = 0; d <= _viewDistance; d++)
            {
                for (int x = -d; x <= d; x++)
                {
                    var z = d - Math.Abs(x);

                    if (await StreamChunk(new ChunkWorldPos(currentChunk.X + x, currentChunk.Z + z)))
                        return false;
                    if (await StreamChunk(new ChunkWorldPos(currentChunk.X + x, currentChunk.Z - z)))
                        return false;
                }
            }

            _lastStreamedChunk = currentChunk;
            return true;
        }

        private async Task<bool> StreamChunk(ChunkWorldPos chunkPos)
        {
            var trunkSender = GrainFactory.GetGrain<IChunkSender>(_world.GetPrimaryKeyString());
            if (!_sentChunks.Contains(chunkPos) && _sendingChunks.Add(chunkPos))
            {
                await trunkSender.PostChunk(chunkPos, new[] { _sink }, new[] { this.AsReference<IUserChunkLoader>() });
                await GrainFactory.GetGrain<IChunkTrackingHub>(_world.MakeAddressByPartitionKey(chunkPos)).Subscribe(_player);
                return true;
            }

            return false;
        }

        private readonly List<ChunkWorldPos> _clonedSentChunks = new List<ChunkWorldPos>();

        private List<ChunkWorldPos> CloneSentChunks()
        {
            _clonedSentChunks.Clear();
            _clonedSentChunks.AddRange(_sentChunks);
            return _clonedSentChunks;
        }

        private async Task UnloadOutOfRangeChunks()
        {
            var currentChunk = (await _player.GetPosition()).ToChunkWorldPos();
            foreach (var chunk in CloneSentChunks())
            {
                var distance = Math.Abs(chunk.X - currentChunk.X) + Math.Abs(chunk.Z - currentChunk.Z);
                if (distance > _viewDistance)
                {
                    await GrainFactory.GetGrain<IChunkTrackingHub>(_world.MakeAddressByPartitionKey(chunk)).Unsubscribe(_player);
                    await _generator.UnloadChunk(chunk.X, chunk.Z);
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
            _sendingChunks = new HashSet<ChunkWorldPos>();
            _sentChunks = new HashSet<ChunkWorldPos>();
            return Task.CompletedTask;
        }

        public Task SetViewDistance(int viewDistance)
        {
            _viewDistance = viewDistance;
            return Task.CompletedTask;
        }
    }
}
