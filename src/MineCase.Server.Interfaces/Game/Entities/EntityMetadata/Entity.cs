using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Entities.EntityMetadata
{
    public class Entity
    {
        public bool OnFire { get; set; }

        public bool Crouched { get; set; }

        public bool Sprinting { get; set; }

        public bool Invisible { get; set; }

        public bool GlowingEffect { get; set; }

        public bool FlyingWithElytra { get; set; }

        public uint Air { get; set; } = 300;

        public string CustomName { get; set; } = string.Empty;

        public bool IsCustomNameVisible { get; set; }

        public bool IsSilent { get; set; }

        public bool NoGravity { get; set; }
    }
}
