using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Game.Entities.Components
{
    internal abstract class EntityDiscoveryComponentBase<T> : Component<T>, IHandle<DiscoveredByPlayer>, IHandle<BroadcastDiscovered>
        where T : EntityGrain
    {
        public EntityDiscoveryComponentBase(string name)
            : base(name)
        {
        }

        private TaskCompletionSource<object> _spawned;

        protected override Task OnAttached()
        {
            _spawned = new TaskCompletionSource<object>();
            return base.OnAttached();
        }

        private ClientPlayPacketGenerator GetPlayerPacketGenerator(IPlayer player) =>
            new ClientPlayPacketGenerator(new ForwardToPlayerPacketSink(player, ServiceProvider.GetRequiredService<IPacketPackager>()));

        async Task IHandle<DiscoveredByPlayer>.Handle(DiscoveredByPlayer message)
        {
            await _spawned.Task;
            await SendSpawnPacket(GetPlayerPacketGenerator(message.Player));
        }

        async Task IHandle<BroadcastDiscovered>.Handle(BroadcastDiscovered message)
        {
            await _spawned.Task;
            await SendSpawnPacket(AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator());
        }

        protected abstract Task SendSpawnPacket(ClientPlayPacketGenerator generator);

        protected void CompleteSpawn()
        {
            _spawned.TrySetResult(null);
        }
    }
}
