using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.World;
using UnityEngine;

namespace MineCase.Client.Game.Blocks
{
    public partial class BlockHandler
    {
        private static readonly Vector3[,] _positions = new Vector3[6, 4]
        {
            { new Vector3(-1, -1, -1), new Vector3(-1, -1, +1), new Vector3(-1, +1, -1), new Vector3(-1, +1, +1) },
            { new Vector3(+1, -1, -1), new Vector3(+1, -1, +1), new Vector3(+1, +1, -1), new Vector3(+1, +1, +1) },
            { new Vector3(-1, +1, -1), new Vector3(-1, +1, +1), new Vector3(+1, +1, -1), new Vector3(+1, +1, +1) },
            { new Vector3(-1, -1, -1), new Vector3(-1, -1, +1), new Vector3(+1, -1, -1), new Vector3(+1, -1, +1) },
            { new Vector3(-1, -1, -1), new Vector3(-1, +1, -1), new Vector3(+1, -1, -1), new Vector3(+1, +1, -1) },
            { new Vector3(-1, -1, +1), new Vector3(-1, +1, +1), new Vector3(+1, -1, +1), new Vector3(+1, +1, +1) }
        };

        private static readonly Vector3Int[] _normals = new Vector3Int[6]
        {
            new Vector3Int(-1, 0, 0),
            new Vector3Int(+1, 0, 0),
            new Vector3Int(0, +1, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(0, 0, -1),
            new Vector3Int(0, 0, +1)
        };

        private static readonly Vector2[,] _uvs = new Vector2[6, 4]
        {
            { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) },
            { new Vector2(1, 0), new Vector2(0, 0), new Vector2(1, 1), new Vector2(0, 1) },
            { new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 1), new Vector2(1, 0) },
            { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) },
            { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) },
            { new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 0), new Vector2(0, 1) }
        };

        private static readonly int[,] _indices = new int[6, 6]
        {
            { 0, 3, 2, 0, 1, 3 },
            { 0, 3, 1, 0, 2, 3 },
            { 0, 3, 2, 0, 1, 3 },
            { 0, 3, 1, 0, 2, 3 },
            { 0, 3, 2, 0, 1, 3 },
            { 0, 3, 1, 0, 2, 3 }
        };

        public static int CalculatePlanesCount(Vector3Int offset, ChunkSectionCompactStorage section)
        {
            if (!HasNonTransparentBlock(section, offset)) return 0;

            int count = 0;
            for (int i = 0; i < 6; i++)
            {
                if (HasNonTransparentBlock(section, offset + _normals[i])) continue;
                count++;
            }

            return count;
        }

        public static void CreateMesh(Vector3[] vertices, Vector3[] normals, Vector2[] uvs, int[] triangles, ref int planeIndex, Vector3Int offset, ChunkSectionCompactStorage section)
        {
            if (!HasNonTransparentBlock(section, offset)) return;

            for (int i = 0; i < 6; i++)
            {
                if (HasNonTransparentBlock(section, offset + _normals[i])) continue;

                for (int n = planeIndex * 4, k = 0; k < 4; k++, n++)
                {
                    var v = _positions[i, k] * 0.5f;
                    v += offset;
                    vertices[n] = v;
                    normals[n] = _normals[i];
                    uvs[n] = _uvs[i, k];
                }

                for (int n = planeIndex * 6, k = 0; k < 6; k++, n++)
                    triangles[n] = planeIndex * 4 + _indices[i, k];

                planeIndex++;
            }
        }

        private static readonly HashSet<uint> _transparentBlockIds = new HashSet<uint>
        {
            (uint)BlockId.Air
        };

        private static bool HasNonTransparentBlock(ChunkSectionCompactStorage storage, Vector3Int position)
        {
            bool IsOutOfRange(int value)
            {
                if (value < 0 || value >= ChunkConstants.BlockEdgeWidthInSection) return true;
                return false;
            }

            if (IsOutOfRange(position.x) || IsOutOfRange(position.y) || IsOutOfRange(position.z)) return false;
            return !_transparentBlockIds.Contains(storage.Data[position.x, position.y, position.z].Id);
        }
    }
}
