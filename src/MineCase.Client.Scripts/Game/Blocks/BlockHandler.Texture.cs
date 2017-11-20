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
        public IBlockTextureLoader BlockTextureLoader { get; set; }

        private readonly Vector2Int[] _faceTextureCoords = new Vector2Int[6];

        private void CacheTextureAware()
        {
            for (int i = 0; i < _faceTextureCoords.Length; i++)
            {
                _faceTextureCoords[i] = BlockTextureLoader.TextureOffsets[GetTextureName((BlockFace)i)];
            }
        }

        public Vector2 GetUVOffset(BlockFace face, int vertexIndex)
        {
            var coord = _faceTextureCoords[(int)face];
            return BlockTextureLoader.GetCubeUV(coord, face, vertexIndex);
        }

        protected virtual string GetTextureName(BlockFace face)
        {
            return "stone";
        }
    }
}
