// deprecated file
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Game.Entities.Components
{
    internal class PlayerLookComponent : EntityLookComponentBase<EntityGrain>, IHandle<EntityLook>
    {
        public PlayerLookComponent(string name = "playerLook")
            : base(name)
        {
        }

        protected override Task SendLookPacket(ClientPlayPacketGenerator generator)
        {
            uint eid = AttachedObject.GetComponent<EntityIdComponent>().EntityId;
            float yaw = AttachedObject.GetComponent<EntityLookComponent>().Yaw;
            float pitch = AttachedObject.GetComponent<EntityLookComponent>().Pitch;
            bool onGround = AttachedObject.GetComponent<EntityOnGroundComponent>().IsOnGround;

            // TODO player look
            // generator.;
            return Task.CompletedTask;
        }
    }
}
