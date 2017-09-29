using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Algorithm.World.Plants
{
    public abstract class AbstractTreeGenerator : PlantsGenerator
    {
        public static bool CanSustainTree(PlantsType type, BlockState state)
        {
            if (state == BlockStates.Dirt() ||
                state == BlockStates.GrassBlock())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
