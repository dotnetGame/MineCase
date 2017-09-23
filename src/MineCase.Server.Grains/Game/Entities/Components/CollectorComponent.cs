using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;

namespace MineCase.Server.Game.Entities.Components
{
    internal class CollectorComponent : Component<EntityGrain>, IHandle<CollisionWith>
    {
        public CollectorComponent(string name = "collector")
            : base(name)
        {
        }

        Task IHandle<CollisionWith>.Handle(CollisionWith message)
        {
            return Task.WhenAll(from e in message.Entities
                                select e.Tell(new CollectBy { Entity = AttachedObject }));
        }
    }
}
