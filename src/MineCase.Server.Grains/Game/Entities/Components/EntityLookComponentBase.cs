// deprecated file
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
    internal abstract class EntityLookComponentBase<T> : Component<T>, IHandle<EntityLook>
        where T : EntityGrain
    {
        public EntityLookComponentBase(string name)
            : base(name)
        {
        }

        private ClientPlayPacketGenerator GetPlayerPacketGenerator(IPlayer player) =>
            new ClientPlayPacketGenerator(new ForwardToPlayerPacketSink(player, ServiceProvider.GetRequiredService<IPacketPackager>()));

        async Task IHandle<EntityLook>.Handle(EntityLook message)
        {
            await SendLookPacket(AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator());
        }

        protected abstract Task SendLookPacket(ClientPlayPacketGenerator generator);
    }
}
