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

        async Task IHandle<SpawnBlockEntity>.Handle(SpawnBlockEntity message)
        {
            await AttachedObject.GetComponent<WorldComponent>().SetWorld(message.World);
            await AttachedObject.GetComponent<BlockWorldPositionComponent>().SetBlockWorldPosition(message.Position);
        }

        Task IHandle<DestroyBlockEntity>.Handle(DestroyBlockEntity message)
        {
            return AttachedObject.Tell(Disable.Default);
        }
    }
}
