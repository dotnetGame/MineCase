using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using MineCase.World;

namespace MineCase.World
{
    public struct BlockWorldPos
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public BlockWorldPos(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator BlockWorldPos(Position position)
        {
            return new BlockWorldPos(position.X, position.Y, position.Z);
        }

        public static implicit operator Position(BlockWorldPos position)
        {
            return new Position { X = position.X, Y = position.Y, Z = position.Z };
        }

        public static implicit operator Vector3(BlockWorldPos position)
        {
            return new Vector3 { X = position.X, Y = position.Y, Z = position.Z };
        }

        public BlockChunkPos ToBlockChunkPos()
        {
            int blockPosX = X % ChunkConstants.BlockEdgeWidthInSection;
            int blockPosZ = Z % ChunkConstants.BlockEdgeWidthInSection;
            if (blockPosX < 0) blockPosX += ChunkConstants.BlockEdgeWidthInSection;
            if (blockPosZ < 0) blockPosZ += ChunkConstants.BlockEdgeWidthInSection;
            return new BlockChunkPos(blockPosX, Y, blockPosZ);
        }

        public ChunkWorldPos ToChunkWorldPos()
        {
            int chunkPosX = X / ChunkConstants.BlockEdgeWidthInSection;
            int chunkPosZ = Z / ChunkConstants.BlockEdgeWidthInSection;
            if (chunkPosX < 0) chunkPosX -= 1;
            if (chunkPosZ < 0) chunkPosZ -= 1;
            return new ChunkWorldPos(chunkPosX, chunkPosZ);
        }

        public static BlockWorldPos Add(BlockWorldPos pos, int x, int y, int z)
        {
            return new BlockWorldPos(pos.X + x, pos.Y + y, pos.Z + z);
        }

        public static int Clamp(int value, int max, int min)
        {
            int result;
            if (value > max) result = max;
            else if (value < min) result = min;
            else result = value;
            return result;
        }

        public static BlockWorldPos Clamp(BlockWorldPos value, BlockWorldPos max, BlockWorldPos min)
        {
            return new BlockWorldPos(
                Clamp(value.X, max.X, min.X),
                Clamp(value.Y, max.Y, min.Y),
                Clamp(value.Z, max.Z, min.Z));
        }

        public static double Distance(BlockWorldPos pos1, BlockWorldPos pos2)
        {
            int x = pos1.X - pos2.X;
            int y = pos1.Y - pos2.Y;
            int z = pos1.Z - pos2.Z;
            return Math.Sqrt(x * x + y * y + z * z);
        }
    }

    public struct BlockChunkPos : IEquatable<BlockChunkPos>
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public BlockChunkPos(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public BlockWorldPos ToBlockWorldPos(ChunkWorldPos chunkPos)
        {
            int blockX = X + chunkPos.X * 16;
            int blockY = Y;
            int blockZ = Z + chunkPos.Z * 16;
            return new BlockWorldPos(blockX, blockY, blockZ);
        }

        public static BlockChunkPos Add(BlockChunkPos pos, int x, int y, int z)
        {
            return new BlockChunkPos(pos.X + x, pos.Y + y, pos.Z + z);
        }

        public static int Clamp(int value, int max, int min)
        {
            int result;
            if (value > max) result = max;
            else if (value < min) result = min;
            else result = value;
            return result;
        }

        public static BlockChunkPos Clamp(BlockChunkPos value, BlockChunkPos max, BlockChunkPos min)
        {
            return new BlockChunkPos(
                Clamp(value.X, max.X, min.X),
                Clamp(value.Y, max.Y, min.Y),
                Clamp(value.Z, max.Z, min.Z));
        }

        public static double Distance(BlockChunkPos pos1, BlockChunkPos pos2)
        {
            int x = pos1.X - pos2.X;
            int y = pos1.Y - pos2.Y;
            int z = pos1.Z - pos2.Z;
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }

        public override bool Equals(object obj)
        {
            return obj is BlockChunkPos && Equals((BlockChunkPos)obj);
        }

        public bool Equals(BlockChunkPos other)
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

        public static bool operator ==(BlockChunkPos pos1, BlockChunkPos pos2)
        {
            return pos1.Equals(pos2);
        }

        public static bool operator !=(BlockChunkPos pos1, BlockChunkPos pos2)
        {
            return !(pos1 == pos2);
        }
    }

    public struct BlockSectionPos
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public BlockSectionPos(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public struct ChunkWorldPos : IEquatable<ChunkWorldPos>
    {
        public int X { get; set; }

        public int Z { get; set; }

        public ChunkWorldPos(int x, int z)
        {
            X = x;
            Z = z;
        }

        public BlockWorldPos ToBlockWorldPos()
        {
            return new BlockWorldPos(X * 16, 0, Z * 16);
        }

        public override bool Equals(object obj)
        {
            return obj is ChunkWorldPos && Equals((ChunkWorldPos)obj);
        }

        public bool Equals(ChunkWorldPos other)
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

        public static bool operator ==(ChunkWorldPos pos1, ChunkWorldPos pos2)
        {
            return pos1.Equals(pos2);
        }

        public static bool operator !=(ChunkWorldPos pos1, ChunkWorldPos pos2)
        {
            return !(pos1 == pos2);
        }
    }

    public struct EntityWorldPos
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public EntityWorldPos(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator Vector3(EntityWorldPos pos)
        {
            return new Vector3(pos.X, pos.Y, pos.Z);
        }

        public static implicit operator EntityWorldPos(Vector3 pos)
        {
            return new EntityWorldPos(pos.X, pos.Y, pos.Z);
        }

        public ChunkWorldPos ToChunkWorldPos()
        {
            float chunkX = (float)Math.Floor((double)X / ChunkConstants.BlockEdgeWidthInSection);
            float chunkZ = (float)Math.Floor((double)Z / ChunkConstants.BlockEdgeWidthInSection);
            return new ChunkWorldPos((int)chunkX, (int)chunkZ);
        }

        public EntityChunkPos ToEntityChunkPos()
        {
            float entityX = X % ChunkConstants.BlockEdgeWidthInSection;
            float entityZ = Z % ChunkConstants.BlockEdgeWidthInSection;
            if (entityX < 0) entityX += ChunkConstants.BlockEdgeWidthInSection;
            if (entityZ < 0) entityZ += ChunkConstants.BlockEdgeWidthInSection;
            return new EntityChunkPos(entityX, Y, entityZ);
        }

        public static EntityWorldPos Add(EntityWorldPos pos, float x, float y, float z)
        {
            return new EntityWorldPos(pos.X + x, pos.Y + y, pos.Z + z);
        }

        public static float Clamp(float value, float max, float min)
        {
            float result;
            if (value > max) result = max;
            else if (value < min) result = min;
            else result = value;
            return result;
        }

        public static EntityWorldPos Clamp(EntityWorldPos value, EntityWorldPos max, EntityWorldPos min)
        {
            return new EntityWorldPos(
                Clamp(value.X, max.X, min.X),
                Clamp(value.Y, max.Y, min.Y),
                Clamp(value.Z, max.Z, min.Z));
        }

        public static double Distance(EntityWorldPos pos1, EntityWorldPos pos2)
        {
            float x = pos1.X - pos2.X;
            float y = pos1.Y - pos2.Y;
            float z = pos1.Z - pos2.Z;
            return Math.Sqrt(x * x + y * y + z * z);
        }
    }

    public struct EntityChunkPos
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public EntityChunkPos(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public EntityWorldPos ToEntityWorldPos(ChunkWorldPos pos)
        {
            return new EntityWorldPos(X + pos.X, Y, Z + pos.Z);
        }

        public static EntityChunkPos Add(EntityChunkPos pos, float x, float y, float z)
        {
            return new EntityChunkPos(pos.X + x, pos.Y + y, pos.Z + z);
        }

        public static float Clamp(float value, float max, float min)
        {
            float result;
            if (value > max) result = max;
            else if (value < min) result = min;
            else result = value;
            return result;
        }

        public static EntityChunkPos Clamp(EntityChunkPos value, EntityChunkPos max, EntityChunkPos min)
        {
            return new EntityChunkPos(
                Clamp(value.X, max.X, min.X),
                Clamp(value.Y, max.Y, min.Y),
                Clamp(value.Z, max.Z, min.Z));
        }

        public static double Distance(EntityChunkPos pos1, EntityChunkPos pos2)
        {
            float x = pos1.X - pos2.X;
            float y = pos1.Y - pos2.Y;
            float z = pos1.Z - pos2.Z;
            return Math.Sqrt(x * x + y * y + z * z);
        }
    }
}
