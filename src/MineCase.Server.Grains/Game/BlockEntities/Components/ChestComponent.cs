using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.Game.Windows;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game.BlockEntities.Components
{
    internal class ChestComponent : Component<BlockEntityGrain>, IHandle<NeighborEntityChanged>, IHandle<DestroyBlockEntity>, IHandle<UseBy>
    {
        public static readonly DependencyProperty<IBlockEntity> NeighborEntityProperty =
            DependencyProperty.Register("NeighborEntity", typeof(ChestComponent), new PropertyMetadata<IBlockEntity>(null, OnNeighborEntityChanged));

        public static readonly DependencyProperty<IChestWindow> ChestWindowProperty =
            DependencyProperty.Register<IChestWindow>("ChestWindow", typeof(ChestComponent));

        public IBlockEntity NeighborEntity => AttachedObject.GetValue(NeighborEntityProperty);

        public IChestWindow ChestWindow => AttachedObject.GetValue(ChestWindowProperty);

        public ChestComponent(string name = "chest")
            : base(name)
        {
        }

        Task IHandle<NeighborEntityChanged>.Handle(NeighborEntityChanged message) =>
            AttachedObject.SetLocalValue(NeighborEntityProperty, message.Entity);

        private static async Task OnNeighborEntityChanged(object sender, PropertyChangedEventArgs<IBlockEntity> e)
        {
            var component = ((DependencyObject)sender).GetComponent<ChestComponent>();
            var window = component?.ChestWindow;
            if (window == null) return;
            if (e.NewValue == null)
            {
                await window.Destroy();
                await window.SetEntities(new[] { component.AttachedObject.AsReference<IDependencyObject>() }.AsImmutable());
            }
            else
            {
                await window.Destroy();
                await window.SetEntities(new[] { component.AttachedObject.AsReference<IDependencyObject>(), e.NewValue.AsReference<IDependencyObject>() }.AsImmutable());
            }
        }

        async Task IHandle<DestroyBlockEntity>.Handle(DestroyBlockEntity message)
        {
            if (ChestWindow != null)
                await ChestWindow.Destroy();
        }

        async Task IHandle<UseBy>.Handle(UseBy message)
        {
            var masterEntity = await FindMasterEntity(NeighborEntity);
            if (masterEntity.GetPrimaryKey() == AttachedObject.GetPrimaryKey())
            {
                if (ChestWindow == null)
                {
                    await AttachedObject.SetLocalValue(ChestWindowProperty, GrainFactory.GetGrain<IChestWindow>(Guid.NewGuid()));
                    await ChestWindow.SetEntities((NeighborEntity == null ?
                        new[] { AttachedObject.AsReference<IDependencyObject>() } :
                        new[] { AttachedObject.AsReference<IDependencyObject>(), NeighborEntity }).AsImmutable());
                }

                await message.Entity.Tell(new OpenWindow { Window = ChestWindow });
            }
            else
            {
                await masterEntity.Tell(message);
            }
        }

        private async Task<IBlockEntity> FindMasterEntity(IBlockEntity neighborEntity)
        {
            if (NeighborEntity == null)
                return AttachedObject.AsReference<IBlockEntity>();

            async Task<(IBlockEntity entity, BlockWorldPos position)> GetPosition(IBlockEntity entity) =>
                (entity, await entity.GetPosition());

            // 按 X, Z 排序取最小
            return (from e in await Task.WhenAll(new[] { GetPosition(AttachedObject.AsReference<IBlockEntity>()), GetPosition(NeighborEntity) })
                    orderby e.position.X, e.position.Z
                    select e.entity).First();
        }

        private void MarkDirty()
        {
            AttachedObject.ValueStorage.IsDirty = true;
        }
    }
}
