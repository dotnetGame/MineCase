using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MineCase.Client.Game.Blocks
{
    [BlockHandler(BlockId.GrassBlock)]
    public class GrassBlockHandler : BlockHandler
    {
        public override bool IsUsable => true;

        public GrassBlockHandler(BlockId blockId)
            : base(blockId)
        {
        }

        protected override string GetTextureName(BlockFace face)
        {
            if (face == BlockFace.Top) return "dirt";
            return "grass_side";
        }
    }
}
