using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World
{
    public enum BlockId : uint
    {
        Air = 0,
        Stone = 1,
        Grass = 2,
        Dirt = 3,
        Cobblestone = 4,
        Planks = 5,
        Sapling = 6,
        Bedrock = 7,
        FlowingWater = 8,
        Water = 9,
        FlowingLava = 10,
        Lava = 11,
        Sand = 12,
        Gravel = 13
    }

    public struct BlockState : IEquatable<BlockState>
    {
        public uint Id { get; set; }

        public uint MetaValue { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BlockState && Equals((BlockState)obj);
        }

        public bool Equals(BlockState other)
        {
            return Id == other.Id &&
                   MetaValue == other.MetaValue;
        }

        public override int GetHashCode()
        {
            var hashCode = -81208087;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + MetaValue.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(BlockState state1, BlockState state2)
        {
            return state1.Equals(state2);
        }

        public static bool operator !=(BlockState state1, BlockState state2)
        {
            return !(state1 == state2);
        }

        public uint ToUInt32()
        {
            return ChunkSectionStorage.SerializeBlockState(this);
        }
    }
}
