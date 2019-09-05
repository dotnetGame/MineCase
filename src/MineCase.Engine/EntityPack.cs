using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Engine
{
    public class EntityPack
    {
        private List<Entity> _entities;
        private HashSet<Guid> _guids;
        
        public EntityPack()
        {
            _entities = new List<Entity>();
            _guids = new HashSet<Guid>();
        }

        public void AddEntity(Entity entity)
        {
            if (_guids.Contains(entity.GetGuid()))
                throw new ArgumentException("This pack already has the entity.");

            _entities.Add(entity);
            _guids.Add(entity.GetGuid());
        }

        public void RemoveEntity(Guid id)
        {
            if (!_guids.Contains(id))
                throw new ArgumentException("This pack does not have the entity.");

            _guids.Remove(id);
            _entities.RemoveAll(entity => entity.GetGuid() == id);
        }

        public bool HasEntity(Guid id)
        {
            return _guids.Contains(id);
        }
    }
}
