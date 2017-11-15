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
using MineCase.World;

namespace MineCase.Server.Game.Entities.Components
{
    internal class EntityMoveComponent<T> : Component<T>, IHandle<EntityMove>
        where T : EntityGrain
    {
        public EntityMoveComponent(string name)
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            return base.OnAttached();
        }

        private ClientPlayPacketGenerator GetPlayerPacketGenerator(IPlayer player) =>
            new ClientPlayPacketGenerator(new ForwardToPlayerPacketSink(player, ServiceProvider.GetRequiredService<IPacketPackager>()));

        async Task IHandle<EntityMove>.Handle(EntityMove message)
        {
            await SendMovePacket(AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator());
        }

        protected Task SendMovePacket(ClientPlayPacketGenerator generator)
        {
            uint eid = AttachedObject.GetValue(EntityIdComponent.EntityIdProperty);
            EntityWorldPos pos = AttachedObject.GetValue(EntityWorldPositionComponent.EntityWorldPositionProperty);
            short x = (short)(pos.X * 32 * 128);
            short y = (short)(pos.Y * 32 * 128);
            short z = (short)(pos.Z * 32 * 128);
            bool onGround = AttachedObject.GetValue(EntityOnGroundComponent.IsOnGroundProperty);
            return generator.EntityRelativeMove(eid, x, y, z, onGround);
        }
    }
}
