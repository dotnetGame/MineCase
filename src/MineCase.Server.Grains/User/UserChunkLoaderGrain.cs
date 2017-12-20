﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.Settings;
using MineCase.Server.World;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.User
{
    [Reentrant]
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

        public Task OnChunkSent(ChunkWorldPos chunkPos)
        {
            _sendingChunks.Remove(chunkPos);
            _sentChunks.Add(chunkPos);
            return Task.CompletedTask;
        }

        public async Task OnGameTick(GameTickArgs e, EntityWorldPos playerPosition)
        {
            if (e.WorldAge % 10 == 0)
            {
                for (int i = 0; i < 4 && _sendingChunks.Count <= 4; i++)
                {
                    if (await StreamNextChunk(playerPosition.ToChunkWorldPos())) break;
                }
            }

            // unload per 5 seconds
            if (e.WorldAge % 100 == 0)
                await UnloadOutOfRangeChunks();
        }

        private async Task<bool> StreamNextChunk(ChunkWorldPos currentChunk)
        {
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
                await GrainFactory.GetPartitionGrain<IChunkTrackingHub>(_world, chunkPos).Subscribe(_player);
                await GrainFactory.GetPartitionGrain<IWorldPartition>(_world, chunkPos).Enter(_player);
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
            foreach (var chunkPos in CloneSentChunks())
            {
                var distance = Math.Abs(chunkPos.X - currentChunk.X) + Math.Abs(chunkPos.Z - currentChunk.Z);
                if (distance > _viewDistance)
                {
                    await GrainFactory.GetPartitionGrain<IChunkTrackingHub>(_world, chunkPos).Unsubscribe(_player);
                    await GrainFactory.GetPartitionGrain<IWorldPartition>(_world, chunkPos).Leave(_player);
                    await _generator.UnloadChunk(chunkPos.X, chunkPos.Z);
                    _sentChunks.Remove(chunkPos);
                }
            }
        }

        public Task SetClientPacketSink(IClientboundPacketSink sink)
        {
            _sink = sink;
            _generator = new ClientPlayPacketGenerator(sink);
            return Task.CompletedTask;
        }

        public async Task JoinGame(IWorld world, IPlayer player)
        {
            _world = world;
            _player = player;
            _lastStreamedChunk = null;
            if (_sendingChunks != null)
            {
                foreach (var chunkPos in _sendingChunks)
                {
                    await GrainFactory.GetPartitionGrain<IChunkTrackingHub>(_world, chunkPos).Unsubscribe(_player);
                    await GrainFactory.GetPartitionGrain<IWorldPartition>(_world, chunkPos).Leave(_player);
                }
            }

            _sendingChunks = new HashSet<ChunkWorldPos>();

            if (_sentChunks != null)
            {
                foreach (var chunkPos in _sentChunks)
                {
                    await GrainFactory.GetPartitionGrain<IChunkTrackingHub>(_world, chunkPos).Unsubscribe(_player);
                    await GrainFactory.GetPartitionGrain<IWorldPartition>(_world, chunkPos).Leave(_player);
                }
            }

            _sentChunks = new HashSet<ChunkWorldPos>();
        }

        public async Task SetViewDistance(int viewDistance)
        {
            var settings = GrainFactory.GetGrain<IServerSettings>(0);
            var maxViewDistance = (await settings.GetSettings()).ViewDistance;
            if (viewDistance >= 2 && viewDistance <= maxViewDistance)
                _viewDistance = viewDistance;
            else
                _viewDistance = (int)maxViewDistance;
        }
    }
}
