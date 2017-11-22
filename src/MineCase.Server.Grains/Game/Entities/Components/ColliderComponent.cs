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
    internal class ColliderComponent : Component<EntityGrain>, IHandle<Disable>, IHandle<Enable>
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
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged += AddressByPartitionKeyChanged;
            AttachedObject.QueueOperation(TrySubscribe);
            return base.OnAttached();
        }

        protected override async Task OnDetached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged -= AddressByPartitionKeyChanged;
            await TryUnsubscribe();
        }

        private async Task AddressByPartitionKeyChanged(object sender, (string oldKey, string newKey) e)
        {
            var shape = ColliderShape;
            if (!string.IsNullOrEmpty(e.oldKey))
                await GrainFactory.GetGrain<ICollectableFinder>(e.oldKey).UnregisterCollider(AttachedObject);
            if (!string.IsNullOrEmpty(e.newKey) && shape != null)
                await GrainFactory.GetGrain<ICollectableFinder>(e.newKey).RegisterCollider(AttachedObject, shape);
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

        Task IHandle<Enable>.Handle(Enable message)
        {
            return TrySubscribe();
        }

        Task IHandle<Disable>.Handle(Disable message)
        {
            return TryUnsubscribe();
        }

        private async Task TrySubscribe()
        {
            var key = AttachedObject.GetAddressByPartitionKey();
            var shape = ColliderShape;
            if (!string.IsNullOrEmpty(key) && shape != null)
                await GrainFactory.GetGrain<ICollectableFinder>(key).RegisterCollider(AttachedObject, ColliderShape);
        }

        private async Task TryUnsubscribe()
        {
            var key = AttachedObject.GetAddressByPartitionKey();
            if (!string.IsNullOrEmpty(key))
                await GrainFactory.GetGrain<ICollectableFinder>(key).UnregisterCollider(AttachedObject);
        }
    }
}
