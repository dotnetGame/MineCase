using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;

namespace MineCase.Server.Game.BlockEntities.Components
{
    internal class BlockEntityLiftTimeComponent : Component<BlockEntityGrain>, IHandle<SpawnBlockEntity>, IHandle<DestroyBlockEntity>
    {
        public BlockEntityLiftTimeComponent(string name = "blockEntityLifeTime")
            : base(name)
        {
        }

        Task IHandle<SpawnBlockEntity>.Handle(SpawnBlockEntity message)
        {
            AttachedObject.GetComponent<WorldComponent>().SetWorld(message.World);
            AttachedObject.GetComponent<BlockWorldPositionComponent>().SetBlockWorldPosition(message.Position);
            AttachedObject.QueueOperation(async () =>
            {
                await AttachedObject.Tell(Enable.Default);
                if (AttachedObject.ValueStorage.IsDirty)
                    await AttachedObject.WriteStateAsync();
            });
            return Task.CompletedTask;
        }

        Task IHandle<DestroyBlockEntity>.Handle(DestroyBlockEntity message)
        {
            return AttachedObject.Tell(Disable.Default);
        }
    }
}
