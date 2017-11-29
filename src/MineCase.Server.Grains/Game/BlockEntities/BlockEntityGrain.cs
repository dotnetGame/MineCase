using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.BlockEntities.Components;
using MineCase.Server.Persistence;
using MineCase.Server.Persistence.Components;
using MineCase.Server.World;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game.BlockEntities
{
    [PersistTableName("blockEntity")]
    [Reentrant]
    internal abstract class BlockEntityGrain : PersistableDependencyObject, IBlockEntity
    {
        public IWorld World => GetValue(WorldComponent.WorldProperty);

        public BlockWorldPos Position => GetValue(BlockWorldPositionComponent.BlockWorldPositionProperty);

        protected override void InitializeComponents()
        {
            SetComponent(new IsEnabledComponent());
            SetComponent(new WorldComponent());
            SetComponent(new BlockWorldPositionComponent());
            SetComponent(new AddressByPartitionKeyComponent());
            SetComponent(new ChunkEventBroadcastComponent());
            SetComponent(new GameTickComponent());
            SetComponent(new BlockEntityLiftTimeComponent());
            SetComponent(new AutoSaveStateComponent(AutoSaveStateComponent.PerMinute));
        }

        Task<IWorld> IBlockEntity.GetWorld() =>
            Task.FromResult(World);

        Task<BlockWorldPos> IBlockEntity.GetPosition() =>
            Task.FromResult(Position);
    }
}
