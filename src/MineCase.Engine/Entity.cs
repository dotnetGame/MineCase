using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Engine
{
    public class Entity
    {
        private Guid _entityId;
        private List<IComponent> _components;
        private Dictionary<string, int> _nameIndex;

        public Entity()
        {
            _entityId = new Guid();
            _components = new List<IComponent>();
            _nameIndex = new Dictionary<string, int>();
        }

        public void AddComponent(IComponent component)
        {
            var name = component.GetType().Name;
            if (_nameIndex.ContainsKey(name))
                throw new Exception("The component has been added to the entity");

            _components.Add(component);
            _nameIndex.Add(name, _components.Count - 1);
        }

        public void RemoveComponent<T>()
        {
            var name = typeof(T).Name;
            if (!_nameIndex.ContainsKey(name))
                throw new Exception("The entity does not have this component");
            int index = _nameIndex[name];
            _nameIndex.Remove(name);
            _components.RemoveAt(index);
        }

        public T GetComponent<T>()
        {
            var name = typeof(T).Name;
            if (!_nameIndex.ContainsKey(name))
                throw new Exception("The entity does not have this component");
            int index = _nameIndex[name];
            return (T)_components[index];
        }

        public void SetComponent(IComponent component)
        {
            var name = component.GetType().Name;
            if (!_nameIndex.ContainsKey(name))
                throw new Exception("The entity does not have this component");
            int index = _nameIndex[name];
            _components[index] = component;
        }

    }
}
