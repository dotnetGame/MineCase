using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Engine
{
    struct ComponentStorage
    {
        public string Name { get; set; }

        public IComponent Component { get; set; }
    }

    public class Entity
    {
        private Guid _entityId;
        private List<ComponentStorage> _components;

        public Entity()
        {
            _entityId = new Guid();
            _components = new List<ComponentStorage>();
        }

        public Guid GetGuid()
        {
            return _entityId;
        }

        public void AddComponent(IComponent component)
        {
            var name = component.GetType().Name;
            if (HasComponent(name))
                throw new Exception("The component has been added to the entity");

            _components.Add(new ComponentStorage { Name = name, Component = component });
        }

        public void RemoveComponent<T>()
        {
            var name = typeof(T).Name;
            if (!HasComponent(name))
                throw new Exception("The entity does not have this component");
            _components.RemoveAll(c => c.Name == name);
        }

        public T GetComponent<T>()
        {
            var name = typeof(T).Name;
            for (int i = 0; i < _components.Count; ++i)
            {
                if (_components[i].Name == name)
                {
                    return (T)_components[i].Component;
                }
            }
            throw new ArgumentException("The entity does not have this component");
        }

        public void SetComponent(IComponent component)
        {
            var name = component.GetType().Name;
            for (int i = 0; i < _components.Count; ++i)
            {
                if (_components[i].Name == name)
                {
                    _components[i] = new ComponentStorage { Name = name, Component = component };
                    return;
                }
            }
            throw new ArgumentException("The entity does not have this component");
        }

        public bool HasComponent<T>()
        {
            var name = typeof(T).Name;
            foreach (var each in _components)
            {
                if (each.Name == name)
                    return true;
            }

            return false;
        }

        public bool HasComponent(string name)
        {
            foreach (var each in _components)
            {
                if (each.Name == name)
                    return true;
            }

            return false;
        }

    }
}
