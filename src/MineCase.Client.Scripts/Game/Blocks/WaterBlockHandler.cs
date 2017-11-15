using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MineCase.Client.Game.Blocks
{
    [BlockHandler(BlockId.Water)]
    public class WaterBlockHandler : BlockHandler
    {
        public override bool IsUsable => true;

        public WaterBlockHandler(BlockId blockId)
            : base(blockId)
        {
        }

        protected override string GetTextureName(BlockFace face)
        {
            return "water_overlay";
        }
    }
}
