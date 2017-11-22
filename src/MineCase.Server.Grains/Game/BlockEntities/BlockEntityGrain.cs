using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.BlockEntities.Components;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Game.BlockEntities
{
    internal abstract class BlockEntityGrain : DependencyObject, IBlockEntity
    {
        public IWorld World => GetValue(WorldComponent.WorldProperty);

        public BlockWorldPos Position => GetValue(BlockWorldPositionComponent.BlockWorldPositionProperty);

        protected override async Task InitializeComponents()
        {
            await SetComponent(new IsEnabledComponent());
            await SetComponent(new WorldComponent());
            await SetComponent(new BlockWorldPositionComponent());
            await SetComponent(new AddressByPartitionKeyComponent());
            await SetComponent(new ChunkEventBroadcastComponent());
            await SetComponent(new GameTickComponent());
            await SetComponent(new BlockEntityLiftTimeComponent());
        }

        Task<IWorld> IBlockEntity.GetWorld() =>
            Task.FromResult(World);

        Task<BlockWorldPos> IBlockEntity.GetPosition() =>
            Task.FromResult(Position);
    }
}
