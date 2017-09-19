using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.World;
using MineCase.World;

namespace MineCase.Server.Components
{
    internal class AddressByPartitionKeyComponent : Component
    {
        public static readonly DependencyProperty<string> AddressByPartitionKeyProperty =
            DependencyProperty.Register("AddressByPartitionKey", typeof(AddressByPartitionKeyComponent), new PropertyMetadata<string>(string.Empty, OnAddressByPartitionKeyChanged));

        public event AsyncEventHandler<(string oldKey, string newKey)> KeyChanged;

        public AddressByPartitionKeyComponent(string name = "addressByPartitionKey")
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            AttachedObject.RegisterPropertyChangedHandler(WorldComponent.WorldProperty, OnWorldChanged);
            AttachedObject.RegisterPropertyChangedHandler(EntityWorldPositionComponent.EntityWorldPositionProperty, OnEntityWorldPositionChanged);
            return Task.CompletedTask;
        }

        private Task OnWorldChanged(object sender, PropertyChangedEventArgs<IWorld> e)
        {
            if (AttachedObject.TryGetEntityWorldPosition(out var entityPos))
            {
                var chunkWorldPos = entityPos.ToChunkWorldPos();
                var key = e.NewValue.MakeAddressByPartitionKey(chunkWorldPos);
                AttachedObject.SetLocalValue(AddressByPartitionKeyProperty, key);
            }

            return Task.CompletedTask;
        }

        private Task OnEntityWorldPositionChanged(object sender, PropertyChangedEventArgs<EntityWorldPos> e)
        {
            if (AttachedObject.TryGetWorld(out var world))
            {
                var chunkWorldPos = e.NewValue.ToChunkWorldPos();
                var key = world.MakeAddressByPartitionKey(chunkWorldPos);
                AttachedObject.SetLocalValue(AddressByPartitionKeyProperty, key);
            }

            return Task.CompletedTask;
        }

        private static async Task OnAddressByPartitionKeyChanged(object sender, PropertyChangedEventArgs<string> e)
        {
            var component = ((DependencyObject)sender).GetComponent<AddressByPartitionKeyComponent>();
            if (component != null)
                await component.KeyChanged.InvokeSerial(sender, (e.OldValue, e.NewValue));
        }
    }

    public static class AddressByPartitionKeyComponentExtensions
    {
        public static string GetAddressByPartitionKey(this DependencyObject d) =>
            d.GetValue(AddressByPartitionKeyComponent.AddressByPartitionKeyProperty);
    }
}
