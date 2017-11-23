// deprecated file
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using MineCase.World;

namespace MineCase.Algorithm.Game.Entity.Ai.Action
{
    public class CreatureActionDistance
    {
        public float? HeadYaw { get; set; }

        public float? Yaw { get; set; }

        public float? Pitch { get; set; }

        public Vector3? Position { get; set; }

        public static CreatureActionDistance operator /(CreatureActionDistance lhs, float n)
        {
            CreatureActionDistance result = new CreatureActionDistance();
            if (lhs.HeadYaw.HasValue)
                result.HeadYaw = lhs.HeadYaw / n;
            if (lhs.Yaw.HasValue)
                result.Yaw = lhs.Yaw / n;
            if (lhs.Pitch.HasValue)
                result.Pitch = lhs.Pitch / n;
            if (lhs.Position.HasValue)
                result.Position = new Vector3(lhs.Position.Value.X / n, lhs.Position.Value.Y / n, lhs.Position.Value.Z / n);
            return result;
        }

        public static CreatureActionDistance operator *(CreatureActionDistance lhs, float n)
        {
            CreatureActionDistance result = new CreatureActionDistance();
            if (lhs.HeadYaw.HasValue)
                result.HeadYaw = lhs.HeadYaw * n;
            if (lhs.Yaw.HasValue)
                result.Yaw = lhs.Yaw * n;
            if (lhs.Pitch.HasValue)
                result.Pitch = lhs.Pitch * n;
            if (lhs.Position.HasValue)
                result.Position = new Vector3(lhs.Position.Value.X * n, lhs.Position.Value.Y * n, lhs.Position.Value.Z * n);
            return result;
        }
    }

    public class CreatureAction
    {
        public float? HeadYaw { get; set; }

        public float? Yaw { get; set; }

        public float? Pitch { get; set; }

        public EntityWorldPos? Position { get; set; }

        public CreatureAction()
        {
        }

        public CreatureAction(CreatureAction other)
        {
            HeadYaw = other.HeadYaw;
            Yaw = other.Yaw;
            Pitch = other.Pitch;
            Position = other.Position;
        }

        public static CreatureActionDistance operator -(CreatureAction lhs, CreatureAction rhs)
        {
            CreatureActionDistance result = new CreatureActionDistance();
            if (lhs.HeadYaw.HasValue && rhs.HeadYaw.HasValue)
                result.HeadYaw = lhs.HeadYaw - rhs.HeadYaw;
            if (lhs.Yaw.HasValue && rhs.Yaw.HasValue)
                result.Yaw = lhs.Yaw - rhs.Yaw;
            if (lhs.Pitch.HasValue && rhs.Pitch.HasValue)
                result.Pitch = lhs.Pitch - rhs.Pitch;
            if (lhs.Position.HasValue && rhs.Position.HasValue)
                result.Position = new Vector3(lhs.Position.Value.X - rhs.Position.Value.X, lhs.Position.Value.Y - rhs.Position.Value.Y, lhs.Position.Value.Z - rhs.Position.Value.Z);
            return result;
        }

        public static CreatureAction operator +(CreatureAction lhs, CreatureActionDistance rhs)
        {
            CreatureAction result = new CreatureAction();
            if (lhs.HeadYaw.HasValue && rhs.HeadYaw.HasValue)
                result.HeadYaw = lhs.HeadYaw + rhs.HeadYaw;
            if (lhs.Yaw.HasValue && rhs.Yaw.HasValue)
                result.Yaw = lhs.Yaw + rhs.Yaw;
            if (lhs.Pitch.HasValue && rhs.Pitch.HasValue)
                result.Pitch = lhs.Pitch + rhs.Pitch;
            if (lhs.Position.HasValue && rhs.Position.HasValue)
                result.Position = new Vector3(lhs.Position.Value.X + rhs.Position.Value.X, lhs.Position.Value.Y + rhs.Position.Value.Y, lhs.Position.Value.Z + rhs.Position.Value.Z);
            return result;
        }
    }
}
