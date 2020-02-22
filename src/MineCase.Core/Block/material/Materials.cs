using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Block.Material
{
    public class Materials
    {
        public static Material Air = MaterialBuilder.Create(MaterialColors.Air).NotBlocksMovement().NotOpaque().NotSolid().Replaceable().Build();
    }
}
