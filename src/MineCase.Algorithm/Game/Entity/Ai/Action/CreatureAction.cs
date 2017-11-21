using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using MineCase.World;

namespace MineCase.Algorithm.Game.Entity.Ai.Action
{
    public class CreatureActionDistance
    {
        public float Yaw { get; set; }

        public float Pitch { get; set; }

        public Vector3 Position { get; set; }

        public static CreatureActionDistance operator /(CreatureActionDistance lhs, float n)
        {
            CreatureActionDistance result = new CreatureActionDistance();
            result.Yaw = lhs.Yaw / n;
            result.Pitch = lhs.Pitch / n;
            result.Position = new Vector3(lhs.Position.X / n, lhs.Position.Y / n, lhs.Position.Z / n);
            return result;
        }

        public static CreatureActionDistance operator *(CreatureActionDistance lhs, float n)
        {
            CreatureActionDistance result = new CreatureActionDistance();
            result.Yaw = lhs.Yaw * n;
            result.Pitch = lhs.Pitch * n;
            result.Position = new Vector3(lhs.Position.X * n, lhs.Position.Y * n, lhs.Position.Z * n);
            return result;
        }
    }

    public class CreatureAction
    {
        public float Yaw { get; set; }

        public float Pitch { get; set; }

        public EntityWorldPos Position { get; set; }

        public static CreatureActionDistance operator -(CreatureAction lhs, CreatureAction rhs)
        {
            CreatureActionDistance result = new CreatureActionDistance();
            result.Yaw = lhs.Yaw - rhs.Yaw;
            result.Pitch = lhs.Pitch - rhs.Pitch;
            result.Position = new Vector3(lhs.Position.X - rhs.Position.X, lhs.Position.Y - rhs.Position.Y, lhs.Position.Z - rhs.Position.Z);
            return result;
        }

        public static CreatureAction operator +(CreatureAction lhs, CreatureActionDistance rhs)
        {
            CreatureAction result = new CreatureAction();
            result.Yaw = lhs.Yaw + rhs.Yaw;
            result.Pitch = lhs.Pitch + rhs.Pitch;
            result.Position = new EntityWorldPos(lhs.Position.X + rhs.Position.X, lhs.Position.Y + rhs.Position.Y, lhs.Position.Z + rhs.Position.Z);
            return result;
        }
    }
}
