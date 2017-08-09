using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Game;
using System.Threading.Tasks;

namespace MineCase.Server.World
{
    class WorldGrain : Grain, IWorld
    {
        private Dictionary<uint, IEntity> _entities;
        private uint _nextAvailEId;

        public override Task OnActivateAsync()
        {
            _nextAvailEId = 0;
            _entities = new Dictionary<uint, IEntity>();
            return base.OnActivateAsync();
        }

        public Task<uint> AttachEntity(IEntity entity)
        {
            var id = _nextAvailEId++;
            _entities.Add(id, entity);
            return Task.FromResult(id);
        }

        public Task<IEntity> FindEntity(uint eid)
        {
            if (_entities.TryGetValue(eid, out var entity))
                return Task.FromResult(entity);
            return Task.FromException<IEntity>(new EntityNotFoundException());
        }
    }
}
