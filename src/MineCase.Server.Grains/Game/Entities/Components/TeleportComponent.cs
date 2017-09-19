using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

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

        public Task ConfirmTeleport(uint teleportId)
        {
            return Task.CompletedTask;
        }
    }
}
