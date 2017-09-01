using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World
{
    public class Vector3i
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public Vector3i(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public struct Vector2i
    {
        public int X { get; set; }

        public int Z { get; set; }

        public Vector2i(int x, int z)
        {
            X = x;
            Z = z;
        }
    }

    public struct Vector3f
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public Vector3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public struct Vector2f
    {
        public float X { get; set; }

        public float Z { get; set; }

        public Vector2f(float x, float z)
        {
            X = x;
            Z = z;
        }
    }

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
    }

    public struct BlockChunkPos
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

    public struct ChunkWorldPos
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
    }
}
