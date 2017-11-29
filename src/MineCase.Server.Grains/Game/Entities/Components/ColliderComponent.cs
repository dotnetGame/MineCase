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
    internal class ColliderComponent : Component<EntityGrain>
    {
        public static readonly DependencyProperty<Shape> ColliderShapeProperty =
            DependencyProperty.Register<Shape>("ColliderShape", typeof(ColliderComponent), new PropertyMetadata<Shape>(null, OnColliderShapeChanged));

        public Shape ColliderShape => AttachedObject.GetValue(ColliderShapeProperty);

        public ColliderComponent(string name = "collider")
            : base(name)
        {
        }

        protected override void OnAttached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged += AddressByPartitionKeyChanged;
            AttachedObject.RegisterPropertyChangedHandler(IsEnabledComponent.IsEnabledProperty, OnIsEnabledChanged);
            AttachedObject.QueueOperation(TrySubscribe);
        }

        protected override void OnDetached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged -= AddressByPartitionKeyChanged;
            AttachedObject.QueueOperation(TryUnsubscribe);
        }

        private void AddressByPartitionKeyChanged(object sender, (string oldKey, string newKey) e)
        {
            AttachedObject.QueueOperation(async () =>
            {
                var shape = ColliderShape;
                if (!string.IsNullOrEmpty(e.oldKey))
                    await GrainFactory.GetGrain<ICollectableFinder>(e.oldKey).UnregisterCollider(AttachedObject);
                await TrySubscribe();
            });
        }

        private void OnIsEnabledChanged(object sender, PropertyChangedEventArgs<bool> e)
        {
            if (e.NewValue)
                AttachedObject.QueueOperation(TrySubscribe);
            else
                AttachedObject.QueueOperation(TryUnsubscribe);
        }

        private void OnColliderShapeChanged(PropertyChangedEventArgs<Shape> e)
        {
            var shape = ColliderShape;
            var key = AttachedObject.GetValue(AddressByPartitionKeyComponent.AddressByPartitionKeyProperty);
            if (shape != null)
                AttachedObject.QueueOperation(() => GrainFactory.GetGrain<ICollectableFinder>(key).RegisterCollider(AttachedObject, shape));
            else
                AttachedObject.QueueOperation(TrySubscribe);
        }

        private static void OnColliderShapeChanged(object sender, PropertyChangedEventArgs<Shape> e)
        {
            var component = ((DependencyObject)sender).GetComponent<ColliderComponent>();
            component.OnColliderShapeChanged(e);
        }

        public void SetColliderShape(Shape value) =>
            AttachedObject.SetLocalValue(ColliderShapeProperty, value);

        private async Task TrySubscribe()
        {
            if (AttachedObject.GetValue(IsEnabledComponent.IsEnabledProperty))
            {
                var key = AttachedObject.GetAddressByPartitionKey();
                var shape = ColliderShape;
                if (!string.IsNullOrEmpty(key) && shape != null)
                    await GrainFactory.GetGrain<ICollectableFinder>(key).RegisterCollider(AttachedObject, ColliderShape);
            }
        }

        private async Task TryUnsubscribe()
        {
            var key = AttachedObject.GetAddressByPartitionKey();
            if (!string.IsNullOrEmpty(key))
                await GrainFactory.GetGrain<ICollectableFinder>(key).UnregisterCollider(AttachedObject);
        }
    }
}
