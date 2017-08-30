using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World
{
    public static class BlockStates
    {
        public static BlockState Air()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Air,
                MetaValue = 0
            };
        }

        public static BlockState Stone()
        {
            return new BlockState
            {
                 Id = (uint)BlockId.Stone,
                 MetaValue = 0
                 };
        }

        public static BlockState Grass()
        {
            return new BlockState
            {
                 Id = (uint)BlockId.Grass,
                 MetaValue = 0
            };
        }

        public static BlockState Dirt()
        {
            return new BlockState
            {
                 Id = (uint)BlockId.Dirt,
                 MetaValue = 0
             };
        }

        public static BlockState Bedrock()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Bedrock,
                MetaValue = 0
            };
        }

        public static BlockState Water()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Water,
                MetaValue = 0
            };
        }

        public static BlockState Gravel()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Gravel,
                MetaValue = 0
            };
        }
    }
}