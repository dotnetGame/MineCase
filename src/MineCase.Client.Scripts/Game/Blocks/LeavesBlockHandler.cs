using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MineCase.Client.Game.Blocks
{
    [BlockHandler(BlockId.Leaves)]
    public class LeavesBlockHandler : BlockHandler
    {
        public override bool IsUsable => true;

        public LeavesBlockHandler(BlockId blockId)
            : base(blockId)
        {
        }

        protected override string GetTextureName(BlockFace face)
        {
            return "leaves_oak";
        }
    }
}
