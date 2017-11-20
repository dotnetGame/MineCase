using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MineCase.Client.Game.Blocks
{
    [BlockHandler(BlockId.WoodPlanks)]
    public class WoodPlanksBlockHandler : BlockHandler
    {
        public override bool IsUsable => true;

        public WoodPlanksBlockHandler(BlockId blockId)
            : base(blockId)
        {
        }

        protected override string GetTextureName(BlockFace face)
        {
            return "planks_oak";
        }
    }
}
