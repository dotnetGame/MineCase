using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World
{
    public class Blocks
    {
        public static readonly Block Air = new Block
        {
            Id = (uint)BlockId.Air,
            MetaValue = 0
        };

        public static readonly Block Stone = new Block
        {
            Id = (uint)BlockId.Stone,
            MetaValue = 0
        };

        public static readonly Block Grass = new Block
        {
            Id = (uint)BlockId.Grass,
            MetaValue = 0
        };

        public static readonly Block Dirt = new Block
        {
            Id = (uint)BlockId.Dirt,
            MetaValue = 0
        };

        public static readonly Block CobbleStone = new Block
        {
            Id = (uint)BlockId.Cobblestone,
            MetaValue = 0
        };

        public static readonly Block Bedrock = new Block
        {
            Id = (uint)BlockId.Bedrock,
            MetaValue = 0
        };

        public static readonly Block Water = new Block
        {
            Id = (uint)BlockId.Water,
            MetaValue = 0
        };
    }

    public class BlockStates
    {
        public static readonly BlockState Air = new BlockState
        {
            Id = (uint)BlockId.Air,
            MetaValue = 0
        };

        public static readonly BlockState Stone = new BlockState
        {
            Id = (uint)BlockId.Stone,
            MetaValue = 0
        };

        public static readonly BlockState Grass = new BlockState
        {
            Id = (uint)BlockId.Grass,
            MetaValue = 0
        };

        public static readonly BlockState Dirt = new BlockState
        {
            Id = (uint)BlockId.Dirt,
            MetaValue = 0
        };

        public static readonly BlockState CobbleStone = new BlockState
        {
            Id = (uint)BlockId.Cobblestone,
            MetaValue = 0
        };

        public static readonly BlockState Bedrock = new BlockState
        {
            Id = (uint)BlockId.Bedrock,
            MetaValue = 0
        };

        public static readonly BlockState Water = new BlockState
        {
            Id = (uint)BlockId.Water,
            MetaValue = 0
        };

        public static readonly BlockState Sand = new BlockState
        {
            Id = (uint)BlockId.Sand,
            MetaValue = 0
        };

        public static readonly BlockState Gravel = new BlockState
        {
            Id = (uint)BlockId.Gravel,
            MetaValue = 0
        };
    }
}