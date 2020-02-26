using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Game.Server;
using MineCase.Util.Math;
using Orleans;
using Orleans.Providers;

namespace MineCase.Server.World.Chunk
{
    public class ChunkHolderState
    {
        public Chunk Chunk { get; set; }
    }

    [StorageProvider(ProviderName = "MongoDBStore")]
    public class ChunkHolder : Grain<ChunkHolderState>, IChunkHolder
    {
        private Dictionary<Guid, IGameSession> _gameSessions;

        public Task Subscribe(IGameSession gameSession)
        {
            if (!_gameSessions.ContainsKey(gameSession.GetPrimaryKey()))
            {
                _gameSessions[gameSession.GetPrimaryKey()] = gameSession;
            }

            return Task.CompletedTask;
        }

        public Task Unsubscribe(IGameSession gameSession)
        {
            if (_gameSessions.ContainsKey(gameSession.GetPrimaryKey()))
            {
                _gameSessions.Remove(gameSession.GetPrimaryKey());
            }

            return Task.CompletedTask;
        }

        public Task<IChunk> Load()
        {
            return Task.FromResult((IChunk)State.Chunk);
        }

        public Task Save(IChunk chunk)
        {
            State.Chunk = (Chunk)chunk;
            return Task.CompletedTask;
        }

        public Task SetBlockState(BlockPos position, BlockState state)
        {
            return Task.CompletedTask;
        }
    }
}
