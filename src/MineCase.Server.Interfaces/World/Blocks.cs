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

        public static BlockState Sand()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Sand,
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

        public static BlockState Wood()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Wood,
                MetaValue = 0
            };
        }

        public static BlockState Leaves()
        {
            return new BlockState
            {
                Id = (uint)BlockId.Leaves,
                MetaValue = 0
            };
        }

        public static BlockState Tallgrass(GrassType type = GrassType.TallGrass)
        {
            return new BlockState
            {
                Id = (uint)BlockId.Tallgrass,
                MetaValue = (uint)type
            };
        }

        public static BlockState YellowFlower()
        {
            return new BlockState
            {
                Id = (uint)BlockId.YellowFlower,
                MetaValue = 0
            };
        }

        public static BlockState RedFlower()
        {
            return new BlockState
            {
                Id = (uint)BlockId.RedFlower,
                MetaValue = 0
            };
        }
    }
}