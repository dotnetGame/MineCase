using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Server.World.EntitySpawner
{
    public struct CreatureTask
    {
        public uint Health { get; set; }

        public Vector3 Position { get; set; }

        public byte Pitch { get; set; }

        public byte Yaw { get; set; }

        public bool OnGround { get; set; }
    }
}
