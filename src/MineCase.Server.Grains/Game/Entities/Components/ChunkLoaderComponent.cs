using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Game.Entities.Components
{
    internal class ChunkLoaderComponent : Component<PlayerGrain>, IHandle<PlayerLoggedIn>, IHandle<BindToUser>
    {
        private IUserChunkLoader _chunkLoader;
        private bool _loaded = false;

        public ChunkLoaderComponent(string name = "chunkLoader")
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            _chunkLoader = GrainFactory.GetGrain<IUserChunkLoader>(AttachedObject.GetPrimaryKey());
            AttachedObject.RegisterPropertyChangedHandler(ViewDistanceComponent.ViewDistanceProperty, OnViewDistanceChanged);
            AttachedObject.GetComponent<GameTickComponent>().Tick += OnGameTick;
            return base.OnAttached();
        }

        private Task OnGameTick(object sender, (TimeSpan deltaTime, long worldAge) e)
        {
            if (_loaded)
                return _chunkLoader.OnGameTick(e.worldAge);
            return Task.CompletedTask;
        }

        private Task OnViewDistanceChanged(object sender, PropertyChangedEventArgs<byte> e)
        {
            return _chunkLoader.SetViewDistance(e.NewValue);
        }

        async Task IHandle<PlayerLoggedIn>.Handle(PlayerLoggedIn message)
        {
            await _chunkLoader.JoinGame(AttachedObject.GetWorld(), AttachedObject);
            _loaded = true;
        }

        async Task IHandle<BindToUser>.Handle(BindToUser message)
        {
            await _chunkLoader.SetClientPacketSink(await message.User.GetClientPacketSink());
        }
    }
}
