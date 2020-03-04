using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block.Material
{
    public class Material : IEquatable<Material>
    {
        public MaterialColor Color { get; set; }

        public PushReaction PushReaction { get; set; } = PushReaction.Normal;

        public bool BlocksMovement { get; set; } = true;

        public bool Flammable { get; set; } = false;

        public bool RequiresNoTool { get; set; } = true;

        public bool Liquid { get; set; } = false;

        public bool Opaque { get; set; } = true;

        public bool Replaceable { get; set; } = false;

        public bool Solid { get; set; } = true;

        public override bool Equals(object obj)
        {
            return obj is Material && Equals((Material)obj);
        }

        public bool Equals(Material other)
        {
            return Color == other.Color &&
                   PushReaction == other.PushReaction &&
                   BlocksMovement == other.BlocksMovement &&
                   Flammable == other.Flammable &&
                   RequiresNoTool == other.RequiresNoTool &&
                   Liquid == other.Liquid &&
                   Opaque == other.Opaque &&
                   Replaceable == other.Replaceable &&
                   Solid == other.Solid;
        }

        public override int GetHashCode()
        {
            var hashCode = -81208087;
            hashCode = hashCode * -1521134295 + Color.GetHashCode();
            hashCode = hashCode * -1521134295 + PushReaction.GetHashCode();
            hashCode = hashCode * -1521134295 + BlocksMovement.GetHashCode();
            hashCode = hashCode * -1521134295 + Flammable.GetHashCode();
            hashCode = hashCode * -1521134295 + RequiresNoTool.GetHashCode();
            hashCode = hashCode * -1521134295 + Liquid.GetHashCode();
            hashCode = hashCode * -1521134295 + Opaque.GetHashCode();
            hashCode = hashCode * -1521134295 + Replaceable.GetHashCode();
            hashCode = hashCode * -1521134295 + Solid.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Material state1, Material state2)
        {
            return state1.Equals(state2);
        }

        public static bool operator !=(Material state1, Material state2)
        {
            return !(state1 == state2);
        }
    }
}
