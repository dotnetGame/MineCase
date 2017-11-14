using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MineCase.Client.Game.Blocks
{
    [BlockHandler(BlockId.Wood)]
    public class WoodBlockHandler : BlockHandler
    {
        public override bool IsUsable => true;

        public WoodBlockHandler(BlockId blockId)
            : base(blockId)
        {
        }

        protected override string GetTextureName(BlockFace face)
        {
            return "log_oak";
        }
    }
}
