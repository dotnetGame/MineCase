using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MineCase.Client.Game.Blocks
{
    public abstract partial class BlockHandler
    {
        public Vector2 GetUVOffset(Vector2 uv, BlockFace face)
        {
            var offset = ServerManager.TextureOffsets[GetTextureName(face)];
            double x = (uv.x > 0 ? 15.5 : 0.5) + offset.x;
            double y = (uv.y > 0 ? 15.5 : 0.5) + offset.y;

            x /= ServerManager.TerrainSize.x;
            y /= ServerManager.TerrainSize.y;
            return new Vector2((float)x, (float)y);
        }

        protected virtual string GetTextureName(BlockFace face)
        {
            return "stone";
        }
    }
}
