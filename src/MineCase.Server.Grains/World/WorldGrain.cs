using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using Orleans;

namespace MineCase.Server.World
{
    internal class WorldGrain : Grain, IWorld
    {
        private Dictionary<uint, IEntity> _entities;
        private uint _nextAvailEId;
        private DateTime _worldStartTime;

        public override Task OnActivateAsync()
        {
            _nextAvailEId = 0;
            _entities = new Dictionary<uint, IEntity>();
            _worldStartTime = DateTime.UtcNow;
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
            var age = (long)((DateTime.UtcNow - _worldStartTime).TotalSeconds * 20);
            return Task.FromResult((age, age % 24000));
        }

        public Task<uint> NewEntityId()
        {
            var id = _nextAvailEId++;
            return Task.FromResult(id);
        }
    }
}
