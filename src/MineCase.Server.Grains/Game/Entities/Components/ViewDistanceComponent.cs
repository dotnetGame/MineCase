using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Engine;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Game.Entities.Components
{
    internal class ViewDistanceComponent : Component<PlayerGrain>
    {
        public static readonly DependencyProperty<byte> ViewDistanceProperty =
            DependencyProperty.Register("ViewDistance", typeof(ViewDistanceComponent), new PropertyMetadata<byte>(10));

        private IUserChunkLoader _chunkLoader;

        public ViewDistanceComponent(string name = "viewDistance")
            : base(name)
        {
        }

        protected override void OnAttached()
        {
            _chunkLoader = GrainFactory.GetGrain<IUserChunkLoader>(AttachedObject.GetPrimaryKey());
            AttachedObject.RegisterPropertyChangedHandler(ViewDistanceComponent.ViewDistanceProperty, OnViewDistanceChanged);
        }

        private void OnViewDistanceChanged(object sender, PropertyChangedEventArgs<byte> e)
        {
            AttachedObject.QueueOperation(() => _chunkLoader.SetViewDistance(e.NewValue));
        }
    }
}
