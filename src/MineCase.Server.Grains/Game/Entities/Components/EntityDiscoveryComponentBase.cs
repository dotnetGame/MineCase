using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using Orleans;

namespace MineCase.Server.Game.Entities.Components
{
    internal abstract class EntityDiscoveryComponentBase<T> : Component<T>, IHandle<DiscoveredByPlayer>, IHandle<BroadcastDiscovered>
        where T : EntityGrain
    {
        public EntityDiscoveryComponentBase(string name)
            : base(name)
        {
        }

        private ClientPlayPacketGenerator GetPlayerPacketGenerator(IPlayer player) =>
            new ClientPlayPacketGenerator(new ForwardToPlayerPacketSink(player, ServiceProvider.GetRequiredService<IPacketPackager>()));

        async Task IHandle<DiscoveredByPlayer>.Handle(DiscoveredByPlayer message)
        {
            await SendSpawnPacket(GetPlayerPacketGenerator(message.Player));
        }

        async Task IHandle<BroadcastDiscovered>.Handle(BroadcastDiscovered message)
        {
            await SendSpawnPacket(AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator());
        }

        protected abstract Task SendSpawnPacket(ClientPlayPacketGenerator generator);

        protected void CompleteSpawn()
        {
            AttachedObject.QueueOperation(async () =>
            {
                await AttachedObject.Tell(Enable.Default);
                if (AttachedObject.ValueStorage.IsDirty)
                    await AttachedObject.WriteStateAsync();
            });
        }
    }
}
