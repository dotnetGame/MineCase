using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class WorldGrain : Grain, IWorld
    {
        private Dictionary<uint, IEntity> _entities;
        private uint _nextAvailEId;
        private long _worldAge;
        private IBlockAccessor _blockAccessor;

        public override Task OnActivateAsync()
        {
            _nextAvailEId = 0;
            _entities = new Dictionary<uint, IEntity>();
            _blockAccessor = GrainFactory.GetGrain<IBlockAccessor>(this.GetPrimaryKeyString());
            return base.OnActivateAsync();
        }

        public Task AttachEntity(IEntity entity)
        {
            _entities.Add(entity.GetEntityId(), entity);
            return Task.CompletedTask;
        }

        public Task<IEntity> FindEntity(uint eid)
        {
            if (_entities.TryGetValue(eid, out var entity))
                return Task.FromResult(entity);
            return Task.FromException<IEntity>(new EntityNotFoundException());
        }

        public Task<(long age, long timeOfDay)> GetTime()
        {
            return Task.FromResult((_worldAge, _worldAge % 24000));
        }

        public Task<uint> NewEntityId()
        {
            var id = _nextAvailEId++;
            return Task.FromResult(id);
        }

        public Task OnGameTick(TimeSpan deltaTime)
        {
            _worldAge += 20;
            return Task.CompletedTask;
        }

        public Task<long> GetAge() => Task.FromResult(_worldAge);

        public Task<IBlockAccessor> GetBlockAccessor()
        {
            return Task.FromResult(_blockAccessor);
        }
    }
}
