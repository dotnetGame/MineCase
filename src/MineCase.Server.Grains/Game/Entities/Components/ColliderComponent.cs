using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Graphics;
using MineCase.Server.Components;
using MineCase.Server.World;
using MineCase.World;

namespace MineCase.Server.Game.Entities.Components
{
    internal class ColliderComponent : Component<EntityGrain>, IHandle<Disable>
    {
        public static readonly DependencyProperty<Shape> ColliderShapeProperty =
            DependencyProperty.Register<Shape>("ColliderShape", typeof(ColliderComponent), new PropertyMetadata<Shape>(null, OnColliderShapeChanged));

        public Shape ColliderShape => AttachedObject.GetValue(ColliderShapeProperty);

        public ColliderComponent(string name = "collider")
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            AttachedObject.RegisterPropertyChangedHandler(AddressByPartitionKeyComponent.AddressByPartitionKeyProperty, OnAddressByPartitionKeyChanged);
            return base.OnAttached();
        }

        private async Task OnAddressByPartitionKeyChanged(object sender, PropertyChangedEventArgs<string> e)
        {
            var shape = ColliderShape;
            if (!string.IsNullOrEmpty(e.OldValue))
                await GrainFactory.GetGrain<ICollectableFinder>(e.OldValue).UnregisterCollider(AttachedObject);
            if (!string.IsNullOrEmpty(e.NewValue) && shape != null)
                await GrainFactory.GetGrain<ICollectableFinder>(e.NewValue).RegisterCollider(AttachedObject, shape);
        }

        private async Task OnColliderShapeChanged(PropertyChangedEventArgs<Shape> e)
        {
            var shape = ColliderShape;
            var key = AttachedObject.GetValue(AddressByPartitionKeyComponent.AddressByPartitionKeyProperty);
            if (shape != null)
                await GrainFactory.GetGrain<ICollectableFinder>(key).RegisterCollider(AttachedObject, shape);
            else
                await GrainFactory.GetGrain<ICollectableFinder>(key).UnregisterCollider(AttachedObject);
        }

        private static Task OnColliderShapeChanged(object sender, PropertyChangedEventArgs<Shape> e)
        {
            var component = ((DependencyObject)sender).GetComponent<ColliderComponent>();
            return component.OnColliderShapeChanged(e);
        }

        public Task SetColliderShape(Shape value) =>
            AttachedObject.SetLocalValue(ColliderShapeProperty, value);

        async Task IHandle<Disable>.Handle(Disable message)
        {
            var key = AttachedObject.GetValue(AddressByPartitionKeyComponent.AddressByPartitionKeyProperty);
            await GrainFactory.GetGrain<ICollectableFinder>(key).UnregisterCollider(AttachedObject);
        }
    }
}
