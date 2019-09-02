using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Engine
{
    public class Entity
    {
        private Guid _entityId;
        private List<IComponent> _components;
    }
}
