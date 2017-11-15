using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.BlockEntities;

namespace MineCase.Server.Game.Entities.Components
{
    internal class EntityLifeTimeComponent : Component<EntityGrain>, IHandle<SpawnEntity>, IHandle<DestroyEntity>
    {
        public EntityLifeTimeComponent(string name = "entityLiftTime")
            : base(name)
        {
        }

        async Task IHandle<SpawnEntity>.Handle(SpawnEntity message)
        {
            await AttachedObject.GetComponent<WorldComponent>().SetWorld(message.World);
            await AttachedObject.GetComponent<EntityWorldPositionComponent>().SetPosition(message.Position);
            var lookComponent = AttachedObject.GetComponent<EntityLookComponent>();
            await lookComponent.SetPitch(message.Pitch);
            await lookComponent.SetYaw(message.Yaw);
        }

        async Task IHandle<DestroyEntity>.Handle(DestroyEntity message)
        {
            await AttachedObject.Tell(Disable.Default);
        }
    }
}
