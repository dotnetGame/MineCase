using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using MineCase.Server.Game.Entities;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World.EntitySpawner.Ai.Action
{
    internal class CreatureAiActionLookAtPlayer : CreatureAiAction
    {
        public CreatureAiActionLookAtPlayer()
            : base(CreatureState.Look)
        {
        }

        public override void Action(IEntity creature)
        {
        }

        public static (byte, byte) VectorToYawAndPitch(Vector3 from, Vector3 to)
        {
            Vector3 v = to - from;
            v = Vector3.Normalize(v);

            double tmpYaw = -Math.Atan2(v.X, v.Z) / Math.PI * 180;
            if (tmpYaw < 0)
                tmpYaw = 360 + tmpYaw;
            double tmppitch = -Math.Asin(v.Y) / Math.PI * 180;

            byte yaw = (byte)(tmpYaw * 255 / 360);
            byte pitch = (byte)(tmppitch * 255 / 360);
            return (yaw, pitch);
        }
    }
}
