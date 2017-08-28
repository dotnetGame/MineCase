using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World
{
    public class Blocks
    {
        public static Block GetBlockStone()
        {
            return new Block { Id = (uint)BlockId.Stone };
        }

        public static Block GetBlockGrass()
        {
            return new Block { Id = (uint)BlockId.Grass };
        }

        public static Block GetBlockDirt()
        {
            return new Block { Id = (uint)BlockId.Dirt };
        }
    }

    public class BlockStates
    {
        public static BlockState GetBlockStateStone()
        {
            return new BlockState
            {
                 Id = (uint)BlockId.Stone,
                 MetaValue = 0
                 };
        }

        public static BlockState GetBlockStateGrass()
        {
            return new BlockState
            {
                 Id = (uint)BlockId.Grass,
                 MetaValue = 0
            };
        }

        public static BlockState GetBlockStateDirt()
        {
            return new BlockState
            {
                 Id = (uint)BlockId.Dirt,
                 MetaValue = 0
             };
        }
    }
}