using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.User;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Game.Entities.Components
{
    internal class ChunkLoaderComponent : Component<PlayerGrain>, IHandle<BeginLogin>, IHandle<PlayerLoggedIn>, IHandle<BindToUser>
    {
        private IUserChunkLoader _chunkLoader;
        private bool _loaded;

        public ChunkLoaderComponent(string name = "chunkLoader")
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            _loaded = false;
            _chunkLoader = GrainFactory.GetGrain<IUserChunkLoader>(AttachedObject.GetPrimaryKey());
            AttachedObject.RegisterPropertyChangedHandler(ViewDistanceComponent.ViewDistanceProperty, OnViewDistanceChanged);
            AttachedObject.GetComponent<GameTickComponent>().Tick += OnGameTick;
            return base.OnAttached();
        }

        private Task OnGameTick(object sender, GameTickArgs e)
        {
            if (_loaded)
                return _chunkLoader.OnGameTick(e.WorldAge);
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

        Task IHandle<BeginLogin>.Handle(BeginLogin message)
        {
            _loaded = false;
            return Task.CompletedTask;
        }
    }
}
