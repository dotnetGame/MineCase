using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Game.Server;
using MineCase.Game.Server.MultiPlayer;
using MineCase.Server.Entity;
using MineCase.Server.Entity.Player;
using MineCase.Util.Math;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World.Chunk
{
    public class ChunkManager
    {
        private IGrainFactory _grainFactory;

        private IWorld _world;

        private IGameSession _gameSession;

        public Dictionary<ChunkPos, IChunk> LoadedChunks { get; set; } = new Dictionary<ChunkPos, IChunk>();

        private Dictionary<Guid, IEntity> _entities;

        private int _viewDistance;

        public ChunkManager(IGrainFactory grainFactory, IWorld world, IGameSession gameSession, Dictionary<Guid, IEntity> entityList)
        {
            _grainFactory = grainFactory;
            _world = world;
            _gameSession = gameSession;
            _entities = entityList;
        }

        public async Task OnGameTick(object sender, GameTickArgs tickArgs)
        {
            foreach ((Guid id, IEntity entity) in _entities)
            {
                if (entity is PlayerEntity)
                {
                    await UpdatePlayerPosition((PlayerEntity)entity);
                }
            }
        }

        public async Task LoadChunk(ChunkPos pos)
        {
            if (!LoadedChunks.ContainsKey(pos))
            {
                var chunkHolder = _grainFactory.GetPartitionGrain<IChunkHolder>(_world, pos);
                await chunkHolder.Lock(_gameSession);
                LoadedChunks.Add(pos, await chunkHolder.Load());
            }
        }

        public async Task UnLoadChunk(ChunkPos pos)
        {
            if (LoadedChunks.ContainsKey(pos))
            {
                var chunkHolder = _grainFactory.GetPartitionGrain<IChunkHolder>(_world, pos);
                await chunkHolder.Save(LoadedChunks[pos]);
                LoadedChunks.Remove(pos);
                await chunkHolder.UnLock(_gameSession);
            }
        }

        public async Task<IChunk> GetChunk(ChunkPos pos)
        {
            if (!LoadedChunks.ContainsKey(pos))
            {
                await LoadChunk(pos);
            }

            return LoadedChunks[pos];
        }

        private async Task UpdatePlayerPosition(PlayerEntity player)
        {
            // TODO: update tracking

            // Unload chunks out of range and load new chunks
            var chunkPos = player.Position.ToChunkPos();
            var lastSectionPos = player.LastSectionPos;
            if (Math.Abs(lastSectionPos.X - chunkPos.X) <= _viewDistance * 2
                && Math.Abs(lastSectionPos.Z - lastSectionPos.Z) <= _viewDistance * 2)
            {
                int xMin = Math.Min(chunkPos.X, lastSectionPos.X) - _viewDistance;
                int zMin = Math.Min(chunkPos.Z, lastSectionPos.Z) - _viewDistance;
                int xMax = Math.Max(chunkPos.X, lastSectionPos.X) + _viewDistance;
                int zMax = Math.Max(chunkPos.Z, lastSectionPos.Z) + _viewDistance;

                for (int x = xMin; x <= xMax; ++x)
                {
                    for (int z = zMin; z <= zMax; ++z)
                    {
                        ChunkPos eachChunkPos = new ChunkPos(x, z);
                        bool isLoaded = GetChunkDistance(eachChunkPos, lastSectionPos.X, lastSectionPos.Z) <= _viewDistance;
                        bool needLoad = GetChunkDistance(eachChunkPos, chunkPos.X, chunkPos.Z) <= _viewDistance;
                        await SyncClientChunkState(player, eachChunkPos, isLoaded, needLoad);
                    }
                }
            }
            else
            {
                for (int i = lastSectionPos.X - _viewDistance; i <= lastSectionPos.X + _viewDistance; ++i)
                {
                    for (int j = lastSectionPos.Z - _viewDistance; j <= lastSectionPos.Z + _viewDistance; ++j)
                    {
                        ChunkPos chunkpos = new ChunkPos(i, j);
                        bool isLoaded = true;
                        bool needLoad = false;
                        await SyncClientChunkState(player, chunkpos, isLoaded, needLoad);
                    }
                }

                for (int i = chunkPos.X - _viewDistance; i <= chunkPos.X + _viewDistance; ++i)
                {
                    for (int j = chunkPos.Z - _viewDistance; j <= chunkPos.Z + _viewDistance; ++j)
                    {
                        ChunkPos chunkpos = new ChunkPos(i, j);
                        bool isLoaded = true;
                        bool needLoad = false;
                        await SyncClientChunkState(player, chunkpos, isLoaded, needLoad);
                    }
                }
            }
        }

        protected async Task SyncClientChunkState(PlayerEntity player, ChunkPos chunkPos, bool isLoaded, bool needLoad)
        {
            if (player.World == _world)
            {
                var user = _grainFactory.GetGrain<IUser>(player.PrimaryKey);
                if (needLoad && !isLoaded)
                {
                    IChunk chunk = await GetChunk(chunkPos);
                    await user.SendChunkData(chunkPos.X, chunkPos.Z, await chunk.GetChunkColumn());
                }

                if (!needLoad && isLoaded)
                {
                    // user.SendChunkUnload(chunkPos);
                }
            }
        }

        private static int GetChunkDistance(ChunkPos chunkPos, int x, int y)
        {
            int xDiff = chunkPos.X - x;
            int zDiff = chunkPos.Z - y;
            return Math.Max(Math.Abs(xDiff), Math.Abs(zDiff));
        }
    }
}
