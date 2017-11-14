using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.World;
using UnityEngine;

namespace MineCase.Client.Game.Blocks
{
    public struct NeighborSections
    {
        public ChunkSectionCompactStorage Left;

        public ChunkSectionCompactStorage Right;

        public ChunkSectionCompactStorage Bottom;

        public ChunkSectionCompactStorage Top;

        public ChunkSectionCompactStorage Front;

        public ChunkSectionCompactStorage Back;

        public ChunkSectionCompactStorage this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return Left;
                    case 1:
                        return Right;
                    case 2:
                        return Bottom;
                    case 3:
                        return Top;
                    case 4:
                        return Front;
                    case 5:
                        return Back;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }
    }

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

        private static readonly int[,] _indices = new int[6, 6]
        {
            { 0, 3, 2, 0, 1, 3 },
            { 0, 3, 1, 0, 2, 3 },
            { 0, 3, 2, 0, 1, 3 },
            { 0, 3, 1, 0, 2, 3 },
            { 0, 3, 2, 0, 1, 3 },
            { 0, 3, 1, 0, 2, 3 }
        };

        public int CalculatePlanesCount(Vector3Int offset, ChunkSectionCompactStorage section, NeighborSections neighbor)
        {
            if (!HasNonTransparentBlock(section.Data[offset.x, offset.y, offset.z])) return 0;

            int count = 0;
            for (int i = 0; i < 6; i++)
            {
                if (HasNonTransparentBlock(section, offset + _normals[i], neighbor)) continue;
                count++;
            }

            return count;
        }

        public enum BlockFace : int
        {
            Left = 0,
            Right = 1,
            Top = 2,
            Bottom = 3,
            Front = 4,
            Back = 5
        }

        public void CreateMesh(Vector3[] vertices, Vector3[] normals, Vector2[] uvs, int[] triangles, ref int planeIndex, Vector3Int offset, ChunkSectionCompactStorage section, NeighborSections neighbor)
        {
            if (!HasNonTransparentBlock(section.Data[offset.x, offset.y, offset.z])) return;

            for (int i = 0; i < 6; i++)
            {
                if (HasNonTransparentBlock(section, offset + _normals[i], neighbor)) continue;

                for (int n = planeIndex * 4, k = 0; k < 4; k++, n++)
                {
                    var v = _positions[i, k] * 0.5f;
                    v += offset;
                    vertices[n] = v;
                    normals[n] = _normals[i];
                    uvs[n] = GetUVOffset((BlockFace)i, k);
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

        private bool HasNonTransparentBlock(BlockState blockState)
        {
            return !_transparentBlockIds.Contains(blockState.Id);
        }

        private bool HasNonTransparentBlock(ChunkSectionCompactStorage storage, Vector3Int position, NeighborSections neighbor)
        {
            var target = storage;
            for (int i = 0; i < 3; i++)
            {
                if (position[i] < 0)
                {
                    target = neighbor[i * 2];
                    position[i] += ChunkConstants.BlockEdgeWidthInSection;
                }
                else if (position[i] >= ChunkConstants.BlockEdgeWidthInSection)
                {
                    target = neighbor[i * 2 + 1];
                    position[i] -= ChunkConstants.BlockEdgeWidthInSection;
                }
            }

            if (target == null) return false;

            return !_transparentBlockIds.Contains(target.Data[position.x, position.y, position.z].Id);
        }
    }
}
