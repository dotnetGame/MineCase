using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Game.Server;
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
        private IGameSession _gameSession;

        public Task<bool> Lock(IGameSession gameSession)
        {
            if (_gameSession == null)
            {
                _gameSession = gameSession;
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> UnLock(IGameSession gameSession)
        {
            if (_gameSession == gameSession)
            {
                _gameSession = null;
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
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
    }
}
