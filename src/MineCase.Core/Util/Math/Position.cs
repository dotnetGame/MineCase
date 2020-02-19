using MineCase.World;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Util.Math
{
    public struct BlockPos
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public BlockPos(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator BlockPos(Vector3i position)
        {
            return new BlockPos(position.X, position.Y, position.Z);
        }

        public static implicit operator Vector3i(BlockPos position)
        {
            return new Vector3i { X = position.X, Y = position.Y, Z = position.Z };
        }

        public static implicit operator Vector3(BlockPos position)
        {
            return new Vector3 { X = position.X, Y = position.Y, Z = position.Z };
        }

        public ChunkPos ToChunkPos()
        {
            int chunkPosX, chunkPosZ;
            if (X >= 0)
            {
                chunkPosX = X / ChunkConstants.BlockEdgeWidthInSection;
            }
            else
            {
                chunkPosX = -(((-X - 1) / ChunkConstants.BlockEdgeWidthInSection) + 1);
            }

            if (Z >= 0)
            {
                chunkPosZ = Z / ChunkConstants.BlockEdgeWidthInSection;
            }
            else
            {
                chunkPosZ = -(((-Z - 1) / ChunkConstants.BlockEdgeWidthInSection) + 1);
            }

            return new ChunkPos(chunkPosX, chunkPosZ);
        }

        public static BlockPos Add(BlockPos pos, int x, int y, int z)
        {
            return new BlockPos(pos.X + x, pos.Y + y, pos.Z + z);
        }

        public static BlockPos Add(BlockPos pos, Vector3i vec)
        {
            return new BlockPos(pos.X + vec.X, pos.Y + vec.Y, pos.Z + vec.Z);
        }

        public static BlockPos Subtract(BlockPos pos, int x, int y, int z)
        {
            return new BlockPos(pos.X - x, pos.Y - y, pos.Z - z);
        }

        public static BlockPos Subtract(BlockPos pos, Vector3i vec)
        {
            return new BlockPos(pos.X - vec.X, pos.Y - vec.Y, pos.Z - vec.Z);
        }

        public static int Clamp(int value, int max, int min)
        {
            int result;
            if (value > max) result = max;
            else if (value < min) result = min;
            else result = value;
            return result;
        }

        public static BlockPos Clamp(BlockPos value, BlockPos max, BlockPos min)
        {
            return new BlockPos(
                Clamp(value.X, max.X, min.X),
                Clamp(value.Y, max.Y, min.Y),
                Clamp(value.Z, max.Z, min.Z));
        }

        public static double Distance(BlockPos pos1, BlockPos pos2)
        {
            int x = pos1.X - pos2.X;
            int y = pos1.Y - pos2.Y;
            int z = pos1.Z - pos2.Z;
            return System.Math.Sqrt(x * x + y * y + z * z);
        }
    }


    public static class BlockPosExtension
    {
        public static BlockPos Down(this BlockPos pos)
        {
            return new BlockPos(pos.X, pos.Y - 1, pos.Z);
        }

        public static BlockPos Up(this BlockPos pos)
        {
            return new BlockPos(pos.X, pos.Y + 1, pos.Z);
        }

        public static BlockPos East(this BlockPos pos)
        {
            return new BlockPos(pos.X + 1, pos.Y, pos.Z);
        }

        public static BlockPos South(this BlockPos pos)
        {
            return new BlockPos(pos.X, pos.Y, pos.Z + 1);
        }

        public static BlockPos West(this BlockPos pos)
        {
            return new BlockPos(pos.X - 1, pos.Y, pos.Z);
        }

        public static BlockPos North(this BlockPos pos)
        {
            return new BlockPos(pos.X, pos.Y, pos.Z - 1);
        }

        public static BlockPos Down(this BlockPos pos, int offset)
        {
            return new BlockPos(pos.X, pos.Y - offset, pos.Z);
        }

        public static BlockPos Up(this BlockPos pos, int offset)
        {
            return new BlockPos(pos.X, pos.Y + offset, pos.Z);
        }

        public static BlockPos East(this BlockPos pos, int offset)
        {
            return new BlockPos(pos.X + offset, pos.Y, pos.Z);
        }

        public static BlockPos South(this BlockPos pos, int offset)
        {
            return new BlockPos(pos.X, pos.Y, pos.Z + offset);
        }

        public static BlockPos West(this BlockPos pos, int offset)
        {
            return new BlockPos(pos.X - offset, pos.Y, pos.Z);
        }

        public static BlockPos North(this BlockPos pos, int offset)
        {
            return new BlockPos(pos.X, pos.Y, pos.Z - offset);
        }
    }

    public struct ChunkPos
    {
        public int X { get; set; }
        public int Z { get; set; }

        public ChunkPos(int x, int z)
        {
            X = x;
            Z = z;
        }

        public BlockPos GetBlockPos(int x, int y, int z)
        {
            return new BlockPos((X << 4) + x, y, (Z << 4) + z);
        }
    }

    public struct EntityPos
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public EntityPos(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator Vector3(EntityPos pos)
        {
            return new Vector3(pos.X, pos.Y, pos.Z);
        }

        public static implicit operator EntityPos(Vector3 pos)
        {
            return new EntityPos(pos.X, pos.Y, pos.Z);
        }

        public ChunkPos ToChunkPos()
        {
            float chunkX = (float)System.Math.Floor((double)X / ChunkConstants.BlockEdgeWidthInSection);
            float chunkZ = (float)System.Math.Floor((double)Z / ChunkConstants.BlockEdgeWidthInSection);
            return new ChunkPos((int)chunkX, (int)chunkZ);
        }

        public BlockPos ToBlockPos()
        {
            int x = (int)System.Math.Floor(X);
            int y = (int)System.Math.Floor(Y);
            int z = (int)System.Math.Floor(Z);
            return new BlockPos(x, y, z);
        }

        public static EntityPos Add(EntityPos pos, float x, float y, float z)
        {
            return new EntityPos(pos.X + x, pos.Y + y, pos.Z + z);
        }

        public static float Clamp(float value, float max, float min)
        {
            float result;
            if (value > max) result = max;
            else if (value < min) result = min;
            else result = value;
            return result;
        }

        public static EntityPos Clamp(EntityPos value, EntityPos max, EntityPos min)
        {
            return new EntityPos(
                Clamp(value.X, max.X, min.X),
                Clamp(value.Y, max.Y, min.Y),
                Clamp(value.Z, max.Z, min.Z));
        }

        public static double Distance(EntityPos pos1, EntityPos pos2)
        {
            float x = pos1.X - pos2.X;
            float y = pos1.Y - pos2.Y;
            float z = pos1.Z - pos2.Z;
            return System.Math.Sqrt(x * x + y * y + z * z);
        }
    }
}
