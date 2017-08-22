using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Entities
{
    public sealed class PlayerDescription
    {
        public Guid UUID { get; set; }

        public string Name { get; set; }

        public GameMode GameMode { get; set; }

        public uint Ping { get; set; }

        public string DisplayName { get; set; }
    }
}
