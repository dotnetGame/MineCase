using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Network.Play;
using MineCase.World;

namespace MineCase.Server.Game.Entities.Components
{
    internal class TeleportComponent : Component
    {
        private uint _teleportId = 0;

        public TeleportComponent(string name = "teleport")
            : base(name)
        {
        }

        public uint StartNew() => _teleportId++;

        public Task Teleport(EntityWorldPos position, float yaw, float pitch)
        {
            var generator = AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator();
            uint teleportId = AttachedObject.GetComponent<TeleportComponent>().StartNew();
            return generator.PositionAndLook(position.X, position.Y, position.Z, yaw, pitch, 0, teleportId);
        }

        public Task ConfirmTeleport(uint teleportId)
        {
            return Task.CompletedTask;
        }
    }
}
