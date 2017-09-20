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
            AttachedObject.RegisterPropertyChangedHandler(BlockWorldPositionComponent.BlockWorldPositionProperty, OnBlockWorldPositionChanged);
            return Task.CompletedTask;
        }

        private Task OnBlockWorldPositionChanged(object sender, PropertyChangedEventArgs<BlockWorldPos> e)
        {
            UpdateKey();
            return Task.CompletedTask;
        }

        private Task OnWorldChanged(object sender, PropertyChangedEventArgs<IWorld> e)
        {
            UpdateKey();
            return Task.CompletedTask;
        }

        private Task OnEntityWorldPositionChanged(object sender, PropertyChangedEventArgs<EntityWorldPos> e)
        {
            UpdateKey();
            return Task.CompletedTask;
        }

        private void UpdateKey()
        {
            if (AttachedObject.TryGetWorld(out var world))
            {
                ChunkWorldPos? chunkWorldPos = null;
                if (AttachedObject.TryGetLocalValue(EntityWorldPositionComponent.EntityWorldPositionProperty, out var entityPos))
                    chunkWorldPos = entityPos.ToChunkWorldPos();
                else if (AttachedObject.TryGetLocalValue(BlockWorldPositionComponent.BlockWorldPositionProperty, out var blockPos))
                    chunkWorldPos = blockPos.ToChunkWorldPos();

                if (chunkWorldPos.HasValue)
                {
                    var key = world.MakeAddressByPartitionKey(chunkWorldPos.Value);
                    AttachedObject.SetLocalValue(AddressByPartitionKeyProperty, key);
                }
            }
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
