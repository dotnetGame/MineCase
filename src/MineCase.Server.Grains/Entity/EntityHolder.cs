using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Game.Server;
using Orleans;
using Orleans.Providers;

namespace MineCase.Server.Entity
{
    public class EntityHolderState
    {
        public IEntity Entity { get; set; }
    }

    [StorageProvider(ProviderName = "MongoDBStore")]
    public class EntityHolder : Grain<EntityHolderState>, IEntityHolder
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

        public Task<IEntity> Load()
        {
            return Task.FromResult(State.Entity);
        }

        public Task Save(IEntity entity)
        {
            State.Entity = entity;
            return Task.CompletedTask;
        }
    }
}
