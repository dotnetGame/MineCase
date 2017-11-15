﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MineCase.Client.Game.Blocks
{
    public interface IBlockTextureLoader
    {
        Texture2D Texture { get; }

        Vector2Int TextureSize { get; }

        Dictionary<string, Vector2Int> TextureOffsets { get; }

        void Initialize(Material cubeMaterial);

        Vector2 GetCubeUV(Vector2Int coord, BlockHandler.BlockFace face, int vertexIndex);
    }

    internal class BlockTextureLoader : IBlockTextureLoader
    {
        private static readonly string[] _textureNames = new[]
        {
            "stone",
            "grass_side",
            "grass_top",
            "dirt",
            "water_overlay",
            "planks_oak",
            "log_oak",
            "leaves_oak"
        };

        public Dictionary<string, Vector2Int> TextureOffsets { get; } = new Dictionary<string, Vector2Int>();

        public Vector2Int TextureSize { get; private set; }

        public Texture2D Texture { get; private set; }

        public void Initialize(Material cubeMaterial)
        {
            const int maxWidth = 1024;
            var height = (int)Math.Ceiling((float)_textureNames.Length / (maxWidth / 16)) * 16;
            var texture = new Texture2D(maxWidth, height, TextureFormat.ARGB32, true)
            {
                filterMode = FilterMode.Bilinear,
                alphaIsTransparency = true
            };

            int x = 0, y = 0;
            foreach (var texName in _textureNames)
            {
                var tex = Resources.Load<Texture2D>("textures/blocks/" + texName);
                texture.SetPixels(x, y, 16, 16, tex.GetPixels());
                TextureOffsets.Add(texName, new Vector2Int(x, y));
                x += 16;

                if (x >= maxWidth)
                {
                    y += 16;
                    x = 0;
                }
            }

            texture.Apply();

            Texture = texture;
            TextureSize = new Vector2Int(texture.width, texture.height);
            cubeMaterial.mainTexture = texture;
        }

        private static readonly Vector2Int[,] _cubeUVs = new Vector2Int[6, 4]
        {
            { new Vector2Int(1, 1), new Vector2Int(15, 1), new Vector2Int(1, 15), new Vector2Int(15, 15) },
            { new Vector2Int(15, 1), new Vector2Int(1, 1), new Vector2Int(15, 15), new Vector2Int(1, 15) },
            { new Vector2Int(1, 15), new Vector2Int(1, 1), new Vector2Int(15, 15), new Vector2Int(15, 1) },
            { new Vector2Int(1, 1), new Vector2Int(1, 15), new Vector2Int(15, 1), new Vector2Int(15, 15) },
            { new Vector2Int(1, 1), new Vector2Int(1, 15), new Vector2Int(15, 1), new Vector2Int(15, 15) },
            { new Vector2Int(15, 1), new Vector2Int(15, 15), new Vector2Int(1, 1), new Vector2Int(1, 15) }
        };

        public Vector2 GetCubeUV(Vector2Int coord, BlockHandler.BlockFace face, int vertexIndex)
        {
            var uv = _cubeUVs[(int)face, vertexIndex] + coord;

            return new Vector2((float)uv.x / TextureSize.x, (float)uv.y / TextureSize.y);
        }
    }
}
