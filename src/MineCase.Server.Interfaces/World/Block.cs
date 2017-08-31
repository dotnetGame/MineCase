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
        Gravel = 13,
        Wood = 17,
        Leaves = 18,
        Glass = 20,
        Tallgrass = 31,
        YellowFlower = 37,
        RedFlower = 38
    }

    public enum GrassType : uint
    {
        Shrub = 0,
        TallGrass = 1,
        Fern = 2
    }

    public struct BlockState : IEquatable<BlockState>
    {
        public uint Id { get; set; }

        public uint MetaValue { get; set; }

        // 一些特性
        public bool IsLightOpacity()
        {
            if (Id == (uint)BlockId.Glass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // 相等比较
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
    }

    public struct BlockPos : IEquatable<BlockPos>
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public void Add(int x, int y, int z)
        {
            X += x;
            Y += y;
            Z += z;
        }

        public static BlockPos Add(BlockPos pos, int x, int y, int z)
        {
            return new BlockPos { X = pos.X + x, Y = pos.Y + y, Z = pos.Z + z };
        }

        public override bool Equals(object obj)
        {
            return obj is BlockPos && Equals((BlockPos)obj);
        }

        public bool Equals(BlockPos other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Z == other.Z;
        }

        public override int GetHashCode()
        {
            var hashCode = -307843816;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(BlockPos pos1, BlockPos pos2)
        {
            return pos1.Equals(pos2);
        }

        public static bool operator !=(BlockPos pos1, BlockPos pos2)
        {
            return !(pos1 == pos2);
        }
    }

    public struct ChunkPos : IEquatable<ChunkPos>
    {
        public int X { get; set; }

        public int Z { get; set; }

        public void Add(int x, int z)
        {
            X += x;
            Z += z;
        }

        public static ChunkPos Add(ChunkPos pos, int x, int z)
        {
            return new ChunkPos { X = pos.X + x, Z = pos.Z + z };
        }

        public override bool Equals(object obj)
        {
            return obj is ChunkPos && Equals((ChunkPos)obj);
        }

        public bool Equals(ChunkPos other)
        {
            return X == other.X &&
                   Z == other.Z;
        }

        public override int GetHashCode()
        {
            var hashCode = 1911744652;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(ChunkPos pos1, ChunkPos pos2)
        {
            return pos1.Equals(pos2);
        }

        public static bool operator !=(ChunkPos pos1, ChunkPos pos2)
        {
            return !(pos1 == pos2);
        }
    }
}
